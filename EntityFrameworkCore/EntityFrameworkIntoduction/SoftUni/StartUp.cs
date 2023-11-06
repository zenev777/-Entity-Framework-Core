using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployee147(context));
        }

        //03. Employees Full Information
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

        //04. Employees with Salary Over 50 000
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

        //05. Employees from Research and Development
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

        //06. Adding a New Address and Updating Employee
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
        
        //unsloved   07. Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                }).ToList();

            var projects = context.Projects.Select(p => new
            {
                p.Name,
                p.StartDate,
                p.EndDate
            }).ToList();

            StringBuilder sb = new StringBuilder();


            //"--<ProjectName> - <StartDate> - <EndDate>
            foreach(var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName:f2}");
                foreach (var p in projects)
                {
                    var endDateStr = p.EndDate.HasValue ? p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                    sb.AppendLine($"--{p.Name} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDateStr}");
                }
            } 
                

            return sb.ToString().Trim();
        }

        //08. Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {

            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    Town = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                }).OrderByDescending(a=>a.EmployeesCount)
                .ThenBy(a=>a.Town)
                .Take(10)
                .ToList();

            //"<AddressText>, <TownName> - <EmployeeCount> employees"
            string result = string.Join(Environment.NewLine, addresses.Select((a => $"{a.AddressText}, {a.Town} - {a.EmployeesCount} employees")));

            return result;
        }

        //09. Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {

            var employees = context.Employees.Include(e => e.EmployeesProjects).Where(e => e.EmployeeId == 147)
                    .FirstOrDefault();

            StringBuilder sb = new StringBuilder();


       
            
                sb.AppendLine($"{employees.FirstName} {employees.LastName} - {employees.JobTitle}");
                foreach (var ep in employees.EmployeesProjects)
                {
                    sb.AppendLine(ep.Project.Name);
                }
            


            return sb.ToString().Trim();
        }

        //13. Find Employees by First Name Starting With Sa
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {

            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                }).Where(e => e.FirstName.StartsWith("Sa")).OrderBy(e => e.FirstName).ThenBy(e=>e.LastName).ToList();

            string result = string.Join(Environment.NewLine, employees.Select((e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})")));

            return result;
        }



    }
}