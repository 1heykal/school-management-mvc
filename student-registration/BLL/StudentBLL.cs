using student_registration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace student_registration.BLL
{

    public class StudentBLL : IStudent
    {
        ITIContext context = new();

        public Student GetByID(int Id)
        {
            return context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == Id);
        }

        public List<Student> GetAll() 
        {
            return context.Students.Include(s => s.Department).ToList();
        }

        public Student Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Edit(Student student)
        {
            context.Students.Update(student);
            context.SaveChanges();
            return student;
        }

        public void Delete(int id)
        {
            var student = GetByID(id);
            context.Students.Remove(student);
            context.SaveChanges();
        }

    }
}
