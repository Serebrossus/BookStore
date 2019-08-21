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
    private readonly LoginContext loginContext;

    public AuthController(LoginContext context)
    {
      loginContext = context;
    }

    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]Login user)
    {
      if (user == null)
      {
        return BadRequest("Invalid client request");
      }

      if (user.UserName == "johndoe" && user.Password == "def@123")
      {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "Manager")// "Operator"
        };
        var tokeOptions = new JwtSecurityToken(
            issuer: "http://localhost:5000",
            audience: "http://localhost:5000",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return Ok(new { Token = tokenString });
      }
      else
      {
        return Unauthorized();
      }
    }
  }
}
}
