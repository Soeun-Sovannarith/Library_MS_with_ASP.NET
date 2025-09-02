using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_MS.Model.Student
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Column("StudentID")]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
    }
}
// ...existing code...