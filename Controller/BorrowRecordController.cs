using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Library_MS.Model.BorrowRecord; // BorrowRecord model
using Library_MS.Data;               // DbContext

namespace Library_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BorrowRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Student) // Update from Member to Student
                .ToListAsync();
        }

        // POST: api/BorrowRecords
        [HttpPost]
        public async Task<ActionResult<BorrowRecord>> BorrowBook(BorrowRecord record)
        {
            record.BorrowDate = DateTime.Now;
            record.Status = "Borrowed";
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBorrowRecords), new { id = record.BorrowID }, record);
        }

        // PUT: api/BorrowRecords/{id}/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null) return NotFound();

            record.ReturnDate = DateTime.Now;
            record.Status = "Returned";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
