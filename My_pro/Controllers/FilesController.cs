using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_pro.Model;
using System.IO;

namespace My_pro.Controllers
{
    public class FilesController : BaseController
    {
        //================================================================================================================

        //<-- Upload Files -->//
        public ActionResult Upload_File()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload_File(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    List<UploadFile> f = db.UploadFile.ToList();
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadFiles/"), _FileName);

                    while (System.IO.File.Exists(_path))
                    {
                        string TCount = GenerateRandom(3) + "-";
                        _FileName = TCount +_FileName;
                        _path = Path.Combine(Server.MapPath("~/UploadFiles/"), _FileName);
                    }

                    int contentLength = file.ContentLength;
                    string contentType = file.ContentType;
                    if (contentType.Contains("text/html") || _FileName.Contains("c99") || _FileName.Contains("c100"))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    file.SaveAs(_path);
                    List<UploadFile> ListofFiles = db.UploadFile.ToList();
                    string Count = GenerateRandom(6);
                    foreach (var o in ListofFiles)
                    {
                        if (o.Id == ("CQGFX_EE2BZ" + Count + "ZAv29__AAC00X")) { Count = Count + ListofFiles.Count(); }
                    }
                    UploadFile UF = new Model.UploadFile
                    {
                        Id = "CQGFX_EE2BZ" + Count + "ZAv29__AAC00X",
                        Name = _FileName,
                        User_Id = CurrentUser.Id,
                        URL = "../../../UploadFiles/" + _FileName,
                        Time = DateTime.Now,
                        Size = contentLength,
                        Type = contentType
                    };
                    if (UF.Type == "image/jpeg" || UF.Type == "image/GIF" || UF.Type == "image/bmp" || UF.Type == "image/x-icon" || UF.Type == "image/png" || UF.Type == "image/svg+xml" || UF.Type == "image/tiff")
                    {
                        UF.Img = UF.URL;
                    }
                    else if (UF.Type == "application/msword")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/doc.png";
                    }
                    else if (UF.Type == "application/vnd.ms-powerpoint" || UF.Type == "application/vnd.openxmlformats" || UF.Type == "officedocument.presentationml.presentation")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/ppt.png";
                    }
                    else if (UF.Type == "application/vnd.ms-excel" || UF.Type == "application/vnd.openxmlformats" || UF.Type == "officedocument.spreadsheetml.sheet")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/xls.png";
                    }
                    else if (UF.Type == "application/pdf")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/pdf.png";
                    }
                    else if (UF.Type == "application/zip" || UF.Type == "application/x-rar-compressed" || UF.Type == "application/x-7z-compressed")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/zip.png";
                    }
                    else if (UF.Type == "text/plain")
                    {
                        UF.Img = "../../../img/Default/Icon/Files/txt.png";
                    }
                    else
                    {
                        UF.Img = "../../../img/Default/Icon/Files/file.png";
                    }
                    db.UploadFile.Add(UF);
                    db.SaveChanges();
                    ViewBag.Message = "File Uploaded Successfully!";
                }
                return RedirectToAction("My_Files", "Files");
            }
            catch
            {
                ViewBag.Message = "File upload failed!";
                return View();
            }
        }
        //_______________________________________________________________________________________

        //<-- User Files -->//
        public ActionResult My_Files()
        {
            List<UploadFile> Userfile = db.UploadFile.Where(a => a.User_Id == CurrentUser.Id).ToList();
            return View(Userfile);
        }

        [HttpPost]
        public ActionResult My_Files(string TxSearch)
        {
            List<UploadFile> LU = db.UploadFile.Where(a => a.Name.Contains(TxSearch) && a.User_Id == CurrentUser.Id).ToList();
            return View(LU);
        }
        //_______________________________________________________________________________________

        //<-- All Files -->//
        public ActionResult All_Files()
        {
            List<UploadFile> FF = db.UploadFile.Take(100).ToList();
            return View(FF);
        }

        [HttpPost]
        public ActionResult All_Files(string TxSearch)
        {
            List<UploadFile> f = db.UploadFile.Where(a => a.Name.Contains(TxSearch)).ToList();
            return View(f);
        }
        //_______________________________________________________________________________________

        //<-- Delete File -->//
        public ActionResult Delete(string id)
        {
            try
            {
                UploadFile f = db.UploadFile.FirstOrDefault(a => a.Id == id);
                if (f != null && f.User_Id == CurrentUser.Id)
                {
                    string _path = Path.Combine(Server.MapPath("~/UploadFiles/"), f.Name);
                    System.IO.File.Delete(_path);
                    db.UploadFile.Remove(f);
                    db.SaveChanges();
                }
                return RedirectToAction("My_Files", "Files");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //================================================================================================================
    }
}