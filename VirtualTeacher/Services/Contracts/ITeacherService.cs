﻿using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITeacherService
    {
        Teacher Register(RegisterModel registerModel);
        Teacher GetTeacherByEmail(string email);

    }
}