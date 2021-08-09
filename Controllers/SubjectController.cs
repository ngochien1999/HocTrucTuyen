using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KH()
        {
            return View();
        }
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            var query = from co in db.Courses
                        join te in db.Teachers on co.Teacher_ID equals te.ID
                        select new CourseDTO()
                        {
                            ID = co.ID,
                            Title = co.Title,
                            Description = co.Description,
                            Images = co.Images,
                            CreatedDate = co.CreatedDate,
                            Teacher_Name = te.Name
                        };
            var model = query.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
            return View(model);
        }
      
    }
}