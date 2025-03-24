using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class Team : ObservableCollection<Employee>
    {

        public Team(): base() {
            //Add(new Employee("Adam", "Davidson", DateOnly.Parse("13.03.2001"), "15000", "Programista", "Umowa zlecenie"));
            //Add(new Employee("Emilia", "Backwoods", DateOnly.Parse("21.04.1991"), "9000", "Programista", "Umowa zlecenie"));
            //Add(new Employee("Kamil", "Stan", DateOnly.Parse("23.12.1999"), "17000", "Tester", "Umowa na czas nieokreślony"));
            //Add(new Employee("Wikotria", "Avioli", DateOnly.Parse("06.09.2003"), "15000", "Programista", "Umowa zlecenie"));
            //Add(new Employee("Daniel", "Menders", DateOnly.Parse("19.01.2002"), "25000", "Programista", "Umowa zlecenie"));
        }

        public void AddEmployee(Employee employee)
        {
            Add(employee);
        }

    }
}
