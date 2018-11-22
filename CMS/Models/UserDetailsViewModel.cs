using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Dto;

namespace CMS.Models
{
    public class UserDetailsViewModel
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string LastQualification { get; set; }
        public bool IsDelete { get; set; }
        public string MainStream { get; set; }
        public string SubStream { get; set; }
        public List<QualificationDto> Course = new List<QualificationDto>();
        public List<CourseDetail> CourseDetails = new List<CourseDetail>();
    }

    public class CourseDetail
    {
        public string College { get; set; }
        public string University { get; set; }
        public string duration { get; set; }
    }

    public class UserDetails
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string LastQualification { get; set; }
        public bool IsDelete { get; set; }
        public string MainStream { get; set; }
        public string SubStream { get; set; }
    }
}