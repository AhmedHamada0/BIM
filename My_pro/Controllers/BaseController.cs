using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_pro.Model.Entites;
using Microsoft.AspNet.Identity;
using My_pro.Model;
using Microsoft.AspNet.Identity.Owin;
using System.Text;

namespace My_pro.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        //###################################################################################################################################
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (CurrentUser.lang != null && CurrentUser != null)
        //    {
        //        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CurrentUser.lang);
        //        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CurrentUser.lang);
        //    }
        //}
        //###################################################################################################################################
        public UserContext db = new UserContext();
        public UserManager<Users> um => HttpContext.GetOwinContext().Get<UserManager<Users>>();
        //_______________________________________________________________________________________
        //<-- IsRole(); -->//
        public bool IsAdmin(string UserId)
        {
            Users U = db.Users.FirstOrDefault(a => a.Id == UserId);
            if (U != null)
            {
                if (um.IsInRole(UserId, "Admin"))
                {
                    return true;
                }
            }
            return false;
        }

        //_______________________________________________________________________________________

        //<-- All User Levels -->//          { Completed Level Or UnCompleted or free }
        public List<Levels> MyLevels()
        {
            if (IsAdmin(CurrentUser.Id))
            {
                List<Levels> SLevels = db.Levels.ToList();
                return SLevels;
            }
            List<Levels> L = db.Users_Levels.Where(a => a.User_Id == CurrentUser.Id).Select(a => a.Levels).ToList();
            List<Levels> Levels = db.Levels.Where(a => a.IsFree == true).ToList();
            foreach (var i in Levels)
            {
                foreach (var o in L)
                {
                    if (o == i)
                    {
                        L.Remove(o); break;
                    }
                }
            }
            L.AddRange(Levels);
            return L;
        }
        //_______________________________________________________________________________________

        //<-- Levels Completed -->//
        public List<Levels> Complete_Levels(string U_id)
        {
            List<string> UL = db.Diploma.Where(a => a.UserId == U_id).Select(a => a.Level).ToList();
            List<Levels> Levels = new List<Levels>();
            foreach (string id in UL)
            {
                Levels l = db.Levels.FirstOrDefault(a => a.Id == id);
                if (l != null)
                {
                    Levels.Add(l);
                }
            }
            return Levels;
        }
        //_______________________________________________________________________________________

        //<-- Levels UnCompleted -->//
        public List<Levels> Un_Complete_Levels(string U_id)
        {
            List<string> UL = db.Users_Levels.Where(a => a.User_Id == U_id).Select(a => a.Level_Id).ToList();
            List<string> DL = db.Diploma.Where(a => a.UserId == U_id).Select(a => a.Level).ToList();

            foreach (string id in UL) { foreach (string i in DL) { if (id == i) { UL.Remove(id); } } }

            List<Levels> Levels = new List<Levels>();
            foreach (string id in UL)
            {
                Levels l = db.Levels.FirstOrDefault(a => a.Id == id);
                if (l != null)
                {
                    Levels.Add(l);
                }
            }
            return Levels;
        }
        //_______________________________________________________________________________________

        //<-- Un User Levels -->//       { Buy }
        public List<Levels> Un_User_Levels(string U_id)
        {
            if (IsAdmin(U_id))
            {
                List<Levels> L = new List<Model.Levels>();
                return L;
            }
            List<Levels> UL = db.Users_Levels.Where(a => a.User_Id == U_id).Select(a => a.Levels).ToList();
            List<Levels> Levels = db.Levels.Where(a => a.IsFree == false).ToList();
            foreach (var o in UL)
            {
                try { Levels.Remove(o); } catch { }
            }
            return Levels;
        }
        //_______________________________________________________________________________________

        //<-- Number of Friends -->//
        public int FriendsNum(string id)
        {
            if (id == null) { id = CurrentUser.Id; }
            List<string> friend = db.Friends.Where(a => (a.UserID1 == id && a.Accept == true) || (a.UserID2 == id && a.Accept == true)).Select(a => a.Sender).ToList();
            return friend.Count();
        }
        //_______________________________________________________________________________________

        //<-- Num. of Levels Completed -->//
        public int CompleteLevelNum(string id)
        {
            if (id == null) { id = CurrentUser.Id; }
            List<string> MD = db.Diploma.Where(a => a.UserId == id).Select(a => a.UserId).ToList();
            int x = MD.Count();
            return x;
        }
        //_______________________________________________________________________________________

        //<-- Click --> [ User-Level ] -->//
        public bool ClickUserLevel(string Levelid)
        {
            Levels L = db.Levels.FirstOrDefault(a => a.Id == Levelid);
            if (L == null) { return false; }
            else { if (L.IsFree) { return true; } }

            Users_Levels UL = db.Users_Levels.FirstOrDefault(a => a.User_Id == CurrentUser.Id && a.Level_Id == Levelid);
            if (UL == null) { return false; } else { return true; }
        }
        //_______________________________________________________________________________________

        //<-- Get User friends -->//
        public List<Users> GetUserfriends()
        {
            List<string> friend = db.Friends.Where(a => a.UserID1 == CurrentUser.Id).Select(a => a.UserID2).ToList();
            List<string> friend2 = db.Friends.Where(a => a.UserID2 == CurrentUser.Id).Select(a => a.UserID1).ToList();
            friend.AddRange(friend2);
            List<Users> fr = new List<Users>();
            foreach (string id in friend)
            {
                Users u = db.Users.FirstOrDefault(a => a.Id == id);
                if (u != null)
                {
                    fr.Add(u);
                }
            }
            return fr;
        }
        //_______________________________________________________________________________________

        //<-- Current User Definition -->//
        public Users CurrentUser
        {
            get
            {
                if (Session["User"] == null)
                {
                    return new Users();
                }
                return (Users)Session["User"];
            }
            set
            {
                Session["User"] = value;
            }
        }
        //_______________________________________________________________________________________

        //<-- Get Random -->//
        public static string GenerateRandom(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        
        //================================================================================================================
        //public ActionResult English()
        //{
        //    CurrentUser.lang = "en-US";
        //    return Redirect(Request.UrlReferrer.ToString());
        //}
        //public ActionResult Arabic()
        //{
        //    CurrentUser.lang = "ar-EG";
        //    return Redirect(Request.UrlReferrer.ToString());
        //}
        //================================================================================================================
    }
}