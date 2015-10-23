using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspireApps.Web.Controllers
{
    public class HomeController : Controller
    {
        HomeDataController apiController = new HomeDataController();
        public ActionResult Index()
        {
            List<HomePageItem> homepageItems = apiController.GetHomePageItems().ToList();
            List<HomePageItemVM> list = new List<HomePageItemVM>();
            foreach (HomePageItem item in homepageItems)
            {
                list.Add(new HomePageItemVM
                {
                    Heading = item.Heading,
                    Description = item.Description,
                    ImageURL = item.ImageName
                });
            }

            List<App> apps = apiController.GetAppList().ToList();
            List<AppVM> appList = new List<AppVM>();

            foreach (App item in apps)
            {
                appList.Add(new AppVM
                {
                    Category = item.PartitionKey,
                    Name = item.Name,
                    Description = item.Description,
                    StoreURL = item.StoreURL,
                    ImageURL = item.ImageURL,
                    Active = item.Active
                });
            }

            IndexVM vm = new IndexVM();

            if (list != null)
            {
                vm.HomePageItems = list;
            }
            else
            {
                vm.HomePageItems = new List<HomePageItemVM>();
            }

            if (appList.Any(i => i.Category == "AndroidApp"))
            {
                vm.AndroidAppList = appList.Where(i => i.Category == "AndroidApp").ToList();
            }
            else
            {
                vm.AndroidAppList = new List<AppVM>();
            }

            if (appList.Any(i => i.Category == "WindowsApp"))
            {
                vm.WindowsAppList = appList.Where(i => i.Category == "WindowsApp").ToList();
            }
            else
            {
                vm.WindowsAppList = new List<AppVM>();
            }

            return View(vm);
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [HttpPost, ActionName("SendEmail")]
        public void SendEmail(string Name, string EmailFrom, string Subject, string Message)
        {
            var SMTPHost = GetValueFromConfig("SMTPHost");
            var SMTPPort = Convert.ToInt32(GetValueFromConfig("SMTPPort"));
            var ContactEmail = GetValueFromConfig("ContactEmail");
            var MailPassword = GetValueFromConfig("MailPassword");

            //var SMTPHost = "smtp.gmail.com";
            //var SMTPPort = 587;
            //var ContactEmail = "contact@aspireapps.in";
            //var MailPassword = "Chaitu@3";

            MailAddress userEmail = new MailAddress(EmailFrom, Name);
            MailAddress contactEmail = new MailAddress(ContactEmail, "Support @ Aspire Apps");
            MailMessage mail = new MailMessage(contactEmail, contactEmail);
            mail.Body = Message;
            mail.Subject = Subject;
            mail.ReplyToList.Clear();
            mail.ReplyToList.Add(userEmail);

            SmtpClient client = new SmtpClient(SMTPHost, SMTPPort);
            NetworkCredential cred = new NetworkCredential(contactEmail.Address, MailPassword);
            client.EnableSsl = true;
            client.Credentials = cred;
            client.ServicePoint.MaxIdleTime = 1;

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }

        private string GetValueFromConfig(string key)
        {
            //var settings = System.Configuration.ConfigurationManager.AppSettings;
            return Environment.GetEnvironmentVariable(key);
        }
    }
}