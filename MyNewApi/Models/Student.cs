using System.ComponentModel.DataAnnotations;

namespace MyNewApi.Models
{
    public class Student
    {
        [Key]
        public int Sid { get; set; }
        public string Sname { get; set; }
    }
}
