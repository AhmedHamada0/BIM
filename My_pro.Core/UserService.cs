using Microsoft.AspNet.Identity.EntityFramework;
using My_pro.Model;
using My_pro.Model.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace My_pro.Core
{
    public class UserService
    {

        public static UserContext db = new UserContext();

        

        //###################################################################################################################################
        


































        // List_GetFriendRequest:
        //=======================
        public static List<Users> GetFriendRequest(Users u)
        {
            List<string> Friends = db.Friends.Where(a => a.user1.Id == u.Id && a.Accept == false).Select(a => a.user2.Id).ToList();
            List<string> Friends2 = db.Friends.Where(a => a.user2.Id == u.Id && a.Accept == false).Select(a => a.user1.Id).ToList();

            Friends.AddRange(Friends2);

            List<Users> friends = new List<Users>();
            foreach (string id in Friends)
            {
                friends.Add(db.Users.FirstOrDefault(a => a.Id == id));
            }
            return friends;
        }
        //################################################
        //List_GetFriends:
        //================
        public static List<Users> GetFriends(Users u)
        {
            List<string> MyFriends = db.Friends.Where(a => a.user1.Id == u.Id && a.Accept == true).Select(a => a.user2.Id).ToList();
            List<string> MyFriends2 = db.Friends.Where(a => a.user2.Id == u.Id && a.Accept == true).Select(a => a.user1.Id).ToList();

            MyFriends.AddRange(MyFriends2);

            List<Users> friends = new List<Users>();
            foreach (string id in MyFriends)
            {
                Users user = db.Users.FirstOrDefault(a => a.Id == id);
                friends.Add(user);
            }
            return friends;
        }
        //################################################
        //Search_NewFriends:
        //==================
        public static List<Users> Search(string search, Users u)
        {
            if (search == null || search == "")
            {
                List<Users> us = db.Users.Where(a => a.Id != u.Id).ToList();
                List<Users> fr = GetFriends(u);
                for (int i = us.Count - 1; i >= 0; i--)
                {
                    foreach (Users friend in fr)
                    {
                        if (us[i].Id == friend.Id)
                        {
                            us.RemoveAt(i);
                            break;
                        }
                    }
                }
                List<Users> fr2 = GetFriendRequest(u);
                for (int i = us.Count - 1; i >= 0; i--)
                {
                    foreach (Users frie in fr2)
                    {
                        if (us[i].Id == frie.Id)
                        {
                            us.RemoveAt(i);
                            break;
                        }
                    }
                }
                return us;
            }
            List<Users> users = db.Users.Where(a => a.UserName.Contains(search) && a.Id != u.Id).ToList();
            List<Users> friends = GetFriends(u);
            for (int i = users.Count - 1; i >= 0; i--)
            {
                foreach (Users friend in friends)
                {
                    if (users[i].Id == friend.Id)
                    {
                        users.RemoveAt(i);
                        break;
                    }
                }
            }
            List<Users> fri = GetFriendRequest(u);
            for (int i = users.Count - 1; i >= 0; i--)
            {
                foreach (Users frie in fri)
                {
                    if (users[i].Id == frie.Id)
                    {
                        users.RemoveAt(i);
                        break;
                    }
                }
            }
            return users;
        }
    
        //################################################
        //AddFriend:
        //==========
        public static void AddFriend(Users u, string id)
        {
            Users user1 = db.Users.Find(u.Id);
            Users user2 = db.Users.Find(id);

            Friends exist = db.Friends.FirstOrDefault(a => a.user1.Id == user1.Id && a.user2.Id == user2.Id || a.user1.Id == user2.Id && a.user2.Id == user1.Id);
            if (exist == null)
            {
                Friends friend = new Friends
                {
                    UserID1 = user1.Id,
                    UserID2 = user2.Id,
                    Accept = false,
                    user1 = user1,
                    user2 = user2,
                    Sender = u.Id
                };
                db.Friends.Add(friend);
                db.SaveChanges();
            }
        }
        //################################################
        //################################################
        ////UpdateLanguage:
        ////===============
        //public static void UpdateLanguage(Users u, string lang)
        //{
        //    Users user = db.Users.FirstOrDefault(a => a.Id == u.Id);
        //    db.SaveChanges();
        //}
        //################################################
        //GetRole:
        //========
        public string GetRole(string id)
        {
            IdentityRole role = db.Roles.FirstOrDefault(a => a.Id == id);
            return role.Name;
        }
        //################################################
        //Delete:
        //=======
        public void Delete(Users u)
        {
            db.Users.Remove(u);
            db.SaveChanges();
        }
        //################################################
        //Actions:
        //========
        public void Add(Users users)
        {
            db.Users.Add(users);
            db.SaveChanges();
        }
        public void Edit(Users user)
        {
            Users b = db.Users.FirstOrDefault(a => a.Id == user.Id);
            if (user.UserName != null && user.UserName != "") { b.UserName = user.UserName; }
            if (user.Email != null && user.Email != "") { b.Email = user.Email; }
            if (user.PhoneNumber != null && user.PhoneNumber != "") { b.PhoneNumber = user.PhoneNumber; }
            b.About = user.About;
            b.Country = user.Country;
            b.Facebook_Link = user.Facebook_Link;
            b.Linkedin_Link = user.Linkedin_Link;
            b.Link = user.Link;
            if(user.Picture != null && user.Picture != "") { b.Picture = user.Picture; }
            db.SaveChanges();
        }
        
        //################################################

        //###################################################################################################################################
    }
}






