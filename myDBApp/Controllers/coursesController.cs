using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myDBApp.Data;

namespace myDBApp.Controllers
{
    public class coursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // GET: coursesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: coursesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: coursesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: coursesController/Create
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

        // GET: coursesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: coursesController/Edit/5
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

        // GET: coursesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: coursesController/Delete/5
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
