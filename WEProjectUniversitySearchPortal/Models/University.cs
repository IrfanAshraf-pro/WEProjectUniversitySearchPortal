using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WEProjectUniversitySearchPortal.Models
{
    public class University
    {
        public String u_id { get; set; }
        public String  name { get; set; }
        public String  location { get; set; }
        public String registered { get; set; }
        public HttpPostedFileBase image { get; set; }
        public String path { get; set; }

    }
}