using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class SubStreamDto
    {
        public int sid { get; set; }
        public int MainStreamId { get; set; }
        public string SubStreamName { get; set; }
        public bool DeleteFlag { get; set; }
    }
}