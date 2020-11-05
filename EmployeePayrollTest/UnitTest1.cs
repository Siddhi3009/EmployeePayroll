using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollService;
using System;

namespace EmployeePayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepo repo = new EmployeeRepo();
        [TestMethod]
        public void GivenNameAndUpdatedSalary_ShouldUpdateSalaryInDatabase()
        {
            bool updateResult = repo.UpdateSalary("Terissa", Convert.ToDecimal("300000"));
            bool expected = true;
            Assert.AreEqual(updateResult, expected);
        }
    }
}
