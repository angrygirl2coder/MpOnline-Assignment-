DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Departments;

CREATE TABLE Departments
(
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(50),
    City VARCHAR(50)
);

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    EmployeeName VARCHAR(50),
    Salary DECIMAL(10,2),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID)
        REFERENCES Departments(DepartmentID)
);

INSERT INTO Departments VALUES
(1,'HR','Pune'),
(2,'IT','Mumbai'),
(3,'Finance','Pune'),
(4,'Sales','Delhi');

INSERT INTO Employees VALUES
(101,'Rahul',60000,1),
(102,'Priya',45000,2),
(103,'Amit',70000,3),
(104,'Sneha',55000,1),
(105,'Karan',40000,4);

SELECT EmployeeName, Salary
FROM Employees
WHERE Salary >
(
    SELECT AVG(Salary)
    FROM Employees
);

SELECT E.EmployeeName
FROM Employees E
JOIN Departments D
ON E.DepartmentID = D.DepartmentID
WHERE D.City = 'Pune';

SELECT E.EmployeeName,
       E.Salary,
       (
           SELECT AVG(Salary)
           FROM Employees
           WHERE DepartmentID = E.DepartmentID
       ) AS DepartmentAverageSalary
FROM Employees E;

SELECT D.DepartmentName,
       AVG(E.Salary) AS AverageSalary
FROM Departments D
JOIN Employees E
ON D.DepartmentID = E.DepartmentID
GROUP BY D.DepartmentName
HAVING AVG(E.Salary) > 50000;

SELECT DISTINCT D.DepartmentName
FROM Departments D
JOIN Employees E
ON D.DepartmentID = E.DepartmentID;

SELECT E.EmployeeName
FROM Employees E
JOIN Departments D
ON E.DepartmentID = D.DepartmentID
WHERE D.DepartmentName = 'HR';
