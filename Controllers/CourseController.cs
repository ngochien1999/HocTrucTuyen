using HocTrucTuyen.Models;
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

            Session["CourseID"] = id;
        
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
          private List<CourseDTO> lstcoursedetail = new List<CourseDTO>();

        public ActionResult CourseDetail(long ID)
        {
            var course = new CourseBusiness().detailCourse(ID);
            ViewBag.Course = course;

            //Lấy chi tiết bài giảng của khóa học
          /*  ViewBag.CourseDetail = db.Course_Detail.Where(x => x.Course_ID == ID).ToList();
            Session["CourseDetailID"] = lstcoursedetail;*/
            string courseDetail = "";
            //CourseDetailID = db.Course_Detail.Add.
          //  Session.Timeout = 0;

            if (Session["CourseDetailID"] != null)
            {
                courseDetail = Session["CourseDetailID"].ToString();
                TempData["msg"] = "<script>alert( '" + courseDetail + "')</script>";
            }
            var Course_Detail = new Course_Detail();
            Course_Detail = db.Course_Detail.Find(ID);
            Session["CourseDetailID"] = Course_Detail.Name;
            ViewBag.CourseDetail = db.Course_Detail.Find(ID);
            return View();
        }
        public ActionResult SX(string sortOrder)
        {
            // 1. Thêm biến NameSortParm để biết trạng thái sắp xếp tăng, giảm ở View
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // 2. Truy vấn lấy tất cả đường dẫn
            var links = from l in db.Courses
                        select l;

            // 3. Thứ tự sắp xếp theo thuộc tính LinkName
            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo LinkName
                case "name_desc":
                    links = links.OrderByDescending(s => s.CreatedDate);
                    break;

                // 3.2 Mặc định thì sẽ sắp tăng
                default:
                    links = links.OrderBy(s => s.CreatedDate);
                    break;
            }

            // 4. Trả kết quả về Views
            return View(links.ToList());
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