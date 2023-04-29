using System.Collections.Generic;
using System.Threading.Tasks;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Profile;
using ShipBase.Domain.SectionOne.ViewModels.User;

namespace ShipBase.Service.SectionOne.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}