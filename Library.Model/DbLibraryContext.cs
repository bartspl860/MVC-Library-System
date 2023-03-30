using Microsoft.EntityFrameworkCore;

namespace Library.Model
{
    public class DbLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var publishingHouses = new PublishingHouse[]
            {
                new PublishingHouse { Id = 1, Name = "GREG" },
                new PublishingHouse { Id = 2, Name = "Społeczny Instytut Wydawniczy Znak" },
            };

            var authors = new Author[]
            {
                new Author { Id = 1, Name = "Adam", Surname = "Mickiewicz", DateOfBirth = DateTime.Parse("1798-12-24")},
                new Author { Id = 2, Name = "Henryk", Surname = "Sienkiewicz", DateOfBirth = DateTime.Parse("1846-05-05")},
                new Author { Id = 3, Name = "Wisława", Surname = "Szymborska", DateOfBirth = DateTime.Parse("1923-07-02")}
            };
            
            var books = new Book[]
            {
                new Book { Id = 1, Title = "Pan Tadeusz", PublishingHouse = publishingHouses[0], Authors =  new List<Author>{ authors[0] }},
                new Book { Id = 2, Title = "Ballady i romanse", PublishingHouse = publishingHouses[0], Authors =  new List<Author>{ authors[0] }},
                new Book { Id = 3, Title = "Dziady", PublishingHouse = publishingHouses[0], Authors = new List < Author > { authors[0] }},
                new Book { Id = 4, Title = "W pustyni i w puszczy", PublishingHouse = publishingHouses[0], Authors = new List < Author > { authors[1] }},
                new Book { Id = 5, Title = "Potop", PublishingHouse = publishingHouses[0], Authors = new List < Author > { authors[1] }},
                new Book { Id = 6, Title = "Dwukropek", PublishingHouse = publishingHouses[1], Authors = new List < Author > { authors[2] }},
                new Book { Id = 7, Title = "Chwila ", PublishingHouse = publishingHouses[1], Authors = new List < Author > { authors[2] }}
            };

            var readers = new Reader[]
            {
                new Reader { Name = "Janek", Surname = "Betoniarek", DateOfBirth = DateTime.Parse("2005-12-01"), LibraryCardNumber = 5678, LibraryCardExpirationDate = DateTime.Parse("2024-01-01") }
            };

            /*
            books[0].Authors.Add(authors[0]);
            books[1].Authors.Add(authors[0]);
            books[2].Authors.Add(authors[0]);
            books[3].Authors.Add(authors[1]);
            books[4].Authors.Add(authors[1]);
            books[5].Authors.Add(authors[2]);
            books[6].Authors.Add(authors[2]);

            readers[0].BorrowedBooks = new List<Book> { books[0], books[4] };

            modelBuilder.Entity<PublishingHouse>().HasData(
               publishingHouses[0],
               publishingHouses[1]
               );

            modelBuilder.Entity<Reader>().HasData(readers[0]);

            modelBuilder.Entity<Book>().HasData(
                books[0],
                books[1],
                books[2],
                books[3],
                books[4],
                books[5],
                books[6]
                );
           
           modelBuilder.Entity<Author>().HasData(
               authors[0],
               authors[1],
               authors[2]
               );       
            */
        }
    }
}
