using System;
using System.Collections.Generic;

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
                Console.WriteLine("Choose \n1. View all records \n2. Add record \n3. Update salary \n4. Retrieve information from name \n5. Retrieve Employees with joining date in a range \n6. Sum of basic pay gender wise \n7. Average of basic pay gender wise \n8. Minimum basic pay gender wise \n9. Maximum basic pay gender wise \n10. Count of employees gender wise \n11. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        List<EmployeeModel> employeeList = repo.GetAllEmployee();
                        foreach (EmployeeModel employeeModel in employeeList)
                        {
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
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
                        Console.WriteLine("Enter Name");
                        string name = Console.ReadLine(); 
                        Console.WriteLine("Enter Basic Pay");
                        decimal salary = Convert.ToDecimal(Console.ReadLine());
                        bool result = repo.UpdateSalary(name, salary);
                        Console.WriteLine(result == true ? "Salary Updated" : "Salary cannot be updated"); 
                        break;
                    case 4:
                        Console.WriteLine("Enter Name");
                        string employeeName = Console.ReadLine();
                        List<EmployeeModel> employeeNameList = repo.RetrieveDataByName(employeeName);
                        foreach (EmployeeModel employeeInfo in employeeNameList)
                        {
                            System.Console.WriteLine(employeeInfo.EmployeeName + " " + employeeInfo.BasicPay + " " + employeeInfo.StartDate + " " + employeeInfo.Gender + " " + employeeInfo.PhoneNumber + " " + employeeInfo.Address + " " + employeeInfo.Department + " " + employeeInfo.Deductions + " " + employeeInfo.TaxablePay + " " + employeeInfo.Tax + " " + employeeInfo.NetPay);
                            System.Console.WriteLine("\n");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter Start date");
                        DateTime startDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Enter End date");
                        DateTime endDate = Convert.ToDateTime(Console.ReadLine());
                        List<EmployeeModel> employeeWithStartDateList = repo.RetrieveEmployeesWithParticularDateRange(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                        foreach (EmployeeModel employeeModel in employeeWithStartDateList)
                        {
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                        break;
                    case 6:
                        repo.SumOfSalaryGenderWise();
                        break;
                    case 7:
                        repo.AverageOfSalaryGenderWise();
                        break;
                    case 8:
                        repo.MinimumSalaryGenderWise();
                        break;
                    case 9:
                        repo.MaximumSalaryGenderWise();
                        break;
                    case 10:
                        repo.CountOfEmployeesGenderWise();
                        break;
                    case 11:
                        loop = 0;
                        break;
                }
            }
        }
    }
}
