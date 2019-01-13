using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using My_pro.Model;
using My_pro.Model.Entites;

namespace My_pro.Controllers
{
    public class BlogController : BaseController
    {
        //================================================================================================================

        //<-- Blogs List -->// { - Admin - }
        [Authorize(Roles = "Admin")]
        public ActionResult BlogsList()
        {
            List<Blogs> Bs = db.Blogs.ToList();
            Bs.OrderByDescending(a => a.Date);
            return View(Bs);
        }
        //_______________________________________________________________________________________

        //<-- Search Blog -->//
        public ActionResult SearchBlog()
        {
            return View(db.Blogs.Take(100).ToList());
        }
        [HttpPost]
        public ActionResult SearchBlog(string TxSearch)
        {
            try
            {
                if (TxSearch == "" || TxSearch == null) { return View(db.Blogs.ToList()); }
                return View(db.Blogs.Where(a => a.UserName.Contains(TxSearch) || a.Toptext.Contains(TxSearch) || a.Buttomtext.Contains(TxSearch)).ToList());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- View Blog -->//
        public ActionResult ViewB(string id)
        {
            try
            {
                Blogs B = db.Blogs.FirstOrDefault(a => a.Id == id);
                if (B == null) { return RedirectToAction("Error", "Home"); }
                Users Bloger = db.Users.FirstOrDefault(a => a.Id == B.UserId);
                List<Blog_Comments> CommentsList = db.Blog_Comments.Where(a => a.Blogid == id).ToList();
                ViewData["BlogV"] = B;
                ViewData["BlogerV"] = Bloger;
                ViewData["U"] = CurrentUser;
                ViewData["BList"] = CommentsList;
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Add New Comment -->//
        [HttpPost]
        public ActionResult AddComment(string Comment_TX, string B_id)
        {
            try
            {
                if (Comment_TX == null) { return RedirectToAction("Error", "Home"); }
                List<Blog_Comments> Bloglist = db.Blog_Comments.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in Bloglist)
                {
                    if (o.Id == ("CQR6DX_ECAV" + Count + "ZBN56__QZZ67M")) { Count = Count + Bloglist.Count(); }
                }
                Blog_Comments BC = new Blog_Comments
                {
                    Id = "CQR6DX_ECAV" + Count + "ZBN56__QZZ67M",
                    Blogid = B_id,
                    User_id_Co = CurrentUser.Id,
                    User_img = CurrentUser.Picture,
                    User_Name = CurrentUser.UserName,
                    Comment = Comment_TX,
                    Date = DateTime.Now
                };
                db.Blog_Comments.Add(BC);
                db.SaveChanges();

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult DelComment(string id)
        {
            try
            {
                Blog_Comments BC = db.Blog_Comments.FirstOrDefault(a => a.Id == id);
                if (BC != null)
                {
                    if (IsAdmin(CurrentUser.Id) == true || BC.User_id_Co == CurrentUser.Id) { db.Blog_Comments.Remove(BC); db.SaveChanges(); }
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Add Blog -->//
        [Authorize(Roles = "Admin")]
        public ActionResult Add_Blog()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add_Blog(string TopTX, string imgvideo, string TURL, string Link1, string Link2, string Link3, string ButtomTX)
        {
            try
            {
                bool IV = false;
                if (imgvideo == "Video") { IV = false; }
                else { IV = true; }
                List<Blogs> L = db.Blogs.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in L)
                {
                    if (o.Id == ("CNEEX_EQQJKZ_" + Count + "_ZMMR6__V66H")) { Count = Count + L.Count(); }
                }
                Blogs NewBlog = new Blogs
                {
                    Id = "CNEEX_EQQJKZ_" + Count + "_ZMMR6__V66H",
                    Toptext = TopTX,
                    img = IV,
                    URL = TURL,
                    Link1 = Link1,
                    Link2 = Link2,
                    Link3 = Link3,
                    Date = DateTime.Now,
                    Buttomtext = ButtomTX,
                    UserId = CurrentUser.Id,
                    UserName = CurrentUser.UserName
                };
                db.Blogs.Add(NewBlog);
                db.SaveChanges();

                return RedirectToAction("BlogsList", "Blog");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Delete Blog -->//
        [Authorize(Roles = "Admin")]
        public ActionResult Del_Blog(string Id)
        {
            try
            {
                Blogs B = db.Blogs.FirstOrDefault(a => a.Id == Id);
                if (B == null && B.UserId != CurrentUser.Id)
                {
                    return RedirectToAction("Error", "Home");
                }
                db.Blogs.Remove(B);
                List<Blog_Comments> BComments = db.Blog_Comments.Where(a => a.Blogid == Id).ToList();
                db.Blog_Comments.RemoveRange(BComments);
                db.SaveChanges();
                return RedirectToAction("BlogsList", "Blog");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //================================================================================================================
    }
}