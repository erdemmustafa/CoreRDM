using CoreRDM.Entities;
using CoreRDM.Helpers;
using CoreRDM.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using NHibernate.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreRDM.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
    }
    public class UserService : IUserService
    {
        private readonly NHibernate.ISession _session;


        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, NHibernate.ISession session)
        {
            _appSettings = appSettings.Value;
            _session = session;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _session.Query<Users>().Where(x => x.UserCode == model.Username && x.Password == model.Password).SingleOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<Users> GetAll()
        {
            return _session.Query<Users>();
        }

        public Users GetById(int id)
        {
            return _session.Query<Users>().FirstOrDefault(x => x.User_Id == id);
        }

        // helper methods

        private string generateJwtToken(Users user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("id", user.User_Id.ToString()),
                        new Claim("expiry", DateTime.Now.AddDays(1).ToString()
                        )
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenExp = tokenDescriptor.Expires;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch { return null; }
        }
    }
}
