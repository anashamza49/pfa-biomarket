using AutoMapper;
using pfaproject.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping de Client vers ClientDTO
        CreateMap<Client, ClientDTO>();

        // Mapping de ClientDTO vers Client
        CreateMap<ClientDTO, Client>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Ignorer UserId lors du mappage
            .ForMember(dest => dest.User, opt => opt.Ignore()); // Ignorer ApplicationUser lors du mappage
    }
}
