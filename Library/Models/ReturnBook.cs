using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("ReturnBook")]
    public class ReturnBook
    {
        [Key]
        public int ReturnId { get; set; }
        public int Qty { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Id { get; set; }
        public virtual BookRent Bookrent { get; set; }
    }
}
