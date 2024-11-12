using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public User()
        {

        }

        public override string ToString()
        {
            if (IsActive == true)
            {
                return $"ID: {Id} | Username: {Username} | Password: {Password} | Status: Active";
            }
            else
            {
                return $"ID: {Id} | Username: {Username} | Password: {Password} | Status: Inactive";
            }
        }
    }
}
