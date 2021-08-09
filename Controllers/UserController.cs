using HocTrucTuyen.Models.Business;
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
    public class UserController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User entity)
        {
            var res = new UserBusiness().checkLogin(entity);
            if (entity.Email == "admin@gmail.com" && res)
            {
                Session["adminLogin"] = new UserBusiness().FindUserByEmail(entity.Email);
                return Redirect("/Admin/Home");
            }
            else if (res)
            {
                Session["error_login"] = null;
                Session["user"] = new UserBusiness().FindUserByEmail(entity.Email);
                return Redirect("/");
            }
            else
            {
                Session["error_login"] = "Tài khoản hoặc mật khẩu không đúng!! Vui lòng Đăng nhập lại.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Timkiem(string Searchby, string search)
        {
            if (Searchby == "Title")
            {
                var model = db.Courses.Where(emp => emp.Title == search || search == null).ToList();
                return View(model);
            }
            var Couses = db.Courses.ToList();
            return View(Couses.ToList());

        }

        public ActionResult Register()
        {
            return View();
        }
      
        public ActionResult Blog()
        {
            return View();
        }
       
        public ActionResult b1()
        {
            return View();
        }

        public ActionResult b8()
        {
            return View();
        }
        public ActionResult b2()
        {
            return View();
        }
        public ActionResult b5()
        {
            return View();
        }
        public ActionResult b7()
        {
            return View();
        }
        public ActionResult b6()
        {
            return View();
        }
      
        public ActionResult b3()
        {
            return View();
        }
        public ActionResult b4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User entity)
        {
            var user = new UserBusiness().checkExitsEmail(entity.Email);
            if(user)
            {
                Session["error_login"] = "Email đã tồn tại, vui lòng chọn email khác.";
                return RedirectToAction("Register");
            }
            else if (new UserBusiness().addUser(entity))
            {
                Session["error_login"] = "Đăng ký thành công. Vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index");
            }
            else
            {
                Session["error_login"] = "Đăng ký tài khoản không thành công, vui lòng thử lại sau";
                return RedirectToAction("Register");
            }
        }

        public ActionResult HistoryTest(int page = 1, int pagesize = 5)
        {
            if (Session["user"] == null)
            {
                Session["error_login"] = "Bạn vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "User");
            }
            var user = Session["user"] as User;
            var model = db.Tests.Where(x => x.User_ID == user.ID).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect("/");
        }
    }
}