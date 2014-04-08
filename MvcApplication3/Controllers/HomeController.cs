using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                //UserInfo userinfo = db.UserInfos.SingleOrDefault(m => m.UserName == User.Identity.Name);
                UserProfile userProfile = db.UserProfiles.Single(m => m.UserName == User.Identity.Name);
                UserInfo userInfo = db.UserInfos.Find(userProfile.UserId);
                if (userInfo == null)
                {
                    userInfo = new UserInfo()
                    {
                        UserId = userProfile.UserId,
                        UserName = userProfile.UserName
                    };
                }
                return View(userInfo);
            }
            return View();
        }
    }
}
