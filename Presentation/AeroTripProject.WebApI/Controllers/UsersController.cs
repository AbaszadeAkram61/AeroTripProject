using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using FluentValidation;
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
        private readonly IValidator<UserSignUp> _validatorSignUp;
        private readonly IValidator<UserSignIn> _validatorSignIn;
        private readonly IValidator<UpdateUserDto> _validatorUpdateUser;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IValidator<UserSignUp> validatorSignUp, IValidator<UserSignIn> validatorSignIn, IValidator<UpdateUserDto> validatorUpdateUser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validatorSignUp = validatorSignUp;
            _validatorSignIn = validatorSignIn;
            _validatorUpdateUser = validatorUpdateUser;
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
            var validationresult = _validatorSignUp.Validate(createUser);

            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }

            AppUser user = new AppUser
            {
                NameSurname = createUser.NameSurname,
                UserName = createUser.Username,
                Email = createUser.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, createUser.Password);

            if (result.Succeeded)
            {
                return Ok("İstifadəçi əlavə olundu");
            }

            return BadRequest(result.Errors.Select(x => x.Description));
        }

        [HttpPost("UserSignIn")]
        public async Task<IActionResult> UserSignIn(UserSignIn userSignIn)
        {
            var validationResult = _validatorSignIn.Validate(userSignIn);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }

            AppUser user = await _userManager.FindByNameAsync(userSignIn.Username);

            if (user == null)
            {
                return BadRequest(new List<object>
        {
            new
            {
                PropertyName = "Username",
                ErrorMessage = "İstifadəçi tapılmadı"
            }
        });
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

            return BadRequest(new List<object>
    {
        new
        {
            PropertyName = "Password",
            ErrorMessage = "İstifadəçi adı və ya şifrə yanlışdır"
        }
    });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var validationresult = _validatorUpdateUser.Validate(updateUserDto);

            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }

            var user = await _userManager.FindByNameAsync(updateUserDto.OldUsername);

           

            if (!string.IsNullOrWhiteSpace(updateUserDto.Username) &&
                updateUserDto.Username != user.UserName)
            {
                var checkUsername = await _userManager.FindByNameAsync(updateUserDto.Username);

              

                user.UserName = updateUserDto.Username;
                user.NormalizedUserName = updateUserDto.Username.ToUpper();
            }

            user.NameSurname = updateUserDto.NameSurname;
            user.Email = updateUserDto.Email;
            user.NormalizedEmail = updateUserDto.Email.ToUpper();

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
