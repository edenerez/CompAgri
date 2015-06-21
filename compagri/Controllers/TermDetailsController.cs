using CompAgri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    [Common.AuthorizeFilter]
    public class TermDetailsController : ApiController
    {
        public TermDetails Get(int id)
        {
            return Bll.CompAgriBll.TermDetails(id);
        }
    }
}
