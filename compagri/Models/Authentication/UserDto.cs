using System;
using System.ComponentModel.DataAnnotations;
namespace CompAgri.Models.Authentication
{
    public class UserDto
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public int UserProfile_Id { get; set; }
        public UserProfileDto UserProfile { get; set; }

        public UserDto()
        {

        }

        public UserDto(User user, bool includeToken = false)
        {
            User_Id = user.User_Id;
            Name = user.Name;
            LastName = user.LastName;
            Email = user.Email;
            UserProfile_Id = user.UserProfile_Id;
            if (includeToken)
            {
                Token = user.Token;
            }
        }

        public User User()
        {
            return new User
            {
                Email = this.Email,
                LastName = this.LastName,
                Name = this.Name,
                Password = this.Password,
                Token = this.Token,
                User_Id = this.User_Id,
                UserProfile_Id = this.UserProfile_Id
            };
        }

        public bool IsValid()
        {
            bool valid = !String.IsNullOrWhiteSpace(Email);
            valid = valid && !String.IsNullOrWhiteSpace(Password);
            valid = valid && UserProfile_Id != 0;

            return valid;
        }
    }
}