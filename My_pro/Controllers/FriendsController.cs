using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using My_pro.Model;
using My_pro.Model.Entites;
using My_pro.Core;

namespace My_pro.Controllers
{
    public class FriendsController : BaseController
    {
        //<-- Search -->//
        public ActionResult Search()
        {
            try
            {
                return View(UserService.Search("", CurrentUser));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return View(UserService.Search(search, CurrentUser));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Requests -->//
        public ActionResult Requests()
        {
            //List<string> friend1 = db.Friends.Where(a => a.UserID1 == CurrentUser.Id && a.Sender != CurrentUser.Id && a.Accept == false).Select(a => a.UserID2).ToList();
            List<string> friend = db.Friends.Where(b => b.UserID2 == CurrentUser.Id && b.Sender != CurrentUser.Id && b.Accept == false).Select(a => a.UserID1).ToList();
            //friend1.AddRange(friend2);
            List<Users> users = new List<Users>();
            foreach (string id in friend)
            {
                Users u = db.Users.FirstOrDefault(a => a.Id == id);
                users.Add(u);
            }
            return View(users);
        }
        //_______________________________________________________________________________________

        //<-- GetMyFriends -->//
        public ActionResult MyFriends()
        {
            return View(GetMyFriends());
        }

        public List<Users> GetMyFriends()
        {
            List<string> MyFriends = db.Friends.Where(a => a.UserID1 == CurrentUser.Id && a.Accept == true).Select(a => a.UserID2).ToList();
            List<string> MyFriends2 = db.Friends.Where(a => a.UserID2 == CurrentUser.Id && a.Accept == true).Select(a => a.UserID1).ToList();
            MyFriends.AddRange(MyFriends2);
            List<Users> listMyFriends = new List<Users>();

            foreach (string id in MyFriends)
            {
                Users u = db.Users.FirstOrDefault(a => a.Id == id);
                listMyFriends.Add(u);
            }
            return listMyFriends;
        }

        //_______________________________________________________________________________________

        //<-- AddFriend -->//
        public ActionResult AddFriend(string id)
        {
            try
            {
                if (id == CurrentUser.Id) { return View(); }
                if (ModelState.IsValid)
                {
                    Users user1 = db.Users.Find(CurrentUser.Id);
                    Users user2 = db.Users.Find(id);

                    Friends friend = new Friends();
                    friend.UserID1 = CurrentUser.Id;
                    friend.UserID2 = id;
                    friend.user1 = user1;
                    friend.user2 = user2;
                    friend.Sender = user1.Id;
                    friend.Accept = false;
                    Friends exist = db.Friends.FirstOrDefault(a => a.UserID1 == friend.UserID1 && a.UserID2 == friend.UserID2 || a.UserID1 == friend.UserID2 && a.UserID2 == friend.UserID1);
                    if (exist == null)
                    {
                        db.Friends.Add(friend);
                        db.SaveChanges();
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Accept -->
        public ActionResult Accept(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                Friends MyRow = db.Friends.FirstOrDefault(a => a.UserID1 == id || a.UserID2 == id);
                if (MyRow == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                MyRow.Accept = true;
                db.SaveChanges();
                return RedirectToAction("Requests");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Accept([Bind(Include = "UserID1,UserID2,Sender,Accept,user1,user2")] Friends friends)
        {
            if (ModelState.IsValid)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(friends);
        }
        //_______________________________________________________________________________________

        //<-- Refusal -->
        public ActionResult Refusal(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                Friends friends = db.Friends.FirstOrDefault(a => a.UserID1 == id || a.UserID2 == id);
                if (friends == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                db.Friends.Remove(friends);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Another Action -->//
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friends friends = db.Friends.Find(id);
            if (friends == null)
            {
                return HttpNotFound();
            }
            return View(friends);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "UserID1,UserID2,Sender,Accept,user1,user2")] Friends friends)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friends).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(friends);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friends friends = db.Friends.Find(id);
            if (friends == null)
            {
                return HttpNotFound();
            }
            return View(friends);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Friends friends = db.Friends.Find(id);
                db.Friends.Remove(friends);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friends friends = db.Friends.Find(id);
            if (friends == null)
            {
                return HttpNotFound();
            }
            return View(friends);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Friends.ToList());
        }
        //_______________________________________________________________________________________









        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SendMessage(string id)
        //{
        //    return RedirectToAction("Messages");
        //}
        //public List<Users> FSearch()
        //{
        //    List<string> users = db.Users.Where(a => a.Id != CurrentUser.Id).Select(a => a.Id).ToList();
        //    List<string> FriendsList1 = db.Friends.Where(a => a.UserID1 == CurrentUser.Id).Select(a => a.UserID2).ToList();
        //    List<string> FriendsList2 = db.Friends.Where(a => a.UserID2 == CurrentUser.Id).Select(a => a.UserID1).ToList();
        //    FriendsList1.AddRange(FriendsList2);
        //    foreach (string friendid in FriendsList1)
        //    {
        //        foreach (string user_id in users)
        //        {
        //            if (user_id == friendid)
        //            {
        //                users.Remove(user_id);
        //                break;
        //            }
        //        }
        //    }
        //    List<Users> UU = new List<Users>();
        //    foreach (string id in users)
        //    {
        //        Users u = db.Users.FirstOrDefault(a => a.Id == id);
        //        if (u != null)
        //        {
        //            UU.Add(u);
        //        }
        //    }
        //    return UU;
        //}
        ////ProfileView:
        ////============
        //public ActionResult FProfile(string id)
        //{
        //    Users F = db.Users.FirstOrDefault(a => a.Id == id);
        //    if (F != null)
        //    {
        //        Session["FProfile"] = F;
        //    }
        //    else
        //    {
        //        Session["FProfile"] = null;
        //    }
        //    return RedirectToAction("Profile", "Home");
        //}
        //###################################################################################################################################
        ////EditProfile
        //public ActionResult EditProfile()
        //{
        //    return View(CurrentUser);
        //}

        //################################################
        //################################################
        ////UpdateLanguage:
        ////===============
        //public static void UpdateLanguage(Users u, string lang)
        //{
        //    Users user = db.Users.FirstOrDefault(a => a.Id == u.Id);
        //    db.SaveChanges();
        //}
    }
}