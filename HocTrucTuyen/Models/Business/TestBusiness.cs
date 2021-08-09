using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HocTrucTuyen.Models.Business
{
    public class TestBusiness
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        
        public void addTest(Test entity)
        {
            db.Tests.Add(entity);
            db.SaveChanges();
        }
    }
}