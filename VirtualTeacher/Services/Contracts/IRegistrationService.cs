﻿using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IRegistrationService
    {
        PasswordHashInfo GeneratePasswordHashAndSalt(RegisterDto registerModel);
    }
}
