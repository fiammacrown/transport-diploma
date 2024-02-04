
using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Services
{
    public class LoginResult
    {
        public bool IsAuthorized { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class AuthService
    {
        private readonly IAuthTokenStore _tokenStore;
        private readonly IApiService _apiService;

		public AuthService(
            IAuthTokenStore tokenStore,
            IApiService apiService)
        {
            _tokenStore = tokenStore;
            _apiService = apiService;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
				var userDto = new Transport.DTOs.UserDto
				{
					Username = username,
					Password = password
				};

				var result = await _apiService.Login(userDto);

				_tokenStore.Token = result.Token;

                if (_tokenStore.IsAdmin())
                {
                    return new LoginResult { IsAuthorized = true, IsAdmin = true };
                }

				return new LoginResult { IsAuthorized = true, IsAdmin = false };
			}
            catch (Exception)
            {
                return new LoginResult { IsAuthorized = false, IsAdmin = false }; ;
            }
		}

        public void Logout()
        {
            _tokenStore.Token = null;
		}
    }
}
