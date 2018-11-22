using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Dto
{
    public class StatasticsDto
    {
        //public List<Cols> cols = new List<Cols>();
        //public rows rows = new rows();
       
        public string ChartJson { get; set; }
    }

    public class chart
    {
        public int y { get; set; }
        public string indexLabel { get; set; }
    }


    //public class Cols
    //{
    //    public string id { get; set; }
    //    public string label { get; set; }
    //    public string pattern { get; set; }
    //    public string type { get; set; }
    //}

    //public class rows
    //{
    //    public List<c> c = new List<c>();
    //    public List<dynamic> c1 = new List<dynamic>();
    //}

    //public class c
    //{
    //    public string v { get; set; }
    //    public string f { get; set; }
    //}
}