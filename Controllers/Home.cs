using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [Area("Home")]
    public class Home : Controller
    {
        const string _PAGE_HOME = "Views/Pages/Home.cshtml";

        [Route("")]
        public IActionResult Index() => View(_PAGE_HOME);
    }
}
