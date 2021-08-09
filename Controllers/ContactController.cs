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
   
    public class ContactController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult Contact()
        {
            return View();
        }
      



    }
}