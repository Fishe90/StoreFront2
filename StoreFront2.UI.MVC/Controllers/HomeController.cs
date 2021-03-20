using StoreFront2.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace StoreFront2.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Products()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Contact()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message: <br/>" +
                $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

                MailMessage m = new MailMessage("admin@dillonfisher.com", "dillon.f12.df@gmail.com", cvm.Subject, body);

                m.IsBodyHtml = true;

                m.Priority = MailPriority.High;

                m.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.dillonfisher.com");

                client.Credentials = new NetworkCredential("admin@dillonfisher.com", "@Ilikecheese617");

                client.Port = 8889;

                try
                {
                    client.Send(m);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }
                return View("EmailConfirmation");
            }            
            return View(cvm);
        }
    }
}
