namespace api.Dtos;

public record CreatePlayerDto(string Email, string Name, string Passwordhash, DateTime Createdat,  string Role);