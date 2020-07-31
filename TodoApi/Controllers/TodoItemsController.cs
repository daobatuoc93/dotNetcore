﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Service;

namespace TodoApi.Controllers
{
    
    //[Route("")]
    //[Route("Home")]
    //[Route("Home/Index")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext acontext;
        static List<Student> students = Student.students;
        
        public IEnumerable<Student> Newallsv { get; private set; }

        public TodoItemsController(TodoContext context)
        {
            acontext = context;            
        }
     
        [HttpGet("GetAllSV")]
        public IEnumerable<Student> GetAllSV()
        {
            foreach (var student in students)
            {
                yield return student;
            }
        }
        //
        [HttpGet("Score/{Average}")]
        public IEnumerable<Student> GetDataAverageScore(float average)
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
        [HttpGet("Findid/{id}")]
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
        [HttpDelete("Delete/{id}")]
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
                    foreach(var student in studentGroup)
                    {
                        yield return student;
                    }
                }
                    
            }        
        }
        [HttpPost("AddSv")]
        public ActionResult<Student> AddSv(Student Sv)
        {
            students.Add(Sv);
            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction("Find",
                                   new { id = Sv.Id },
                                   Sv);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            
            return await acontext.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await acontext.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // Fix: api/TodoItems/Fix/{id}
        [HttpPut("Fix/{id}")]
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
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            acontext.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await acontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            acontext.TodoItems.Add(todoItem);
            await acontext.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await acontext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            acontext.TodoItems.Remove(todoItem);
            await acontext.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return acontext.TodoItems.Any(e => e.Id == id);
        }
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}