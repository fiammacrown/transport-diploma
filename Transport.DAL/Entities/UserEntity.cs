using Microsoft.AspNetCore.Identity;

namespace Transport.DAL.Entities;

public class ApplicationUser : IdentityUser
{
	public string? Name { get; set; }
}
