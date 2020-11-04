using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Employee Payroll Service System");
            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployee();
        }
    }
}
