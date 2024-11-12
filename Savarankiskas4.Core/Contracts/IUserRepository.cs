using Savarankiskas4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Contracts
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserById(int id);
        List<User> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUser(int id);
        void ChangePassword(int id, string newPassword);
        void SetUserStatus(int id, bool isActive);
    }
}
