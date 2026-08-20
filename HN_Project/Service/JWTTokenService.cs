using HN_Backend.Data;
using HN_Backend.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HN_Backend.Service
{
    public class JWTTokenService
    {
        private readonly JwtSettingsDto _jwtSettings;

        public JWTTokenService(IOptions<JwtSettingsDto> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        public string GenerateToken(LoginUser user, string jwtIdentifier)
        {
            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.LoginUserId.ToString()),
                                new Claim("userId",user.LoginUserId.ToString()),
                                new Claim("companyId", user.CompanyId.ToString()),
                                new Claim("locationId", user.LocationId.ToString()),
                                new Claim("jwtIdentifier",jwtIdentifier)
                            };

            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
         
    }
}
