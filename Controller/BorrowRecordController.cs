using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Library_MS.Model.BorrowRecord;
using Library_MS.Data;
using Library_MS.DTO;

namespace Library_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BorrowRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Student)
                .ToListAsync();
        }

        // POST: api/BorrowRecord
        [HttpPost]
        public async Task<ActionResult<BorrowRecord>> BorrowBook(BorrowRecordCreateDto dto)
        {
            // Fetch the book
            var book = await _context.Books.FindAsync(dto.BookID);
            if (book == null) return NotFound("Book not found.");
            if (book.Quantity <= 0) return BadRequest("Book is not available.");

            // Reduce available copies
            book.Quantity -= 1;

            // Create borrow record
            var record = new BorrowRecord
            {
                BookID = dto.BookID,
                StudentID = dto.StudentID,
                BorrowDate = DateTime.Now,
                Status = "Borrowed"
            };

            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecords), new { id = record.BorrowID }, record);
        }

        // PUT: api/BorrowRecord/{id}/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            // Find the borrow record and include the book
            var record = await _context.BorrowRecords
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.BorrowID == id);

            if (record == null) return NotFound("Borrow record not found.");
            if (record.Status == "Returned") return BadRequest("Book already returned.");

            // Update record
            record.ReturnDate = DateTime.Now;
            record.Status = "Returned";

            // Increase available copies
            record.Book.Quantity += 1;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
