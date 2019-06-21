using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class BookRentsController : Controller
    {
        private readonly LibraryContext _context;

        public BookRentsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookRents
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Bookrent.Include(b => b.Book).Include(b => b.Student);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BookRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.Bookrent
                .Include(b => b.Book)
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookRent == null)
            {
                return NotFound();
            }

            return View(bookRent);
        }

        // GET: BookRents/Create
        public IActionResult Create()
        {
            var booklist = _context.Book.Where(b => b.Qty > 0).ToList();

            ViewData["BookId"] = new SelectList(booklist, "BookId", "BookName");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "Name");
            return View();
        }

        // POST: BookRents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookRent bookRent)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Book.Find(bookRent.BookId);
                book.Qty -= bookRent.Qty;
                _context.Entry(book).State = EntityState.Modified;
                _context.Add(bookRent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }
        public IActionResult ViewBookRent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewBookRent(BookRentViewModel bookview)
        {
            return View();
        }


        // GET: BookRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.Bookrent.FindAsync(id);
            if (bookRent == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }

        // POST: BookRents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,StudentId,RentDate,Qty")] BookRent bookRent)
        {
            if (id != bookRent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookRent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookRentExists(bookRent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }

        // GET: BookRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.Bookrent
                .Include(b => b.Book)
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookRent == null)
            {
                return NotFound();
            }

            return View(bookRent);
        }

        // POST: BookRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookRent = await _context.Bookrent.FindAsync(id);
            _context.Bookrent.Remove(bookRent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookRentExists(int id)
        {
            return _context.Bookrent.Any(e => e.Id == id);
        }
    }
}

