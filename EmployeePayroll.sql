/*Creating a database*/
create database Payroll_Service;
/*Using Database*/
use Payroll_Service;
/*Viewing database*/
select DB_NAME();
/*Creating table*/
create table employee_payroll
(
id int identity(1,1),
name varchar(25) not null,
salary money not null,
start date not null
);
/*Displaying table details*/
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'employee_payroll';
/*Inserting data in table*/
insert into employee_payroll values
('Bill',100000.00,'2018-01-03'),
('Terissa',200000.00,'2019-11-13'),
('Charlie',300000.00,'2020-05-21');
/*Selecting all columns and rows of table*/
select * from employee_payroll;
/*Selecting salary of Bill*/
select salary from employee_payroll where name = 'Bill';
/*Selecting all employees with start date between 1/1/2018 and present date*/
select * from employee_payroll where start between '2018-01-01' and GETDATE();
/*Add extra column of gender*/
Alter table employee_payroll
Add Gender char;
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'employee_payroll';
/*Update gender of employees*/
update employee_payroll set Gender = 'M' where name = 'Bill' or name = 'Charlie';
update employee_payroll set Gender = 'F' where name = 'Terissa';
select * from employee_payroll;
/*Sum of salary of all males*/
select SUM(salary) from employee_payroll
where gender = 'M'
group by gender
/*Average salary according to gender*/
select AVG(salary), gender from employee_payroll
group by gender;
/*Minimum salary according to gender*/
select MIN(salary), gender from employee_payroll
group by gender;
/*Maximum salary acording to gender*/
select MAX(salary), gender from employee_payroll
group by gender;
/*Employee count according to gender*/
select COUNT(gender), gender from employee_payroll
group by gender;