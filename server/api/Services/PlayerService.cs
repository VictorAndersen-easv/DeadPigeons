using api.Dtos;
using dataccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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
            Createdat = DateTime.UtcNow,
            Role = dto.Role
        };
        
        var hasher = new PasswordHasher<Player>();
        myPlayer.Passwordhash = hasher.HashPassword(myPlayer,  dto.Passwordhash);
        
        dbContext.Add(myPlayer);
        dbContext.SaveChanges();
        return myPlayer;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        return dbContext.Players.ToList();
    }

    public async Task<Player> Login(LoginDto dto)
    {
       PasswordHasher<Player> hasher = new PasswordHasher<Player>();
        
        
        var player = dbContext.Players
            .FirstOrDefault(p => p.Email == dto.Email);

        if (player == null)
            return null;

        var result = hasher.VerifyHashedPassword(
            player,
            player.Passwordhash,
            dto.Password
        );

        if (result != PasswordVerificationResult.Success)
            return null;

        return player;
    }
    
    
}