using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.ModelsData;
using BCryptNet = BCrypt.Net.BCrypt;
using User = SpotMixesBlazor.Shared.Models.User;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] User user)
        {
            var userExist = await _userService.GetUserByEmail(user.Email);

            if (!string.IsNullOrEmpty(userExist.Email))
            {
                return BadRequest("El correo proporcionado ya está asociada a una cuenta.");
            }
            
            var firebase = await _userService.CreateUserWithEmailAndPassword(user.Email, user.Password);
            user.Password = BCryptNet.HashPassword(user.Password);
            await _userService.CreateUser(user);
            var userRegister = await _userService.GetUserByEmail(user.Email);

            if (!string.IsNullOrEmpty(userRegister.Id))
            {
                userRegister.UrlProfile = userRegister.Id;
                await _userService.UpdateUser(userRegister);
            }

            var userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                DisplayName = userRegister.DisplayName,
                UrlProfilePicture = userRegister.UrlProfilePicture,
                UrlProfile = userRegister.UrlProfile
            };

            return Created("Created", userClaims);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserLogin userLogin)
        {
            var firebase = await _userService.SignInWithEmailAndPassword(userLogin.Email, userLogin.Password);

            if (firebase == null)
            {
                return BadRequest("El correo electronico o contraseña son incorrectos.");
            }

            var user = await _userService.GetUserByEmail(firebase.User.Email);

            var userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                DisplayName = user.DisplayName,
                UrlProfilePicture = user.UrlProfilePicture,
                UrlProfile = user.UrlProfile
            };

            return Ok(userClaims);
        }
        
        [HttpGet("getUserByEmail/{email}")]
        public async Task<ActionResult> SearchUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (string.IsNullOrEmpty(user.Id))
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(user);
        }

        [HttpGet("GetUserDataByUrlProfile/{urlProfile}")]
        public async Task<ActionResult> GetUserDataByUrlProfile(string urlProfile)
        {
            var user = await _userService.GetUserDataByUrlProfile(urlProfile);

            if (string.IsNullOrEmpty(user.Id))
            {
                return BadRequest("El usuario no existe");
            }
            
            return Ok(user);
        }
    }
}