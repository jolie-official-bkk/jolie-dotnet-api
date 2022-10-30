using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using JolieApi.DataContext;

namespace SEENApiV2_Admin.Repository
{
    public interface IJWTManagerRepository
    {
        string GenerateJwtToken(string email);
        bool ValidateToken(string token);
        string GetEmailFromToken(string token);
    }

    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _IConfig;
        private readonly JolieDataContext _context;

        public JWTManagerRepository(IConfiguration IConfig, JolieDataContext JolieDataContext)
        {
            this._IConfig = IConfig;
            this._context = JolieDataContext;
        }

        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_IConfig["JWT:JWT_SECRET_KEY"]);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "Admin",
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_IConfig["JWT:JWT_SECRET_KEY"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                return true;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }

        public string GetEmailFromToken(string token)
        {
            if (token == null)
                return "";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_IConfig["JWT:JWT_SECRET_KEY"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                return email;
            }
            catch
            {
                // return null if validation fails
                return "";
            }
        }
    }
}