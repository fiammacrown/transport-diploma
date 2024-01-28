using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) {
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}

	[HttpPost]
	[Route("Login")]
	public async Task<ActionResult> Login(UserDto user)
	{
		var dbUser = await _userManager.FindByNameAsync(user.Username);
		if (dbUser == null)
		{
			return Unauthorized();
		}

		if (!await _userManager.CheckPasswordAsync(dbUser, user.Password))
		{
			return Unauthorized();
		}

		var userRoles = await _userManager.GetRolesAsync(dbUser);
		var authClaims = new List<Claim>
			{
			   new Claim(ClaimTypes.Name, dbUser.UserName),
			   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

		foreach (var userRole in userRoles)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, userRole));
		}


		var issuer = _configuration["Jwt:Issuer"];
		var audience = _configuration["Jwt:Audience"];
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecurityKey"]);


		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = issuer,
			Audience = audience,
			Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(authClaims)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return Ok(new { token = tokenHandler.WriteToken(token) });
	
		
	}


	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
	{
		return Ok();
	}
}
