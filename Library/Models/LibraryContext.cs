using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Models
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options):base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookRent> Bookrent { get; set; }
        public DbSet<ReturnBook> Returnbook { get; set; }
        public DbSet<Library.Models.BookRentViewModel> BookRentViewModel { get; set; }
    }
}
