using SchoolManagement.Models;
using System.Collections.Generic;

namespace SchoolManagement.BLL
{
    public interface IDepartment
    {
        public Department GetById(int id);

        public List<Department> GetAll();

        public Department Add(Department department);

        public Department Edit(Department department);

        public void Delete(int id);

    }
}
