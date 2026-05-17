using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound("İstifadəçi tapılmadı");
            }

            return Ok(user);
        }

        [HttpGet("GetUserListCount")]
        public async Task<IActionResult> GetUserListCount()
        {
          return Ok( await _userManager.Users.CountAsync());
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok( await _userManager.Users.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdUser(int Id)
        {
            var value=await _userManager.FindByIdAsync(Id.ToString());
            return Ok(value);
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
            AppUser user = await _userManager.FindByNameAsync(userSignIn.Username);

            if (user == null)
            {
                throw new Exception("İstifadəçi tapılmadı");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                userSignIn.Password,
                false
            );

            if (result.Succeeded)
            {
                return Ok(new
                {
                    id = user.Id,
                    username = user.UserName
                });
            }
            else
            {
                return BadRequest("İstifadəçi adı və ya şifrə yanlışdır");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByNameAsync(updateUserDto.Username);

            if (user == null)
            {
                return NotFound("User tapılmadı");
            }

            user.NameSurname = updateUserDto.NameSurname;
            user.Email = updateUserDto.Email;

            if (!string.IsNullOrWhiteSpace(updateUserDto.ImageUrl))
            {
                user.ImageUrl = updateUserDto.ImageUrl;
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUserDto.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Məlumat dəyişdirildi");
            }

            return BadRequest(result.Errors.Select(x => x.Description));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
           var value=await _userManager.FindByIdAsync(Id.ToString());
           await _userManager.DeleteAsync(value);
           return Ok("Melumat silindi");
        }
    }
}
