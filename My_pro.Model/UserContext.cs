using System;
using System.Collections.Generic;
using System.Data.Entity;
using My_pro.Model.Entites;
using System.Linq;
using System.Web;
using My_pro.Model.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using My_pro;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace My_pro.Model.Entites
{
    public class UserContext : IdentityDbContext<Users> 
    {

        static UserContext db = new UserContext();
        public static readonly Func<object> Create;


        //  Users CurrentUser = ();

        public UserContext() : base("MyDBContext") { }

        public static void Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserContext, Configuration>());
        }


        public static Users GetByName(string id)
        {
            Users existUser = db.Users.FirstOrDefault(a => a.Id == id);
            if (existUser != null)
            {
                return existUser;
            }
            return null;
        }
        public static string GetRole(string id)
        {
            IdentityRole role = db.Roles.FirstOrDefault(a => a.Id == id);
            return role.Name;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Users>().HasMany(a => a.Friends).WithRequired().HasForeignKey(b => b.UserID1).WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>().HasMany(a => a.Friends).WithRequired().HasForeignKey(b => b.UserID2).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Users>().HasMany(u => u.Messages).WithRequired(u => u.user1).WillCascadeOnDelete(false); //add this line code
            //modelBuilder.Entity<Users>().HasMany(u => u.Messages).WithRequired(u => u.user2).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Users>().HasMany(u => u.Msgs).WithRequired(u => u.User).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Users>().HasMany(u => u.Messages).WithRequired(u => u.user2).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Users>().HasMany(u => u.Users_Groups).WithRequired(u => u.User).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Groups>().HasMany(u => u.Users_Groups).WithRequired(u => u.Group).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Groups>().HasMany(u => u.Messages).WithRequired(u => u.g).WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>().HasMany(u => u.Users_Levels).WithRequired(u => u.User).WillCascadeOnDelete(false);
            modelBuilder.Entity<Levels>().HasMany(u => u.Users_Levels).WithRequired(u => u.Levels).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Users>().HasMany(u => u.Users_Chapters).WithRequired(u => u.User).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Chapters>().HasMany(u => u.Users_Chapters).WithRequired(u => u.Chapter).WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>().HasMany(u => u.Users_Diploma).WithRequired(u => u.User).WillCascadeOnDelete(false);
            modelBuilder.Entity<Diploma>().HasMany(u => u.Users_Diploma).WithRequired(u => u.Diploma).WillCascadeOnDelete(false);

            modelBuilder.Entity<Levels>().HasMany(u => u.Levels_Videos).WithRequired(u => u.Level).WillCascadeOnDelete(false);
            modelBuilder.Entity<Videos>().HasMany(u => u.Levels_Videos).WithRequired(u => u.Video).WillCascadeOnDelete(false);

            modelBuilder.Entity<QExams>().HasMany(u => u.QExams_Chapters).WithRequired(u => u.QExam).WillCascadeOnDelete(false);
            modelBuilder.Entity<Chapters>().HasMany(u => u.QExams_Chapters).WithRequired(u => u.Chapter).WillCascadeOnDelete(false);

            modelBuilder.Entity<QExams>().HasMany(u => u.QExams_Finals).WithRequired(u => u.QExam).WillCascadeOnDelete(false);
            modelBuilder.Entity<Finals>().HasMany(u => u.QExams_Finals).WithRequired(u => u.Final).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Users>().HasMany(u => u.Users_Books).WithRequired(u => u.User).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Books>().HasMany(u => u.Users_Books).WithRequired(u => u.Book).WillCascadeOnDelete(false);

        }







        public DbSet<Friends> Friends { get; set; }
        //public DbSet<ChatGroup> ChatGroup { get; set; }
        public DbSet<Finals> Finals { get; set; }
        public DbSet<Chapters> Chapters { get; set; }
        public DbSet<Diploma> Diploma { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        //public DbSet<Books> Books { get; set; }
        public DbSet<QExams> QExams { get; set; }
        //public DbSet<Messages> Messages { get; set; }
        public DbSet<Levels> Levels { get; set; }
        
        public DbSet<Blog_Comments> Blog_Comments { get; set; }
        //public DbSet<Users_Groups> Users_Groups { get; set; }
        public DbSet<Levels_Videos> Levels_Videos { get; set; }
        public DbSet<QExams_Chapters> QExams_Chapters { get; set; }
        public DbSet<QExams_Finals> QExams_Finals { get; set; }
        public DbSet<Users_Chapters> Users_Chapters { get; set; }
        public DbSet<UploadFile> UploadFile { get; set; }
        public DbSet<Users_Finals> Users_Finals { get; set; }
        public DbSet<Users_Diplomacd> Users_Diploma { get; set; }
        //public DbSet<Users_Books> Users_Books { get; set; }
        public DbSet<Users_Levels> Users_Levels { get; set; }

        //currentuser
    }
}