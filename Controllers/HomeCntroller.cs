using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("[controller]")]
    public class HomeCntroller : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}