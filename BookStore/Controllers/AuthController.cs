using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    //private readonly LoginContext loginContext;

    //public AuthController(LoginContext context)
    //{
    //  loginContext = context;
    //}

    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]Login user)
    {
      if (user == null)
      {
        return BadRequest("Invalid client request");
      }

      var loginsList = new List<Login>() {
        new Login() {
          UserName = "johndoe",
          Password ="def@123",
        },
        new Login() {
          UserName = "nikolasjfury",
          Password ="def@234",
        },
      };

      if (loginsList.FirstOrDefault(x=> x.UserName == user.UserName && x.Password == user.Password) != null)
      {
        var tokeOptions = GetJwtSecurityToken(user.UserName, user.UserName == "nikolasjfury");

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return Ok(new { Token = tokenString });
      }
      else
      {
        return Unauthorized();
      }
    }

    private JwtSecurityToken GetJwtSecurityToken(string login, bool isAdmin = false)
    {
      var rnd = new Random();
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"superSecretKey@345"));// {rnd.Next(1000)}
      var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, isAdmin ? "Operator" : "Manager")
        };

      var tokeOptions = new JwtSecurityToken(
          issuer: "http://localhost:5000",
          audience: "http://localhost:5000",
          claims: claims,
          expires: DateTime.Now.AddMinutes(5),
          signingCredentials: signinCredentials
      );
      return tokeOptions;
    }
  }
}
