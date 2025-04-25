using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myDBApp.Controllers
{
    public class studentsController : Controller
    {
        // GET: studentsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: studentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: studentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: studentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: studentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: studentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: studentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: studentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
