using Library_MS.Model.Book;
using Library_MS.Model.Student;
namespace Library_MS.Model.BorrowRecord
{
    public class BorrowRecord
    {
        public int BorrowID { get; set; }
        public int BookID { get; set; }
        public int StudentID { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Borrowed";

    public Library_MS.Model.Book.Book Book { get; set; }
    public Library_MS.Model.Student.Student Student { get; set; }
    }
}
// ...existing code...