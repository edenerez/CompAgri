using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    public class LikeController : ApiController
    {
        [HttpGet]
        [Common.CSVLogger.ActionLogger("Liked a connection")]
        public void Like(int id)
        {
            using (var db = new DB_9BA48E_xmldbEntities())
            {
                if (db.Connection.FirstOrDefault(c => c.Connection_Id == id) != null)
                {
                    var user = CompAgri.Common.UserUtils.GetUser(Request);
                    var opinion = db.UserOpinions.FirstOrDefault(uo => uo.Connection_Id == id && uo.User_Id == user.User_Id);
                    if (opinion != null)
                    {
                        opinion.Opinion = true;
                    }
                    else
                    {
                        opinion = new UserOpinion
                        {
                            Connection_Id = id,
                            User_Id = user.User_Id,
                            Opinion = true
                        };
                        db.UserOpinions.Add(opinion);
                    }

                    db.SaveChanges();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        [HttpGet]
        [Common.CSVLogger.ActionLogger("Unliked a connection")]
        public void Unlike(int id)
        {
            using (var db = new DB_9BA48E_xmldbEntities())
            {
                if (db.Connection.FirstOrDefault(c => c.Connection_Id == id) != null)
                {
                    var user = CompAgri.Common.UserUtils.GetUser(Request);
                    var opinion = db.UserOpinions.FirstOrDefault(uo => uo.Connection_Id == id && uo.User_Id == user.User_Id);
                    if (opinion != null)
                    {
                        opinion.Opinion = false;
                    }
                    else
                    {
                        opinion = new UserOpinion
                        {
                            Connection_Id = id,
                            User_Id = user.User_Id,
                            Opinion = false
                        };
                        db.UserOpinions.Add(opinion);
                    }

                    db.SaveChanges();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}
