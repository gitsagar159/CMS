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
    }
}