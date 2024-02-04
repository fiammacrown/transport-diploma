using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Services
{
	public class AuthTokenStore : IAuthTokenStore
	{
		public string? Token { get; set; }
		public bool IsAdmin()
		{
			if (Token == null)
			{
				return false;
			}

			var handler = new JwtSecurityTokenHandler();
			var decoded = handler.ReadJwtToken(Token);
			return "Admin" == decoded.Claims.First(claim => claim.Type == "role").Value;
		}
	}
}
