using Microsoft.AspNetCore.Mvc;
using Spam_Detection.Data;
using Spam_Detection.ViewModel;

namespace Spam_Detection.Controllers
{
    public class SpamDetectionController : Controller
    {
        public IActionResult Predict()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Predict(SpamInput input)
        {
            var predictVm = new PredictViewModel();
            var result = predictVm.IsTrue(input);

            if (!ModelState.IsValid)
                return View();

            if (result)
            {
                ModelState.AddModelError("Message", "Invalid text found");
                input.Message = "";
                return View(input);
            }

            return View();
        }
    }
}
