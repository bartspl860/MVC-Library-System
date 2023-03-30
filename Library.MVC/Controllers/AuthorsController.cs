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
    public class AuthorsController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Authors
        public ViewResult Index()
        {
            var authors = _unitOfWork.AuthorsRepository.Get(includeProperties: "WrittenBooks");
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            Author? author = _unitOfWork.AuthorsRepository.GetByID(id);
            if (author == null)
                return Redirect("/Home/Error");
            
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            _unitOfWork.AuthorsRepository.Insert(author);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int id)
        {
            Author? author = _unitOfWork.AuthorsRepository.GetByID(id);
            if (author == null)
                return Redirect("/Home/Error");
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Surname,DateOfBirth")] Author author)
        {
            _unitOfWork.AuthorsRepository.Update(author);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int id)
        {
            Author? author = _unitOfWork.AuthorsRepository.GetByID(id);
            if(author == null)
                return Redirect("/Home/Error");

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author? author = _unitOfWork.AuthorsRepository.GetByID(id);
            _unitOfWork.AuthorsRepository.Delete(id);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _unitOfWork.AuthorsRepository.GetByID(id) != null;
        }
    }
}
