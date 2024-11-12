using Microsoft.Data.SqlClient;
using Savarankiskas4.Core.Contracts;
using Savarankiskas4.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Methods

        public void ActivateUser(int id)
        {
            bool isActive = true;
            _userRepository.SetUserStatus(id, isActive);
        }

        public void DeactivateUser(int id)
        {
            bool isActive = false;
            _userRepository.SetUserStatus(id, isActive);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUserById(id);
        }

        // Klaidingas metodas
        public List<User> ListUsersByRole(string role)
        {
            List<User> usersByRole = new List<User>();
            List<User> allUsers = GetAllUsers();
            if(role == "Administrator")
            {
                foreach (User a in allUsers)
                {
                    if (a is Admin)
                    {
                        usersByRole.Add(a);
                    }
                }
            }
            else if(role == "Standard User")
            {
                foreach (User a in allUsers)
                {
                    if (a is StandardUser)
                    {
                        usersByRole.Add(a);
                    }
                }
            }
            return usersByRole;
        }

        public void RegisterUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public void RemoveUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void UpdatePassword(int id, string newPassword)
        {
            _userRepository.ChangePassword(id, newPassword);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
    }
}
