using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using My_pro.Model.Entites;
using My_pro.Core;
using System.Web.Security;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.IO;


namespace My_pro.Controllers
{
    public class HomeController : BaseController
    {
        //################################################################################################################
        public UserService _user = new UserService();
        public UserManager<Users> um => HttpContext.GetOwinContext().Get<UserManager<Users>>();
        public SignInManager<Users, string> SignInManager => HttpContext.GetOwinContext().Get<SignInManager<Users, string>>();
        IdentityRole role;
        IdentityUser I = new IdentityUser();
        UserContext uc;
        RoleStore<IdentityRole> rs;
        RoleManager<IdentityRole> rm;
        public HomeController()
        {
            uc = new UserContext();
            rs = new RoleStore<IdentityRole>(uc);
            rm = new RoleManager<IdentityRole>(rs);
        }
        //################################################################################################################

        //<-- Base -->//       
        public ActionResult Base() { return Redirect("http://bimarabia.com"); }
        //_______________________________________________________________________________________

        //<-- Main -->//       
        public ActionResult Main()
        {
            if(CurrentUser == null || Session["CUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //_______________________________________________________________________________________

        //<-- Sign-Up -->//  
        [HttpPost]
        public ActionResult Create(Users user, string Pass)
        {
            try
            {
                Pass = Pass.Trim();
                Users Testun = db.Users.FirstOrDefault(a => a.UserName == user.UserName);
                if(Testun != null)
                {
                    ModelState.AddModelError("Invalid", "This User-Name already exists");
                    return RedirectToAction("Login");
                }
                user.Email = user.Email.Trim();
                user.UserName = user.UserName.Replace(" ", string.Empty);
                user.PhoneNumber = user.PhoneNumber.Replace(" ", string.Empty);
                if (user != null)
                {
                    Users u = db.Users.FirstOrDefault(a => a.Email == user.Email);
                    if (u == null)
                    {
                        user.About = "Hello, I am a new member.";
                        user.Picture = "../../../Img/Default/Profile/avatar1.png";
                        user.Facebook_Link = "https://www.facebook.com/your-Account/";
                        user.Linkedin_Link = "https://linkedin.com/your-Account/";
                        user.Link = "https://www.example.CV.com/Your-CV/";
                        user.XP = 0;
                        //<-- Set as a user -->//
                        if (rm.RoleExists("User") == false)
                        {
                            role = new IdentityRole();
                            role.Name = "User";
                            rs = new RoleStore<IdentityRole>(new UserContext());
                            rm = new RoleManager<IdentityRole>(rs);
                            rm.Create(role);
                        }
                        um.Create(user, Pass);
                        um.AddToRole(user.Id, "User");
                        db.SaveChanges();
                        return Login(user.Email, Pass);
                    }
                    else
                    {
                        return RedirectToAction("Login" , new { error = "This email already exists" });
                    }
                }
                return RedirectToAction("Login", new { error = "Error passing data, please try again" });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- Main -->//       
        public ActionResult Login(string error)
        {
            if (error != null && error != "")
            {
                Session["view"] = "true";
                ModelState.AddModelError("Invalid", error);
                return View();
            }
            Session["view"] = "false";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string password)
        {
            try
            {
                Email = Email.Trim();
                password = password.Trim();
                Users U = db.Users.FirstOrDefault(a => a.Email == Email);
                if (U == null)
                {
                    return RedirectToAction("Login", new { error = "login failed, E-mail or password is invalid"});
                }
                SignInStatus SIS = SignInManager.PasswordSignIn(U.UserName, password, false, false);
                switch (SIS)
                {
                    case SignInStatus.Failure:
                        return RedirectToAction("Login", new { error = "login failed, E-mail or password is invalid"});
                    case SignInStatus.LockedOut:
                        return RedirectToAction("Login", new { error = "login failed, E-mail or password is invalid"});
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("Login", new { error = "login failed, E-mail or password is invalid"});
                    case SignInStatus.Success:
                        Users user = UserContext.GetByName(U.Id);
                        CurrentUser = user;
                        Session["User"] = user;
                        Session["CUser"] = user;
                        Session["Pic"] = user.Picture;
                        List<IdentityUserRole> role = user.Roles.ToList();
                        string URole = "";
                        foreach (IdentityUserRole r in role)
                        {
                            URole = UserContext.GetRole(r.RoleId);
                        }
                        if (URole == null || URole == "" || URole == string.Empty)
                        {
                            URole = "User";
                        }
                        Session["URole"] = URole;
                        return RedirectToAction("Main", "Home");
                    default:
                        return RedirectToAction("Login", new { error = "login failed, E-mail or password is invalid"});
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        //<-- LogOut -->//
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        //_______________________________________________________________________________________

        //<-- Actions --> //
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        //_______________________________________________________________________________________

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        //_______________________________________________________________________________________

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,PhoneNumber,Email,FB,About,XP,country")] Users users)
        {
            if (ModelState.IsValid)
            {
                _user.Edit(users);
                return RedirectToAction("Main", "Home");
            }
            return View(users);
        }
        //_______________________________________________________________________________________

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                Users users = db.Users.Find(id);
                um.RemoveFromRole(id, "User");
                List<Friends> f = db.Friends.Where(a => a.UserID1 == id || a.UserID2 == id).ToList();
                db.Friends.RemoveRange(f);
                db.Users.Remove(users);
                db.SaveChanges();
                return RedirectToAction("Main", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //_______________________________________________________________________________________

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //_______________________________________________________________________________________

        //<-- Add_Admin -->//
        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin(Users user, string password, string confirm)
        {
            try
            {
                if (confirm != "2y4a2w5e2l2c4o1m") { return RedirectToAction("Error", "Home"); }
                password = password.Trim();
                user.Email = user.Email.Replace(" ", string.Empty);
                user.UserName = user.UserName.Replace(" ", string.Empty);
                user.PhoneNumber = user.PhoneNumber.Replace(" ", string.Empty);
                if (user != null)
                {
                    Users u = db.Users.FirstOrDefault(a => a.Email == user.Email);
                    if (u == null)
                    {
                        if (ModelState.IsValid)
                        {
                            user.About = "Hello, I am a new Admin.";
                            user.Picture = "../../../Img/Default/Profile/avatar2.png";
                            user.Facebook_Link = "https://www.facebook.com/your-Account/";
                            user.Linkedin_Link = "https://linkedin.com/your-Account/";
                            user.Link = "https://www.example.CV.com/Your-CV/";
                            user.XP = 0;
                            //<-- Set as a user -->//
                            if (rm.RoleExists("Admin") == false)
                            {
                                role = new IdentityRole();
                                role.Name = "Admin";
                                rs = new RoleStore<IdentityRole>(new UserContext());
                                rm = new RoleManager<IdentityRole>(rs);
                                rm.Create(role);
                            }
                            um.Create(user, password);
                            um.AddToRole(user.Id, "Admin");
                            db.SaveChanges();
                            return Login(user.Email, password);
                        }
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

        //<-- profile -->//
        public ActionResult TProfile(string id)
        {
            Session["User"] = CurrentUser;
            Session["CUser"] = CurrentUser;
            Session["Pic"] = CurrentUser.Picture;
            if (id == null || id == "")
            {
                CurrentUser = db.Users.FirstOrDefault(a => a.Id == CurrentUser.Id);
                ViewData["U"] = CurrentUser;
                if (IsAdmin(id)) { ViewData["ProfileRole"] = "Admin"; } else { ViewData["ProfileRole"] = "User"; }
                return View(CurrentUser);
            }
            Users u = db.Users.FirstOrDefault(a => a.Id == id);
            if (IsAdmin(id)) { ViewData["ProfileRole"] = "Admin"; } else { ViewData["ProfileRole"] = "User"; }
            ViewData["U"] = u;
            return View(u);
        }
        //_______________________________________________________________________________________

        //<-- Edit Profile -->//
        public ActionResult EditProfile(string error)
        {
            if(error != null && error != "")
            {
                ModelState.AddModelError("EditError", error);
            }
            else
            {
                ModelState.AddModelError("EditError", "This information about you that reflect your personality so please enter the correct information.");
            }
            CurrentUser = db.Users.FirstOrDefault(a => a.Id == CurrentUser.Id);
            ViewData["U"] = CurrentUser;
            Session["User"] = CurrentUser;
            Session["CUser"] = CurrentUser;
            Session["Pic"] = CurrentUser.Picture;
            return View(CurrentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ID,UserName,PhoneNumber,Email,About,Picture,Birthday,Facebook_Link,Linkedin_Link,Link,Gender,Country,XP")] Users users, HttpPostedFileBase file)
        {
            try
            {
                Users useredit = db.Users.FirstOrDefault(a => a.UserName == users.UserName && a.Id != CurrentUser.Id);
                if (useredit != null)
                {
                    return RedirectToAction("EditProfile" , new { error = "This Username already exists" });
                }
                Users userEedit = db.Users.FirstOrDefault(a => a.Email == users.Email && a.Id != CurrentUser.Id);
                if (userEedit != null)
                {
                    return RedirectToAction("EditProfile", new { error = "This E-mail already exists" });
                }
                users.Email = users.Email.Trim();
                users.UserName = users.UserName.Trim();
                users.PhoneNumber = users.PhoneNumber.Replace(" ", string.Empty);
                if (users.Email != null && users.UserName != null && users.PhoneNumber != null && users.About != null)
                {

                    if (ModelState.IsValid) { _user.Edit(users); }
                    users = db.Users.FirstOrDefault(a => a.Id == CurrentUser.Id);
                    if (file != null && file.ContentLength > 0)
                    {
                        string _IMGName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/img/Default/Profile/"), _IMGName);

                        while (System.IO.File.Exists(_path))
                        {
                            string Count = GenerateRandom(2) + "-";
                            _IMGName = Count + _IMGName;
                            _path = Path.Combine(Server.MapPath("~/img/Default/Profile/"), _IMGName);
                        }

                        file.SaveAs(_path);
                        _path = "../../../img/Default/Profile/" + _IMGName;
                        users.Picture = _path;
                        db.SaveChanges();
                        _user.Edit(users);
                        ViewBag.Message = "File Uploaded Successfully!";
                        db.SaveChanges();
                    }
                    return RedirectToAction("TProfile", "Home");
                }
                else
                {
                    string e = "Edit failed, Please fill out this field.";
                    return RedirectToAction("EditProfile", new { error = e });
                }
            }
            catch
            {
                string e = "Edit failed, Please try again!";
                return RedirectToAction("EditProfile", new { error = e });
            }
        }
        //_______________________________________________________________________________________

        //<-- Another Action -->//
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            if (CurrentUser == null || Session["CUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //================================================================================================================

        //#######################################################################################
        //#######################################################################################
        //private Uri RedirectUriF
        //{
        //    get
        //    {
        //        var uriBuilder = new UriBuilder(Request.Url);
        //        uriBuilder.Query = null;
        //        uriBuilder.Fragment = null;
        //        uriBuilder.Path = Url.Action("FacebookCallback");
        //        return uriBuilder.Uri;
        //    }
        //}
        //[AllowAnonymous]
        //public ActionResult Facebook()
        //{
        //    var fb = new FacebookClient();
        //    var loginUrl = fb.GetLoginUrl(new
        //    {
        //        client_id = "2164407013839118",
        //        client_secret = "d7d84dbc5d6d8ef3f67b9be7a2f876d1",
        //        redirect_uri = RedirectUriF.AbsoluteUri,
        //        response_type = "code",
        //        scope = "email",

        //        // Add other permissions as needed
        //    });

        //    return Redirect(loginUrl.AbsoluteUri);
        //}
        //public ActionResult FacebookCallback(string code)
        //{
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Post("oauth/access_token", new
        //    {
        //        client_id = "2164407013839118",
        //        client_secret = "d7d84dbc5d6d8ef3f67b9be7a2f876d1",
        //        redirect_uri = RedirectUriF.AbsoluteUri,
        //        code = code
        //    });

        //    var accessToken = result.access_token;

        //    // Store the access token in the session for farther use
        //    Session["AccessToken"] = accessToken;

        //    fb.AccessToken = accessToken;

        //    // Get the user's information
        //    dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
        //    string email = me.email;
        //    string firstname = me.first_name;
        //    string middlename = me.middle_name;
        //    string lastname = me.last_name;
        //    string phone = "Phone: Not Found";
        //    string ID = Convert.ToString(me.id);

        //    // Set the auth cookie
        //    FormsAuthentication.SetAuthCookie(email, false);

        //    Users FBUser = db.Users.FirstOrDefault(a => a.Email == email);
        //    string Pass = ID + "9923";
        //    if (FBUser == null)
        //    {
        //        List<Users> LU = db.Users.ToList();
        //        Users NewFBUser = new Users()
        //        {
        //            UserName = firstname + lastname + LU.Count,
        //            Email = email,
        //            PhoneNumber = phone
        //        };
        //        return Create(NewFBUser, Pass);
        //    }
        //    else
        //    {
        //        SignInStatus SIS = SignInManager.PasswordSignIn(FBUser.UserName, Pass, false, false);
        //        switch (SIS)
        //        {
        //            case SignInStatus.Success:
        //                return Login(FBUser.Email, Pass);
        //            default:
        //                string e = "login failed," + Environment.NewLine + "Your email already exists" + Environment.NewLine + "You must login by e-mail and password";
        //                return RedirectToAction("Login", new { error = e, Invalid = 2 });
        //        }
        //    }
        //}
        //#######################################################################################
        //#######################################################################################
        //public string Token(string Id, string type)
        //{
        //    string[] token = Decrypt(Id.Replace("@", "/").Replace("_", "+")).Split('@');

        //    string User_id = token[0];
        //    string User_pass = token[1];

        //    return ("Id: " + Id + "type: " + type);
        //}

        //public string ResetPassword(string UserName)
        //{
        //    Users A = db.Users.FirstOrDefault(a => a.UserName == UserName);
        //    if (A != null)
        //    {
        //        string s = A.Id + "@" + A.PasswordHash;
        //        string Enc = Encrypt(s);
        //        string url = "Http://" + Request.Url.Host + ":" + Request.Url.Port + "/Home/Token/1/" + (Enc).Replace("/", "@").Replace("+", "_");
        //        return url;
        //    }

        //    return "";
        //}
        //static readonly string PasswordHash = "P@@Sw0rd";
        //static readonly string SaltKey = "S@LT&KEY";
        //static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        //public static string Encrypt(string plainText)
        //{
        //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        //    byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        //    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        //    byte[] cipherTextBytes;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //        {
        //            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //            cryptoStream.FlushFinalBlock();
        //            cipherTextBytes = memoryStream.ToArray();
        //            cryptoStream.Close();
        //        }
        //        memoryStream.Close();
        //    }
        //    return Convert.ToBase64String(cipherTextBytes);
        //}
        //public static string Decrypt(string encryptedText)
        //{
        //    byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        //    byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        //    var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        //    var memoryStream = new MemoryStream(cipherTextBytes);
        //    var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        //    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //    memoryStream.Close();
        //    cryptoStream.Close();
        //    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        //}
        //================================================================================================================
    }
}


