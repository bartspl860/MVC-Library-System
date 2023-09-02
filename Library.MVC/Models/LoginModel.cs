using Library.Model;
using System.Text.Json.Serialization;

namespace Library.MVC.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonConstructor]
        public LoginModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginModel(UserRequestModel userRequest)
        {
            Username = userRequest.Username;
            Password = userRequest.Password;
        }
    }
}
