using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollService;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    }
}
