using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTRestApi.Controllers
{
    [ApiController]
    [Route("api/Authcontroller")]
    public class authController : ControllerBase
    {
        public static User user = new User();

        public IConfiguration _configuration { get; }

        public authController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: authController
        [HttpPost("register")]
        public ActionResult<User> Register(userDetail request)
        {

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.UserName = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> login(userDetail request) {

            if (user.UserName != request.Username) {
                return BadRequest("User Not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) {
                return BadRequest("Password is Wrong");
            }
            string token = CreateToken (user);
            return Ok(token);
                
        }

            private string CreateToken(User user)
            {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(60),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
            }
            private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) {

            using (var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

    }
}
