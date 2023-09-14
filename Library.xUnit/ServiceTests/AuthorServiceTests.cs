using Library.DAL;
using Library.Model;
using Moq;
using Library.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Library.xUnit.ServiceTests
{
    public class AuthorServiceTests
    {
        [Fact]
        public void AddAuthorTest_ShouldPass()
        {
            var unitofwork = new UnitOfWork();

            var author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Johnson",
                DateOfBirth = DateTime.Now,
            };

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.AddAuthor(It.IsAny<Author>())).Verifiable();

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            authorService.AddAuthor(author);
            mockAuthorsRepo.Verify(repo => repo.AddAuthor(author), Times.Once);
        }
        
        [Fact]
        public void FindAuthorTest_ShouldPass_Found()
        {
            var unitofwork = new UnitOfWork();

            var author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Johnson",
                DateOfBirth = DateTime.Now,
            };

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.GetAuthor(1)).Returns(author);

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            var foundAuthor = authorService.FindAuthor(1);

            Assert.Equal(author, foundAuthor);
        }
        [Fact]
        public void FindAuthorTest_ShouldPass_NotFound()
        {
            var unitofwork = new UnitOfWork();

            var author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Johnson",
                DateOfBirth = DateTime.Now,
            };

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            authorService.AddAuthor(author);

            var notFoundAuthor = authorService.FindAuthor(2);

            Assert.Null(notFoundAuthor);
        }
        [Fact]
        public void DeleteAuthorTest_ShouldPass()
        {
            var unitofwork = new UnitOfWork();

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.DeleteAuthor(It.IsAny<int>()));

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            try
            {
                authorService.DeleteAuthor(1);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void DeleteAuthorTest_ShouldThrow_NoObject()
        {
            var unitofwork = new UnitOfWork();

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.DeleteAuthor(It.IsAny<int>())).Throws<DbUpdateConcurrencyException>();

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            Assert.Throws<DbUpdateConcurrencyException>(() => authorService.DeleteAuthor(1));
        }
        [Fact]
        public void GetAuthorsTest_ShouldPass_NotEmpty()
        {
            var unitofwork = new UnitOfWork();

            var author1 = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Johnson",
                DateOfBirth = DateTime.Now,
            };
            var author2 = new Author()
            {
                Id = 2,
                Name = "Thomas",
                Surname = "Berger",
                DateOfBirth = DateTime.Now,
            };

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.GetAllAuthors()).Returns(new List<Author> { author1, author2 });

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            Assert.Equal(2 ,authorService.GetAuthors().Count());
        }
        [Fact]
        public void GetAuthorsTest_ShouldPass_Empty()
        {
            var unitofwork = new UnitOfWork();

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.GetAllAuthors()).Returns(new List<Author> {});

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            Assert.Empty(authorService.GetAuthors());
        }
        [Fact]
        public void UpdateAuthorTest_ShouldPass_Exists()
        {
            var unitofwork = new UnitOfWork();

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.UpdateAuthor(It.IsAny<Author>()));

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            Author authorToUpdate = new Author
            {
                Id = 1,
                Name = "Author",
                Surname = "Authorsky",
                DateOfBirth = DateTime.Now
            };

            try
            {
                authorService.UpdateAuthor(authorToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void UpdateAuthorTest_ShouldThrow_NotExists()
        {
            var unitofwork = new UnitOfWork();

            var mockAuthorsRepo = new Mock<IAuthorRepository>();

            mockAuthorsRepo.Setup(repo => repo.UpdateAuthor(It.IsAny<Author>())).Throws<DbUpdateConcurrencyException>();

            unitofwork.AuthorsRepository = mockAuthorsRepo.Object;

            var authorService = new AuthorService(unitofwork);

            Author authorToUpdate = new Author
            {
                Id = 1,
                Name = "Author",
                Surname = "Authorsky",
                DateOfBirth = DateTime.Now
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => authorService.UpdateAuthor(authorToUpdate));
        }
    }
}
