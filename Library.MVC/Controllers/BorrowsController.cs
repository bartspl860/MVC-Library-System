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
    public class BorrowsController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Borrows
        public ViewResult Index()
        {
            var borrows = _unitOfWork.BorrowsRepository.Get(includeProperties: "Reader,Book");
            return View(borrows.ToList());
        }

        // GET: Borrows/Details/5
        public ActionResult Details(int id)
        {
            Borrow? borrow = _unitOfWork.BorrowsRepository.GetByID(id);
            if (borrow == null)
                return Redirect("/Home/Error");
            Reader? reader = _unitOfWork.ReadersRepository.GetByID(borrow.ReaderId);
            if (reader == null)
                return Redirect("/Home/Error");
            Book? book = _unitOfWork.BooksRepository.GetByID(borrow.BookId);
            if (book == null)
                return Redirect("/Home/Error");

            ViewBag.ReaderName = reader.Name;
            ViewBag.BookTitle = book.Title;

            return View(borrow);
        }

        // GET: Borrows/Create
        public IActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Borrow borrow)
        {
            _unitOfWork.BorrowsRepository.Insert(borrow);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Borrows/Edit/5
        public ActionResult Edit(int id)
        {
            Borrow? borrow = _unitOfWork.BorrowsRepository.GetByID(id);
            if (borrow == null)
                return Redirect("/Home/Error");

            PopulateDropDownList(borrow.ReaderId, borrow.BookId);

            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Date")] Borrow borrow)
        {
            _unitOfWork.BorrowsRepository.Update(borrow);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDownList(object? selectedReader = null, object? selectedBook = null)
        {
            var readers = _unitOfWork.ReadersRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name)
            );

            var books = _unitOfWork.BooksRepository.Get(
                orderBy: q => q.OrderBy(d => d.Title)
            );

            ViewBag.ReaderId = new SelectList(readers, "Id", "Name", selectedReader);
            ViewBag.BookId = new SelectList(books, "Id", "Title", selectedBook);
        }

        // GET: Borrows/Delete/5
        public ActionResult Delete(int id)
        {
            Borrow? borrow = _unitOfWork.BorrowsRepository.GetByID(id);
            if (borrow == null)
                return Redirect("/Home/Error");

            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrow? borrow = _unitOfWork.BorrowsRepository.GetByID(id);
            _unitOfWork.BorrowsRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowExists(int id)
        {
          return _unitOfWork.BorrowsRepository.GetByID(id) != null;
        }
    }
}
