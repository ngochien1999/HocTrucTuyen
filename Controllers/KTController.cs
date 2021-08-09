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
    public class KTController : Controller
    {
        // GET: KT
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        public ActionResult Index()
        {
            return View();
        }
      

        public ActionResult CheckExcercise1()
        {
            if (Session["user"] == null)
            {
                Session["error_login"] = "Bạn vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "User");
            }
            ViewBag.lstQuestion = new QuestionBusiness().getQuestionAV_1();
            ViewBag.lstAnswer = new AnswerBusiness().getAllAnswer();
            return View();
        }

        public ActionResult CheckExercise2()
        {
            if (Session["user"] == null)
            {
                Session["error_login"] = "Bạn vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "User");
            }
            ViewBag.lstQuestion = new QuestionBusiness().getQuestionAV_1();
            ViewBag.lstAnswer = new AnswerBusiness().getAllAnswer();
            return View();
        }

        public ActionResult Exercise_TH3()
        {
            if (Session["user"] == null)
            {
                Session["error_login"] = "Bạn vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "User");
            }
            ViewBag.lstQuestion = new QuestionBusiness().getQuestionTH_1();
            ViewBag.lstAnswer = new AnswerBusiness().getAllAnswer();
            return View();
        }

        public ActionResult Exercise_TH4()
        {
            if (Session["user"] == null)
            {
                Session["error_login"] = "Bạn vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "User");
            }
            ViewBag.lstQuestion = new QuestionBusiness().getQuestionTH_1();
            ViewBag.lstAnswer = new AnswerBusiness().getAllAnswer();
            return View();
        }

        //Bài thi trắc nghiệm cuối khóa học
       
     
        public JsonResult CheckExcercise(string Model/*List<TestDTO> lst*/)
        {

            var lst = new JavaScriptSerializer().Deserialize<List<TestDTO>>(Model);
            var answer = new AnswerBusiness().getAllAnswer();
            var lstquestion = new List<Question>();
            int point = 0;

            foreach (var item in lst)
            {
                var question = db.Questions.Find(item.Question_ID);
                lstquestion.Add(question);
                if (db.Answers.Count(x => x.Question_ID == item.Question_ID && x.ID == item.Answer_ID && x.Status == true) > 0)
                {
                    point++;
                    item.Status = true;
                }
                else
                {
                    item.Status = false;
                }
            }


            //hiển thị đáp án và câu đã chọn
            Session["DapAn"] = lst;
            ViewBag.point = point;
            ViewBag.Answered = lst;
            ViewBag.lstAnswer = answer;
            ViewBag.lstQuestion = lstquestion;

            return Json(new
            {
                status = true,
                result = point.ToString(),
                count = lst.Count.ToString()
            });

        }

        public ActionResult Point(string result, string count)
        {
            ViewBag.result = result + "/" + count;
            return View();
        }

        public ActionResult ResultExcercise()
        {
            var lst = Session["DapAn"] as List<TestDTO>;
            var answer = new AnswerBusiness().getAllAnswer();
            var lstquestion = new List<Question>();
            int point = 0;
            foreach (var item in lst)
            {
                var question = db.Questions.Find(item.Question_ID);
                lstquestion.Add(question);
                if (db.Answers.Count(x => x.Question_ID == item.Question_ID && x.ID == item.Answer_ID && x.Status == true) > 0)
                {
                    point++;
                    item.Status = true;
                }
                else
                {
                    item.Status = false;
                }
            }
            ViewBag.point = point;
            ViewBag.Answered = lst;
            ViewBag.lstAnswer = answer;
            ViewBag.lstQuestion = lstquestion;
            return View();
        }
    }
}