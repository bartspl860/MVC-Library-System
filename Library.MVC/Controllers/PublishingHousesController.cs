using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.DAL;

namespace Library.MVC.Controllers
{
    public class PublishingHousesController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: PublishingHouses
        public ViewResult Index()
        {
            var publishingHouses = _unitOfWork.PublishingHousesRepository.Get(includeProperties: "Books");
            return View(publishingHouses.ToList());
        }

        // GET: PublishingHouses/Details/5
        public ActionResult Details(int id)
        {
            PublishingHouse? publishingHouse = _unitOfWork.PublishingHousesRepository.GetByID(id);
            if (publishingHouse == null)
                return Redirect("/Home/Error");

            return View(publishingHouse);
        }

        // GET: PublishingHouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublishingHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PublishingHouse publishingHouse)
        {
            _unitOfWork.PublishingHousesRepository.Insert(publishingHouse);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: PublishingHouses/Edit/5
        public ActionResult Edit(int id)
        {
            PublishingHouse? publishingHouse = _unitOfWork.PublishingHousesRepository.GetByID(id);
            if (publishingHouse == null)
                return Redirect("/Home/Error");
            
            return View(publishingHouse);
        }

        // POST: PublishingHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name")] PublishingHouse publishingHouse)
        {
            _unitOfWork.PublishingHousesRepository.Update(publishingHouse);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: PublishingHouses/Delete/5
        public ActionResult Delete(int id)
        {
            PublishingHouse? publishingHouse = _unitOfWork.PublishingHousesRepository.GetByID(id);
            if (publishingHouse == null)
                return Redirect("/Home/Error");

            return View(publishingHouse);
        }

        // POST: PublishingHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PublishingHouse? publishingHouse = _unitOfWork.PublishingHousesRepository.GetByID(id);
            _unitOfWork.PublishingHousesRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishingHouseExists(int id)
        {
          return _unitOfWork.PublishingHousesRepository.GetByID(id) != null;
        }
    }
}
