namespace api.Dtos;

public record CreatePlayerDto(string Email, string Name, string Passwordhash, string Salt, DateTime Createdat,  string Role);