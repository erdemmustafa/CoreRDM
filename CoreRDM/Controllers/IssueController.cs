using Microsoft.AspNetCore.Mvc;

namespace CoreRDM.Controllers
{
    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
