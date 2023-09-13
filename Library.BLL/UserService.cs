using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteUser(int id)
        {
            var user = _unitOfWork.UsersRepository.GetUser(id);
            if (user != null)
            {
                _unitOfWork.UsersRepository.DeleteUser(user.Id);
            }
        }

        public void RegisterUser(UserRequestModel user)
        {
            if (user.Username != null && user.Password != null && !IsUsernameExists(user.Username))
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);

                User newUser = new User();
                newUser.Username = user.Username;
                newUser.HashedPassword = hashedPassword;
                newUser.PasswordSalt = salt;

                _unitOfWork.UsersRepository.CreateUser(newUser);
            }
        }

        public void UpdateUserPassword(string tokenString, string oldPassword, string newPassword)
        {
            string? username = JWTWhoAmI(tokenString);

            if(username == null || username == "")
            {
                return;
            }

            if (oldPassword == null || newPassword == null || !IsUsernameExists(username))
            {
                return;
            }

            User? updatedUser = _unitOfWork.UsersRepository.GetUser(username);

            if (!IsCredentialsValid(new UserRequestModel(username, oldPassword)))
            {
                return;
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);

            updatedUser.HashedPassword = hashedPassword;
            updatedUser.PasswordSalt = salt;
            _unitOfWork.UsersRepository.UpdateUser(updatedUser);
        }

        public string? JWTWhoAmI(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);

            Claim usernameClaim = token.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

            if (usernameClaim == null)
            {
                return null;
            }

            return usernameClaim.Value;
        }

        public bool IsUsernameExists(string username)
        {
            return _unitOfWork.UsersRepository.GetUser(username) != null ? true : false;
        }

        public bool IsCredentialsValid(UserRequestModel user)
        {
            if (user.Username == null || user.Password == null || !IsUsernameExists(user.Username))
            {
                return false;
            }

            User userToCheck = _unitOfWork.UsersRepository.GetUser(user.Username);
            
            return BCrypt.Net.BCrypt.Verify(user.Password, userToCheck.HashedPassword);
        }

        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.UsersRepository.GetAllUsers();
        }
    }
}
