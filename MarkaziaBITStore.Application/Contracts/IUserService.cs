using MarkaziaBITStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Contracts
{
    public interface IUserService   
    {
        Task<UserResponseDto?> Register(RegisterDto dto);

        Task<UserResponseDto?> Login(LoginDto dto);

        //string GenerateJwtToken(Bituser user);

        string HashPassword(string password);

        Task<string?> RequestPasswordResetAsync(string email);


        Task<bool> ResetPasswordAsync(string token, string newPassword);


        bool VerifyPassword(string password, string storedHash);

    }
}
