using AeroTripProject.Application.Dtos.AppRole;
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
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidator<AppRole> _validator;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<AppRole> roleManager, IValidator<AppRole> validator, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _validator = validator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
           return Ok(await _roleManager.Roles.ToListAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdRole(int Id)
        {
            var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAppRole createAppRole)
        {
            AppRole role = new AppRole
            {
                Name=createAppRole.Name
            };
            var validationresult= _validator.Validate(role);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }));
            }

           var result= await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok("Rol uğurla əlavə olundu");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var value =await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            await _roleManager.DeleteAsync(value);
            return Ok("Melumat silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateAppRole updateAppRole)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(x => x.Id == updateAppRole.Id);

            if (role == null)
            {
                return NotFound("Rol tapılmadı");
            }

            role.Name = updateAppRole.Name;
            role.NormalizedName = updateAppRole.Name.ToUpperInvariant();

            var validationResult = _validator.Validate(role);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }));
            }

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok("Məlumat dəyişdirildi");
            }

            return BadRequest(result.Errors.Select(x => new
            {
                PropertyName = "Name",
                ErrorMessage = x.Description
            }));
        }


        [HttpGet("AssignRole/{Id}")]
        public async Task<IActionResult> AssignRole(int Id)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            var roles =await _roleManager.Roles.ToListAsync();
            var useRoles =await _userManager.GetRolesAsync(user);
            List<RoleAssign> roleAssigns = new List<RoleAssign>();

            foreach (var role in roles)
            {
                RoleAssign assign = new RoleAssign();
                assign.Id = role.Id;
                assign.RoleName = role.Name;
                assign.RoleExist = useRoles.Contains(role.Name);
                roleAssigns.Add(assign);
            }

            return Ok(roleAssigns);

        }

        [HttpPost("AssignRole/{userId}")]
        public async Task<IActionResult> AssignRole(int userId, List<RoleAssign> model)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return NotFound("İstifadəçi tapılmadı");

            foreach (var item in model)
            {
                if (item.RoleExist)
                {
                    if (!await _userManager.IsInRoleAsync(user, item.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, item.RoleName);
                    }
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(user, item.RoleName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }
            }

            return Ok("Rol təyinatları yeniləndi");
        }

    }
}
