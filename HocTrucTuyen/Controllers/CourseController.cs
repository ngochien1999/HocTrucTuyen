using HocTrucTuyen.Models.Business;
using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HocTrucTuyen.Controllers
{
    public class CourseController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        private List<CourseDTO> lstcourse = new List<CourseDTO>();
        public ActionResult Detail(long id)
        {
            var course = new CourseBusiness().detailCourse(id);
            ViewBag.Course = course;

            //Lấy chi tiết bài giảng của khóa học
            ViewBag.CourseDetail = db.Course_Detail.Where(x => x.Course_ID == id).ToList();

            //Lấy bài thi trắc nghiệm
            ViewBag.MutipleChoice = db.MutipleChoices.Where(x => x.Course_ID == id).ToList();
            //Lưu bài giảng đã xem
            lstcourse.Add(course);
            Session["Course_Seen"] = lstcourse;
            ViewBag.lstReview = new CourseBusiness().getReview(id);
            return View();
        }

        public ActionResult CourseDetail(long ID)
        {
            ViewBag.CourseDetail = db.Course_Detail.Find(ID);
            return View();
        }

        public JsonResult addReview(string json_review)
        {
            var JsonReview = new JavaScriptSerializer().Deserialize<List<ReviewDTO>>(json_review);
            var review = new Review();
            foreach (var item in JsonReview)
            {
                review.Content = item.Content;
                review.Rating = item.Rating;
                review.CreatedDate = DateTime.Now;
                review.User_ID = item.User_ID;
                review.Course_ID = item.Course_ID;
                review.Status = true;
            }

            var res = new CourseBusiness().addReview(review);
            if (res)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}