using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Service
{
    public interface IStudentService
    {
        public IList<Student> GetStudents();
        public  IEnumerable<Student> GetScoreAverage(float average);
        public IEnumerable<Student> Find(long id);
        public IEnumerable<Student> DeleteStudent(long id);
        public IList<Student> AddSv(Student Sv);
        public IEnumerable<ActionResult<string>> FixStudent(Student Newstudent);
    }
}
