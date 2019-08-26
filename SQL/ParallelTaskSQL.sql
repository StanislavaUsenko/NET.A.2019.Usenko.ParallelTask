use [Northwind];
--select * from Categories;
--������� 1
--�������� ������, ������� ������� ������ ���������� � ���� �������, ��������� �� ���� �������� CustomerId � CompanyName. 
--������ ������� ������ ���� ������������� �� ���� ���������.
Select Customers.CustomerID, Customers.CompanyName
from Customers
Order by Customers.PostalCode;

--������� 2
--�������� ������, ������� ������� EmpoyeeID ���������� �������� ��������� ����������
Select top 1 Employees.EmployeeID
from Employees
order by Employees.HireDate desc;

--������� 3
--�������� ������, ������� ������� ������ ���� ����� �� ������� dbo.Customers.Country. ������ ������ ���� ������������ � 
--���������� �������, ������ ��������� ������ ���������� �������� � �� ������ ��������� ���������
Select distinct Customers.Country 
from Customers
order by Customers.Country;
--������� 4
--�������� ������, ������� ������� ������ �������� ��������-����������, ������������� � ��������� �������: ������, ������,
--������, �������, �����. ������ ������ ���� ������������ �� ����-������������  �������� � �������� ���������� �������. 
Select CompanyName
from Customers
Where City in('Berlin', 'London','Madrid', 'Brussels', 'Paris')
order by CustomerID desc;

--������� 5
--�������� ������, ������� ������� ������ ��������������� ��������, ��� ������� ������ ���� ���������� 
--(dbo.Orders.RequiredDate) � �������� 1996 ����. ������ ������ ���� ������������ � ���������� �������.
Select Orders.CustomerID
from Orders
where DATEPART(YEAR,Orders.RequiredDate) = '1996' and DATEPART (MONTH,Orders.RequiredDate) = '9'
order by Orders.CustomerID;

--������� 6
--�������� ������, ������� ������� ��� ����������� ���� ��������-���������, ��� ����� �������� ���������� 
--� ���� "171" � �������� "77", � ����� ����� ����� ���������� � ���� "171" � ������������ �� "50".
Select Customers.ContactName
from Customers
where (Customers.Phone like'(171)%' and Customers.Phone like '%77%') and (Customers.Fax like '(171)%' and Customers.Fax like '%50' );

--������� 7
--�������� ������, ������� ������� ���������� ��������-����������, ������� ��������� � �������, ������������� 
--���� ������������� �������. �������������� ������� ������ �������� �� ���� ������� City � CustomerCount.
Select Country, count(Country) as CustomerCount
from Customers
where Country in ('Norway', 'Sweden', 'Denmark')
Group by Country;

--������� 8
--�������� ������, ������� ������� ���������� ��������-���������� � �������, � ������� ���� 10 � ����� ����������.
--�������������� ������� ������ ����� ������� Country � CustomerCount, ������ ������� ������ ���� ������������� �
--�������� ������� �� ���������� ���������� � ������.
Select Country, count(Country) as CustomerCount
from Customers
group by Country
having count(Country)>10
order by count(Country) desc;

--������� 9
--�������� ������, ������� ������� ������� ��������� ������ (dbo.Order.Freight) ������� ��� ��������-����������, ������� 
--��������� ������ �������� ������ �����, ������������� �������������� ��� ������. �������������� ��������� ������� �������� 
--�������� ������� ��������� ������ ������ - ������ ��� ����� 100, ��� ������ 10. �������������� ������� ������ ����� ������� 
--CustomerID � FreightAvg, �������� ������� ��������� ������ ���� ��������� �� ������ ��������, ������ ������ ���� ������������� 
--� �������� ������� �� �������� �������� �������� ������.
--Select avg(Orders.Freight)
--from Orders join Customers on Orders.CustomerID = Customers.CustomerID
--where Orders.ShipCountry in ('Canada',
Select *
from Orders;


--������� 10
--�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� ��������� ��� 
--���������� ���������� �������� ����������.
Select top 1 Employees.EmployeeID
from Employees
where HireDate <(select max(HireDate) from Employees)
order by Employees.HireDate desc


--������� 11
--�������� ������, ������� ������� EmployeeID �������������� �������� ��������� ����������. ����������� �������� ����� 
--OFFSET � FETCH.
Select Employees.EmployeeID
from Employees
order by Employees.HireDate desc
OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;

--������� 12
--�������� ������, ������� ������� ����� ����� ������� ������� ��� ��������-���������� ��� �������, ��������� ������ 
--������� ������ ��� ����� ������� �������� ��������� ������ ���� �������, � ����� ���� �������� ������ ������ ��������� �� 
--������ �������� ���� 1996 ����. �������������� ������� ������ ����� ������� CustomerID � FreightSum, ������ 
--������� ������ ���� ������������� �� ����� ������� �������.

--������� 13
--�������� ������, ������� ������� 3 ������ � ���������� ����������, ������� ���� ������� ����� 1 �������� 1997 ���� 
--������������ � ���� ���������� � ������ ����� �������. ����� ��������� �������������� ��� ����� ��������� ������� ������ 
--� ������ ��������. �������������� ������� ������ ����� ������� CustomerID, ShipCountry � OrderPrice, ������ ������� ������ 
--���� ������������� �� ��������� ������ � �������� �������.

--������� 14
--���������� ������ � �������������� �����������:
--SELECT DISTINCT s.CompanyName,
--(SELECT min(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MinPrice,
--(SELECT max(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MaxPrice
--FROM dbo.Products AS p
--INNER JOIN dbo.Suppliers AS s ON p.SupplierID = s.SupplierID
--ORDER BY s.CompanyName

--������� 15
--�������� ������, ������� ������� ������ ��������-���������� �� �������, ������� ������ ������ � ����������� ����������� ����� 
--� �������� �������� ����� ������ Speedy Express. �������������� ������� ������ ����� ������� Customer � Employee, ������� 
--Employee ������ ��������� FirstName � LastName ����������.

--������� 16
--�������� ������, ������� ������� ������ ��������� �� ��������� Beverages � Seafood, ������� ����� �������� � ����������� 
--(Discontinued) � ������� �������� �� ������ � ���������� ������ 20 ����. �������������� ������� ������ ����� ������� ProductName,
--UnitsInStock, ContactName � Phone ����������. ������ ������� ������ ���� ������������� �� �������� ���������� ������.
