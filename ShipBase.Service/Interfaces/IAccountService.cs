using System.Security.Claims;
using System.Threading.Tasks;
using ShipBase.Domain.Response;
using ShipBase.Domain.ViewModels.Account;

namespace ShipBase.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}