using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL
{
    public interface IUserService
    {
        public void RegisterUser(UserRequestModel user);
        public void DeleteUser(UserRequestModel user);
        public void UpdateUserPassword(string tokenString, string oldPassword, string newPassword);
        public bool IsUsernameExists(string username);
        public bool IsCredentialsValid(UserRequestModel user);
        public string JWTWhoAmI(string tokenString);
    }
}
