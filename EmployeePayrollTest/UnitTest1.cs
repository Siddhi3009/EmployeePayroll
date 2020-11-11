using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepo repo = new EmployeeRepo();
        [TestMethod]
        public void EmployeeDatabase_WhenViewed_ShouldReturnListOfEmployees()
        {
            List<EmployeeModel> employees = repo.GetAllEmployee();
            Assert.AreEqual(employees.Count(), 8);
        }
        [TestMethod]
        public void GivenNameAndUpdatedSalary_WhenUpdated_ShouldSyncWithDatabase()
        {
            bool updateResult = repo.UpdateSalary("Terissa", Convert.ToDouble("300000"));
            bool expected = true;
            Assert.AreEqual(updateResult, expected);
        }
        [TestMethod]
        public void GivenNewEmployee_WhenAdded_ShouldSyncWithDB()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                EmployeeName = "Mark",
                PhoneNumber = "9568214587",
                Address = "Panagar",
                Department = "Sales",
                Gender = 'M',
                BasicPay = 50000,
                Deductions = 200,
                TaxablePay = 49800,
                Tax = 500,
                NetPay = 49300,
                StartDate = Convert.ToDateTime("8-5-2019")
            };
            bool result = repo.AddEmployee(employee);
            List<EmployeeModel> employeeAdded = repo.RetrieveDataByName("Mark");
            Assert.AreEqual(employeeAdded.Count(), 1);
        }
        [TestMethod]
        public void GivenDateRange_ShouldReturnListOfEmployeesWithStartingDateWithinRange()
        {
            DateTime startDate = Convert.ToDateTime("01-01-2018");
            DateTime endDate = Convert.ToDateTime("01-01-2019");
            List<EmployeeModel> employees = repo.RetrieveEmployeesWithParticularDateRange(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            Assert.AreEqual(employees.Count(), 1);
        }
        [TestMethod]
        public void GivenMultipleEmployees_WhenAdded_ShouldReturnNoOfEmployeesAdded()
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            EmployeeModel employee1 = new EmployeeModel();
            employee1.EmployeeName = "Ganesh";
            employee1.Department = "Marketting";
            employee1.PhoneNumber = "7845985625";
            employee1.Address = "Mathura";
            employee1.Gender = 'M';
            employee1.BasicPay = 500000M;
            employee1.Deductions = 0.2M * employee1.BasicPay;
            employee1.TaxablePay = employee1.BasicPay - employee1.Deductions;
            employee1.Tax = 0.1M * employee1.TaxablePay;
            employee1.NetPay = employee1.BasicPay - employee1.Tax;
            employee1.StartDate = Convert.ToDateTime("23-10-2018");
            employee1.DepartmentId = 516;
            empList.Add(employee1);
            EmployeeModel employee2 = new EmployeeModel();
            employee2.EmployeeName = "Kanishk";
            employee2.Department = "Construction";
            employee2.PhoneNumber = "9856985685";
            employee2.Address = "Hyderabad";
            employee2.Gender = 'M';
            employee2.BasicPay = 400000M;
            employee2.Deductions = 0.2M * employee2.BasicPay;
            employee2.TaxablePay = employee2.BasicPay - employee2.Deductions;
            employee2.Tax = 0.1M * employee2.TaxablePay;
            employee2.NetPay = employee2.BasicPay - employee2.Tax;
            employee2.StartDate = Convert.ToDateTime("08-09-2019");
            employee2.DepartmentId = 584;
            empList.Add(employee2);
            //without thread addition
            DateTime startDateTime = DateTime.Now;
            int noOfEmployeesAdded = repo.AddMultipleEmployees(empList);
            DateTime stopDateTime = DateTime.Now;
            Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));
            Assert.AreEqual(noOfEmployeesAdded, 2);
        }
        [TestMethod]
        public void GivenMultipleEmployees_WhenAddedUsingThread_ShouldReturnNoOfEmployeesAdded()
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            EmployeeModel employee1 = new EmployeeModel();
            employee1.EmployeeName = "Ganesh";
            employee1.Department = "Marketting";
            employee1.PhoneNumber = "7845985625";
            employee1.Address = "Mathura";
            employee1.Gender = 'M';
            employee1.BasicPay = 500000M;
            employee1.Deductions = 0.2M * employee1.BasicPay;
            employee1.TaxablePay = employee1.BasicPay - employee1.Deductions;
            employee1.Tax = 0.1M * employee1.TaxablePay;
            employee1.NetPay = employee1.BasicPay - employee1.Tax;
            employee1.StartDate = Convert.ToDateTime("23-10-2018");
            employee1.DepartmentId = 517;
            empList.Add(employee1);
            EmployeeModel employee2 = new EmployeeModel();
            employee2.EmployeeName = "Kanishk";
            employee2.Department = "Construction";
            employee2.PhoneNumber = "9856985685";
            employee2.Address = "Hyderabad";
            employee2.Gender = 'M';
            employee2.BasicPay = 400000M;
            employee2.Deductions = 0.2M * employee2.BasicPay;
            employee2.TaxablePay = employee2.BasicPay - employee2.Deductions;
            employee2.Tax = 0.1M * employee2.TaxablePay;
            employee2.NetPay = employee2.BasicPay - employee2.Tax;
            employee2.StartDate = Convert.ToDateTime("08-09-2019");
            employee2.DepartmentId = 585;
            empList.Add(employee2);
            DateTime startDateTimeThread = DateTime.Now;
            int noOfEmployeesAddedUsingThread = repo.AddMultipleEmployeesUsingThread(empList);
            DateTime stopDateTimeThread = DateTime.Now;
            Console.WriteLine("Duration with thread: " + (stopDateTimeThread - startDateTimeThread));
            Assert.AreEqual(noOfEmployeesAddedUsingThread, 2);
        }

    }
}