using RobotTR.Portal.MVC.Models;
using System.Threading.Tasks;

namespace RobotTR.Portal.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLogin userLogin);
        Task<UserLoginResponse> Register(UserRegister userRegister);
    }
}