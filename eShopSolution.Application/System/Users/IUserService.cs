using eShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<string> AuthencateAsync(LoginRequest request);

        Task<bool> RegisterAsync(RegisterRequest request);
    }
}