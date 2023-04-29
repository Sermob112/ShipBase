using System.Collections.Generic;
using System.Threading.Tasks;
using ShipBase.Domain.Entity;
using ShipBase.Domain.Response;
using ShipBase.Domain.ViewModels.User;

namespace ShipBase.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);
        
        BaseResponse<Dictionary<int, string>> GetRoles();
        
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<IBaseResponse<UserViewModel>> GetUser(long id);

        Task<IBaseResponse<bool>> DeleteUser(long id);

        Task<IBaseResponse<User>> Edit(long id, UserViewModel model);
    }
}