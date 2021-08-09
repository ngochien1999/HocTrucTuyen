using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.DTO
{
    public class CourseDetailDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        //public Nullable<long> Teacher_ID { get; set; }
        public string Course_ID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
       /* public Nullable<bool> Status { get; set; }

        public string Teacher_Name { get; set; }
        public int CountCourse { get; set; }*/
    }
}