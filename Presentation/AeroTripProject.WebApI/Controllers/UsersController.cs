using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUser createUser)
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
                return BadRequest(result.Errors.First().Description);
            }
        }
    }
}
