using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=DESKTOP-6S6I6GO\SQLEXPRESS;Initial Catalog=Payroll_Service;Integrated Security=True";
        public List<EmployeeModel> GetAllEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            try
            {
                using (connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel();
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = !dr.IsDBNull(1) ? dr.GetString(1) : "NA";
                            employeeModel.BasicPay = !dr.IsDBNull(2) ? dr.GetDecimal(2) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(3) ? dr.GetDateTime(3) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(4) ? Convert.ToChar(dr.GetString(4)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(5) ? dr.GetString(5) : "NA";
                            employeeModel.Address = !dr.IsDBNull(6) ? dr.GetString(6) : "NA";
                            employeeModel.Department = !dr.IsDBNull(7) ? dr.GetString(7) : "NA";
                            employeeModel.Deductions = !dr.IsDBNull(8) ? dr.GetDecimal(8) : 0;
                            employeeModel.TaxablePay = !dr.IsDBNull(9) ? dr.GetDecimal(9) : 0;
                            employeeModel.Tax = !dr.IsDBNull(10) ? dr.GetDecimal(10) : 0;
                            employeeModel.NetPay = !dr.IsDBNull(11) ? dr.GetDecimal(11) : 0;
                            employeeList.Add(employeeModel);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return employeeList;
        }
        public bool AddEmployee(EmployeeModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate.ToString("yyyy-MM-dd"));
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public bool UpdateSalary(string name, decimal salary)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"Update employee_payroll set basic_pay = '" + salary + "' where name = '" + name + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public List<EmployeeModel> RetrieveDataByName(string name)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            try
            {
                using (connection)
                {
                    string query = @"Select * from employee_payroll where name = '" + name + "';";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel();
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = !dr.IsDBNull(1) ? dr.GetString(1) : "NA";
                            employeeModel.BasicPay = !dr.IsDBNull(2) ? dr.GetDecimal(2) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(3) ? dr.GetDateTime(3) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(4) ? Convert.ToChar(dr.GetString(4)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(5) ? dr.GetString(5) : "NA";
                            employeeModel.Address = !dr.IsDBNull(6) ? dr.GetString(6) : "NA";
                            employeeModel.Department = !dr.IsDBNull(7) ? dr.GetString(7) : "NA";
                            employeeModel.Deductions = !dr.IsDBNull(8) ? dr.GetDecimal(8) : 0;
                            employeeModel.TaxablePay = !dr.IsDBNull(9) ? dr.GetDecimal(9) : 0;
                            employeeModel.Tax = !dr.IsDBNull(10) ? dr.GetDecimal(10) : 0;
                            employeeModel.NetPay = !dr.IsDBNull(11) ? dr.GetDecimal(11) : 0;
                            employeeList.Add(employeeModel);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return employeeList;
        }
        public List<EmployeeModel> RetrieveEmployeesWithParticularDateRange(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            try
            {
                using (connection)
                {
                    string query = @"select * from employee_payroll where start between '" + startDate + "'and '" + endDate + "';";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel();
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = !dr.IsDBNull(1) ? dr.GetString(1) : "NA";
                            employeeModel.BasicPay = !dr.IsDBNull(2) ? dr.GetDecimal(2) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(3) ? dr.GetDateTime(3) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(4) ? Convert.ToChar(dr.GetString(4)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(5) ? dr.GetString(5) : "NA";
                            employeeModel.Address = !dr.IsDBNull(6) ? dr.GetString(6) : "NA";
                            employeeModel.Department = !dr.IsDBNull(7) ? dr.GetString(7) : "NA";
                            employeeModel.Deductions = !dr.IsDBNull(8) ? dr.GetDecimal(8) : 0;
                            employeeModel.TaxablePay = !dr.IsDBNull(9) ? dr.GetDecimal(9) : 0;
                            employeeModel.Tax = !dr.IsDBNull(10) ? dr.GetDecimal(10) : 0;
                            employeeModel.NetPay = !dr.IsDBNull(11) ? dr.GetDecimal(11) : 0;
                            employeeList.Add(employeeModel);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
            return employeeList;
        }
        public void SumOfSalaryGenderWise()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select gender, SUM(basic_pay) from employee_payroll group by gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.Write(dr.GetString(0) + "\t");
                            Console.Write(dr.GetDecimal(1));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void AverageOfSalaryGenderWise()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select gender, AVG(basic_pay) from employee_payroll group by gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.Write(dr.GetString(0) + "\t");
                            Console.Write(dr.GetDecimal(1));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void MinimumSalaryGenderWise()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select gender, MIN(basic_pay) from employee_payroll group by gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.Write(dr.GetString(0) + "\t");
                            Console.Write(dr.GetDecimal(1));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void MaximumSalaryGenderWise()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select gender, MAX(basic_pay) from employee_payroll group by gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.Write(dr.GetString(0) + "\t");
                            Console.Write(dr.GetDecimal(1));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void CountOfEmployeesGenderWise()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select gender, COUNT(gender) from employee_payroll group by gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.Write(dr.GetString(0) + "\t");
                            Console.Write(dr.GetInt32(1));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
