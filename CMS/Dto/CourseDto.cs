using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class CourseDto
    {
        public int cid { get; set; }
        public int MainStreamId { get; set; }
        public int SubStreamId { get; set; }
        public string CourseName { get; set; }
        public string EligibleCourseId { get; set; }
        public bool DeleteFlag { get; set; }
        public string College { get; set; }
        public string University { get; set; }
    }
}