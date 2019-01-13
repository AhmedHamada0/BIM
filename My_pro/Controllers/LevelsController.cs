using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using My_pro.Model;

using System.IO;

namespace My_pro.Controllers
{
    public class LevelsController : BaseController
    {
        //################################################################################################################
        public static int NumOfCurrentChapter = 0;
        //################################################################################################################

        //<-- View Courses for Admins -->   { - Done - }
        [Authorize(Roles = "Admin")]
        public ActionResult ViewCoursesAdmin()
        {
            try
            {
                List<Levels> AllLevelsForAdmin = db.Levels.ToList();
                AllLevelsForAdmin.OrderByDescending(a => a.Date);
                ViewData["AllLevelForAdmin"] = AllLevelsForAdmin.ToList();
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Add New Level -->//   { - Done - }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddLevel(string CName, string CAbout, HttpPostedFileBase file)
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in L)
                {
                    if (o.Id == ("RLFDCVMX_EQBJSZ_" + Count + "_Z99T6__VCM")) { Count = Count + L.Count(); }
                }
                Levels NewL = new Levels
                {
                    Id = "RLFDCVMX_EQBJSZ_" + Count + "_Z99T6__VCM",
                    Name = CName,
                    Pic = "",
                    About = CAbout,
                    Date = DateTime.Now,
                    IsFree = true
                };
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string _IMGName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/img/Level/Levelimgs/"), _IMGName);
                            file.SaveAs(_path);
                            _path = "../../../img/Level/Levelimgs/" + _IMGName;
                            NewL.Pic = _path;
                            db.Levels.Add(NewL);
                            db.SaveChanges();
                        }
                        ViewBag.Message = "File Uploaded Successfully!";
                        return RedirectToAction("ViewCoursesAdmin", "Levels");
                    }
                }
                catch
                {
                    ViewBag.Message = "File upload failed!";
                }
                return RedirectToAction("Error", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Edit Level -->//
        [Authorize(Roles = "Admin")]
        public ActionResult EditLevel(string Level_id)
        {
            if (Level_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Levels level = db.Levels.Find(Level_id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditLevel([Bind(Include = "Id,Name,Pic,Date,About")] Levels level, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Levels b = db.Levels.FirstOrDefault(a => a.Id == level.Id);
                    b.Name = level.Name;
                    b.About = level.About;
                    if (file != null && file.ContentLength > 0)
                    {
                        string _IMGName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/images/Levelimgs/"), _IMGName);
                        file.SaveAs(_path);
                        _path = "../../../images/Levelimgs/" + _IMGName;
                        b.Pic = _path;
                        db.SaveChanges();
                        db.SaveChanges();

                    }
                    ViewBag.Message = "File Uploaded Successfully!";
                    return RedirectToAction("ViewCoursesAdmin", "Levels");
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!";
                return RedirectToAction("Error", "Home");
            }
            return View(level);
        }
        //_______________________________________________________________________________________

        //<-- Delete Level -->//   { - Done - }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCourse(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Levels L = db.Levels.Find(id);
                db.Levels.Remove(L);
                db.SaveChanges();
                return Json(new { Url = Url.Action("ViewCoursesAdmin", "Levels") });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Buy Course -->//   { - Done - }
        public ActionResult BuyCourse()
        {
            return View();
        }
        //_______________________________________________________________________________________

        //<-- Question -->//   { - Done - }
        [Authorize(Roles = "Admin")]
        public ActionResult Question()
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                return View(db.QExams.ToList());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddQuestion(string TLevelId, string TQuestion, string Ts1, string Ts2, string Ts3, string Ts4, string Ts, int? TChapterNum, string TChapter, string TFinal, string TMoreQ)
        {
            try
            {
                bool F = false, C = false, M = false;
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                List<QExams> LQ = db.QExams.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in LQ)
                {
                    if (o.Id == ("OWEfdsX_ECD6Z_" + Count + "_MNN22__QvDVQ")) { Count = Count + LQ.Count(); }
                }
                if (TFinal == "true") { F = true; } else { F = false; }
                if (TChapter == "true") { C = true; } else { C = false; }
                if (TMoreQ == "true") { M = true; } else { M = false; }
                Levels QLevel = db.Levels.FirstOrDefault(a => a.Id == TLevelId);
                QExams Q = new QExams
                {
                    Id = "OWEfdsX_ECD6Z_" + Count + "_MNN22__QvDVQ",
                    LevelId = TLevelId,
                    LevelName = QLevel.Name,
                    Question = TQuestion,
                    S1 = Ts1,
                    S2 = Ts2,
                    S3 = Ts3,
                    S4 = Ts4,
                    Final = F,
                    MoreQ = M,
                    Chapter = C,
                };
                if (TChapterNum > 0 && C == true)
                {
                    Q.NumChapter = Convert.ToInt16(TChapterNum);
                }
                if (Ts == "Sol-1") { Q.S = Ts1; }
                else if (Ts == "Sol-2") { Q.S = Ts2; }
                else if (Ts == "Sol-3") { Q.S = Ts3; }
                else if (Ts == "Sol-4") { Q.S = Ts4; }

                db.QExams.Add(Q);
                db.SaveChanges();
                return RedirectToAction("Question", "Levels");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DelQuestion(string id)
        {
            try
            {
                QExams q = db.QExams.FirstOrDefault(a => a.Id == id);
                db.QExams.Remove(q);
                db.SaveChanges();

                return RedirectToAction("Question", "Levels");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }
        //_______________________________________________________________________________________

        //<-- Add New Vedio -->//   { - Done - }
        [Authorize(Roles = "Admin")]
        public ActionResult Lectures()
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                return View(db.Videos.ToList());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddLecture(string TName, string TURL, string LevelId)
        {
            try
            {
                List<Videos> V = db.Videos.ToList();
                List<Videos> Vi = db.Videos.Where(a => a.Level_id == LevelId).ToList();
                List<Chapters> ch = db.Chapters.Where(a => a.Level_id == LevelId).ToList();
                Levels l = db.Levels.FirstOrDefault(a => a.Id == LevelId);
                int Co = 0;
                Co = Vi.Count + ch.Count + 1;
                string Count = GenerateRandom(6);
                foreach (var o in V)
                {
                    if (o.Id == ("CfHNvFX_EE5QZ_" + Count + "_PAZ66__QvV5M")) { Count = Count + V.Count(); }
                }
                Videos NewVideo = new Videos
                {
                    Id = "CfHNvFX_EE5QZ_" + Count + "_PAZ66__QvV5M",
                    Lectrue_Name = TName,
                    Date = DateTime.Now,
                    Count = Co,
                    Level_id = LevelId,
                    Level_Name = l.Name,
                    URL = TURL

                };
                db.Videos.Add(NewVideo);
                db.SaveChanges();
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                return RedirectToAction("Lectures", "Levels");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddChannel(string CName, string postData, string CevelId)
        {
            try
            {
                string[] lines = postData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int o = 0; o < lines.Count(); o++)
                {
                    lines[o].Replace(" ", String.Empty);
                    if (lines[o].Contains("https://www.youtube.com/watch?v="))
                    {
                        lines[o] = lines[o].Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                    }
                }
                List<string> Links = new List<string>(lines);
                int x = 1;
                List<Videos> Channel = new List<Videos>();

                List<Videos> Vi = db.Videos.Where(a => a.Level_id == CevelId).ToList();
                List<Chapters> ch = db.Chapters.Where(a => a.Level_id == CevelId).ToList();
                Levels l = db.Levels.FirstOrDefault(a => a.Id == CevelId);
                int IntVI = Vi.Count(), Intch = ch.Count();
                foreach (var item in Links)
                {
                    if (item.Contains("https://www.youtube.com/embed/"))
                    {
                        List<Videos> V = db.Videos.ToList();
                        int Co = 0;
                        Co = IntVI + Intch + 1;
                        string Count = GenerateRandom(6);
                        foreach (var o in V)
                        {
                            if (o.Id == ("CfHNvFX_EE5QZ_" + Count + "_PAZ66__QvV5M")) { Count = Count + V.Count(); }
                        }
                        Videos NewVideo = new Videos
                        {
                            Id = "CfHNvFX_EE5QZ_" + Count + "_PAZ66__QvV5M",
                            Lectrue_Name = CName + x,
                            Date = DateTime.Now,
                            Count = Co,
                            Level_id = CevelId,
                            Level_Name = l.Name,
                            URL = item
                        };
                        Channel.Add(NewVideo);
                        x++; IntVI++; Intch++;
                    }
                    else
                    {
                        Links.Remove(item);
                    }
                }
                db.Videos.AddRange(Channel);
                db.SaveChanges();
            }
            catch {
                return RedirectToAction("Login", new { error = "Error: Connection failed & Session has ended, please login again" });
            }
            return RedirectToAction("Lectures", "Levels");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DelLecture(string id)
        {
            try
            {
                if (id != null)
                {
                    Videos v = db.Videos.FirstOrDefault(a => a.Id == id);
                    db.Videos.Remove(v);
                    db.SaveChanges();
                    return RedirectToAction("Lectures", "Levels");
                }
                return RedirectToAction("Error", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Chapters -->//   { - Done - }
        [Authorize(Roles = "Admin")]
        public ActionResult Chapter()
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                return View(db.Chapters.ToList());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult NewChapter(string TName, string LevelId)
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                List<Chapters> LCh = db.Chapters.ToList();
                List<Videos> Vi = db.Videos.Where(a => a.Level_id == LevelId).ToList();
                List<Chapters> ch = db.Chapters.Where(a => a.Level_id == LevelId).ToList();
                Levels v = db.Levels.FirstOrDefault(a => a.Id == LevelId);
                int Co = Vi.Count + ch.Count + 1;
                string Count = GenerateRandom(6);
                foreach (var o in LCh)
                {
                    if (o.Id == ("RoQRMMX_EE5QZ_" + Count + "_ZQW66__BSM")) { Count = Count + LCh.Count(); }
                }
                Chapters NewCh = new Chapters
                {
                    Id = "RoQRMMX_EE5QZ_" + Count + "_ZQW66__BSM",
                    Name = TName,
                    Level_id = LevelId,
                    Count = Co,
                    Level_Name = v.Name,
                    Date = DateTime.Now
                };
                db.Chapters.Add(NewCh);
                db.SaveChanges();
                return RedirectToAction("Chapter", "Levels");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DelChapter(string id)
        {
            try
            {
                Chapters ch = db.Chapters.FirstOrDefault(a => a.Id == id);
                db.Chapters.Remove(ch);
                db.SaveChanges();
                return RedirectToAction("Chapter", "Levels");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- View Levels for Users -->//   { - Done - }
        public ActionResult LevelsView()     //For Users
        {
            try
            {
                ViewData["AllUserLevel"] = MyLevels();
                ViewData["UnUserLevel"] = Un_User_Levels(CurrentUser.Id);
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Join Level -->// { - Done - }
        public ActionResult LevelJoin()
        {
            try
            {
                string id = Session["LID"].ToString();
                if (id == null || id == "") { return RedirectToAction("LevelsView", "Levels"); }
                if (ClickUserLevel(id) || IsAdmin(id))
                {
                    Levels L = db.Levels.FirstOrDefault(a => a.Id == id);
                    Session["LID"] = L.Id;
                    Diploma D = db.Diploma.FirstOrDefault(a => a.UserId == CurrentUser.Id && a.Level == id);
                    if (D == null) { Session["Diploma"] = "false"; } else { Session["Diploma"] = D.Id; }
                    return View(L);
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult LevelJoin(string id) // ~~ Level-ID ~ Level-Object ~~ //
        {
            try
            {
                Session["LID"] = id;
                if (ClickUserLevel(id) || IsAdmin(id))
                {
                    Levels L = db.Levels.FirstOrDefault(a => a.Id == id);
                    Session["LID"] = L.Id;
                    Diploma D = db.Diploma.FirstOrDefault(a => a.UserId == CurrentUser.Id && a.Level == id);
                    if (D == null) { Session["Diploma"] = "false"; } else { Session["Diploma"] = D.Id; }
                    return View(L);
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Chapter-Exam & Videos -->//    { - Done - }
        public ActionResult CourseList()
        {
            try
            {
                string levelId = Session["LID"].ToString();
                List<Chapters> Ch = db.Chapters.Where(a => a.Level_id == levelId).ToList();
                List<Videos> Vi = db.Videos.Where(a => a.Level_id == levelId).ToList();
                ViewData["Chapters"] = Ch.OrderBy(x => x.Date).ToList();
                ViewData["Videos"] = Vi.OrderBy(x => x.Date).ToList();
                Levels L = db.Levels.FirstOrDefault(a => a.Id == levelId);
                ViewData["Level"] = L;
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________ 

        //<-- Chapter Quiz -->//     { - Done - }
        public static int Chz = 0, Ch = 0;
        public ActionResult ChQ(int id)
        {
            try
            {
                NumOfCurrentChapter = id;
                if (Ch != id) { Ch = id; Chz = 0; }
                string Level_Id = Session["LID"].ToString();
                List<QExams> Q = db.QExams.Where(a => a.LevelId == Level_Id && a.NumChapter == id).ToList();
                if (Chz < Q.Count())
                {
                    QExams E = Q.ElementAt(Chz);
                    Chz++;
                    ViewData["chapterQ"] = id;
                    ViewData["CountQ"] = Chz;
                    return View(E);
                }
                else
                {
                    Chz = 0;
                    Ch = 0;
                    return RedirectToAction("FinshQ", "Levels");
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult FinshQ()
        {
            return View();
        }
        //_______________________________________________________________________________________

        //<-- Saving Chapter Exam -->//     { - Done - }
        [HttpPost]
        public ActionResult SaveChQ()
        {
            try
            {
                List<Levels> L = db.Levels.ToList();
                ViewData["AllLevels"] = L;
                string Level_Id = Session["LID"].ToString();
                Users_Chapters exist = db.Users_Chapters.FirstOrDefault(a => a.User_Id == CurrentUser.Id && a.Level == Level_Id && a.Chapter == NumOfCurrentChapter);
                if (exist != null)
                {

                    return null;
                }
                List<Users_Chapters> UCh = db.Users_Chapters.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in UCh)
                {
                    if (o.Id == ("WEwUFX_TT2QZ_" + Count + "_ZCv00__0q0CYM")) { Count = Count + UCh.Count(); }
                }
                Users_Chapters Ch = new Users_Chapters
                {
                    Id = "WEwUFX_TT2QZ_" + Count + "_ZCv00__0q0CYM",
                    User_Id = CurrentUser.Id,
                    Date = DateTime.Now,
                    Level = Level_Id,
                    Chapter = NumOfCurrentChapter,
                };
                db.Users_Chapters.Add(Ch);
                db.SaveChanges();
                return null;
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Get Final Qustions -->//     { - Done - }
        public ActionResult Final()
        {
            try
            {
                string LevelId = Session["LID"].ToString();
                List<Users_Finals> Fexist = db.Users_Finals.Where(a => a.User_Id == CurrentUser.Id && a.Level_Id == LevelId).ToList();
                if (Fexist.Count() >= 2)
                {
                    return RedirectToAction("LevelJoin", "Levels", new { id = LevelId });
                }
                bool boo = false;
                foreach (Users_Finals o in Fexist)
                {
                    if ((o.Date.AddHours(2)) > (DateTime.Now))
                    {
                        Session["Timeout"] = o.Date; boo = true;
                        break;
                    }
                }
                if (boo == false)
                {
                    List<Users_Finals> UF = db.Users_Finals.ToList();
                    string Count = GenerateRandom(6);
                    foreach (var o in UF)
                    {
                        if (o.Id == ("OIVUFX_TGGJZ_" + Count + "_ZC99H__35HY6M")) { Count = Count + UF.Count(); }
                    }
                    Users_Finals F = new Users_Finals
                    {
                        Id = "OIVUFX_TGGJZ_" + Count + "_ZC99H__35HY6M",
                        User_Id = CurrentUser.Id,
                        Level_Id = LevelId,
                        Date = DateTime.Now,
                        Marks = 0,
                    };
                    db.Users_Finals.Add(F);
                    db.SaveChanges();
                    Session["Timeout"] = F.Date;
                }
                List<QExams> Q = db.QExams.Where(a => a.LevelId == LevelId && a.Final == true).ToList();
                return View(Q);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Saving Final Exam -->//   { - Done - }
        [HttpPost]
        public ActionResult SaveFinal(int M)
        {
            try
            {
                DateTime D = Convert.ToDateTime(Session["Timeout"].ToString());
                if ((D.AddHours(2).AddMinutes(5)) >= (DateTime.Now))
                {
                    string LevelId = Session["LID"].ToString();
                    DateTime timeo = Convert.ToDateTime(Session["Timeout"]);
                    List<Levels> L = db.Levels.ToList();
                    ViewData["AllLevels"] = L;
                    List<Users_Finals> Fexist = db.Users_Finals.Where(a => a.User_Id == CurrentUser.Id && a.Level_Id == LevelId).ToList();
                    if (Fexist.Count() > 2)
                    {
                        return Redirect("../../Levels/LevelJoin/" + LevelId);
                    }
                    Users_Finals s = db.Users_Finals.FirstOrDefault(a => a.User_Id == CurrentUser.Id && a.Level_Id == LevelId && a.Date == timeo);
                    s.Marks = M;
                    db.SaveChanges();
                    string Diploma_id = CreateDiploma(LevelId, M);
                    Session["Diploma"] = Diploma_id;
                    return new JsonResult { Data = "../../../Levels/Diploma/" + Diploma_id , JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return null;
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Exercises -->//     { - Done - }
        public static int Exz = 0;
        public ActionResult Exercises()
        {
            try
            {
                string Level_Id = Session["LID"].ToString();
                List<QExams> QL = db.QExams.Where(a => a.LevelId == Level_Id && a.MoreQ == true).ToList();
                if (Exz < QL.Count())
                {
                    QExams E = QL.ElementAt(Exz);
                    Exz++;
                    ViewData["CountQ"] = Exz;
                    return View(E);
                }
                else
                {
                    Exz = 0;
                    return RedirectToAction("FinshEx", "Levels");
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult FinshEx()
        {
            return View();
        }
        //_______________________________________________________________________________________

        //<-- Create New Diploma -->//     { - Done - }
        public string CreateDiploma(string Level_id, int FinalMark)
        {

            Diploma exist = db.Diploma.FirstOrDefault(a => a.Level == Level_id && a.UserId == CurrentUser.Id);
            if (exist != null)
            {
                exist.Marks = FinalMark;
                db.SaveChanges();
                return exist.Id;
            }
            else
            {
                List<Diploma> ListofDiploma = db.Diploma.ToList();
                string Count = GenerateRandom(6);
                foreach (var o in ListofDiploma)
                {
                    if (o.Id == ("OREfgtX_ECU9Z_" + Count + "_ZWW52__QoROP")) { Count = Count + ListofDiploma.Count(); }
                }
                Diploma NewD = new Model.Diploma
                {
                    Id = "OREfgtX_ECU9Z_" + Count + "_ZWW52__QoROP",
                    UserId = CurrentUser.Id,
                    UserName = CurrentUser.UserName,
                    Level = Level_id,
                    Date = DateTime.Now,
                    Marks = FinalMark
                };
                db.Diploma.Add(NewD);
                db.SaveChanges();

                return NewD.Id;
            }
        }
        //_______________________________________________________________________________________

        //<-- View Diploma -->//     { - Done - }
        public ActionResult Diploma(string id)
        {
            try
            {
                Diploma D = db.Diploma.FirstOrDefault(a => a.Id == id);
                Levels L = db.Levels.FirstOrDefault(a => a.Id == D.Level);
                if (D == null || L == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                db.SaveChanges();
                ViewData["Level_Dip"] = L;
                return View(D);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- View List of Diplomas-User -->//     { - Done - }
        public ActionResult UserDiploma()
        {
            List<Diploma> UD = db.Diploma.Where(a => a.UserId == CurrentUser.Id).ToList();
            return View(UD);
        }
        //_______________________________________________________________________________________

        //<-- View List of Level-Diplomas For [ Admin ] -->//      { - Done - }
        [Authorize(Roles = "Admin")]
        public ActionResult LevelDiploma(string id)
        {
            List<Diploma> Level_Dips = db.Diploma.Where(a => a.Level == id).ToList();
            return View(Level_Dips);
        }
        //================================================================================================================
    }
}