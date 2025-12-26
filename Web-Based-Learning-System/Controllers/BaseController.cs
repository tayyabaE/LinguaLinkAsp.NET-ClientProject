using Microsoft.AspNetCore.Mvc;

namespace Web_Based_Learning_System.Controllers
{
    public class BaseController : Controller
    {
        protected int? GetUserId()
        {
            var userId = HttpContext.Session.GetString("UserId");
            return string.IsNullOrEmpty(userId) ? null : int.Parse(userId);
        }

        protected string GetUserRole()
        {
            return HttpContext.Session.GetString("UserRole");
        }
    }
}
