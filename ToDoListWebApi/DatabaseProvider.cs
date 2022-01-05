using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ToDoListWebApi
{
    public class DatabaseProvider
    {
        public DatabaseProvider()
        {
        }

        public DataTable GetTodos(string storedProcedureName, string connectionString)
        {
            DataTable todos = new DataTable("Todos");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(todos);
            }

            return todos;
        }

        public DataTable GetTodosByDate(string todosDate, string connectionString)
        {
            DataTable todos = new DataTable("Todos");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetTodosByDate", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter todosDateParam = new SqlParameter
                {
                    ParameterName = "@todoDate",
                    Value = todosDate
                };

                command.Parameters.Add(todosDateParam);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(todos);
            }

            return todos;
        }

        public async void DeleteTodos(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DeleteTodos", connection);
                command.CommandType = CommandType.StoredProcedure;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async void DeleteTodo(int todoId, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DeleteTodo", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter todoIdParam = new SqlParameter
                {
                    ParameterName = "@todoId",
                    Value = todoId
                };

                command.Parameters.Add(todoIdParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public void CreateTodo(Todo todo, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CreateTodo", connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlParameter todoDescriptionParam = new SqlParameter
                {
                    ParameterName = "@todoDescription",
                    Value = todo.TodoDescription
                };

                SqlParameter todoStatusParam = new SqlParameter
                {
                    ParameterName = "@todoStatus",
                    Value = todo.TodoStatus
                };

                SqlParameter todoDateParam = new SqlParameter
                {
                    ParameterName = "@todoDate",
                    Value = todo.TodoDate
                };

                command.Parameters.Add(todoDescriptionParam);
                command.Parameters.Add(todoStatusParam);
                command.Parameters.Add(todoDateParam);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateTodo(Todo todo, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateTodo", connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlParameter todoIdParam = new SqlParameter
                {
                    ParameterName = "@todoId",
                    Value = todo.TodoId
                };

                SqlParameter todoDescriptionParam = new SqlParameter
                {
                    ParameterName = "@todoDescription",
                    Value = todo.TodoDescription
                };

                SqlParameter todoStatusParam = new SqlParameter
                {
                    ParameterName = "@todoStatus",
                    Value = todo.TodoStatus
                };

                command.Parameters.Add(todoIdParam);
                command.Parameters.Add(todoDescriptionParam);
                command.Parameters.Add(todoStatusParam);

                command.ExecuteNonQuery();
            }
        }
    }
}
