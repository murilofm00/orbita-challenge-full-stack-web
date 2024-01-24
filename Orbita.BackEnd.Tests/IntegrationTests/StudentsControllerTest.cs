using Microsoft.AspNetCore.Mvc;
using Orbita.BackEnd.Api.Controllers;
using Orbita.BackEnd.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Orbita.BackEnd.Tests.UnitTests
{
    public class StudentsControllerTest : BaseIntegrationTest
    {
        public StudentsControllerTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task GetStudents_ShouldReturn_AllStudents()
        {
            // Arrange
            var controller = new StudentsController(_context);

            //Act
            var result = await controller.GetStudents();
            var students = result.Value;

            //Assert
            var studentsCount = _context.Students.Count();

            Assert.Equal(studentsCount, students?.Count());
        }

        [Fact]
        public async Task GetStudent_ShouldReturn_Student()
        {
            //Arrange
            var controller = new StudentsController(_context);
            Student student = new Student { Name = "John Wick", CPF = "75315984260", Email = "john@gmail.com", RA = "24045114" };

            _context.Students.Add(student);
            _context.SaveChanges();

            //Act
            var result = await controller.GetStudent(student.Id);
            var studentResult = result.Value;

            //Assert
            Assert.NotNull(studentResult);
            Assert.Equal(student.RA, studentResult.RA);
        }

        [Fact]
        public async Task PostStudent_ShouldAdd_NewStudentToDatabase()
        {
            //Arrange
            var controller = new StudentsController(_context);
            Student newStudent = new Student { Name = "Raissa Gonçalves", CPF = "10987654321", Email = "raissa@gmail.com", RA = "24023214" };

            //Act
            var result = await controller.PostStudent(newStudent);
            var createdStudent = result.Value;

            //Assert
            var studentDB = _context.Set<Student>().FirstOrDefault(x => x.RA == newStudent.RA);

            Assert.NotNull(studentDB);
        }

        [Fact]
        public async Task PostStudent_ShouldNotAdd_WhenStudentRaAlredyExists()
        {
            //Arrange
            var controller = new StudentsController(_context);
            Student baseStudent = new Student { Name = "Raissa Gonçalves", CPF = "12378421211", Email = "testera@gmail.com", RA = "19003466" };
            _context.Students.Add(baseStudent);
            _context.SaveChanges();

            Student newStudent = new Student { Name = "Aluno 2", CPF = "98564315201", Email = "testera2@gmail.com", RA = "19003466" };

            //Act
            await controller.PostStudent(newStudent);

            //Assert
            var storedStudent = _context.Students.FirstOrDefault(x => x.CPF == newStudent.CPF);
            Assert.Null(storedStudent);
        }

        [Fact]
        public async Task PutStudent_ShouldEdit_StudentInDatabase()
        {
            //Arrange
            var oldName = "João Mesquita";
            var newName = "Lucas Souza";
            var controller = new StudentsController(_context);
            Student student = new Student { Name = oldName, CPF = "45612378910", Email = "john@outlook.com", RA = "24001594" };

            _context.Students.Add(student);
            _context.SaveChanges();

            //Act
            student.Name = newName;
            await controller.PutStudent(student.Id, student);

            //Assert
            var storedStudent = _context.Students.First(x => x.RA == student.RA);
            Assert.Equal(newName, storedStudent.Name);
            Assert.NotEqual(oldName, storedStudent.Name);
        }

        [Fact]
        public async Task DeleteStudent_ShouldDelete_StudentFromDatabase()
        {
            //Arrange
            Student student = new Student { Name = "Eduardo Costa", CPF = "74823615912", Email = "teste@gmail.com", RA = "24025014" };
            var controller = new StudentsController(_context);

            _context.Students.Add(student);
            _context.SaveChanges();

            //Act
            await controller.DeleteStudent(student.Id);

            //Assert
            var storedStudent = _context.Students.FirstOrDefault(x => x.RA == student.RA);
            Assert.Null(storedStudent);
        }
    }
}
