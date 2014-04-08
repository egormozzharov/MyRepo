using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication3.Models
{
    public class DbManager
    {
        private UsersContext db = new UsersContext();

        public void Update(UserInfo u)
        {
            try
            {
                UserInfo userInfo = db.UserInfos.First(
                    i => i.UserId == u.UserId
                    );
                userInfo.UserName = u.UserName;
                userInfo.SecondName = u.SecondName;
                userInfo.Sex = u.Sex;
                userInfo.About = u.About;
                userInfo.UserProfile = u.UserProfile;
                userInfo.CountryId = u.CountryId;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }
    }
}