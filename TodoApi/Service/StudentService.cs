using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Enums;

namespace TodoApi.Service
{
    public class StudentService : IStudentService
    {
        public static List<Student> students = new List<Student>
        {
        new Student { LastName = "Adams", Id = 120,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 99, 82, 81, 79}},
        new Student {FirstName = "Fadi", LastName = "Fakhouri", Id = 116,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 99, 86, 90, 94}},
        new Student {FirstName = "Hanying", LastName = "Feng", Id = 117,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 93, 92, 80, 87}},
        new Student {FirstName = "Cesar", LastName = "Garcia", Id = 114,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 97, 89, 85, 82}},
        new Student {FirstName = "Debra", LastName = "Garcia", Id = 115,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 35, 72, 91, 70}},
        new Student {FirstName = "Hugo", LastName = "Garcia", Id = 118,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 92, 90, 83, 78}},
        new Student {FirstName = "Sven", LastName = "Mortensen", Id = 113,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 88, 94, 65, 91}},
        new Student {FirstName = "Claire", LastName = "O'Donnell", Id = 112,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 75, 84, 91, 39}},
        new Student {FirstName = "Svetlana", LastName = "Omelchenko", Id = 111,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 97, 92, 81, 60}},
        new Student {FirstName = "Lance", LastName = "Tucker", Id = 119,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 68, 79, 88, 92}},
        new Student {FirstName = "Michael", LastName = "Tucker", Id = 122,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 94, 92, 91, 91}},
        new Student {FirstName = "Eugene", LastName = "Zabokritski", Id = 121,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 96, 85, 91, 60}}
            };

        public IList<Student> AddSv(Student Sv)
        {
            students.Add(Sv);
            return students;
        }

        public IEnumerable<Student> DeleteStudent(long id)
        {
            var CheckifId = from student in students
                            group student by student.Id == id;
            foreach (var studentGroup in CheckifId)
            {
                if (studentGroup.Key == true)
                {
                    foreach (var student in studentGroup)
                    {
                        students.Remove(student);
                    }
                }
                else
                {
                    foreach (var student in studentGroup)
                    {
                        yield return student;
                    }
                }

            }
        }

        public IEnumerable<Student> Find(long id)
        {
            var findId = from student in students
                         where student.Id == id
                         select student;

            foreach (var student in findId)
            {
                yield return student;
            }
        }

        public IEnumerable<ActionResult<string>> FixStudent(Student Newstudent)
        {
            var CheckifId = from student in students
                            group student by student.Id == Newstudent.Id;
            string Complete = "Student has been changed!";
            string Completechanged = "Finished!";
            string Isnotneeddedchangingstudent = "Is not needded changing student!";
            foreach (var studentGroup in CheckifId)
            {
                if (studentGroup.Key == true)
                {
                    foreach (var student in studentGroup)
                    {
                        student.Id = Newstudent.Id;
                        student.FirstName = Newstudent.FirstName;
                        student.LastName = Newstudent.LastName;
                        student.ExamScores = Newstudent.ExamScores;
                    }
                    yield return Complete;
                }
                else
                    yield return Isnotneeddedchangingstudent;
            }
            yield return Completechanged;
        }

        public IEnumerable<Student> GetScoreAverage(float average)
        {
            var booleanGroupQuery = from student in students
                                    group student by student.ExamScores.Average() > average;
            foreach (var studentGroup in booleanGroupQuery)
            {
                if (studentGroup.Key == true)
                {
                    foreach (var student in studentGroup)
                    {
                        yield return student;
                    }
                }
            }
        }

        public IList<Student> GetStudents()
        {
            return students;
        }
    }
}
