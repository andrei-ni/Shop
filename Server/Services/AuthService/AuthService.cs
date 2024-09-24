using DrPrint.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Loader;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DrPrint.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public readonly DataContext _context;
        public readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor http)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = http;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Email.ToLower().Equals(email.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "Contul nu exista.";
            }
            else if(user.Deleted == true)
            {
                response.Success = false;
                response.Message = "Contul a fost sters.";
            }
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success=false;
                response.Message = "Parola este gresita.";
            }
            else if(user.VerifiedAt == null)
            {
                response.Success = false;
                response.Message = "Contul nu este verificat.";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Verify(string token)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Simbol invalid.";
            }
            else
            {
                user.VerifiedAt = DateTime.Now;
                response.Success = true;
                response.Message = "Contul a fost verificat. Multumim.";
            }
            
            await _context.SaveChangesAsync();
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await UserExists(user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "Contul exista deja." };
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.VerificationToken = CreateRandomToken();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<int> { Data = user.Id, Message = "Contul a fost creat. Verifica email-ul pentru a activa contul." };
        }

        private string CreateRandomToken()
        {
          
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Contul nu a fost gasit."
                };
            }
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Message = "Parola a fost schimbata." };
        }

        public async Task<ServiceResponse<string>> ResetPassword(UserResetPassword request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Simbol invalid.";
            }
            else
            {
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordResetToken = null;
                user.ResetTokenExpires = null;
                response.Success = true;
                response.Message = "Parola a fost schimbata.";
            }
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUser(string email)
        {

            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            if(user == null)
            {
                response.Success = false;
                response.Message = "Contul nu exista.";
            }

            else
            {
                user.Deleted = true;
                user.Email = string.Empty;
                response.Message = "Contul a fost sters.";
            }
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<UserResponseDTO> GetUsers(int page)
        {
            var address = await _context.Addresses.ToListAsync();

            var pageResults = 50f;
            var pageCount = Math.Ceiling(_context.Users.Count() / pageResults);
            var users = await _context.Users.Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();

            var response = new UserResponseDTO
            {
                Users = users,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return response;
        }

    }
}
