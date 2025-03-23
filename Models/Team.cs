using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class Team : List<Employee>
    {

        public Team(): base() {
            Add(new Employee("Adam", "Davidson", new DateOnly(), "15000", "Programista", "Umowa zlecenie"));
        }

        public void AddEmployee(Employee employee)
        {
            Add(employee);
        }

    }
}
