using HocTrucTuyen.Models.Business;
using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Areas.Admin.Controllers
{

    public class CourseController : Controller
    {

        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Admin/Course
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var model = new CourseBusiness().getCourse(page, pagesize);
            return View(model);
        }
       

        /*  public ActionResult KHCT(long id, int page = 1, int pagesize = 5)
          {
              var model = new CourseBusiness().getCourse( page, pagesize);
              ViewBag.User = new Course().Course_Detail(id);
              return View(model);
          }
  */
        public ActionResult CT(long id, int page = 1, int pagesize = 5)
        {
             var model = new UserBusiness().getInfoUser(id, page, pagesize);
            ViewBag.User = new UserBusiness().findUser(id);
            return View(model);
        }


        public ActionResult edit(long id)
        {
            ViewBag.Course = new CourseBusiness().findID(id);
            ViewBag.Teacher = new CourseBusiness().GetTeacher();
            return View();
        }
        public ActionResult DetailKH(long id)
        {
            var model = (from t in db.Tests
                         join khhhhh in db.Courses on t.Course_ID equals khhhhh.ID
                         join n in db.Users on t.User_ID equals n.ID
                         where t.Course_ID == id
                         select new TestDTO()
                         {
                             Fullname = n.FullName,
                             Result = t.Result,
                             CreateDate = t.CreatedDate,
                         }).Distinct();
            ViewBag.KH = db.Courses.SingleOrDefault(x => x.ID == id);
            return View(model.OrderByDescending(x =>x.CreateDate).ToPagedList(1, 10));
            

        }
       
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult editCourse(Course entity, HttpPostedFileBase Image)
        {
            try
            {
                var course = db.Courses.Find(entity.ID);
                course.Title = entity.Title;
                course.Description = entity.Description;
                course.Teacher_ID = entity.Teacher_ID;
                if (Image != null && course.Images != Image.FileName)
                {
                    //Xóa file cũ
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Asset/images/course"), course.Images));
                    //Thêm hình ảnh
                    var path = Path.Combine(Server.MapPath("~/Asset/images/course"), Image.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        string extensionName = Path.GetExtension(Image.FileName);
                        string filename = Image.FileName + DateTime.Now.ToString("ddMMyyyy") + extensionName;
                        path = Path.Combine(Server.MapPath("~/Asset/images/course/"), filename);
                        Image.SaveAs(path);
                        course.Images = "/Asset/images/course/" + filename;
                    }
                    else
                    {
                        Image.SaveAs(path);
                        course.Images = "/Asset/images/course/" + Image.FileName;
                    }
                }
                db.SaveChanges();
                TempData["add_success"] = "Sửa khóa học thành công.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["add_success"] = "Sửa khóa học KHÔNG thành công.";
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult add()
        {
            ViewBag.Teacher = new CourseBusiness().GetTeacher();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult add(Course entity, HttpPostedFileBase Image)
        {
            var course = new Course();
            course.Title = entity.Title;
            course.Description = entity.Description;
            course.Teacher_ID = entity.Teacher_ID;
            course.CreatedDate = DateTime.Now;
            course.Status = true;
            //Thêm hình ảnh
            var path = Path.Combine(Server.MapPath("~/Asset/images/course"), Image.FileName);
            if (System.IO.File.Exists(path))
            {
                string extensionName = Path.GetExtension(Image.FileName);
                string filename = Image.FileName + DateTime.Now.ToString("ddMMyyyy") + extensionName;
                path = Path.Combine(Server.MapPath("~/Asset/images/course/"), filename);
                Image.SaveAs(path);
                course.Images = "/Asset/images/course/" + filename;
            }
            else
            {
                Image.SaveAs(path);
                course.Images = "/Asset/images/course/" + Image.FileName;
            }

            var res = new CourseBusiness().addCourse(course);
            if (res)
            {
                TempData["add_success"] = "Thêm khóa học thành công.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["add_success"] = "Thêm khóa học KHÔNG thành công.";
                return RedirectToAction("add");
            }              
        }

        public JsonResult deleteCourse(long id)
        {
            var res = new CourseBusiness().deleteCourse(id);
            if (res)
                return Json(new
                {
                    status = true
                });
            else
                return Json(new
                {
                    status = false
                });
        }
       

     
   


    }
}