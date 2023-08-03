using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAuthor(Author author)
        {
            _unitOfWork.AuthorsRepository.AddAuthor(author);
        }

        public void DeleteAuthor(int id)
        {
            _unitOfWork.AuthorsRepository.DeleteAuthor(id);
        }

        public Author? FindAuthor(int id)
        {
            return _unitOfWork.AuthorsRepository.GetAuthor(id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _unitOfWork.AuthorsRepository.GetAllAuthors();
        }

        public void UpdateAuthor(Author author)
        {
            _unitOfWork.AuthorsRepository.UpdateAuthor(author);
        }
    }
}
