using api.Dtos;
using dataccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Services;

public interface IPlayerService
{
    Task<Player> CreatePlayer(CreatePlayerDto dto);
    
    Task<List<Player>> GetAllPlayers();

    Task<Player> Login(LoginDto dto);
}