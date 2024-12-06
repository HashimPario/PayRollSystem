# Employee payroll management system , a windows form application created with C# and MS SQL.

SQL Scripts for the database

CREATE TABLE [dbo].[Designations] (
    [designation]   VARCHAR (50)   NOT NULL,
    [basic_salary]  DECIMAL (8, 2) NOT NULL,
    [allowances]    DECIMAL (8, 2) NOT NULL,
    [overtime_rate] DECIMAL (4, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([designation] ASC)
);
CREATE TABLE [dbo].[Employees] (
    [employee_id]    INT           IDENTITY (1, 1) NOT NULL,
    [first_name]     VARCHAR (50)  NOT NULL,
    [last_name]      VARCHAR (50)  NOT NULL,
    [gender]         CHAR (2)      NOT NULL,
    [date_of_birth]  DATE          NOT NULL,
    [address]        VARCHAR (200) NOT NULL,
    [contact_number] VARCHAR (10)  NOT NULL,
    [hired_date]     DATE          NOT NULL,
    [designation]    VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([employee_id] ASC),
    FOREIGN KEY ([designation]) REFERENCES [dbo].[Designations] ([designation])
);
CREATE TABLE [dbo].[Salary] (
    [salary_id]      INT            IDENTITY (1, 1) NOT NULL,
    [employee_id]    INT            NOT NULL,
    [month]          VARCHAR (20)   NOT NULL,
    [no_pay_value]   DECIMAL (8, 2) NOT NULL,
    [base_pay_value] DECIMAL (8, 2) NOT NULL,
    [gross_pay]      DECIMAL (8, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([salary_id] ASC),
    FOREIGN KEY ([employee_id]) REFERENCES [dbo].[Employees] ([employee_id])
);
CREATE TABLE [dbo].[Settings] (
    [setting_id]                INT  IDENTITY (1, 1) NOT NULL,
    [date_range_for_cycle]      INT  NOT NULL,
    [cycle_begin_date]          DATE NOT NULL,
    [cycle_end_date]            DATE NOT NULL,
    [no_of_leaves_for_employee] INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([setting_id] ASC)
);
CREATE TABLE [dbo].[Access] (
    [user_name] VARCHAR (50) NOT NULL,
    [password]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([user_name] ASC)
);


//Insert Data 

INSERT INTO Designations(designation,basic_salary,allowances,overtime_rate)
VALUES
	('Assistant Manager'	,2000.00,	125.00	,20.00),
	('Manager',	2500.00,	150.00,	25.00),
	('Online Sales Coordinator'	,2000.00,	100.00,	18.00),
	('Sales Associate',	1250.00,	75.00,	12.50),
	('Stockroom Assistant',	1100.00,	60.00,	11.50),
	('Toy Demonstrator',	1000.00,	60.00	,10.00);

INSERT INTO [dbo].[Access] ([user_name], [password])
VALUES ('admin', 'password');   // if there are more than one admin , you can simply add more usernames and password..

// By Manada & Dulanga

When server is connected , the application should run as below
![image](https://github.com/ManadaHerath/Payroll-Management-System/assets/128967102/a6bc7185-e916-46cf-92fb-c097f3ce49b5)
![image](https://github.com/ManadaHerath/Payroll-Management-System/assets/128967102/db4a9661-c26f-4b05-a1c3-2114e2bb3db4)
![image](https://github.com/ManadaHerath/Payroll-Management-System/assets/128967102/027e564f-de00-4fb0-83ce-88fe7df98236)
![image](https://github.com/ManadaHerath/Payroll-Management-System/assets/128967102/47296195-e243-4441-8494-bb6c82ca3222)
![image](https://github.com/ManadaHerath/Payroll-Management-System/assets/128967102/3f25c9a5-533e-4318-bb78-afb49c8a213a)


