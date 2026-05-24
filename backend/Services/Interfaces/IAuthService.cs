using backend.DTOs;

namespace backend.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegisterDto dto);
    Task<string?> Login(LoginDto dto);
}