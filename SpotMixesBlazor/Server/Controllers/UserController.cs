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
        private UserClaims _userClaims;
        private IReadOnlyList<User> _userList;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            var userEmail = await _userService.SearchUserByEmail(user.Email);

            if (userEmail.Count > 0)
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

            _userList = await _userService.SearchUserByEmail(firebase.User.Email);

            var nickname = "";
            var urlProfilePicture = "";
            
            foreach (var user in _userList)
            {
                nickname = user.Nickname;
                urlProfilePicture = user.UrlProfilePicture;
            }

            _userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                Nickname = nickname,
                UrlProfilePicture = urlProfilePicture
            };

            return Ok(_userClaims);
        }

        [HttpGet("getUserId/{email}")]
        public async Task<ActionResult> GetUserId(string email)
        {
            _userList = await _userService.SearchUserByEmail(email);

            var userId = "";

            foreach (var user in _userList)
            {
                userId = user.Id;
            }

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(userId);
        }
    }
}