using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Model.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepo tokenRepo;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] AddUserRequestDto addUserRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = addUserRequestDto.Username,
                Email = addUserRequestDto.Username
            };
           var identityResult= await userManager.CreateAsync(identityUser, addUserRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (addUserRequestDto.Roles != null && addUserRequestDto.Roles.Any()) {

                    identityResult= await userManager.AddToRolesAsync(identityUser,addUserRequestDto.Roles);

                    if (identityResult.Succeeded) {

                        return Ok("User Was Registered! Now You Can login");
                    }
                }

            }

            return BadRequest("Something Went Wrong");

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user= await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null) { 
             
                var passCheck=await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (passCheck)
                {
                    //token generation

                    var roles=await userManager.GetRolesAsync(user);

                    if (roles != null) {
                        var JwtToken=tokenRepo.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = JwtToken
                        };

                    return Ok(response);

                    }

                    
                }
            
            }

            return BadRequest("Username And Password is Incoorect");
        }
    }
}
