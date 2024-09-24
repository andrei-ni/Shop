using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DrPrint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendEmail")]
        public async Task<ActionResult<ServiceResponse<bool>>> SendEmail(EmailDTO request)
        {
            var result = _emailService.SendEmail(request);
            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest(await result);
            }
            return Ok(await result);
        }

        [HttpPost("send-register-email")]
        public async Task<ActionResult<ServiceResponse<string>>> SendRegisterEmail(EmailDTO request)
        {
            return await _emailService.SendRegisterEmail(request);

        }

        [HttpPost("send-reset-password-email")]
        public async Task<ActionResult<ServiceResponse<string>>> SendResetEmail(EmailDTO request)
        {
            return await _emailService.SendResetEmail(request);

        }

        [HttpPost("send-message")]
        public async Task<ActionResult<ServiceResponse<string>>> SendClientMessage(EmailDTO request)
        {
            return await _emailService.SendClientMessage(request);

        }
    }
}
