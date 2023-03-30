using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.DAL;
using System.Data;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Books
        public ViewResult Index()
        {
            var books = _unitOfWork.BooksRepository.Get(includeProperties: "PublishingHouse");
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            Book? book = _unitOfWork.BooksRepository.GetByID(id);
            if (book == null)
                return Redirect("/Home/Error");
            PublishingHouse? ph = _unitOfWork.PublishingHousesRepository.GetByID(book.PublishingHouseId);
            if (ph == null)
                return Redirect("/Home/Error");
            ViewBag.PublishingHouseName = ph.Name;
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            PopulatePublishingHousesDropDownList();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            _unitOfWork.BooksRepository.Insert(book);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            Book? book = _unitOfWork.BooksRepository.GetByID(id);
            if (book == null)
                return Redirect("/Home/Error");
            PopulatePublishingHousesDropDownList(book.PublishingHouseId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Title,PublishingHouseId")] Book book)
        {
            _unitOfWork.BooksRepository.Update(book);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private void PopulatePublishingHousesDropDownList(object? selectedPublishingHouse = null)
        {
            var publishingHouses = _unitOfWork.PublishingHousesRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name)
            );
            ViewBag.PublishingHouseId = new SelectList(publishingHouses, "Id", "Name", selectedPublishingHouse);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            Book? book = _unitOfWork.BooksRepository.GetByID(id);
            if (book == null)
                return Redirect("/Home/Error");

            PublishingHouse? ph = _unitOfWork.PublishingHousesRepository.GetByID(book.PublishingHouseId);
            if(ph == null)
                return Redirect("/Home/Error");

            ViewBag.PublishingHouseName = ph.Name;
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book? book = _unitOfWork.BooksRepository.GetByID(id);
            _unitOfWork.BooksRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return _unitOfWork.BooksRepository.GetByID(id) != null;
        }
    }
}
