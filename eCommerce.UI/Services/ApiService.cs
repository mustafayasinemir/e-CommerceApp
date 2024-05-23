using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eCommerce.UI.Models;

namespace eCommerce.UI.Services
{
    public  class ApiService
    {

            protected HttpClient httpClient;

            public ApiService()
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            }

        public async Task<bool> UploadUserImage(byte[] imageArray)
        {
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageArray), "Image", "image.jpg");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/uploadphoto", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ProfileImage> GetUserProfileImage()
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/users/profileimage");
            return JsonConvert.DeserializeObject<ProfileImage>(response);
        }
    }

    }
