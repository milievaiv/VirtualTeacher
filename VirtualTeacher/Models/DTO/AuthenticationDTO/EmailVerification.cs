﻿namespace VirtualTeacher.Models.DTO.AuthenticationDTO
{
    public class EmailVerification
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
