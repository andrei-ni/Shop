using System.Security.Claims;

namespace DrPrint.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Verify(string token);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<string>> DeleteUser(string email);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        int GetUserId();
        Task<UserResponseDTO> GetUsers(int page);

        Task<ServiceResponse<string>> ResetPassword(UserResetPassword request);
    }
}
