﻿using ReactExample.Models;
using ReactExample.Models.DTO;
using ReactExample.Models.QueryParameters;

namespace ReactExample.Services.Contracts
{
    public interface IUserService
    {
        IList<BaseUser> GetAll();       
        BaseUser GetById(int id);
        BaseUser GetByEmail(string email);
        BaseUser GetByFirstName(string firstName);
        BaseUser GetByLastName(string firstName);
        BaseUser Update(int id, BaseUser user);
        IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters);
        BaseUser Register(RegisterDto registerModel);
        void ChangePassword(int userId, string oldPassword, string newPassword); 
        
    }
}
