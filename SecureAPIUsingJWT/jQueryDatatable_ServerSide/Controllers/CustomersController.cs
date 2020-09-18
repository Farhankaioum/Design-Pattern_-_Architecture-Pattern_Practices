using Microsoft.AspNetCore.Mvc;

namespace jQueryDatatable_ServerSide.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
