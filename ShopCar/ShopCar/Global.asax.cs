using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ShopCar
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["SoNguoiTruyCap"] = 0;
            Application["SoNguoiDangOnline"] = 0;
        }

        protected void Session_Start()
        {
            Application.Lock();
            Application["SoNguoiTruyCap"] = (int)Application["SoNguoiTruyCap"] + 1;
            Application["SoNguoiDangOnline"] = (int)Application["SoNguoiDangOnline"] + 1;

            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();
            Application["SoNguoiDangOnline"] = (int)Application["SoNguoiDangOnline"] - 1;
            Application.UnLock();
        }

        //protected void PhanQuyenAdmin(object sender,EventArgs e)
        //{
        //    var TaiKhoanCookie = Session["TaiKhoanAdmin"];
        //    if (TaiKhoanCookie != null)
        //    {
        //        var authTicket = FormsAuthentication.Decrypt(TaiKhoanCookie.Value);
        //        var quyen = authTicket.UserData.Split(new char[] { ',' });
        //        var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), quyen);
        //        Context.User = userPrincipal;
        //    }
        //}

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var TaiKhoanCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (TaiKhoanCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(TaiKhoanCookie.Value);
                var quyen = authTicket.UserData.Split(new char[] { ',' });
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), quyen);
                Context.User = userPrincipal;
            }
        }
    }
}
