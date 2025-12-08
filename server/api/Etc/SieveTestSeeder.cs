using System.Security.Cryptography;
using System.Text;
using Bogus;
using dataccess;

namespace api.Etc;

public class SieveTestSeeder(MyDbContext ctx, TimeProvider timeProvider) : ISeeder
{
    public async Task Seed()
    {
        await ctx.Database.EnsureCreatedAsync();
        // Clear existing data

        await ctx.SaveChangesAsync();
        await ctx.SaveChangesAsync();

        // Set a deterministic seed for reproducibility
        Randomizer.Seed = new Random(12345);

    }
}