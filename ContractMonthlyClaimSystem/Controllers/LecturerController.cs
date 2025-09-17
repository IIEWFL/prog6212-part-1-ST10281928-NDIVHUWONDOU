using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private static List<Claim> claims = new()
        {
            new Claim{Id=1, LecturerName="Dr. Smith", Module="Prog6212", HourlyRate=300, HoursWorked=10, Status="Pending"},
            new Claim{Id=2,  LecturerName="Prof. Johnson", Module="Database", HourlyRate=150, HoursWorked=8, Status="Accessed" }
        };
        public IActionResult Index()
        {
            return View(claims);
        }

        public IActionResult Details(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Claim claim, IFormFile? document)
        {
            if (ModelState.IsValid)
            {
                if (document != null && document.Length > 0)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                    var filepath = Path.Combine(uploadFolder, document.FileName);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }

                    claim.DocumentPath = "/uploads/" + document.FileName;

                }

                claim.Id = claims.Count + 1;
                claims.Add(claim);
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim == null) return NotFound();
            return View(claim);
        }
        [HttpPost]
        public IActionResult Edit(Claim updatedclaim)
        {
            var claim = claims.FirstOrDefault(c => c.Id == updatedclaim.Id);
            if (claim == null) return NotFound();

            claim.LecturerName = updatedclaim.LecturerName;
            claim.Module = updatedclaim.Module;
            claim.HoursWorked = updatedclaim.HoursWorked;
            claim.HourlyRate = updatedclaim.HourlyRate;
            claim.DocumentPath = updatedclaim.DocumentPath;
            claim.Status = updatedclaim.Status;

            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)claims.Remove(claim);
            return RedirectToAction("Index");
        }
    }
}
