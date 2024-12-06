using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using pfaproject.Data;
using pfaproject.DTOs;
using pfaproject.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class CooperativeService : ICooperativeService
{
    private readonly MyContext _context;

    public CooperativeService(MyContext context)
    {
        _context = context;
    }

    public async Task AddCooperativeAsync(CooperativeDTO cooperativeDTO, string userId)
    {
        var existingCooperative = await _context.Cooperatives.FirstOrDefaultAsync(c => c.UserId == userId);
        if (existingCooperative != null)
        {
            throw new Exception("L'utilisateur ne peut ajouter qu'une seule coopérative.");
        }

        // Si aucune coopérative n'existe pour cet utilisateur, ajoutez la nouvelle coopérative
        var cooperative = new Cooperative
        {
            UserId = userId,
            NomCooperative = cooperativeDTO.NomCooperative,
            SecteurCooperative = cooperativeDTO.SecteurCooperative,
            Province = cooperativeDTO.Province,
            SiegeCooperative = cooperativeDTO.SiegeCooperative,
            CertificatPath = await SaveCertificateAsync(cooperativeDTO.Certificat)
        };

        _context.Cooperatives.Add(cooperative);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateCooperativeAsync(CooperativeDTO cooperativeDTO, string userId)
    {
        var cooperative = await _context.Cooperatives.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cooperative == null)
        {
            throw new Exception("Coopérative non trouvée.");
        }

        cooperative.NomCooperative = cooperativeDTO.NomCooperative;
        cooperative.SecteurCooperative = cooperativeDTO.SecteurCooperative;
        cooperative.Province = cooperativeDTO.Province;
        cooperative.SiegeCooperative = cooperativeDTO.SiegeCooperative;

        if (cooperativeDTO.Certificat != null && cooperativeDTO.Certificat.Length > 0)
        {
            cooperative.CertificatPath = await SaveCertificateAsync(cooperativeDTO.Certificat);
        }

        await _context.SaveChangesAsync();
    }

    private async Task<string> SaveCertificateAsync(IFormFile certificat)
    {
        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        var filePath = Path.Combine(uploadDirectory, Guid.NewGuid().ToString() + Path.GetExtension(certificat.FileName));
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await certificat.CopyToAsync(stream);
        }

        return filePath;
    }

    public async Task<bool> AjouterMagasinAsync(MagasinDTO magasinDto, string cooperativeUserId)
    {
        try
        {
            // 1. Récupérer la coopérative correspondante à partir de l'identifiant d'utilisateur
            var cooperative = await _context.Cooperatives.FirstOrDefaultAsync(c => c.UserId == cooperativeUserId);

            if (cooperative == null)
            {
                // La coopérative correspondante n'a pas été trouvée
                // Vous pouvez gérer cela en retournant false ou en lançant une exception
                return false;
            }

            // 2. Créer une instance de Magasin à partir des données fournies dans le DTO
            var magasin = new Magasin
            {
                Nom = magasinDto.Nom,
                Adresse = magasinDto.Adresse,
            };

            // 3. Ajouter le magasin à la liste des magasins de la coopérative
            if (cooperative.Magasins == null)
                cooperative.Magasins = new List<Magasin>();

            cooperative.Magasins.Add(magasin);

            // 4. Effectuer des opérations de sauvegarde dans la base de données, si nécessaire
            // Cela dépend de votre implémentation concrète de la logique d'accès aux données

            // 5. Retourner true si l'ajout du magasin s'est déroulé avec succès
            return true;
        }
        catch (Exception ex)
        {
            // Gérer les erreurs ici, comme les erreurs de validation ou d'accès aux données
            // Vous pouvez également logger l'erreur pour un suivi ultérieur
            Console.WriteLine($"Erreur lors de l'ajout du magasin : {ex.Message}");
            return false;
        }
    }






}
