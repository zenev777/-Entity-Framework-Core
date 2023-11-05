using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(AddNewAddressToEmployee(context));
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                }).Where(s => s.Salary > 50000).OrderBy(e => e.FirstName).ToList();

            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.FirstName} - {e.Salary:f2}")));

            return result;
        }


        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                }).ToList();

            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}")));

            return result;
        }



        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                }).Where(e => e.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:f2}")));

            return result;
        }


        public static string AddNewAddressToEmployee(SoftUniContext context)
        {

            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
               .FirstOrDefault(e => e.LastName == "Nakov");


            employee.Address = address;

            context.SaveChanges();

            var employees = context.Employees.Select(e => new
            {
                e.AddressId,
                e.Address.AddressText

            })
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .ToList();


            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.AddressText}")));

            return result;
        }



        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
               .FirstOrDefault(e => e.LastName == "Nakov");


            employee.Address = address;

            context.SaveChanges();

            var employees = context.Employees.Select(e => new
            {
                e.AddressId,
                e.Address.AddressText

            })
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .ToList();


            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.AddressText}")));

            return result;
        }
    }
}