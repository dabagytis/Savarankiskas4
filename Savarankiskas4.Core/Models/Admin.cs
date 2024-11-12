using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskas4.Core.Models
{
    public class Admin : User
    {
        public string Role { get; set; } = "Administrator";

        public override string ToString()
        {
            if (IsActive == true)
            {
                return $"ID: {Id} | Username: {Username} | Password: {Password} | Status: Active | Role: {Role}";
            }
            else
            {
                return $"ID: {Id} | Username: {Username} | Password: {Password} | Status: Inactive | Role: {Role}";
            }
        }
    }
}
