using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_MS.Model.Book
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        [Column("PublishDate")] 
        public DateTime PublishedDate { get; set; }
        public int Quantity { get; set; }
    }
}
// ...existing code...