using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;

namespace SchoolManagement.BLL
{
    public class DepartmentBLL : IDepartment
    {
        private readonly SchoolContext _context;

        public DepartmentBLL(SchoolContext context)
        {
            _context = context;
        }

        public Department GetByID(int Id)
        {
            return _context.Departments.Find(Id);
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public Department Edit(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return department;
        }

        public void Delete(int id)
        {
            var dept = GetByID(id);
            if (!_context.Students.Any(s => s.Dno == id))
            {
                _context.Departments.Remove(dept);
                _context.SaveChanges();
            }



        }
    }
}
