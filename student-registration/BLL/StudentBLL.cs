using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;

namespace SchoolManagement.BLL
{

    public class StudentBLL : IStudent
    {
        private readonly SchoolContext _context;

        public StudentBLL(SchoolContext context)
        {
            _context = context;
        }

        public Student GetByID(int Id)
        {
            return _context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == Id);
        }

        public List<Student> GetAll()
        {
            return _context.Students.Include(s => s.Department).ToList();
        }

        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Edit(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return student;
        }

        public void Delete(int id)
        {
            var student = GetByID(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

    }
}
