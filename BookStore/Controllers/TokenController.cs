using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TokenController : ControllerBase
  {
    public IConfiguration _config;
    public TokenController(IConfiguration config) {
      _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public dynamic Post([FromBody]Login login) {
      IActionResult response = Unauthorized();
      var user = Authenticate(login);
      if (user != null) {
        var token = BuildToken(user);
        response = Ok( new { token });
      }
      return response;
    }
    private string BuildToken(User user) {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.Sha256);
      var token = new JwtSecurityToken(
        _config["Jwt:Issuer"],
        _config["Jwt.Issuer"],
        expires:DateTime.Now.AddMinutes(40),
        signingCredentials:creds
        );
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private User Authenticate(Login login) {
      User user = null;
      if (login.UserName == "john" && login.Password == "secret") {
        user = new User(login.UserName);
      }
      return user;
    }
  }

  public class User {
    private string _name;
    public string Name {
      get {
        return _name;
      }
    }

    public User(string name) {
      _name = name;
    }
  }
}
