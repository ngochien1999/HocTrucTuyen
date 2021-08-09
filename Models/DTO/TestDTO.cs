using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.DTO
{
    public class TestDTO
    {
        public long Question_ID { get; set; }
        public long Answer_ID { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Fullname{ get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Title { get; set; }


    }
}