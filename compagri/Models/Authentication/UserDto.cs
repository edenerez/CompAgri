namespace CompAgri.Models.Authentication
{
    public class UserDto
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Token { get; set; }
        public int UserProfile_Id { get; set; }

        public UserDto()
        {
            
        }

        public UserDto(User user)
        {
            User_Id = user.User_Id;
            Name = user.Name;
            LastName = user.LastName;
            Password = user.Password;
            PasswordSalt = user.PasswordSalt;
            Email = user.Email;
            Token = user.Token;
            UserProfile_Id = user.UserProfile_Id;
        }

        public User User()
        {
            return new User
            {
                Email = this.Email,
                LastName = this.LastName,
                Name = this.Name,
                Password = this.Password,
                PasswordSalt = this.PasswordSalt,
                Token = this.Token,
                User_Id = this.User_Id,
                UserProfile_Id = this.UserProfile_Id
            };
        }
    }
}