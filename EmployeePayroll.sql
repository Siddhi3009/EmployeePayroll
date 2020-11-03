--Creating a database
create database Payroll_Service;
--Using Database
use Payroll_Service;
--Viewing database
select DB_NAME();
--Creating table
create table employee_payroll
(
id int identity(1,1),
name varchar(25) not null,
salary money not null,
start date not null
);
--Displaying table details
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'employee_payroll';
--Inserting data in table
insert into employee_payroll values
('Bill',100000.00,'2018-01-03'),
('Terissa',200000.00,'2019-11-13'),
('Charlie',300000.00,'2020-05-21');
--Selecting all columns and rows of table
select * from employee_payroll;
--Selecting salary of Bill
select salary from employee_payroll where name = 'Bill';
--Selecting all employees with start date between 1/1/2018 and present date
select * from employee_payroll where start between '2018-01-01' and GETDATE();
--Add extra column of gender
Alter table employee_payroll
Add Gender char;
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'employee_payroll';
--Update gender of employees
update employee_payroll set Gender = 'M' where name = 'Bill' or name = 'Charlie';
update employee_payroll set Gender = 'F' where name = 'Terissa';
select * from employee_payroll;
--Sum of salary of all males
select SUM(salary) from employee_payroll
where gender = 'M'
group by gender
--Average salary according to gender
select AVG(salary), gender from employee_payroll
group by gender;
--Minimum salary according to gender
select MIN(salary), gender from employee_payroll
group by gender;
--Maximum salary acording to gender
select MAX(salary), gender from employee_payroll
group by gender;
--Employee count according to gender
select COUNT(gender), gender from employee_payroll
group by gender;
--Add additional Employee information columns
alter table employee_payroll add phone_number varchar(13)
alter table employee_payroll add address varchar(250), department varchar(20)
--Add department for existing enteries
Update employee_payroll set department = 'Sales' where id in (1 , 3);
Update employee_payroll set department = 'Marketting' where id = 2;
--Adding constraints
alter table employee_payroll add constraint default_address default 'India' for address
alter table employee_payroll alter column department varchar(20) Not null
--Add salary divisions
alter table employee_payroll add deduction float, taxable_pay real, income_tax real, net_pay real
--Rename salary column
EXEC sp_rename 'employee_payroll.salary', 'basic_pay', 'COLUMN';
--Redundant data for Terissa added with department change 
insert into employee_payroll (name, start, basic_pay, department) values
('Terissa', '2019-11-13', '200000', 'Sales');
select * from employee_payroll
--create employee Employeedepartment table
create table EmployeeDepartment
(
id int not null,
name varchar(25) not null,
department varchar(20) not null
);
--insert enteries into the department table
insert into EmployeeDepartment values
(1,'Bill', 'Sales'),
(2,'Terissa', 'Sales'),
(3,'Charlie', 'Sales'),
(2,'Terissa','Marketting');
--creating employee table
create table employee
(
Id int identity(1,1) not null,
Name varchar(25) not null,
Gender char(1) not null,
Phone_Number varchar(13) not null,
Address varchar(250) not null default 'India',
);
-- insert data into employee table
insert into employee values
('Bill', 'M', '9424787443', 'Shanti Nagar'),
('Terissa', 'F', '8109322276', 'Damoh Naka'),
('Charlie', 'M', '9926707344', 'Panchsheel Nagar');
--create payroll table
create table Payroll
(
Id int not null,
Name varchar(25) not null,
Start date not null,
Basic_pay money not null,
Deduction money,
Taxable_pay money,
Income_tax money,
Net_pay money not null
);
--insert data in payroll
insert into Payroll values
(1, 'Bill', '2018-01-03', 100000, 10000, 90000, 1000, 89000),
(2, 'Terissa', '2019-11-13', 200000, 10000, 190000,3000,187000),
(3,'Charlie', '2020-05-21', 300000, 20000, 280000, 5000, 275000);
--Retrieve all data
select * from ((employee emp inner join Payroll payroll on (emp.Id = payroll.Id)) 
inner join EmployeeDepartment department on (emp.Id = department.id))
--retrieve salary information of bill 
select * from Payroll 
where payroll.Name = 'Bill';
--retrieve employee names who started after 2018
select Id, Name, Start  from Payroll where start between cast('2018-01-01' as date) and GETDATE();
--Aggregate operationsby gender

--Total of basic pay by gender
select emp.gender, Sum(payroll.Basic_pay)  
from Payroll payroll inner join employee emp
on payroll.Id = emp.Id
group by gender;
--Average of basic pay by gender
select emp.gender, AVG(payroll.Basic_pay)  
from Payroll payroll inner join employee emp
on payroll.Id = emp.Id
group by gender;
--Count number of employees by gender
select gender, Count(Name)  
from employee 
group by gender;
--Minimum salary of male employees
select MIN(payroll.Basic_pay)  
from Payroll payroll inner join employee emp
on payroll.Id = emp.Id
where emp.Gender = 'M'
group by gender;
--Maximum salary of male employees
select MAX(payroll.Basic_pay)  
from Payroll payroll inner join employee emp
on payroll.Id = emp.Id
where emp.Gender = 'M'
group by gender;