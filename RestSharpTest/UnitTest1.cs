using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollService;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace RestSharpTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client = new RestClient("http://localhost:3000");
        /// <summary>
        /// Interface to get list of employees in the json server
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);
            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// Test method to check the employee list retrieved from json server
        /// </summary>
        [TestMethod]
        public void OnCallingGetApi_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(16, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "Name: " + item.name + "Salary: " + item.Salary);
            }
        }
        /// <summary>
        /// Given employee must be added to the json server
        /// </summary>
        [TestMethod]
        public void GivenEmployee_WhenPosted_ShouldReturnAddedEmployee()
        {
            //arrange
            RestRequest request = new RestRequest("/Employees", Method.POST);
            JObject jObject = new JObject();
            jObject.Add("name", "Mark");
            jObject.Add("Salary", "20000");
            //act
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Mark", dataResponse.name);
            Assert.AreEqual(20000, dataResponse.Salary);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// Test method to check multiple added to json server
        /// </summary>
        [TestMethod]
        public void GivenMultipleEmployees_WhenPosted_ShouldReturnEmployeeListWithAddedEmployees()
        {
            //arrange
            List<Employee> list = new List<Employee>();
            list.Add(new Employee { name = "Mark", Salary = 20000 });
            list.Add(new Employee { name = "Chopper", Salary = 15000 });
            foreach (Employee employee in list)
            {
                //act
                RestRequest request = new RestRequest("/employees/create", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("name", employee.name);
                jObject.Add("salary", employee.Salary);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(employee.name, dataResponse.name);
                Assert.AreEqual(employee.Salary, dataResponse.Salary);
            }
        }
        /// <summary>
        /// Test method to check updated salary of employee
        /// </summary>
        [TestMethod]
        public void GivenEmployee_WhenUpdated_ShouldReturnUpdatedEmployee()
        {
            //arrange
            RestRequest request = new RestRequest("/Employees/22", Method.PUT);
            JObject jObject = new JObject();
            jObject.Add("name", "Chopper");
            jObject.Add("Salary", "16000");
            //act
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Chopper", dataResponse.name);
            Assert.AreEqual(16000, dataResponse.Salary);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// test method to check deletion of employee
        /// </summary>
        [TestMethod]
        public void GivenEmployee_WhenDeleted_ShouldReturnStatusOk()
        {
            //arrange
            RestRequest request = new RestRequest("/Employees/22", Method.DELETE);
            //act
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
