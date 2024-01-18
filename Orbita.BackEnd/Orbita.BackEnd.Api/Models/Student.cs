
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Orbita.BackEnd.Api.Models
{
    public class Student
    {
        [Key]
        public string RA { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string CPF { get; set; }
    }
}
