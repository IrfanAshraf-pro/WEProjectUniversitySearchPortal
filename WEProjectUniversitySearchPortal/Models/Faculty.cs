using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEProjectUniversitySearchPortal.Models
{
    public class Faculty
    {
        public String  u_id { get; set; }
        public String f_id { get; set; }
        public String name { get; set; }
        public String experience { get; set; }
        public HttpPostedFileBase fimage { get; set; }
        public String fpath { get; set; }

    }
}