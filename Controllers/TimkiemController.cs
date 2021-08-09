using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocTrucTuyen.Models;
using PagedList.Mvc;
using PagedList;
using HocTrucTuyen.Models.Entity;

namespace HocTrucTuyen.Controllers
{

    public class TimkiemController : Controller
    {
        HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Timkiem
        
        public ActionResult Search(string keyword, int page = 1, int pagesize = 2)
        {
            string[] key = keyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var document_name = new List<HocTrucTuyen.Models.Entity.Course>();//Tìm theo tên tài liệu
            foreach (var item in key)
            {
                document_name = (from b in db.Courses
                                 where b.Title.Contains(item)
                                 select b).ToList();
            }
            ViewBag.KeyWord = keyword;
            return View(document_name.ToPagedList(page, pagesize));
        }


    }
    }    