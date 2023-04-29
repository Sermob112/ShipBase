using System.Collections.Generic;
using System.Threading.Tasks;
using ShipBase.Domain.Entity;
using ShipBase.Domain.Response;
using ShipBase.Domain.ViewModels.Profile;
using ShipBase.Domain.ViewModels.User;

namespace ShipBase.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}