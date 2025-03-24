using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class Employee
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Salary { get; set; }

        public string? Position { get; set; }

        public string? Contract { get; set; }
        public Employee(){
        }
        public Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Salary, string Position, string Contract)
        {
            Id = Guid.NewGuid().ToString();
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Salary = Salary;
            this.Position = Position;
            this.Contract = Contract;
        }
        public Employee(string Id, string FirstName, string LastName, DateOnly DateOfBirth, string Salary, string Position, string Contract)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Salary = Salary;
            this.Position = Position;
            this.Contract = Contract;
        }
    }
}
