using eCommerce.UI.Models;

namespace eCommerce.UI.Services.UserImageService
{
    internal interface IUserImageService
    {
         Task<bool> UploadUserImage(byte[] imageArray);
        Task<ProfileImage> GetUserProfileImage();
    }
}
