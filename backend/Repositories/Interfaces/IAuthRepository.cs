using backend.Models;

namespace backend.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User> Register(User user);
}