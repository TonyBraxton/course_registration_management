using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myDBApp.Controllers
{
    public class professorsController : Controller
    {
        // GET: professorsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: professorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: professorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: professorsController/Create
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

        // GET: professorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: professorsController/Edit/5
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

        // GET: professorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: professorsController/Delete/5
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
