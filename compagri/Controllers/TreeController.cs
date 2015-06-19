using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    public class TreeController : ApiController
    {
        public IEnumerable<Models.Terms.XMLFile> Get()
        {
            return Models.Terms.XMLFile.GetAll();
        }

        public Models.Tree.Tree<Models.Terms.Term>.RootClass Get(int id)
        {
            var res = Bll.CompAgriBll.GetTree(id);
            if (res == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return res;
        }
    }
}
