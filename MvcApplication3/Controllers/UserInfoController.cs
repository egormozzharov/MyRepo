using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class UserInfoController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            var userinfos = db.UserInfos.Include(u => u.UserProfile).Include(u => u.Country);
            return View(userinfos.ToList());
        }

        //
        // GET: /UserInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            UserInfo userinfo = db.UserInfos.Include(u => u.Country).First(u => u.UserId == id);
            //UserInfo userinfo = db.UserInfos.Find(id);
            if (userinfo == null)
            {
                return HttpNotFound();
            }
            return View(userinfo);
        }

        //
        // GET: /UserInfo/Create

        public ActionResult Create()
        {
            UserProfile userProfile = db.UserProfiles.Single(m => m.UserName == User.Identity.Name);
            UserInfo userInfo = db.UserInfos.Find(userProfile.UserId);
            SelectList selectList = new SelectList(db.Countries, "CountryId", "CountryName", 3);
            ViewBag.CountryId = selectList;
            if (userInfo != null)
            {
                return View(userInfo);
            }
            else
            {
                userInfo = new UserInfo()
                {
                    UserId = userProfile.UserId,
                    UserName = userProfile.UserName
                };
            }
            
            return View(userInfo);
        }

        //
        // POST: /UserInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo userinfo)
        {
            if (ModelState.IsValid)
            {
                if (db.UserInfos.Find(userinfo.UserId) != null)
                {
                    DbManager manager = new DbManager();
                    manager.Update(userinfo);

                }
                else
                {
                    db.UserInfos.Add(userinfo);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", userinfo.UserId);
            return View(userinfo);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}