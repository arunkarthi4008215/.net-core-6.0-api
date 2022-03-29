using Microsoft.AspNetCore.Mvc;
using JWTRestApi.Helpers;

namespace JWTRestApi.Controllers
{
    [ApiController]
    [Route("api/Authcontroller")]
    public class authController : ControllerBase
    {

        private readonly IFauthhelper _authhelper;
                
        public static User user = new User();
        
        public IConfiguration _configuration { get; }

        public authController(IConfiguration configuration, IFauthhelper Authhelper)
        {
            _configuration = configuration;
            _authhelper = Authhelper;
        }
        // GET: authController
        [HttpPost("register")]
        public ActionResult<User> Register(userDetail request)
        {

            _authhelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.UserName = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> login(userDetail request)
        {

            if (user.UserName != request.Username)
            {
                return BadRequest("User Not found");
            }
            if (!_authhelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is Wrong");
            }
            string token = _authhelper.CreateToken(user);
            return Ok(token);

        }



    }
}
