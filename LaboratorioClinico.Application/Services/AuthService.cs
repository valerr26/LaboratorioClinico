using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IConfiguration _cfg;

        public AuthService(IUsuarioRepository repo, IConfiguration cfg)
        {
            _repo = repo;
            _cfg = cfg;
        }

        public async Task<(bool ok, string msg)> RegisterAsync( string username, string password, int idRol)
        {
            var existing = await _repo.GetByUsernameAsync(username);
            if (existing != null) return (false, "El nombre de usuario ya está registrado");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var usuario = new Usuario
            {          
                Username = username,
                PasswordHash = hash,
                IdRol = idRol
            };
            await _repo.AddUsuarioAsync(usuario);
            return (true, "Usuario registrado");
        }

        public async Task<(bool ok, string tokenOrMsg)> LoginAsync(string username, string password)
        {
            var user = await _repo.GetByUsernameAsync(username);
            if (user is null)
                return (false, "Credenciales inválidas");
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return (false, "Credenciales inválidas");
            var token = GenerateJwt(user);
            return (true, token);
        }

        private string GenerateJwt(Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IdRol.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:ExpireMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}