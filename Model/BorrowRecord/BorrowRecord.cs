using Library_MS.Model.Book;
using Library_MS.Model.Student;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_MS.Model.BorrowRecord
{
    [Table("Borrow_Record")]
    public class BorrowRecord
    {
        [Key]
        [Column("Borrow_ID")]
        public int BorrowID { get; set; }

        [Column("BookID")]
        public int BookID { get; set; }

        [Column("StudentID")]
        public int StudentID { get; set; }

        [Column("BorrowDate")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Column("ReturnDate")]
        public DateTime? ReturnDate { get; set; }

        [Column("Status")]
        public string Status { get; set; } = "Borrowed";

        // Navigation properties
        [ForeignKey("BookID")]
        public Library_MS.Model.Book.Book Book { get; set; }

        [ForeignKey("StudentID")]
        public Library_MS.Model.Student.Student Student { get; set; }
    }
}
