using pfaproject.DTOs;
using pfaproject.Models;

public interface ICooperativeService
{
    Task AddCooperativeAsync(CooperativeDTO cooperativeDTO, string userId);
    Task UpdateCooperativeAsync(CooperativeDTO cooperativeDTO, string userId);
    Task<bool> AjouterMagasinAsync(MagasinDTO magasinDto, string cooperativeUserId);

}
