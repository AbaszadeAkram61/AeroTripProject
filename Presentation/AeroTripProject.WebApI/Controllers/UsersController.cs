using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> UserSignUpAsync(UserSignUp createUser)
        {
            AppUser user = new AppUser
            {
                NameSurname=createUser.NameSurname,
                UserName=createUser.Username,
                Email=createUser.Email,
                
                
            };
            if (createUser.Password!=createUser.PasswordConfirm)
            {
                return BadRequest("Şifrələr eyni deyil");
            }
            
            IdentityResult result= await _userManager.CreateAsync(user, createUser.Password);
            if (result.Succeeded)
            {
                return Ok("Istifadeci elave olundu");
            }
            else
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }
        }

        [HttpPost("UserSignIn")]
        public async Task<IActionResult> UserSignIn(UserSignIn userSignIn)
        {
          AppUser user= await _userManager.FindByNameAsync(userSignIn.Username);
            if (user==null)
            {
                throw new Exception("İstifadəçi tapılmadı");
            }

          var result= await _signInManager.CheckPasswordSignInAsync(user, userSignIn.Password, false);
            if (result.Succeeded)
            {
                return Ok("Sign in ugurlu");
            }
            else
            {
                return BadRequest("İstifadəçi adı və ya şifrə yanlışdır");
            }

        }
    }
}
