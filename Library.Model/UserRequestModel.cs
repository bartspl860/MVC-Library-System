using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class UserRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public UserRequestModel(string? username, string? password)
        {
            Username = username;
            Password = password;
        }
    }
}
