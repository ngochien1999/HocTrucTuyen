using HocTrucTuyen.Models.DTO;
using HocTrucTuyen.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Areas.Admin.Controllers
{
    public class MutipleChoiceController : Controller
    {
        private HocTrucTuyenEntities db = new HocTrucTuyenEntities();
        // GET: Admin/MutipleChoice
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var model = db.MutipleChoices.OrderBy(x => x.ID).ToPagedList(page, pagesize);
            ViewBag.ListCourse = db.Courses.ToList();
            return View(model);
        }

        public JsonResult Delete(long ID)
        {

            try
            {
                var MutipleChoice = db.MutipleChoices.Find(ID);
                db.MutipleChoices.Remove(MutipleChoice);
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


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addMutipleChoice(MutipleChoice entity)
        {
            try
            {
                var maxid = db.MutipleChoices.Max(x => x.ID);
                var prv = new MutipleChoice();
                prv.Title = entity.Title;
                prv.Course_ID = entity.Course_ID;
                db.MutipleChoices.Add(prv);
                db.SaveChanges();
                TempData["add_success"] = "Thêm bài trắc nghiệm thành công";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["add_success"] = "Thêm bài trắc nghiệm KHÔNG thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult editMutipleChoice(MutipleChoice entity)
        {
            try
            {
                var prv = db.MutipleChoices.Find(entity.ID);
                prv.Title = entity.Title;
                prv.Course_ID = entity.Course_ID;
                db.SaveChanges();
                TempData["add_success"] = "Sửa bài trắc nghiệm thành công";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["add_success"] = "Sửa bài trắc nghiệm KHÔNG thành công";
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetMutipleChoiceByID(long ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var MutipleChoice = db.MutipleChoices.Find(ID);
            return Json(MutipleChoice, JsonRequestBehavior.AllowGet);
        }


        //Chi tiết các câu hỏi trắc nghiệm
        public ActionResult Detail(long ID, int page = 1, int pagesize = 5)
        {
            var model = db.Questions.Where(x => x.MutipleChoice_ID == ID).OrderBy(x => x.ID).ToPagedList(page, pagesize);
            ViewBag.Mutiple = db.MutipleChoices.Find(ID);
            return View(model);
        }

        //Xóa câu hỏi
        public JsonResult DeleteQuestion(long ID)
        {
            try
            {
                var ques = db.Questions.Find(ID);
                var answer = db.Answers.Where(x => x.Question_ID == ID).ToList();
                foreach(var item in answer)
                {
                    db.Answers.Remove(item);
                }
                db.Questions.Remove(ques);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddQuesAns(MutipleChoiceDTO entity, long MutipleChoice_ID)
        {
            var ques = new Question();
            ques.MutipleChoice_ID = MutipleChoice_ID;
            ques.Description = entity.Question;
            ques.Status = true;
            ques.CreatedDate = DateTime.Now;
            db.Questions.Add(ques);
            db.SaveChanges();

            var maxid = db.Questions.Max(x => x.ID);
            int dem = 0;
            foreach(var item in entity.Answers)
            {
                var ans = new Answer();
                ans.Question_ID = maxid;
                ans.Description = item.Description;
                if (dem == entity.Status)
                    ans.Status = true;
                else
                    ans.Status = false;
                dem++;
                db.Answers.Add(ans);
                db.SaveChanges();
            }

            TempData["add_success"] = "Thêm câu hỏi trắc nghiệm thành công";
            return RedirectToAction("Detail", new {ID = MutipleChoice_ID });
        }

        public ActionResult EditQues_Ans(long ID)
        {
            ViewBag.ques = db.Questions.Find(ID);
            ViewBag.lstAns = db.Answers.Where(x => x.Question_ID == ID).ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditQuesAns(MutipleChoiceDTO entity, long Question_ID, long MutipleChoice_ID)
        {
            var ques = db.Questions.Find(Question_ID);
            ques.Description = entity.Question;
            db.SaveChanges();

            int dem = 0;
            foreach (var item in entity.Answers)
            {
                if(item.ID != 0)
                {
                    var ans = db.Answers.Find(item.ID);
                    ans.Description = item.Description;
                    if (dem == entity.Status)
                        ans.Status = true;
                    else
                        ans.Status = false;
                }
                else
                {
                    var ans = new Answer();
                    ans.Question_ID = Question_ID;
                    ans.Description = item.Description;
                    if (dem == entity.Status)
                        ans.Status = true;
                    else
                        ans.Status = false;
                    db.Answers.Add(ans);
                }
                dem++;
                db.SaveChanges();
            }
            TempData["add_success"] = "Sửa câu hỏi trắc nghiệm thành công";
            return RedirectToAction("Detail", new { ID = MutipleChoice_ID });
        }

        //Xóa câu trả lời
        public JsonResult DeleteAnswer(long ID)
        {
            try
            {
                var answer = db.Answers.Find(ID);
                db.Answers.Remove(answer);
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