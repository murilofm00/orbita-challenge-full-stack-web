using Orbita.BackEnd.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbita.BackEnd.Tests.UnitTests
{
    public class StudentUnitTest
    {
        [Fact]
        public void CreateStudent_ShouldBeValid()
        {
            var student = new Student { Name = "Raissa Gonçalves", CPF = "10987654321", Email = "raissa@gmail.com", RA = "24023214" };

            var context = new ValidationContext(student);
             var teste = context.Items;
            var result = Validator.TryValidateObject(student, context, null, true);

            Assert.True(result);
        }


        [Fact]
        public void CreateStudent_WithInvalidCPF_ShouldBeInvalid()
        {
            var student = new Student { Name = "Raissa Gonçalves", CPF = "45641", Email = "raissa@gmail.com", RA = "24023214" };

            var context = new ValidationContext(student);
            var teste = context.Items;
            var result = Validator.TryValidateObject(student, context, null, true);

            Assert.False(result);
        }


        [Fact]
        public void CreateStudent_WithInvalidEmail_ShouldBeInvalid()
        {
            var student = new Student { Name = "Raissa Gonçalves", CPF = "10987654321", Email = "hellothisisnotaemail", RA = "24023214" };

            var context = new ValidationContext(student);
            var teste = context.Items;
            var result = Validator.TryValidateObject(student, context, null, true);

            Assert.False(result);
        }
    }
}
