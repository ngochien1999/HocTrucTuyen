using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            //Đếm số khóa học
            ViewBag.Count_Course = db.Courses.Count();

            //Số bài giảng
            ViewBag.Count_CourseDetail = db.Course_Detail.Count();

            //Số học viên
            ViewBag.Count_User = db.Users.Count(x => x.FullName != "Administrator");

            //Số giảng viên
            ViewBag.Count_Teacher = db.Teachers.Count();

            //Thống kê đánh giá hôm nay
            ViewBag.Review_today = db.Reviews.Where(x => DbFunctions.TruncateTime(x.CreatedDate) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            //Thống kê tổng đánh giá
            ViewBag.Review_Total = db.Reviews.Count();

            //Bài thi trắc nghiệm
            ViewBag.Count_Test = db.Tests.Count();

            //Học viên tham gia thi
            ViewBag.Count_UserTest = db.Tests.Select(x => x.User_ID).Distinct().Count();

            //Các khóa học mới
            var lstCourse = new List<CourseDTO>();
            foreach(var item in db.Courses.ToList())
            {
                var course = new CourseDTO();
                course.Title = item.Title;
                course.Teacher_Name = item.Teacher.Name;
                course.CreatedDate = item.CreatedDate;
                course.CountCourse = db.Course_Detail.Where(x => x.Course_ID == item.ID).Count();
                lstCourse.Add(course);
            }

            ViewBag.lstCourse = lstCourse.OrderByDescending(x => x.CreatedDate).ToList();
            return View();
        }
    }
}