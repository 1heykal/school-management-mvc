using student_registration.Models;
using System.Collections.Generic;

namespace student_registration.BLL
{
    public interface IStudent
    {
        public Student GetByID(int Id);


        public List<Student> GetAll();


        public Student Add(Student student);


        public Student Edit(Student student);


        public void Delete(int id);
       
    }
}
