using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeerReviews_Server.Models;
using BeerReviews_Server.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BeerReviews_Server.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {

        private BeerReviews_ServerContext db;
        public AccountController(BeerReviews_ServerContext context)
        {
            db = context;
        }
        

        [HttpPost("/login")]
        //[HttpPost]
       // [ValidateAntiForgeryToken]
        //[Route("api/login")]
        public async Task<IActionResult> Login(LoginModel model)//UserModel model
        {
           // User user = new User { Email = "a@gmail.com", Password = "123", Role = "Admin" };
            if (ModelState.IsValid)
            {
              //  User user = new User { Email = "a@gmail.com", Password = "123", Role = "Admin" };
                  User user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == u.Password);

                if (user != null)
                {
                    await Authenticate(user);

                    //Response.StatusCode = 200;
                    //await Response.WriteAsync("Loged In");
                }
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
            }
            return StatusCode(404, "Koki suda tu man nx siunti ?!");

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User _user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(_user == null)
                {
                    User user = new User { Email = model.Email, Password = model.Passwrod, Role = "User" };
                    db.User.Add(user);
                    await Authenticate(user);
                }
                else
                {
                    ModelState.AddModelError("","Incorect Email or password");
                }
            }
            return StatusCode(404, "Koki suda tu man nx siunti ?!");
        }


        public async Task Authenticate(User user)
        {

            var Claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),

                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(Claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = user.Email

            };
            Response.StatusCode = 200;
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }


    }
}