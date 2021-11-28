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

            if (userExist != null)
            {
                return BadRequest("El correo proporcionado ya está asociada a una cuenta.");
            }
            
            var firebase = await _userService.CreateUserWithEmailAndPassword(user.Email, user.Password);
            
            user.Password = BCryptNet.HashPassword(user.Password);
            await _userService.CreateUser(user);
            
            var userRegister = await _userService.GetUserByEmail(user.Email);
            
            userRegister.UrlProfile = userRegister.Id;
            await _userService.UpdateUser(userRegister);
            
            var userClaims = new UserClaims()
            {
                Token = firebase.FirebaseToken,
                Email = firebase.User.Email,
                UserId = userRegister.Id,
            };
            
            return Created("Created", userClaims);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] User userLogin)
        {
            var verifiedUser = await _userService.GetUserByEmail(userLogin.Email);

            if (verifiedUser == null)
            {
                return BadRequest("El correo proporcionado no está asociada a una cuenta.");
            }
            
            if (verifiedUser.VerifiedEmail == false)
            {
                return BadRequest("El correo electrónico no está verificado.");
            }

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
                UserId = user.Id
            };

            return Ok(userClaims);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] User user, string id)
        {
            user.Id = id;
            await _userService.UpdateUser(user);
            return Ok("Update");
        }
        
        [HttpGet("countUsers")]
        public async Task<IActionResult> CountUsers()
        {
            var numberOfUsers = await _userService.CountUsers();
            return Ok(numberOfUsers);
        }
        
        [HttpGet("CountVerifiedUsers")]
        public async Task<IActionResult> CountVerifiedUsers()
        {
            var numberOfUsers = await _userService.CountVerifiedUsers();
            return Ok(numberOfUsers);
        }

        [HttpGet("all/{audioPerPage:int}/{skip:int}")]
        public async Task<ActionResult> GetAllUsers(int audioPerPage, int skip)
        {
            var users = await _userService.GetAllUsers(audioPerPage, skip);

            if (users == null) return BadRequest("Not found");

            return Ok(users);
        }
        
        [HttpGet("recent/{audioPerPage:int}/{skip:int}")]
        public async Task<ActionResult> GetRecentUsers(int audioPerPage, int skip)
        {
            var users = await _userService.GetRecentUsers(audioPerPage, skip);

            if (users == null) return BadRequest("Not found");

            return Ok(users);
        }
        
        [HttpGet("verified/{audioPerPage:int}/{skip:int}")]
        public async Task<ActionResult> GetVerifiedUsers(int audioPerPage, int skip)
        {
            var users = await _userService.GetVerifiedUsers(audioPerPage, skip);

            if (users == null) return BadRequest("Not found");

            return Ok(users);
        }
        
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(user);
        }
        
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return BadRequest("El usuario no existe");
            }

            return Ok(user);
        }

        [HttpGet("GetUserByUrlProfile/{urlProfile}")]
        public async Task<ActionResult> GetUserByUrlProfile(string urlProfile)
        {
            var user = await _userService.GetUserByUrlProfile(urlProfile);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("Usuario no encontrado");
        }
        
        [HttpGet("getUserDataByUrlProfile/{urlProfile}")]
        public async Task<ActionResult> GetUserDataByUrlProfile(string urlProfile)
        {
            var user = await _userService.GetUserDataByUrlProfile(urlProfile);

            if (user == null) return BadRequest("El usuario no existe");

            return Ok(user);
        }
    }
}