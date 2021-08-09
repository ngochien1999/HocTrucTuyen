using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.DTO
{
    public class MutipleChoiceDTO
    {
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
        public int Status { get; set; }
    }
}