using BetsAppMVC.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace BetsAppMVC.Pages
{
    public class LoginModel : PageModel
    {
        IPlayersService _playersService;
        public LoginModel(IPlayersService playersService) 
        {
            _playersService = playersService;
        }    
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "User Name")]
            public string? UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("login");
            if (string.IsNullOrEmpty(Input.UserName))
            {
                ModelState.AddModelError("login", "User name is empty");
                return Page();
            }

            if (string.IsNullOrEmpty(Input.Password))
            {
                ModelState.AddModelError("login", "Password is empty");
                return Page();
            }

            var player = _playersService.GetPlayerByUserNameAndPassword(Input.UserName, Input.Password);
            if (player == null)
            {
                ModelState.AddModelError("login", "Wrong user name or password");
                return Page();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkey@050603020205060302020506030202"));
            var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "BetsApp",
                audience: "https://localhost:44319",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: loginCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            HttpContext.Session.SetString("Token", tokenString);
            HttpContext.Session.SetInt32("PlayerId", player.Id);

            return Redirect("/Home");
        }
    }
}
