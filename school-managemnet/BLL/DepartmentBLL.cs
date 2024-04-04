using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;

namespace SchoolManagement.BLL
{
    public class DepartmentBll : IDepartment
    {
        private readonly SchoolContext _context;

        public DepartmentBll(SchoolContext context)
        {
            _context = context;
        }

        public Department GetById(int id)
        {
            return null; //_context.Departments.Find(Id);
        }

        public List<Department> GetAll()
        {
            return null; //  _context.Departments.ToList();
        }

        public Department Add(Department department)
        {
         //   _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public Department Edit(Department department)
        {
           // _context.Departments.Update(department);
            _context.SaveChanges();
            return department;
        }

        public void Delete(int id)
        {
            var dept = GetById(id);
         //   _context.Departments.Remove(dept);
            _context.SaveChanges();
            



        }
    }
}
