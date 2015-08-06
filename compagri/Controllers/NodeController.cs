using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompAgri.Controllers
{
    [Common.AuthorizeFilter]
    public class NodeController : ApiController
    {
        [Common.CSVLogger.ActionLogger(description: "Added a term")]
        [HttpPost]
        public int AddNode([FromUri] int xmlFileId, [FromUri] String name, [FromUri] int parentId, [FromBody] JObject param)
        {
            return Bll.CompAgriBll.AddNode(xmlFileId, name, parentId, param);
        }

        [HttpGet]
        [Common.CSVLogger.ActionLogger(description: "Changed a term parent (moved a node)")]
        public void MoveNode(int nodeId, int? oldParentId, int? newParentId)
        {
            Bll.CompAgriBll.MoveNode(nodeId, oldParentId, newParentId);
        }

        [HttpGet]
        [Common.CSVLogger.ActionLogger(description: "Deleted a node")]
        public void DeleteNode(int nodeId, int parentId)
        {
            Bll.CompAgriBll.DeleteNode(nodeId, parentId);
        }
    }
}
