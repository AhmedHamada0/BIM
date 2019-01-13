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
using System.IO;
using System.Data.Entity.Validation;

namespace My_pro.Controllers
{
    public class AdminController : BaseController
    {
        //================================================================================================================

        //<-- Admin Control -->//
        [Authorize(Roles = "Admin")]
        public ActionResult Admin_Control()
        {
            List<Blogs> BlogsList = db.Blogs.Take(6).ToList();
            return View(BlogsList);
        }
        //_______________________________________________________________________________________

        //<-- Get All User -->//          ( Add/Del user to/from Level )
        [Authorize(Roles = "Admin")]
        public ActionResult AllUsersList()
        {
            return View(db.Users.ToList());
        }
        //_______________________________________________________________________________________

        //<-- Add User to Level -->//   { View }
        [Authorize(Roles = "Admin")]
        public ActionResult AddUserToLevels_Control(string id)
        {
            Session["LevelIdControl"] = id;
            Levels L = db.Levels.FirstOrDefault(a => a.Id == id);
            if (L == null) { return RedirectToAction("Error", "Home"); }
            List<string> UL = db.Users_Levels.Where(a => a.Level_Id == id).Select(a => a.User_Id).ToList();
            List<Users> UnjoinUsers = db.Users.ToList();
            foreach (var oo in UL)
            {
                foreach (var o in UnjoinUsers)
                {
                    if(oo == o.Id) { UnjoinUsers.Remove(o); break; }
                }
            }
            return View(UnjoinUsers.ToList());
        }
        //_______________________________________________________________________________________

        //<-- Delete User from Level -->//   { View }
        [Authorize(Roles = "Admin")]
        public ActionResult DelUserfromLevels_Control(string id)
        {
            Session["LevelIdControl"] = id;
            Levels L = db.Levels.FirstOrDefault(a => a.Id == id);
            if (L == null) { return RedirectToAction("Error", "Home"); }
            List<string> UL = db.Users_Levels.Where(a => a.Level_Id == id).Select(a => a.User_Id).ToList();
            List<Users> joinUsers = new List<Users>();
            foreach (var o in UL)
            {
                Users searchuser = db.Users.FirstOrDefault(a => a.Id == o);
                if(searchuser != null)
                {
                    joinUsers.Add(searchuser);
                }
            }
            return View(joinUsers.ToList());
        }
        //_______________________________________________________________________________________

        //<-- btn-Action: Add User to Level -->//
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUserToLevel(string LevelId, string UserId)
        {
            if (LevelId == null || UserId == null) { return RedirectToAction("Error", "Home"); }

            Users_Levels AUL = new Users_Levels
            {
                Level_Id = LevelId,
                User_Id = UserId,
                Time = DateTime.Now,
                Marks = 0
            };
            db.Users_Levels.Add(AUL);
            db.SaveChanges();
            return RedirectToAction("AddUserToLevels_Control", "Admin", new { id = LevelId });
        }
        //_______________________________________________________________________________________

        //<-- btn-Action: Delete User from Level -->//
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DelUserFromLevel(string LevelId, string UserId)
        {
            if (LevelId == null || UserId == null) { return RedirectToAction("Error", "Home"); }

            Users_Levels AUL = db.Users_Levels.FirstOrDefault(a => a.Level_Id == LevelId && a.User_Id == UserId);
            db.Users_Levels.Remove(AUL);
            db.SaveChanges();
            return RedirectToAction("DelUserfromLevels_Control", "Admin", new { id = LevelId });
        }
        //================================================================================================================

        //public ActionResult Users_Level(string id)
        //{
        //    List<string> UL = db.Users_Levels.Where(a => a.Level_Id == id).Select(a => a.User_Id).ToList();
        //    List<Users> user = new List<Users>();
        //    foreach(var o in UL)
        //    {
        //        Users u = db.Users.FirstOrDefault(a => a.Id == o);
        //        if(u != null)
        //        {
        //            user.Add(u);
        //        }
        //    }
        //    ViewData["Users_Levelid"] = id;
        //    return View(user);
        //}
        //================================================================================================================
    }
}