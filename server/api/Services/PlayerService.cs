using api.Dtos;
using dataccess;
using Microsoft.AspNetCore.Mvc;
using Player = dataccess.Entities.Player;

namespace api.Services;

public class PlayerService(MyDbContext dbContext) :  IPlayerService
{
    public async Task<Player> CreatePlayer(CreatePlayerDto dto)
    {
        var myPlayer = new Player()
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
            Name = dto.Name,
            Passwordhash = dto.Passwordhash,
            Salt = dto.Salt,
            Createdat = DateTime.UtcNow,
            Role = dto.Role
        };
        dbContext.Add(myPlayer);
        dbContext.SaveChanges();
        return myPlayer;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        return dbContext.Players.ToList();
    }
}