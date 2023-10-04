using student_registration.Models;
using System.Collections.Generic;

namespace student_registration.BLL
{
    public interface IDepartment
    {
        public Department GetByID(int Id);

        public List<Department> GetAll();
      
        public Department Add(Department department);

        public Department Edit(Department department);

        public void Delete(int id);
        
    }
}
