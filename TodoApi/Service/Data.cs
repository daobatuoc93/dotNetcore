using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoApi.Service
{
    public class Student
    {
        #region data
        public enum GradeLevel { FirstYear = 1, SecondYear, ThirdYear, FourthYear };

        public  string FirstName { get; set; } = "lack of First name for this object!";
        public string LastName { get; set; }
        public long Id { get; set; }
        public GradeLevel Year;
        public List<int> ExamScores { get; set; }

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
        
        #endregion

        // Helper method, used in GroupByRange.
        public int GetPercentile(Student s)
        {
            double avg = s.ExamScores.Average();
            return avg > 0 ? (int)avg / 10 : 0;
        }
        //Get all Data of students
        public static List<Student> Getallstudent()
        {
            return students;
        }
        //Add a new student
        public List<Student> addNewStudent(Student X)
        {
            students.Add(X);
            return students;
        }

        //Get students have a HighScore(Average>score setting) 
    }
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }

    /// <summary>
    /// This example creates XML output from a grouped join.
    /// </summary>
    public class XML
    {
       public XElement GroupJoinXMLExample()
        {
        Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
        Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
        Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
        Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };

        Pet barley = new Pet { Name = "Barley", Owner = terry };
        Pet boots = new Pet { Name = "Boots", Owner = terry };
        Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
        Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
        Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

        // Create two lists.
        List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
        List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

        // Create XML to display the hierarchical organization of people and their pets.
        XElement ownersAndPets = new XElement("PetOwners",
            from person in people
            join pet in pets on person equals pet.Owner into gj
            select new XElement("Person",
                new XAttribute("FirstName", person.FirstName),
                new XAttribute("LastName", person.LastName),
                from subpet in gj
                select new XElement("Pet", subpet.Name)));

        return ownersAndPets;
            }
    }
}

