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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Student)
                .ToListAsync();
        }

        [HttpPost]
    public async Task<ActionResult<BorrowRecord>> BorrowBook(BorrowRecordCreateDto dto)
    {
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

