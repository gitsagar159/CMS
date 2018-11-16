using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class UsersDto
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public int MainStreamId { get; set; }
        public int SubStreamId { get; set; }
        public bool IsDelete { get; set; }
    }
}