using Core.Entities;
using Core.Services;
using Core.Settings;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Domain.Services
{
    public class JWTAuthenticationManager: IAuthenticationManager
    {
        AppSettings settings;
        public JWTAuthenticationManager(AppSettings settings)
        {
            this.settings = settings;
        }

        public string GenerateToken(User user, int expireMinutes = 60, params Claim[] claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                }.Concat(claims ?? new Claim[0])),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Auth.SecretKey)), SecurityAlgorithms.HmacSha256Signature),
                Audience = settings.Auth.Audience,
                Issuer = settings.Auth.Authority
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    RequireSignedTokens = false
                };

                if (!string.IsNullOrEmpty(settings.Auth.Audience))
                {
                    validationParameters.ValidAudience = settings.Auth.Audience.ToLower();
                    validationParameters.ValidateAudience = true;
                }

                if (!string.IsNullOrEmpty(settings.Auth.Authority))
                {
                    validationParameters.ValidIssuer = settings.Auth.Authority.ToLower();
                    validationParameters.ValidateIssuer = true;
                }

                validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Auth.SecretKey));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);


                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
