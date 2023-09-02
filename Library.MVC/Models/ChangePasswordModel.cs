using System.Text.Json.Serialization;

namespace Library.MVC.Models
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        [JsonConstructor]
        public ChangePasswordModel(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}