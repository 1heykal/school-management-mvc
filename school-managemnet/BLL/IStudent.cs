using SchoolManagement.Models;
using System.Collections.Generic;

namespace SchoolManagement.BLL
{
    public interface IStudent
    {
        public Task<Student> GetById(int id);


        public IQueryable<Student> GetAll();


        public Task<Student> Add(Student student);


        public Task<Student> Edit(Student student);


        public Task Delete(int id);

    }
}
