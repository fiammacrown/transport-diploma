using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transport.DAL.Data;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly IConfiguration _configuration;
	private readonly ApplicationDbContext _context;

	public UsersController(IConfiguration configuration, ApplicationDbContext context) {
		_configuration = configuration;
		_context = context;
	}

	[HttpPost]
	[Route("Login")]
	public IActionResult Login(UserDto user)
	{
		if (user.Username == "admin" && user.Password == "admin")
		{
			var issuer = _configuration["Jwt:Issuer"];
			var audience = _configuration["Jwt:Audience"];
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecurityKey"]);


			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, "Admin")
				}),
				Expires = DateTime.UtcNow.AddMinutes(5),
				Issuer = issuer,
				Audience = audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return Ok(new { token = tokenHandler.WriteToken(token) });
		}

		return Unauthorized();
	}


	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
	{

		var dbUsers = await _context.Users.ToListAsync();

		var users = dbUsers.Select(Mapper.Map).ToList();

		return Ok(users);
	}
}
