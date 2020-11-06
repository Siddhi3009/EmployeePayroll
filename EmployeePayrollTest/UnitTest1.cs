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
            bool updateResult = repo.UpdateSalary("Terissa", Convert.ToDecimal("300000"));
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
    }
}

