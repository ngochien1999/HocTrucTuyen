using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace HocTrucTuyen.Models.Business
{
    public class SendEmailController : Controller
    {
       // public NetworkCredential Credentials { get; private set; }

        //var db = new HocTrucTuyen.Models.Entity.HocTrucTuyenEntities();
        // GET: SendEmail
        public ActionResult send()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Form(string receiverEmail, string subject, string message)
        {
            try { 
                if(ModelState.IsValid)
                {
                    var  senderEmail = new MailAddress("ntnhien19091999@gmail.com", "Liên hệ từ người dùng");
                     var receivereEmail = new MailAddress(receiverEmail, "Receiver");
                    var password = "ngochien19091999";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                      DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address , password)

                    };
                    using(var messa = new MailMessage(senderEmail , receivereEmail)
                    {
                        Subject = subject,
                        Body = body
                    }
                    )
                    {
                        smtp.Send(messa);
                    }
                 return   ViewBag.Thongbao = "Gởi liên hệ thành công";

                }


            }
           //  return View();


            catch (Exception)
            {
                ViewBag.Error = "There are some problem";
            }
          
            return RedirectToAction("send","SendEmail");
        }
         
    }

}