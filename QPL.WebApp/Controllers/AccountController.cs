using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models.CallRecords;
using QPL.BL.DTOs;
using QPL.BL.Token;
using System.Text.Json;

namespace QPL.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginWithMicrosoft()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("Index", "DetailedSearch") }, scheme);
        }


        public IActionResult Logout()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;

            return SignOut(new AuthenticationProperties { RedirectUri = Url.Action("LoginWithMicrosoft", "Account") }, CookieAuthenticationDefaults.AuthenticationScheme, scheme);
        }

        // todo: check unused
        public void GetCookie()
        {
            string cookieValue = string.Empty;
            HttpContext.Request.Cookies.TryGetValue("", out cookieValue);

            HttpContext.Session.Clear();
        }
    }
}
