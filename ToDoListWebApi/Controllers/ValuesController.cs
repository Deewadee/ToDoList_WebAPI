using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static DatabaseProvider provider = new DatabaseProvider();

        private static IConfiguration configuration;

        public ValuesController(IConfiguration config)
        {
            configuration = config;
        }

        [HttpGet]
        public List<Todo> Get()
        {
            List<Todo> todos = GetTodos();

            return todos;
        }

        [HttpGet("{date}")]
        public List<Todo> Get([FromQuery]Todo todo)
        {
            List<Todo> todos = GetTodosByDate(todo.TodoDate.ToShortDateString());

            return todos;
        }

        [HttpPost]
        public Todo Post(Todo newTodo)
        {
            string connectionString = GetConnectionString();

            provider.CreateTodo(newTodo, connectionString);

            Todo todo = GetLastAddedTodo();

            return todo;
        }

        [HttpPut]
        public void Put(Todo todo)
        {
            string connectionString = GetConnectionString();

            provider.UpdateTodo(todo, connectionString);
        }

        [HttpDelete]
        public void DeleteTodos()
        {
            string connectionString = GetConnectionString();

            provider.DeleteTodos(connectionString);
        }

        [HttpDelete("{id}")]
        public void DeleteTodo(Todo todo)
        {
            string connectionString = GetConnectionString();

            provider.DeleteTodo(todo.TodoId, connectionString);
        }

        private static List<Todo> GetTodos()
        {
            string connectionString = GetConnectionString();

            DataTable todos = provider.GetTodos("GetTodos", connectionString);

            List<Todo> listTodos = new List<Todo>();

            foreach(DataRow row in todos.Rows)
            {
                Todo todo = new Todo();

                todo.TodoId = row.Field<int>("TodoId");
                todo.TodoDescription = row.Field<string>("TodoDescription");
                todo.TodoStatus = row.Field<string>("TodoStatus");
                todo.TodoDate = row.Field<DateTime>("TodoDate");

                listTodos.Add(todo);
            }

            return listTodos;
        }

        private static List<Todo> GetTodosByDate(string todosDate)
        {
            string connectionString = GetConnectionString();

            DataTable todos = provider.GetTodosByDate(todosDate, connectionString);

            List<Todo> listTodos = new List<Todo>();

            foreach (DataRow row in todos.Rows)
            {
                Todo todo = new Todo();

                todo.TodoId = row.Field<int>("TodoId");
                todo.TodoDescription = row.Field<string>("TodoDescription");
                todo.TodoStatus = row.Field<string>("TodoStatus");
                todo.TodoDate = row.Field<DateTime>("TodoDate");

                listTodos.Add(todo);
            }

            return listTodos;
        }

        private static Todo GetLastAddedTodo()
        {
            string connectionString = GetConnectionString();

            DataTable todos = provider.GetTodos("GetLastAddedTodo", connectionString);
            
            Todo todo = new Todo();

            if (todos.Rows.Count == 1)
            {
                foreach (DataRow row in todos.Rows)
                {              
                    todo.TodoId = row.Field<int>("TodoId");
                    todo.TodoDescription = row.Field<string>("TodoDescription");
                    todo.TodoStatus = row.Field<string>("TodoStatus");
                    todo.TodoDate = row.Field<DateTime>("TodoDate");
                }
            }

            return todo;
        }

        private static string GetConnectionString()
        {
            return configuration.GetSection("ConnectionStrings").GetSection("TodosDbConnection").Value.ToString();
        }
    }
}
