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
    public class UserController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var model = new UserBusiness().getAll(page, pagesize);
            return View(model);
        }

        public ActionResult Info(long id, int page = 1, int pagesize = 5)
        {
            var model = new UserBusiness().getInfoUser(id, page, pagesize);
            ViewBag.User = new UserBusiness().findUser(id);
            return View(model);
        }

        public ActionResult Test(int page = 1, int pagesize = 10)
        {
            var model = db.Tests.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
            return View(model);
        }
    }
}