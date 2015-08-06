using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompAgri.Common.CSVLogger
{
    public struct CSVLog
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}