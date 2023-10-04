using student_registration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace student_registration.BLL
{
    public class DepartmentBLL : IDepartment
    {
        ITIContext context = new();

        public Department GetByID(int Id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == Id);
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department Add(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
            return department;
        }

        public Department Edit(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
            return department;
        }

        public void Delete(int id)
        {
            var dept = GetByID(id);
            if(!context.Students.Any(s => s.Dno == id))
            {
                context.Departments.Remove(dept);
                context.SaveChanges();
            }

            
      
        }
    }
}
