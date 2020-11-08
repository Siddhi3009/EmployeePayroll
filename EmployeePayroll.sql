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
--Update information for every employee
Update employee_payroll 
set phone_number = '9926707344', address = 'Damoh Naka', deduction = 1000, taxable_pay = 99000, income_tax = 5000, net_pay = 94000 where id = 1
Update employee_payroll
set phone_number = '8529631478', address = 'PNB Colony', deduction = 3000, taxable_pay = 297000, income_tax = 10000, net_pay = 287000 where id = 3
Update employee_payroll
set phone_number = '9586942335', address = 'Shanti Nagar', deduction = 2000, taxable_pay = 198000, income_tax = 8000, net_pay = 190000 where name = 'Terissa';
--Implement ER Diagram
create table employee
(
Id int identity(1,1) not null primary key,
Name varchar(25) not null ,
Gender char(1) not null,
Phone_Number varchar(13) not null,
Address varchar(250) not null default 'India',
);

create table EmployeeDepartment
(
DepartmentId int not null primary key,
Department varchar (20) not null,
EmployeeId int not null foreign key references Employee(Id) on delete cascade
);

create table Payroll
(
Id int not null foreign key references Employee(Id) on delete cascade,
Start date not null,
Basic_pay money not null,
Deduction money,
Taxable_pay money,
Income_tax money,
Net_pay money not null
);

--insert data in tables

insert into employee values
('Bill', 'M', '9424787443', 'Shanti Nagar'),
('Terissa', 'F', '8109322276', 'Damoh Naka'),
('Charlie', 'M', '9926707344', 'Panchsheel Nagar');

insert into EmployeeDepartment values
(101, 'Sales',26),
(102, 'Sales',27),
(103, 'HR', 28),
(104,'Marketting',27);

insert into Payroll values
(26,'2018-01-03', 100000, 10000, 90000, 1000, 89000),
(27, '2019-11-13', 200000, 10000, 190000,3000,187000),
(28, '2020-05-21', 300000, 20000, 280000, 5000, 275000);

--Retrieve all data
select * from ((employee emp inner join Payroll payroll on (emp.Id = payroll.Id)) 
inner join EmployeeDepartment department on (emp.Id = department.EmployeeId))
--retrieve salary information of bill 
select emp.Name, pay.Basic_pay from Payroll pay inner join employee emp
on pay.Id = emp.Id
where emp.Name = 'Bill';
--retrieve employee names who started after 2018
select emp.Name from employee emp left join payroll pay 
on emp.Id = pay.Id
where pay.Start between cast('2018-01-01' as date) and GETDATE();

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
--Add procedure to store data
create procedure SpAddEmployeeDetails
(
@EmployeeName varchar(255),
@PhoneNumber varchar(255),
@Address varchar(255),
@Department varchar(255),
@Gender char(1),
@BasicPay float,
@Deductions float,
@TaxablePay float,
@Tax float,
@NetPay float,
@StartDate Date
)
as
begin
insert into employee_payroll values
(
@EmployeeName,@BasicPay,@StartDate,@Gender,@PhoneNumber,@Address,@Department,@Deductions,@TaxablePay,@Tax,@NetPay
)
end
--View Newly Added Records
select * from employee
select * from Payroll
select * from EmployeeDepartment
--Adding is_active column in employee
ALTER TABLE employee
ADD is_active bit default 1;