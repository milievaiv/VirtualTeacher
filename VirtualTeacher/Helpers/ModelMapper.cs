using Microsoft.EntityFrameworkCore.Metadata;
using VirtualTeacher.Helpers.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Helpers
{
    public class ModelMapper : IModelMapper
    {
        public BaseUser MapToBaseUser(UserProfileUpdateModel model)
        {
            return new BaseUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public UserResponseDto MapToUserResponseDto(BaseUser user)
        {
            return new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
