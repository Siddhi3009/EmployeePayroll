using System;
namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Employee Payroll Service System");
            EmployeeRepo repo = new EmployeeRepo();
            int loop = 1;
            while (loop == 1)
            {
                Console.WriteLine("Choose \n1. View all records \n2. Add record \n3. Update salary \n4. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        repo.GetAllEmployee();
                        break;
                    case 2:
                        EmployeeModel employee = new EmployeeModel();
                        Console.WriteLine("Enter Name");
                        employee.EmployeeName = Console.ReadLine();
                        Console.WriteLine("Enter Department");
                        employee.Department = Console.ReadLine();
                        Console.WriteLine("Enter Phone Number");
                        employee.PhoneNumber = Console.ReadLine();
                        Console.WriteLine("Enter Address");
                        employee.Address = Console.ReadLine();
                        Console.WriteLine("Enter Gender");
                        employee.Gender = Convert.ToChar(Console.ReadLine());
                        Console.WriteLine("Enter Basic Pay");
                        employee.BasicPay = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter Deductions");
                        employee.Deductions = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter Start date");
                        employee.StartDate = Convert.ToDateTime(Console.ReadLine());
                        if (repo.AddEmployee(employee))
                            Console.WriteLine("Records added successfully");
                        break;
                    case 3:
                        string name = Console.ReadLine();
                        decimal salary = Convert.ToDecimal(Console.ReadLine());
                        repo.UpdateSalary(name, salary);
                        break;
                    case 4:
                        loop = 0;
                        break;
                }
            }
        }
    }
}
