using CompAgri.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    [Common.AuthorizeFilter]
    public class ConnectionController : ApiController
    {
        [HttpGet]
        public Models.Terms.Connection.PosibleValues PosibleValues()
        {
            return Models.Terms.Connection.GetPosibleValues();
        }

        [HttpGet]
        public IEnumerable<Models.Terms.Connection> TermConnections(int termId)
        {
            var term = Models.Terms.Term.Find(termId);

            if (term == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return term.GetConnections();
        }

        [HttpGet]
        public IEnumerable<Models.Terms.Connection> ForTerms(string termIds)
        {
            if (!String.IsNullOrWhiteSpace(termIds))
            {
                return Models.Terms.Connection.GetForTerms(termIds.Split(',').Select(t => int.Parse(t)));

            }
            else
            {
                return new Models.Terms.Connection[] { };
            }
        }

        public IEnumerable<Models.Terms.Connection> Get()
        {
            return Models.Terms.Connection.GetAll();
        }

        public Models.Terms.Connection Get(int id)
        {
            return Models.Terms.Connection.Find(id);
        }

        [Common.CSVLogger.ActionLogger(description: "A connection was added")]
        public Models.Terms.Connection Post([FromBody] Models.Terms.Connection connection)
        {
            connection.Connection_Id_User = Request.GetUser().User_Id;
            connection.Save();
            return connection;
        }

        [Common.CSVLogger.ActionLogger(description: "Deleted a connection")]
        public void Delete(int id)
        {
            Models.Terms.Connection.Delete(new Models.Terms.Connection { Connection_Id = id }, Request.GetUser());
        }
    }
}
