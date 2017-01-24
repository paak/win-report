using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace WINConnect.Web.Controllers
{
    public class SITATextController : Controller
    {
        //
        // GET: /SITAText/
        public IEnumerable<FileInfo> Index()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"D:\dev");

            FileInfo[] files = null;
            if (dirInfo.Exists)
            {
                files = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            }

            if (files != null)
            {
                files = files.OrderBy(x => x.CreationTime).ToArray();
            }

            return files;
        }

        //
        // GET: /SITAText/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SITAText/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SITAText/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SITAText/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SITAText/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SITAText/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SITAText/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
