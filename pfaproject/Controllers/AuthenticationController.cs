using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pfaproject.Models;
using pfaproject.Models.Authentication.Login;
using pfaproject.Models.Authentication.Sign_up;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Management.Service.Models;
using User.Management.Service.Services;
using Microsoft.AspNetCore.SignalR;
using pfaproject.Hubs;
using Microsoft.AspNetCore.Cors;

namespace pfaproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;


        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IHubContext<NotificationHub> hubContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _hubContext = hubContext;
        }
        [EnableCors("AllowReactApps")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            // Vérifie si l'utilisateur existe
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
            }

            // Ajoute l'utilisateur à la base de données
            var user = new ApplicationUser
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };

            if (await _roleManager.RoleExistsAsync(registerUser.Role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "User failed to create." });
                }

                await _userManager.AddToRoleAsync(user, registerUser.Role);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User created & Email sent to {user.Email} successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "This role does not exist." });
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginmodel)
        {
            var user = await _userManager.FindByNameAsync(loginmodel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginmodel.Password))
            {
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id) // Ajout de l'ID utilisateur aux revendications
        };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }


        [HttpPost("delete-account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token claims");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete user");
                }

                return Ok("Account deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
        // Ajoutez cette méthode pour initier le processus de connexion Google
        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Authentication");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // Méthode pour gérer la réponse après l'authentification Google
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal != null)
            {
                var role = DetermineRoleBasedOnGoogleInfo(result.Principal); // Déterminer le rôle de l'utilisateur

                var user = new ApplicationUser
                {
                    UserName = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                };

                var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                // Vérifiez si l'utilisateur existe déjà dans la base de données
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    // Si l'utilisateur n'existe pas, ajoutez-le à la base de données
                    var createUserResult = await userManager.CreateAsync(user);
                    if (!createUserResult.Succeeded)
                    {
                        // Gérer l'erreur si la création de l'utilisateur échoue
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error = "Failed to create user", createUserResult.Errors });
                    }

                    // Ajoutez le rôle à l'utilisateur nouvellement créé
                    var addRoleResult = await userManager.AddToRoleAsync(user, role);
                    if (!addRoleResult.Succeeded)
                    {
                        // Gérer l'erreur si l'attribution du rôle échoue
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new { Error = "Failed to add role to user", addRoleResult.Errors });
                    }
                }
                else
                {
                    // Si l'utilisateur existe déjà, mettez à jour le rôle si nécessaire
                    if (!await userManager.IsInRoleAsync(existingUser, role))
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(existingUser, role);
                        if (!addRoleResult.Succeeded)
                        {
                            // Gérer l'erreur si l'attribution du rôle échoue
                            return StatusCode(StatusCodes.Status500InternalServerError,
                                new { Error = "Failed to add role to user", addRoleResult.Errors });
                        }
                    }
                }

                // Générez le jeton JWT pour l'utilisateur authentifié
                var jwtToken = GetToken(result.Principal.Claims.ToList());
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }

            return Unauthorized();
        }

        // Méthode pour déterminer le rôle de l'utilisateur en fonction des informations Google
        private string DetermineRoleBasedOnGoogleInfo(ClaimsPrincipal principal)
        {
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            // Implémentez la logique pour déterminer le rôle en fonction de l'e-mail ou d'autres informations
            // Par exemple, si l'e-mail se termine par "@admin.com",
            // attribuez le rôle d'administrateur, sinon attribuez le rôle de client par défaut.
            if (email != null && email.EndsWith("@gmail.com"))
            {
                return "Admin";
            }
            else
            {
                return "Client";
            }
        }


    }
}
