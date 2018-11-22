using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class ContactUsDto
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Email { get; set; }
        public string phonenumber { get; set; }
        public string message { get; set; }
        public string isDelete { get; set; }
    }
}