using Microsoft.IdentityModel.Tokens;

namespace Abeslamidze_Kursovaya7.Services
{
	public interface IAuthTokenStore
	{
		public string? Token { get; set; }
		public bool IsAdmin();
	}
}
