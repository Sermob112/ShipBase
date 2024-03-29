﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.DAL.SectionOne.Repositories;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Enum;
using ShipBase.Domain.SectionOne.Extensions;
using ShipBase.Domain.SectionOne.Helpers;
using ShipBase.Domain.SectionOne.Response;


using ShipBase.Domain.SectionOne.ViewModels.User;
using ShipBase.Service.SectionOne.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ShipBase.Service.SectionOne.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository,
            IBaseRepository<Profile> proFileRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _proFileRepository = proFileRepository;
        }
        public async Task<IBaseResponse<User>> Edit(long id, UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }
                user.Id = id;
                user.Login = model.Name;
                user.Password = HashPasswordHelper.HashPassowrd(model.Password).ToString();
                user.Role = Enum.Parse<Role>(model.Role);

                await _userRepository.Update(user);


                return new BaseResponse<User>()
                {
                    Data = user,
                    StatusCode = StatusCode.OK,
                };
               
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                user = new User()
                {
                    Login = model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };
                
                await _userRepository.Create(user);
                
                var profile = new Profile()
                {
                    Address = string.Empty,
                    Age = 0,
                    UserId = user.Id,
                };
                
                await _proFileRepository.Create(profile);
                
                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[]) Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int) k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Login,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");
                
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<UserViewModel>> GetUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
            
                var data = new UserViewModel()
                {
                    Id = user.Id,
                    Password = user.Password,
                    Name = user.Login,
                    Role = user.Role.ToString()
                };

                return new BaseResponse<UserViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}