using VirtualTeacher.Models.DTO;
using VirtualTeacher.Models;

namespace VirtualTeacher.Helpers.Contracts
{
    public interface IModelMapper
    {
        BaseUser MapToBaseUser(UserProfileUpdateModel model);
        UserResponseDto MapToUserResponseDto(BaseUser user);
    }
}
