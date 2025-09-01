// Data/LibraryContext.cs
using Microsoft.EntityFrameworkCore;
using Library_MS.Model.Book;
using Library_MS.Model.Student;
using Library_MS.Model.BorrowRecord;

namespace Library_MS.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) 
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
    }
}
