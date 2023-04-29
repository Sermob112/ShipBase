using System.Collections.Generic;
using System.Threading.Tasks;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.User;

namespace ShipBase.Service.SectionOne.Interfaces
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