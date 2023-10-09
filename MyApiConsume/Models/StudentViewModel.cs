using System.ComponentModel.DataAnnotations;

namespace MyApiConsume.Models
{
    public class StudentViewModel
    {
        [Key]
        public int Sid { get; set; }
        public string Sname { get; set; }
    }
}
