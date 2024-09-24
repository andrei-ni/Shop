

namespace DrPrint.Client.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _http;

        public EmailService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<bool>> SendEmail(EmailDTO request)
        {
            var response = await _http.PostAsJsonAsync("api/email/sendEmail", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> SendRegisterEmail(EmailDTO request)
        {
            var response = await _http.PostAsJsonAsync("api/email/send-register-email", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> SendResetEmail(EmailDTO request)
        {
            var response = await _http.PostAsJsonAsync("api/email/send-reset-password-email", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> SendClientMessage(EmailDTO request)
        {
            var response = await _http.PostAsJsonAsync("api/email/send-message", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}

