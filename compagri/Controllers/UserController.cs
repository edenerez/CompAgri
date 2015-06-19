using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using CompAgri.Common;
using CompAgri.Models.Authentication;
using Newtonsoft.Json;

namespace CompAgri.Controllers
{
    public class UserController : ApiController
    {
        public IEnumerable<UserDto> Get()
        {
            using (var db = new CompAgriConnection())
            {
                return db.User.Select(u => new UserDto(u)).ToList();
            }
        }

        public UserDto Get(int id)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.User_Id == id);
                return new UserDto(user);
            }
        }

        public UserDto Post([FromBody] UserDto userDto)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.Add(userDto.User());
                db.SaveChanges();
                return new UserDto(user);
            }
        }

        public void Delete(int id)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.User_Id == id);
                db.User.Remove(user);
                db.SaveChanges();
            }
        }
    }
}