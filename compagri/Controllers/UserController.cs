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
    [Common.AuthorizeFilter]
    public class UserController : ApiController
    {
        public IEnumerable<UserDto> Get()
        {
            using (var db = new CompAgriConnection())
            {
                return db.User.Select(u => new UserDto()
                {
                    User_Id = u.User_Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserProfile_Id = u.UserProfile_Id,
                }).ToList();
            }
        }

        public UserDto Get(int id)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.User_Id == id);

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return new UserDto(user);
            }
        }

        [AllowAnonymous]
        public UserDto Post([FromBody] UserDto userDto)
        {
            if (!userDto.IsValid())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            using (var db = new CompAgriConnection())
            {
                var previousUser = db.User.FirstOrDefault(u => u.UserName == userDto.UserName || u.Email == userDto.Email);
                if (previousUser != null)
                {
                    throw WebExceptionsFactory.GetUserDuplicatedException();
                }

                var userBeforeSave = userDto.User();
                userBeforeSave.PasswordSalt = PasswordUtils.GenerateSalt();
                userBeforeSave.Password = PasswordUtils.HashPassword(userBeforeSave.Password, userBeforeSave.PasswordSalt);

                var user = db.User.Add(userBeforeSave);
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