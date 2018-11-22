using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class QualificationDto
    {
        public int Eid { get; set; }
        public string EligibleCourseName { get; set; }
        public bool? DeleteFlag { get; set; }
        public string College { get; set; }
        public string University { get; set; }
        public string duration { get; set; }
    }
}