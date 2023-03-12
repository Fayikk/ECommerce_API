using Entities;

namespace ECommerce_API.Services.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExist(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword( string oldPassword,string newPassword,string confirmPassword);
        int GetUserId();
        string GetUserEmail();
    
        Task<ServiceResponse<bool>> RoleForAdmin(string email);
    
        Task<ServiceResponse<bool>> DeleteAccount(string password);

    }
}
