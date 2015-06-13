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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [System.Web.Http.Route("GetAll")]
        [System.Web.Http.HttpGet]
        [WebApiAuthorizeFilter]
        public IEnumerable<UserDto> Get()
        {
            var dtos = new List<UserDto>();

            using (var db = new CompAgriConnection())
            {
                var list = db.User.ToList();

                dtos.AddRange(list.Select(u => new UserDto(u)));
            }

            return dtos;
        }

        [System.Web.Http.Route("Get/{id}")]
        [System.Web.Http.HttpGet]
        [WebApiAuthorizeFilter]
        public UserDto GetUserById(int id)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.User_Id == id);
                return new UserDto(user);
            }
        }

        [System.Web.Http.Route("Delete/{id}")]
        [System.Web.Http.HttpGet]
        [WebApiAuthorizeFilter]
        public bool DeleteUser(int id)
        {
            try
            {
                using (var db = new CompAgriConnection())
                {
                    var user = db.User.FirstOrDefault(u => u.User_Id == id);
                    db.User.Remove(user);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [System.Web.Http.Route("Post")]
        [System.Web.Http.HttpPost]
        [WebApiAuthorizeFilter]
        public UserDto AddNewUser(UserDto userDto)
        {
            using (var db = new CompAgriConnection())
            {
                var user = db.User.Add(userDto.User());
                db.SaveChanges();
                return new UserDto(user);
            }
        }
    }
}