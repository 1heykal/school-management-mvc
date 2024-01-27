using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using student_registration.Models;

namespace student_registration.ViewModels
{
    public class StudentDepartmentViewModel
    {
        public Student student { get; set; }
        public Department department { get; set; }

    }
}
