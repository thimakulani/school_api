using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using school_api.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _config = config;
        }

     
        [HttpPost("login")]
        public async Task<ActionResult<UserLogin>> Login(UserLogin user)
        {
            if (user == null)
            {
                return BadRequest("All fields are required");
            }
            var results = await signInManager.PasswordSignInAsync(user.Username.Trim(), user.Password.Trim(), false, false);
            if (results.Succeeded)
            {
                var user_data = await userManager.FindByEmailAsync(user.Username);
                //role = roleManager

                var token = GenerateToken(user_data);
                AuthResponse authResponse = new()
                {
                    ApplicationUser = user_data,
                    Token = token
                };
                return Ok(authResponse);
            }
            else
            {
                return BadRequest("Incorrect user name or password");
            }

        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegister user) 
        {

            if (user == null)
            {
                return BadRequest("All fields are required");
            }
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = user.Email,
                Name = user.FirstName,
                Surname = user.LastName,
                PhoneNumber = user.Phone,
                UserName = user.Username,
                

            };
            var results = await userManager.CreateAsync(applicationUser, user.Password);

            if (results.Succeeded)
            {
                var _user = await userManager.FindByEmailAsync(user.Email.Trim());
                await userManager.AddToRoleAsync(_user, user.Role);
                var response = new AuthResponse
                {
                    Token = GenerateToken(_user),
                    ApplicationUser = _user
                };
                return Ok(response);
            }
            else
            {
                string errors = "";
                foreach (var item in results.Errors)
                {
                    errors += item.Description + "\n";
                }
                return NotFound(errors);
            }
        }
        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                //new Claim(ClaimTypes.Role, user.),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthResponse
    {
        public string Token { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
    public class UserRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
