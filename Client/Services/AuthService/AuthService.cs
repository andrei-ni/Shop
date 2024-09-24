using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace DrPrint.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public List<User> UserList { get; set; } = new List<User>();
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action? UsersChanged;

        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();

        }

        public async Task<ServiceResponse<string>> Verify(string token)
        {
            var result = await _http.PostAsJsonAsync($"api/auth/{token}", token);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }


        public async Task<ServiceResponse<string>> ResetPassword(UserResetPassword request)
        {
            var result = await _http.PostAsJsonAsync($"api/auth/reset-password", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task DeleteUser(string email)
        {
            var result = await _http.DeleteAsync($"api/auth/{email}");
        }

        public async Task GetUsers(int page)
        {
            var result = await _http.GetFromJsonAsync<UserResponseDTO>($"api/auth/users/{page}");
            if(result != null)
            {
                UserList = result.Users;
                CurrentPage = result.CurrentPage;
                PageCount = result.Pages;
            }
            UsersChanged?.Invoke();
        }

    }
}
