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
            Assert.AreEqual(11, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "Name: " + item.name + "Salary: " + item.Salary);
            }
        }
        [TestMethod]
        public void GivenEmployee_WhenPosted_ShouldReturnAddedEmployee()
        {
            //arrange
            RestRequest request = new RestRequest("/Employees", Method.POST);
            JObject jObject = new JObject();
            jObject.Add("name", "Mark");
            jObject.Add("Salary", "20000");
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Mark", dataResponse.name);
            Assert.AreEqual(20000, dataResponse.Salary);
            Console.WriteLine(response.Content);
        }
    }
}
