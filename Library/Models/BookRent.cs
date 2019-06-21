using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("BookRent")]
    public class BookRent
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime RentDate { get; set; }
        public int Qty { get; set; }
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }
    }
}
