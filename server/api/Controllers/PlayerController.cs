using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly MyDbContext _db;

    public PlayerController(MyDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var players = await _db.Players.ToListAsync();
        return Ok(players);
    }

    [HttpGet("debug")]
         public IActionResult DebugEntities()
        {
            var entityTypes = _db.Model.GetEntityTypes();
            var entityNames = entityTypes.Select(e => e.ClrType.FullName).ToList();
            return Ok(entityNames);
        }
    }
