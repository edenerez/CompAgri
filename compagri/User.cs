//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompAgri
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Connections = new HashSet<Connection>();
        }
    
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Token { get; set; }
        public int UserProfile_Id { get; set; }
        public string UserName { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }
}
