using HocTrucTuyen.Models.Business;
using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Areas.Admin.Controllers
{
    public class Course_DetailController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Admin/Course_Detail
        public ActionResult Index(long ID, int page = 1, int pagesize = 5)
        {
            ViewBag.Course = db.Courses.Find(ID);
            var model = db.Course_Detail.Where(x => x.Course_ID == ID).OrderBy(x => x.Created_Date).ToPagedList(page, pagesize);
            return View(model);
        }
       

        public ActionResult Add(long ID)
        {
            ViewBag.Course = db.Courses.Find(ID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Course_Detail entity)
        {
            entity.Link = "https://www.youtube.com/embed/" + new CourseBusiness().GetLink(entity.Link);
            entity.Created_Date = DateTime.Now;
            db.Course_Detail.Add(entity);
            db.SaveChanges();
            TempData["add_success"] = "Thêm bài giảng thành công.";
            return RedirectToAction("Index", new { ID = entity.Course_ID});
        }
      

        public ActionResult Edit(long ID)
        {
            ViewBag.detail = db.Course_Detail.Find(ID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Course_Detail entity)
        {
            var course = db.Course_Detail.Find(entity.ID);
            course.Link = "https://www.youtube.com/embed/" + new CourseBusiness().GetLink(entity.Link);
            course.Name = entity.Name;
            course.Descriptions = entity.Descriptions;
            db.SaveChanges();
            TempData["add_success"] = "Sửa bài giảng thành công.";
            return RedirectToAction("Index", new { ID = course.Course_ID });
        }

        public JsonResult Delete(long ID)
        {
            try
            {
                var course = db.Course_Detail.Find(ID);
                db.Course_Detail.Remove(course);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
            
        }
    }
}