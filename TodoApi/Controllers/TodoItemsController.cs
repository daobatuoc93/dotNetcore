using System;
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
        static List<Student> students = StudentService.students;
        private IStudentService studentService;
        
        public IEnumerable<Student> Newallsv { get; private set; }

        public TodoItemsController(IStudentService studentService, TodoContext context)
        {
            this.studentService = studentService;
            acontext = context;            
        }
     
        [HttpGet("GetAllSV")]
        public IEnumerable<Student> GetAllSV()
        {
            return studentService.GetStudents();
        }
        //
        [HttpGet("Score/{Average}")]
        public IEnumerable<Student> GetDataAverageScore(float average)
        {
            return studentService.GetScoreAverage(average);
        }
        [HttpGet("Findid/{id}")]
        public IEnumerable<Student> Find(long id)
        {
            return studentService.Find(id);

        }
        [HttpDelete("Delete/{id}")]
        public IEnumerable<Student> DeleteStudent(long id)
        {
            return studentService.DeleteStudent(id);
        }
        [HttpPut("Fix/{id}")]
        public IEnumerable<ActionResult<string>> FixStudent(Student Newstudent)
        {
            return studentService.FixStudent(Newstudent);
        }
        [HttpPost("AddSv")]
        public IEnumerable<Student> AddSv(Student Sv)
        {
            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return studentService.AddSv(Sv);
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
