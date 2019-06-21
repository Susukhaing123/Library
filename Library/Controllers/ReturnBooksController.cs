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
    public class ReturnBooksController : Controller
    {
        private readonly LibraryContext _context;

        public ReturnBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ReturnBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Returnbook.Include(r => r.Bookrent);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ReturnBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.Returnbook
                .Include(r => r.Bookrent)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (returnBook == null)
            {
                return NotFound();
            }

            return View(returnBook);
        }

        // GET: ReturnBooks/Create
        public IActionResult Create()
        {
            var rbook = _context.Bookrent.Where(rb => rb.Qty > 0).ToList();
            var studentlist = (from brent in _context.Bookrent
                               join s in _context.Student on brent.StudentId equals s.StudentId
                               select new Student
                               {
                                   StudentId = brent.Id,
                                   Name = s.Name
                               }).ToList();
            ViewData["Id"] = new SelectList(rbook, "Id", "Id");
            return View();
        }

        // POST: ReturnBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReturnBook returnBook)
        {
            if (ModelState.IsValid)
            {
                var rtbook = _context.Bookrent.Find(returnBook.Id);
                var book = _context.Book.Find(rtbook.BookId);
                book.Qty += returnBook.Qty;
                var bookrent = _context.Bookrent.Find(returnBook.Id);
                bookrent.Qty -= returnBook.Qty;
                _context.Entry(bookrent).State = EntityState.Modified;
                _context.Entry(book).State = EntityState.Modified;

                _context.Add(returnBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Bookrent, "Id", "Id", returnBook.Id);
            return View(returnBook);
        }

        // GET: ReturnBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.Returnbook.FindAsync(id);
            if (returnBook == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Bookrent, "Id", "Id", returnBook.Id);
            return View(returnBook);
        }

        // POST: ReturnBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnId,Qty,ReturnDate,Id")] ReturnBook returnBook)
        {
            if (id != returnBook.ReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(returnBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnBookExists(returnBook.ReturnId))
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
            ViewData["Id"] = new SelectList(_context.Bookrent, "Id", "Id", returnBook.Id);
            return View(returnBook);
        }

        // GET: ReturnBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.Returnbook
                .Include(r => r.Bookrent)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (returnBook == null)
            {
                return NotFound();
            }

            return View(returnBook);
        }

        // POST: ReturnBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var returnBook = await _context.Returnbook.FindAsync(id);
            _context.Returnbook.Remove(returnBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnBookExists(int id)
        {
            return _context.Returnbook.Any(e => e.ReturnId == id);
        }
    }
}