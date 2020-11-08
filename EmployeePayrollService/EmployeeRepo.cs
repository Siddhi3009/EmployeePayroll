﻿using System;
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
                    string query = @"select * from (employee e inner join Payroll p on e.Id = p.Id) inner join EmployeeDepartment ed on e.Id = ed.EmployeeId";
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
                            employeeModel.BasicPay = !dr.IsDBNull(7) ? dr.GetDecimal(7) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(6) ? dr.GetDateTime(6) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(2) ? Convert.ToChar(dr.GetString(2)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(3) ? dr.GetString(3) : "NA";
                            employeeModel.Address = !dr.IsDBNull(4) ? dr.GetString(4) : "NA";
                            employeeModel.Department = !dr.IsDBNull(13) ? dr.GetString(13) : "NA";
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
        public bool UpdateSalary(string name, double salary)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                double deduction = 0.2 * salary;
                double taxablePay = salary - deduction;
                double incomeTax = taxablePay * 0.1;
                double netPay = taxablePay - incomeTax;
                using (connection)
                {
                    string query = @"Update Payroll 
                                   Set Payroll.basic_pay = " + Convert.ToDecimal(salary) +
                                   "from employee e inner join Payroll p on e.Id = p.Id " +
                                   "where e.Name = '" + name + "'" +
                                   "Update Payroll Set Deduction = " + Convert.ToDecimal(deduction) +
                                   ", Taxable_pay = " + Convert.ToDecimal(taxablePay) +
                                   ", Income_tax = " + Convert.ToDecimal(incomeTax) +
                                   ", Net_pay = " + Convert.ToDecimal(netPay);
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
                    string query = @"select * from (employee e inner join Payroll p on e.Id = p.Id)" + 
                        "inner join EmployeeDepartment ed on e.Id = ed.EmployeeId where e.Name = '" + name + "'";
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
                            employeeModel.BasicPay = !dr.IsDBNull(7) ? dr.GetDecimal(7) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(6) ? dr.GetDateTime(6) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(2) ? Convert.ToChar(dr.GetString(2)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(3) ? dr.GetString(3) : "NA";
                            employeeModel.Address = !dr.IsDBNull(4) ? dr.GetString(4) : "NA";
                            employeeModel.Department = !dr.IsDBNull(13) ? dr.GetString(13) : "NA";
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
                    string query = @"select * from (employee e inner join Payroll p on e.Id = p.Id)" +
                        "inner join EmployeeDepartment ed on e.Id = ed.EmployeeId where p.Start between '" + startDate + "'and '" + endDate + "';";
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
                            employeeModel.BasicPay = !dr.IsDBNull(7) ? dr.GetDecimal(7) : 0;
                            employeeModel.StartDate = !dr.IsDBNull(6) ? dr.GetDateTime(6) : Convert.ToDateTime("01/01/0001");
                            employeeModel.Gender = !dr.IsDBNull(2) ? Convert.ToChar(dr.GetString(2)) : 'N';
                            employeeModel.PhoneNumber = !dr.IsDBNull(3) ? dr.GetString(3) : "NA";
                            employeeModel.Address = !dr.IsDBNull(4) ? dr.GetString(4) : "NA";
                            employeeModel.Department = !dr.IsDBNull(13) ? dr.GetString(13) : "NA";
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
                    string query = @"select e.gender, SUM(p.basic_pay) from employee e inner join Payroll p" + 
                        " on e.Id = p.Id group by gender";
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
                    string query = @"select e.gender, AVG(p.basic_pay) from employee e inner join Payroll p" +
                        " on e.Id = p.Id group by gender";
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
                    string query = @"select e.gender, MIN(p.basic_pay) from employee e inner join Payroll p" +
                        " on e.Id = p.Id group by gender";
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
                    string query = @"select e.gender, MAX(p.basic_pay) from employee e inner join Payroll p" +
                        " on e.Id = p.Id group by gender";
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
                    string query = @"select e.gender, COUNT(e.gender) from employee e inner join Payroll p" +
                        " on e.Id = p.Id group by gender";
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
        public void AddEmployeeToDtabase(string employeeName, char gender, string phoneNumber, string address, DateTime startDate, double basicPay, int departmentId, string department)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            int employeeId = -1;
            try
            {
                using (connection)
                {
                    connection.Open();
                    string addEmployeeQuery = @"insert into employee values ('" +
                                               employeeName + "','" + gender + "','" +
                                               phoneNumber + "','" + address + "'); " +
                                               "Select @@identity";
                    transaction = connection.BeginTransaction();
                    SqlCommand addEmployeeCommand = new SqlCommand(addEmployeeQuery, connection, transaction);
                    try
                    {
                        employeeId = Convert.ToInt32(addEmployeeCommand.ExecuteScalar());
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    double deduction = 0.2 * basicPay;
                    double taxablePay = basicPay - deduction;
                    double incomeTax = taxablePay * 0.1;
                    double netPay = taxablePay - incomeTax;
                    string addPayrollQuery = @"insert into payroll values ('" +
                                               employeeId + "','" + startDate.ToString("yyyy-MM-dd") + "','" +
                                               Convert.ToDecimal(basicPay) + "','" + Convert.ToDecimal(deduction) + "','" +
                                               Convert.ToDecimal(taxablePay) + "','" +
                                               Convert.ToDecimal(incomeTax) + "','" + Convert.ToDecimal(netPay) + "');";
                    SqlCommand addPayrollCommand = new SqlCommand(addPayrollQuery, connection, transaction);
                    try
                    {
                        var payrollAdded = addPayrollCommand.ExecuteNonQuery();
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                    }
                    string addDepartmentQuery = @"insert into EmployeeDepartment values ('" +
                                               departmentId + "','" + department + "','" +
                                               employeeId + "'); ";
                    SqlCommand addDepartmentCommand = new SqlCommand(addDepartmentQuery, connection, transaction);
                    try
                    {
                        var departmentAdded = addDepartmentCommand.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    transaction.Commit();
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
        }

    }
}
