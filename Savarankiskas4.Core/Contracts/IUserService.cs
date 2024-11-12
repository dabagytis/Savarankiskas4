using Savarankiskas4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Contracts
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User GetUser(int id);
        List<User> GetAllUsers();
        void UpdateUser(User user);
        void RemoveUser(int id);
        void UpdatePassword(int id, string newPassword);
        void ActivateUser(int id);
        void DeactivateUser(int id);
        List<User> ListUsersByRole(string role);
    }
}
