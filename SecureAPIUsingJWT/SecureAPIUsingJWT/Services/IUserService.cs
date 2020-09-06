using SecureAPIUsingJWT.Models;
using System.Threading.Tasks;

namespace SecureAPIUsingJWT.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        ApplicationUser GetById(string id);
        bool RevokeToken(string token);
    }
}
