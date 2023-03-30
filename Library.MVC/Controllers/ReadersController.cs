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
    public class ReadersController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Readers
        public ViewResult Index()
        {
            var readers = _unitOfWork.ReadersRepository.Get(includeProperties: "BorrowedBooks");
            return View(readers.ToList());
        }

        // GET: Readers/Details/5
        public ActionResult Details(int id)
        {
            Reader? reader = _unitOfWork.ReadersRepository.GetByID(id);
            if (reader == null)
                return Redirect("/Home/Error");

            return View(reader);
        }

        // GET: Readers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reader reader)
        {
            _unitOfWork.ReadersRepository.Insert(reader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Readers/Edit/5
        public IActionResult Edit(int id)
        {
            Reader? reader = _unitOfWork.ReadersRepository.GetByID(id);
            if (reader == null)
                return Redirect("/Home/Error");

            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("LibraryCardNumber,LibraryCardExpirationDate,Id,Name,Surname,DateOfBirth")] Reader reader)
        {
            _unitOfWork.ReadersRepository.Update(reader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Readers/Delete/5
        public ActionResult Delete(int id)
        {
            Reader? reader = _unitOfWork.ReadersRepository.GetByID(id);
            if (reader == null)
                return Redirect("/Home/Error");

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reader? reader = _unitOfWork.ReadersRepository.GetByID(id);
            _unitOfWork.ReadersRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderExists(int id)
        {
          return _unitOfWork.ReadersRepository.GetByID(id) != null;
        }
    }
}
