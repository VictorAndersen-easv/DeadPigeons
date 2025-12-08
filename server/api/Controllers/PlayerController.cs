using api.Dtos;
using api.Services;
using dataccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService) : ControllerBase
{

    [Route(nameof(GetAllPlayers))]
    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetAllPlayers()
    {

        var players = await playerService.GetAllPlayers();
        return players;
    }

    [Route(nameof(CreatePlayer))]
    [HttpPost]
    public async Task<ActionResult<Player>> CreatePlayer([FromBody] CreatePlayerDto dto)
    {
        // Possible server-side validation method
        // if(dto.Name < 0 || > 5) throw new ValidationException("Must be 0-5")

        /* var myPlayer = new Player()
     {
         Id = Guid.NewGuid().ToString(),
         Email = dto.Email,
         Name = dto.Name
     };
     dbContext.Add(myPlayer);
     dbContext.SaveChanges();
     return myPlayer;
 */

        var result = await playerService.CreatePlayer(dto);
        return result;
    }
}