using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Salary, string Position, string Contract)
    {
        public int Id { get; private set; }
        public string? FirstName { get; set; } = FirstName;

        public string? LastName { get; set; } = LastName;

        public DateOnly? DateOfBirth { get; set; } = DateOfBirth;

        public string? Salary { get; set; } = Salary;

        public string? Position { get; set; } = Position;

        public string? Contract { get; set; } = Contract;
    }
}
