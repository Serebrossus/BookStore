using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public dynamic Post([FromBody]LoginViewModel login) {
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
    private User Authenticate(LoginViewModel login) {
      User user = null;
      if (login.username == "john" && login.password == "secret") {
        user = new User(login.username);
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
