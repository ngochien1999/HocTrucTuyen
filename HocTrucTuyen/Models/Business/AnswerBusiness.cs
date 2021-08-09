using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.Business
{
    public class AnswerBusiness
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();

        public List<Answer> getAllAnswer()
        {
            return db.Answers.ToList();
        }
    }
}