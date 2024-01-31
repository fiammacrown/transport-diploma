using System;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Services
{
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

        public async Task<bool> LoginAsync(string username, string password)
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

				return true;
			}
            catch (Exception)
            {
                return false;
            }
		}

        public void Logout()
        {
            _tokenStore.Token = null;
		}
    }
}
