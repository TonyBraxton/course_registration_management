using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myDBApp.Controllers
{
    public class enrollmentController : Controller
    {
        // GET: enrollmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: enrollmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: enrollmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: enrollmentController/Create
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

        // GET: enrollmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: enrollmentController/Edit/5
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

        // GET: enrollmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: enrollmentController/Delete/5
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
