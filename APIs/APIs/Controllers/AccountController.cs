using APIs.DTOs;
using APIs.Models;
using APIs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIs.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Private Fields Used in DependecyInjection
        /// </summary>
        #region PrivateFields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleInManager;
        private readonly GraduationDataBaseContext _context;
        private readonly IJwtService _jwtService;


        #endregion

        /// <summary>
        /// DependecyInjection
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="context"></param>
        #region DependecyInjection
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager, GraduationDataBaseContext context, IJwtService jwtService)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleInManager = roleManager;
            _context = context;
            _jwtService = jwtService;
        }
        #endregion

        /// <summary>
        /// SellerRegister : Take Object from "SellerRegiserDTO"
        /// Then Record All Info Belongs TO "Seller" In The DataBase
        /// And Give The Athu As A "Seller"
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        #region SellerRegister
        [HttpPost("SellerRegiser")]
        public async Task<ActionResult<ApplicationUser>> PostSellerRegister(SellerRegiserDTO registerDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                e.ErrorMessage));
                return Problem(errorMessage);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                NameOfPerson = registerDTO.FirstName
            };

            Seller seller = new Seller
            {
                Email = registerDTO.Email,

                Pass = registerDTO.Password,

                Governate = registerDTO.Governate,

                City = registerDTO.City,

                Street = registerDTO.Street,

                FName = registerDTO.FirstName,

                LName = registerDTO.LastName,

                ShopName = registerDTO.ShopName,

                Lat = registerDTO.Lat,

                Long = registerDTO.Long

            };

            IdentityResult result = await _userManager.CreateAsync
            (user, registerDTO.Password);


            if (result.Succeeded)
            {
                if (!await _roleInManager.RoleExistsAsync("Seller"))
                {
                    var sellerRole = new ApplicationRole { Name = "Seller" };
                    await _roleInManager.CreateAsync(sellerRole);
                }
                await _userManager.AddToRoleAsync(user, "Seller");

                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();
                //sign-in  
                await _signInManager.SignInAsync(user, isPersistent: false);
                IList<string> roles = new List<string> { "Seller" }; // Example for Seller role
                var athenticationResponse = _jwtService.CreateJwtToken(user, roles);
                return Ok(athenticationResponse);
            }

            else
            {
                string errorMessage = string.Join(" | ",
               result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }

        }
        #endregion

        /// <summary>
        /// BuyerRegiser : Take Object from "BuyerRegisterDTO"
        /// Then Record All Info Belongs TO "Buyer" In The DataBase
        /// And Give The Athu As A "Buyer"
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        #region BuyerRegister
        [HttpPost("BuyerRegiser")]
        public async Task<ActionResult<ApplicationUser>> PostBuyerRegister(BuyerRegisterDTO registerDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                e.ErrorMessage));
                return Problem(errorMessage);

            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                NameOfPerson = registerDTO.FirstName
            };

            Buyer buyer = new Buyer
            {
                Email = registerDTO.Email,

                Pass = registerDTO.Password,

                FName = registerDTO.FirstName,

                LName = registerDTO.LastName,

            };

            IdentityResult result = await _userManager.CreateAsync
            (user, registerDTO.Password);



            if (result.Succeeded)
            {
                if (!await _roleInManager.RoleExistsAsync("Buyer"))
                {
                    var buyerRole = new ApplicationRole { Name = "Buyer" };
                    await _roleInManager.CreateAsync(buyerRole);
                }
                await _userManager.AddToRoleAsync(user, "Buyer");

                _context.Buyers.Add(buyer);
                await _context.SaveChangesAsync();
                //sign-in  
                await _signInManager.SignInAsync(user, isPersistent: false);
                IList<string> roles = new List<string> { "Buyer" }; // Example for Seller role
                var athenticationResponse = _jwtService.CreateJwtToken(user, roles);
                return Ok(athenticationResponse);
            }

            else
            {
                string errorMessage = string.Join(" | ",
               result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }


        }
        #endregion

        /// <summary>
        /// check if the Email Already Existing
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>

        #region IsEmailAlreadyRegistered
        [HttpGet("IsEmailAlreadyRegistered")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Ok(true); // Email is not registered
            else
                return Ok(false); // Email is already registered
        }
        #endregion

        /// <summary>
        /// Login EndPoint With "JWT" Athentication
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                e.ErrorMessage));
                return Problem(errorMessage);
            }

            // Attempt to sign in the user
            var result =
                await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: true, lockoutOnFailure: true);

            // Check if login was successful
            if (result.Succeeded)
            {
                ApplicationUser? user = await
                    _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return NoContent();
                }
                //sign-in  
                await _signInManager.SignInAsync(user, isPersistent: false);
                var roles = await _userManager.GetRolesAsync(user);
                var athenticationResponse = _jwtService.CreateJwtToken(user, roles);
                return Ok(athenticationResponse);
            }
            else
            {
                return Problem("Unathoriazed , Invalid Name or Password");
            }

        }
        #endregion

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        #region Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> GetLogout()
        {

            await _signInManager.SignOutAsync();
            return NoContent();
        }
        #endregion

        /// <summary>
        /// AdminRegiser : Take Object from "AdminRegisterDTO"
        /// Then Record All Info Belongs TO "Admin" In The DataBase
        /// And Give The Athu As A "Buyer"
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        #region AdminRegiser
        [Authorize(Roles = "Admin")]
        [HttpPost("AdminRegiser")]
        public async Task<ActionResult<ApplicationUser>> PostAdminRegister(AdminRegisterDTO registerDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e =>
                e.ErrorMessage));
                return Problem(errorMessage);

            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.UserName,

                PasswordHash = registerDTO.Password
            };

            IdentityResult result = await _userManager.CreateAsync
            (user, registerDTO.Password);



            if (result.Succeeded)
            {
                //sign-in  
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "Admin");
                return Ok(user);
            }

            else
            {
                string errorMessage = string.Join(" | ",
               result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }


        }
        #endregion

        /// <summary>
        /// update-password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        #region update-password
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserPasswordUpdateDTO userDTO)
        {
            if (userDTO == null || string.IsNullOrEmpty(userDTO.Email) || string.IsNullOrEmpty(userDTO.OldPassword) || string.IsNullOrEmpty(userDTO.NewPassword))
            {
                return BadRequest("Invalid request data.");
            }

            // Check if the user is a buyer
            var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Email == userDTO.Email);
            // Check if the user is a seller
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == userDTO.Email);

            if (buyer == null && seller == null)
            {
                return NotFound("User not found.");
            }

            ApplicationUser user;
            IdentityResult result;

            if (buyer != null)
            {
                user = await _userManager.FindByEmailAsync(buyer.Email);

                // Verify old password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userDTO.OldPassword);
                if (!passwordCheck)
                {
                    return Unauthorized("Old password is incorrect.");
                }

                // Update buyer's password
                buyer.Pass = userDTO.NewPassword;
                _context.Buyers.Update(buyer);
                result = await _userManager.ChangePasswordAsync(user, userDTO.OldPassword, userDTO.NewPassword);
                if (!result.Succeeded)
                {
                    return StatusCode(500, "Failed to update password.");
                }
            }

            if (seller != null)
            {
                user = await _userManager.FindByEmailAsync(seller.Email);

                // Verify old password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userDTO.OldPassword);
                if (!passwordCheck)
                {
                    return Unauthorized("Old password is incorrect.");
                }

                // Update seller's password
                seller.Pass = userDTO.NewPassword;
                _context.Buyers.Update(buyer);

                result = await _userManager.ChangePasswordAsync(user, userDTO.OldPassword, userDTO.NewPassword);
                if (!result.Succeeded)
                {
                    return StatusCode(500, "Failed to update password.");
                }
            }
            await _context.SaveChangesAsync();
            return Ok("Password updated successfully.");
        }
        #endregion

        //Two Methods to send request and reset pass 
        #region ResetPassword
        /// <summary>
        /// RequestPasswordReset
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Do not reveal that the user does not exist
                return Ok(new { Message = "Password reset request processed." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, protocol: HttpContext.Request.Scheme);

            return Ok(new { Message = "Password reset email has been sent." });
        }
        /// <summary>
        /// Reset-password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserPasswordUpdateDTO userDTO)
        {
            if (userDTO == null || string.IsNullOrEmpty(userDTO.Email) || string.IsNullOrEmpty(userDTO.OldPassword) || string.IsNullOrEmpty(userDTO.NewPassword))
            {
                return BadRequest("Invalid request data.");
            }

            // Check if the user is a buyer
            var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Email == userDTO.Email);
            // Check if the user is a seller
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == userDTO.Email);

            if (buyer == null && seller == null)
            {
                return NotFound("User not found.");
            }

            ApplicationUser user;
            IdentityResult result;

            if (buyer != null)
            {
                user = await _userManager.FindByEmailAsync(buyer.Email);

                // Verify old password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userDTO.OldPassword);
                if (!passwordCheck)
                {
                    return Unauthorized("Old password is incorrect.");
                }

                // Update buyer's password
                buyer.Pass = userDTO.NewPassword;
                result = await _userManager.ChangePasswordAsync(user, userDTO.OldPassword, userDTO.NewPassword);
                if (!result.Succeeded)
                {
                    return StatusCode(500, "Failed to update password.");
                }

                // Save changes to the database
                _context.Buyers.Update(buyer);
            }

            if (seller != null)
            {
                user = await _userManager.FindByEmailAsync(seller.Email);

                // Verify old password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userDTO.OldPassword);
                if (!passwordCheck)
                {
                    return Unauthorized("Old password is incorrect.");
                }

                // Update seller's password
                seller.Pass = userDTO.NewPassword;
                result = await _userManager.ChangePasswordAsync(user, userDTO.OldPassword, userDTO.NewPassword);
                if (!result.Succeeded)
                {
                    return StatusCode(500, "Failed to update password.");
                }

                // Save changes to the database
                _context.Sellers.Update(seller);
            }

            await _context.SaveChangesAsync();

            return Ok("Password updated successfully.");
        }
        #endregion

    }
}

