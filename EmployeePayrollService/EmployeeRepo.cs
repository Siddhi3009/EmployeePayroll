using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace EmployeePayrollService
{
    class EmployeeRepo
    {
        public static string connectionString = @"Data Source=DESKTOP-6S6I6GO\SQLEXPRESS;Initial Catalog=Payroll_Service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = !dr.IsDBNull(1)? dr.GetString(1):"NA";
                            employeeModel.BasicPay = !dr.IsDBNull(2)?dr.GetDecimal(2): 0;
                            employeeModel.StartDate = !dr.IsDBNull(3) ? dr.GetDateTime(3) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(4)? Convert.ToChar(dr.GetString(4)): 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(5)? dr.GetString(5) : "NA";
                            employeeModel.Address = !dr.IsDBNull(6)? dr.GetString(6): "NA";
                            employeeModel.Department = !dr.IsDBNull(7)? dr.GetString(7): "NA";
                            employeeModel.Deductions = !dr.IsDBNull(8)? dr.GetDecimal(8): 0;
                            employeeModel.TaxablePay = !dr.IsDBNull(9)? dr.GetDecimal(9): 0;
                            employeeModel.Tax = !dr.IsDBNull(10)? dr.GetDecimal(10): 0;
                            employeeModel.NetPay = !dr.IsDBNull(11)? dr.GetDecimal(11): 0;
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
