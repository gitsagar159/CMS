using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Dto;

namespace CMS.Models
{
    public class CourseDetailsViewModel
    {
        public int Cid { get; set; }
        public  string MainStreamName { get; set; }
        public string SubStreamName { get; set; }
        public string CourseName { get; set; }
        public string College { get; set; }
        public string University { get; set; }
        public string duration { get; set; }

        public List<QualificationDto> Eligibilitie = new List<QualificationDto>();
    }
}
