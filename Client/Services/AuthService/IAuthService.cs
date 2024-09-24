namespace DrPrint.Client.Services.AuthService
{
    public interface IAuthService
    {
        event Action UsersChanged;
        List<User> UserList { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }

        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<string>> Verify(string token);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
        Task DeleteUser(string email);

        Task GetUsers(int page);

        Task<ServiceResponse<string>> ResetPassword(UserResetPassword request);
    }
}
