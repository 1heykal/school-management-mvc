using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;

namespace SchoolManagement.BLL
{

    public class StudentBll : IStudent
    {
        private readonly SchoolContext _context;

        public StudentBll(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.
                Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> Add(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Edit(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task Delete(int id)
        {
            var student = await GetById(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

    }
}
