
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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

        [Required(ErrorMessage ="O campo RA é obrigatório.")]
        [ReadOnly(true)]
        public required string RA { get; set; }
        
        [Required(ErrorMessage ="O campo Nome é obrigatório.")]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage ="O email especificado não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF especificado não é valido.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "O CPF deve conter apenas números")]
        [Column(TypeName = "char(11)")]
        public required string CPF { get; set; }
    }
}
