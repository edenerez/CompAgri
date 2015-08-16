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
    public class TermController : ApiController
    {
        [HttpGet]
        public TermDetails Details(int id)
        {
            return Bll.CompAgriBll.TermDetails(id);
        }

        [HttpGet]
        public IEnumerable<Models.Terms.Term> GetMatchingTerms(string toMatch, int? treeId)
        {
            return Bll.CompAgriBll.GetMatchingTerms(toMatch, treeId);
        }
    }
}
