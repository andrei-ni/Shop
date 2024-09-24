namespace DrPrint.Server.Services.EmailService
{
    public interface IEmailService
    {
        Task<ServiceResponse<bool>> SendEmail(EmailDTO request);
        Task<ServiceResponse<string>> SendRegisterEmail(EmailDTO request);
        Task<ServiceResponse<string>> SendResetEmail(EmailDTO request);

        Task<ServiceResponse<string>> SendOrderDetails(EmailDTO request);
        Task<ServiceResponse<string>> SendClientMessage(EmailDTO request);
    }
}
