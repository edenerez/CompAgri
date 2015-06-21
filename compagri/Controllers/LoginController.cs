using CompAgri.Common;
using CompAgri.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    public class LoginController : ApiController
    {
        public UserDto Post([FromBody] UserDto user)
        {
            User userFromDatabase;
            using (var db = new CompAgriConnection())
            {
                userFromDatabase = db.User.FirstOrDefault(u => u.Email == user.Email);

                if (userFromDatabase == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                var hashedPassword = PasswordUtils.HashPassword(user.Password, userFromDatabase.PasswordSalt);

                if (hashedPassword != userFromDatabase.Password)
                {
                    throw new UserWrongPasswordException();
                }
                else
                {
                    userFromDatabase.Token = TokenUtils.GenerateToken();
                    db.SaveChanges();

                    var userToSend = new UserDto(userFromDatabase, true);
                    return userToSend;
                }
            }
        }

        public UserDto Get()
        {
            if(!Request.Headers.Contains("CompagriUserToken"))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            string token = Request.Headers.GetValues("CompagriUserToken").FirstOrDefault();

            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.Token == token);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return new UserDto(user, true);
            }
        }

        public void Delete()
        {
            string token = Request.Headers.GetValues("CompagriUserToken").FirstOrDefault();

            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.Token == token);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                user.Token = null;
                db.SaveChanges();
            }
        }
    }
}
