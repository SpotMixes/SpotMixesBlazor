using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared;
using BCryptNet = BCrypt.Net.BCrypt;
using User = SpotMixesBlazor.Shared.User;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private UserClaims _userClaims = new();

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            var userExist = await _userService.SearchUserByEmail(user.Email);

            if (!string.IsNullOrEmpty(userExist.Email))
            {
                return BadRequest("El correo proporcionado ya está asociada a una cuenta.");
            }
            
            var firebase = await _userService.CreateUserWithEmailAndPassword(user.Email, user.Password);

            if (firebase == null)
            {
                return Conflict("Error al registrarse inténtelo más tarde por favor.");
            }
            
            user.Password = BCryptNet.HashPassword(user.Password);
            await _userService.CreateUser(user);
            
            var userRegister = await _userService.SearchUserByEmail(user.Email);

            if (string.IsNullOrEmpty(userExist.Id))
            {
                userRegister.UrlProfile = userRegister.Id;
                await _userService.UpdateUser(userRegister);
            }

            _userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                Nickname = user.Nickname,
                UrlProfilePicture = user.UrlProfilePicture
            };

            return Created("Created", _userClaims);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(UserLogin userLogin)
        {
            var firebase = await _userService.SignInWithEmailAndPassword(userLogin.Email, userLogin.Password);

            if (firebase == null)
            {
                return BadRequest("El correo electronico o contraseña son incorrectos.");
            }

            var user = await _userService.SearchUserByEmail(firebase.User.Email);

            _userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                Nickname = user.Nickname,
                UrlProfilePicture = user.UrlProfilePicture,
                UrlProfile = user.UrlProfile
            };

            return Ok(_userClaims);
        }

        [HttpGet("searchUserByEmail/{email}")]
        public async Task<ActionResult> SearchUserByEmail(string email)
        {
            var user = await _userService.SearchUserByEmail(email);

            if (string.IsNullOrEmpty(user.Id))
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(user);
        }

        [HttpGet("getUserByUrlProfile/{urlProfile}")]
        public async Task<ActionResult> GetUserByUrlProfile(string urlProfile)
        {
            var user = await _userService.GetUserByUrlProfile(urlProfile);

            if (string.IsNullOrEmpty(user.Id))
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(user);
        }
    }
}