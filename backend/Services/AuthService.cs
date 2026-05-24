using BCrypt.Net;
using backend.DTOs;
using backend.Helpers;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly JwtHelper _jwtHelper;

    public AuthService(
        IAuthRepository repository,
        JwtHelper jwtHelper
    )
    {
        _repository = repository;
        _jwtHelper = jwtHelper;
    }

    public async Task<string> Register(RegisterDto dto)
    {
        var existingUser = await _repository.GetUserByEmail(dto.Email);
        
        if(existingUser != null)
        {
            return "User already exists";
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Role = dto.Role,
            CompanyId = dto.CompanyId
        };

        await _repository.Register(user);

        return "User registered successfully";
    }

     public async Task<string?> Login(LoginDto dto)
    {
        var user = await _repository.GetUserByEmail(dto.Email);

        if (user == null)
        {
            return null;
        }

        var isPasswordValid =
            BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash
            );

        if (!isPasswordValid)
        {
            return null;
        }

        return _jwtHelper.GenerateToken(user);
    }
}
