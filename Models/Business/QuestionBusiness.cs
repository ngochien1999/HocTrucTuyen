using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.Business
{
    public class QuestionBusiness
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();

        public List<Question> getQuestionAV_1()
        {
            return db.Questions.Where(x => x.Subject == "Web" && x.Category == 1).ToList();
        }
        public List<Question> getQuestionAV_2()
        {
            return db.Questions.Where(x => x.Subject == "Web1" && x.Category == 1).ToList();
        }

        public List<Question> getQuestionTH_1()
        {
            return db.Questions.Where(x => x.Subject == "C#" && x.Category == 1).ToList();
        }

       
    }
}