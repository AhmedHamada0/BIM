using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using My_pro.Model.Entites;

[assembly: OwinStartup(typeof(My_pro.StartUp))]

namespace My_pro
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserContext());
            app.CreatePerOwinContext<UserStore<Users>>((opt, cont) => new UserStore<Users>(cont.Get<UserContext>()));
            app.CreatePerOwinContext<UserManager<Users>>((opt, cont) => new UserManager<Users>(new UserStore<Users>(cont.Get<UserContext>())));
            app.CreatePerOwinContext<SignInManager<Users, string>>((opt, cont) => new SignInManager<Users, string>(cont.Get<UserManager<Users>>(), cont.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Home/Index"),
            });
            

        }
    }
}