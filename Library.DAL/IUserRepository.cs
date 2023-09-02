using Library.Model;

namespace Library.DAL
{
    public interface IUserRepository
    {
        public void CreateUser(User user);
        public void DeleteUser(int id);
        public void UpdateUser(User user);
        public User? GetUser(string username);
        public User? GetUser(int id);
        public IEnumerable<User> GetAllUsers();
    }
}
