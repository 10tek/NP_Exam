using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class RequestService<T>
    {
        private const string GET_URL = "http://10.2.5.64/user/getall/";
        private const string ADD_URL = "http://10.2.5.64/user/add/";
        private const string UPDATE_URL = "http://10.2.5.64/user/update/";
        private const string REMOVE_URL = "http://10.2.5.64/user/remove/";
        private const string DATA_TYPE = "/user/";

        //public async Task<List<User>> GetAsync()
        //{
        //    var data = new StringContent("giveAllUsers");
        //    using var client = new HttpClient();
        //    var response = await client.PostAsync(ADD_URL, data);
            
        //}

        public async Task<string> AddAsync(string name, string login)
        {
            var user = new User()
            {
                Name = name,
                Login = login
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(ADD_URL, data);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return "Аккаунт создан";
                case HttpStatusCode.Forbidden:
                    return "Пользователь существует";
                default:
                    return "Неизвестная ошибка";
            }
        }

        public async Task<string> UpdateAsync(string login, string name)
        {
            var user = new User()
            {
                Name = name,
                Login = login
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(UPDATE_URL, data); 
            
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return "Имя аккаунта изменено успешно!";
                case HttpStatusCode.NotFound:
                    return $"Не получилось изменить имя с логином {login}. Не найдено.";
                default:
                    return "Неизвестная ошибка!";
            }
        }

        public async Task<string> RemoveAsync(string login)
        {
            var user = new User()
            {
                Name = "Delete",
                Login = login
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(REMOVE_URL, data);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return "Имя аккаунта изменено успешно!";
                case HttpStatusCode.NotFound:
                    return $"Не получилось удалить пользователя с логином {login}. Не найдено.";
                default:
                    return "Неизвестная ошибка!";
            }
        }
    }
}
