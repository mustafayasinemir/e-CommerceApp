using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eCommerce.Client.Models;
using Newtonsoft.Json;
namespace eCommerce.Client.Services
{
    public class ApiService
    {
        public void RegisterUser (string name, string email,string phone,string password)
        {
            var register = new Register()
            {

                Name = name,
                Email = email,
                Phone = phone,
                Password = password,

            }; 

            var httpClient = new HttpClient();
            var json=JsonConvert.SerializeObject(register);
            var content=new StringContent(json,Encoding.UTF8,"application/json");
            httpClient.PostAsync("");
    }
    }
}
