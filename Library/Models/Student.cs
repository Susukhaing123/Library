using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string RollNo { get; set; }
        public string NRC { get; set; }
        public string Grade { get; set; }
        public ICollection<BookRent> Bookrent { get; set; }
    }
}
