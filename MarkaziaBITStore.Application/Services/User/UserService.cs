using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarkaziaBITStore.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly BitStoreDbContext _context;
        private readonly JwtSettings _jwt;
        private readonly IMemoryCache _cache;

        public UserService(BitStoreDbContext context, IOptions<JwtSettings> jwtOptions, IMemoryCache cache)
        {
            _context = context;
            _jwt = jwtOptions.Value;
            _cache = cache;
        }

        public async Task<UserResponseDto?> Register(RegisterDto dto)
        {
            //if (await _context.Bitusers.AnyAsync(u => u.Email == dto.Email))
            //    return null;

            //var user = new Bituser
            //{
            //    UserName = dto.UserName,
            //    Email = dto.Email,
            //    PasswordHash = HashPassword(dto.Password),
            //    AvailablePoints = 0,
            //    EarnedPoints = 0,
            //    CreatedAt = DateTime.UtcNow,
            //    IsDeleted = false
            //};

            //_context.Bitusers.Add(user);
            //await _context.SaveChangesAsync();

            //return new UserResponseDto
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    AvailablePoints = user.AvailablePoints,
            //    Token = GenerateJwtToken(user)
            //};

            return null; // Registration is disabled
        }

        public async Task<UserResponseDto?> Login(LoginDto dto)
        {
            //var user = await _context.Bitusers.FirstOrDefaultAsync(u => u.Email == dto.Email && !u.IsDeleted);
            //if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
            //    return null;

            //return new UserResponseDto
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    AvailablePoints = user.AvailablePoints,
            //    Token = GenerateJwtToken(user)
            //};

            return null; // Login is disabled
        }

        //private string GenerateJwtToken(Bituser user)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _jwt.Issuer,
        //        audience: _jwt.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }




        public async Task<string?> RequestPasswordResetAsync(string email)
        {
            //var user = await _context.Bitusers.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
            //if (user == null) return null;

            //var token = Guid.NewGuid().ToString("N");

            //// Store token in cache with expiry (1 hour)
            //_cache.Set(token, user.UserId, TimeSpan.FromHours(1));

            //// TODO: Send token by email instead of returning
            //return token;

            return null; // Password reset is disabled
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            //if (!_cache.TryGetValue(token, out int userId))
            //    return false;

            //var user = await _context.Bitusers.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
            //if (user == null) return false;

            //user.PasswordHash = HashPassword(newPassword);
            //await _context.SaveChangesAsync();

            //// Remove token after use
            //_cache.Remove(token);

            return true;
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

     
    }
}
