using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace DrPrint.Server.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public IConfiguration _config;
        public readonly DataContext _context;

        public EmailService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ServiceResponse<bool>> SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            var result = new ServiceResponse<bool>();
            email.From.Add(MailboxAddress.Parse(request.From)); //_config.GetSection("EmailConfig:Username").Value
            email.To.Add(MailboxAddress.Parse(request.To)); //_config.GetSection("EmailConfig:Username").Value
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
            smtp.CheckCertificateRevocation = false;
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_config.GetSection("EmailConfig:Host").Value, 465, SecureSocketOptions.SslOnConnect); // ,SecureSocketOptions.StartTls
            smtp.Authenticate(_config.GetSection("EmailConfig:Username").Value, _config.GetSection("EmailConfig:Password").Value);
            //smtp.Send(email);
            try
            {
                await Task.Run(() => smtp.Send(email));
                result.Success = true;
                result.Message = "Mesajul a fost trimis.";
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Eroare. Mesajul nu a fost trimis." + ex.Message.ToString();
                smtp.Disconnect(true);
            }
            return result;
        }

        public async Task<ServiceResponse<string>> SendRegisterEmail(EmailDTO request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.To);
            EmailDTO email = new EmailDTO();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Contul nu exista.";
            }
            else
            {
                string confirmationLink = "https://www.drprint.ro/verify?token=" + user.VerificationToken;

                email.From = "noreply@drprint.ro";
                email.To = request.To;
                email.Subject = "[DrPrint] Verifica contul tau DrPrint";

                email.Body = "<h3>DrPrint</h3> <br/> Te rugam sa confirmi contul facand click <a href=\"" + confirmationLink + "\">aici.</a> <br/>Multumim.<br/>";
                email.Body += "Daca nu poti da click, copiaza link-ul de mai jos si lipeste-l in url-ul browser-ului:<br/>";
                email.Body += confirmationLink;
                email.Body += "<br/><br/><h5> www.drprint.ro</h5>";

                response.Success = true;
                response.Message = "Email trimis.";
                await SendEmail(email);
            }
           
            return response;
        }

        private string CreateRandomToken()
        {

            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<ServiceResponse<string>> SendResetEmail(EmailDTO request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.To);
            EmailDTO email = new EmailDTO();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Contul nu exista.";
            }
            else
            {
                var token = CreateRandomToken();

                user.PasswordResetToken = token;
                user.ResetTokenExpires = DateTime.Now.AddDays(1);

                email.From = "noreply@drprint.ro";
                email.To = request.To;
                email.Subject = "[DrPrint] Modifica parola contui tau DrPrint";

                email.Body = "Salut,<br><br>Am inregistrat cererea ta de resetare a parolei. Ca sa alegi o noua parola pentru contul tau, apasa <a href=\"https://www.drprint.ro/reset-password/?token=" + token + "&um=" + request.To + "\"> aici </a><br><br> ";
                email.Body += "Daca nu poti da click, copiaza link-ul de mai jos si lipeste-l in url-ul browser-ului:<br>";
                email.Body += "<b>https://www.drprint.ro/reset-password/?token=" + token + "&um=" + request.To + "<br>";
                email.Body += "<br><br><b><span style=\"color:red;\">In cazul in care nu tu ai solicitat aceasta resetare de parola, te rugam sa ignori acest email.</b><br>";
                email.Body += "www.drprint.ro";

                response.Success = true;
                response.Message = "Am trimis un email cu link-ul de resetare a parolei la adresa indicata.";
                await SendEmail(email);
            }
            await _context.SaveChangesAsync();
            return response;
        }


        public async Task<ServiceResponse<string>> SendOrderDetails(EmailDTO request)
        {
            var email = new MimeMessage();
            var result = new ServiceResponse<string>();

            email.From.Add(MailboxAddress.Parse(request.From));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Cc.Add(MailboxAddress.Parse(request.Cc));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
            smtp.CheckCertificateRevocation = false;
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_config.GetSection("EmailConfig:Host").Value, 465, SecureSocketOptions.SslOnConnect); 
            smtp.Authenticate(_config.GetSection("EmailConfig:Username").Value, _config.GetSection("EmailConfig:Password").Value);

            try
            {
                await Task.Run(() => smtp.Send(email));
                result.Success = true;
                result.Message = "Comanda a fost trimisa.";
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Eroare. Nu s-a trimis." + ex.Message.ToString();
                smtp.Disconnect(true);
            }
            return result;
        }

        public async Task<ServiceResponse<string>> SendClientMessage(EmailDTO request)
        {
            var response = new ServiceResponse<string>();

            EmailDTO email = new EmailDTO();

            email.To = "contact@drprint.ro";
            email.From = "noreply@drprint.ro";
            email.Body = "Mesaj de la: " + request.Email + "<hr/><br/><br/>" + request.Message;
            email.Subject = "[Contact] Mesaj de la client";

            response.Success = true;
            response.Message = "Mesajul a fost trimis. Multumim.";
            await SendEmail(email);
            return response;
        }
    }
}
