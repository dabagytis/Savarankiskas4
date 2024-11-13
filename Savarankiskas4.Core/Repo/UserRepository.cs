using Dapper;
using Microsoft.Data.SqlClient;
using Savarankiskas4.Core.Contracts;
using Savarankiskas4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Methods

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Each new added user is automatically set as Active
                if(user is Admin)
                {
                    connection.Execute("INSERT INTO Users (Username, Password, IsActive, Role) VALUES (@Username, @Password, 1, 'Administrator')", user);
                }
                else
                {
                    connection.Execute("INSERT INTO Users (Username, Password, IsActive, Role) VALUES (@Username, @Password, 1, 'Standard User')", user);
                }
            }
        }

        public void ChangePassword(int id, string newPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE Users SET Password = @newPassword WHERE Id = @id", new { id, newPassword });
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute("DELETE FROM Users WHERE Id = @id", new { id });
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                allUsers.AddRange(connection.Query<Admin>("SELECT * FROM Users WHERE Role = 'Administrator'").ToList());

                allUsers.AddRange(connection.Query<StandardUser>("SELECT * FROM Users WHERE Role = 'Standard User'").ToList());
            }
            return allUsers;
        }

        public User GetUserById(int id)
        {
            User userById = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    userById = connection.QueryFirst<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
                }
                catch
                {

                }
            }
            return userById;
        }

        public void SetUserStatus(int id, bool isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE Users SET IsActive = @isActive WHERE Id = @id", new { id, isActive });
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE Users SET Username = @Username , Password = @Password, IsActive = @IsActive WHERE Id = @Id", user);
            }
        }
    }
}
