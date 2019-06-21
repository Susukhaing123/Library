using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookRentViewModel
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public int Qty { get; set; }
        public string StudentName { get; set; }
        public string BookName { get; set; }
       
    }
}
