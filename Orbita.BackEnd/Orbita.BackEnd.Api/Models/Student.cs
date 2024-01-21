
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Orbita.BackEnd.Api.Models
{
    [Index(nameof(RA),IsUnique = true)]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RA { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [Column(TypeName = "char(11)")]
        public string CPF { get; set; }
    }
}
