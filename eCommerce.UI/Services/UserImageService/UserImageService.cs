using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eCommerce.UI.Models;
using Newtonsoft.Json;

namespace eCommerce.UI.Services.UserImageService
{
    internal class UserImageService:IUserImageService
    {
        public async Task<bool> UploadUserImage(byte[] imageArray)
        {

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageArray), "Image", "image.jpg");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/uploadphoto", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<ProfileImage> GetUserProfileImage()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/users/profileimage");
            return JsonConvert.DeserializeObject<ProfileImage>(response);
        }
    }
}
