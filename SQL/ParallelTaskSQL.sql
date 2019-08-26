use [Northwind];
--select * from Categories;
--Задание 1
--Напишите запрос, который выводит список заказчиков в виде таблицы, состоящей из двух столбцов CustomerId и CompanyName. 
--Строки таблицы должны быть отсортированы по коду заказчика.
Select Customers.CustomerID, Customers.CompanyName
from Customers
Order by Customers.PostalCode;

--Задание 2
--напишите запрос, который выводит EmpoyeeID последнего нанятого компанией сотрудника
Select top 1 Employees.EmployeeID
from Employees
order by Employees.HireDate desc;

--Задание 3
--напишите запрос, который выводит список всех стран из колонки dbo.Customers.Country. Список должен быть отсортирован в 
--алфавитном порядке, должен содержать только уникальные значения и не должен содержать дубликаты
Select distinct Customers.Country 
from Customers
order by Customers.Country;
--Задание 4
--напишите запрос, который выводит список названий компаний-заказчиков, расположенных в следующих городах: Берлин, Лондон,
--Мадрид, Брюсель, Париж. Список должен быть отсортирован по коду-индефикатору  компании в обратном алфовитном порядке. 
Select CompanyName
from Customers
Where City in('Berlin', 'London','Madrid', 'Brussels', 'Paris')
order by CustomerID desc;

--Задание 5
--Напишите запрос, который выводит список идентификаторов компаний, для которых заказы были доставлены 
--(dbo.Orders.RequiredDate) в сентябре 1996 года. Список должен быть отсортирован в алфавитном порядке.
Select Orders.CustomerID
from Orders
where DATEPART(YEAR,Orders.RequiredDate) = '1996' and DATEPART (MONTH,Orders.RequiredDate) = '9'
order by Orders.CustomerID;

--Задание 6
--Напишите запрос, который выводит имя контактного лица компании-заказчика, чей номер телефона начинается 
--с кода "171" и содержит "77", а также номер факса начинается с кода "171" и оканчивается на "50".
Select Customers.ContactName
from Customers
where (Customers.Phone like'(171)%' and Customers.Phone like '%77%') and (Customers.Fax like '(171)%' and Customers.Fax like '%50' );

--Задание 7
--Напишите запрос, который выводит количество компаний-заказчиков, которые находятся в городах, принадлежащих 
--трем скандинавским странам. Результирующая таблица должна состоять из двух колонок City и CustomerCount.
Select Country, count(Country) as CustomerCount
from Customers
where Country in ('Norway', 'Sweden', 'Denmark')
Group by Country;

--Задание 8
--Напишите запрос, который выводит количество компаний-заказчиков в странах, в которых есть 10 и более заказчиков.
--Результирующая таблица должна иметь колонки Country и CustomerCount, строки которой должны быть отсортированы в
--обратном порядке по количеству заказчиков в стране.
Select Country, count(Country) as CustomerCount
from Customers
group by Country
having count(Country)>10
order by count(Country) desc;

--Задание 9
--Напишите запрос, который выводит среднюю стоимость фрахта (dbo.Order.Freight) заказов для компаний-заказчиков, которые 
--указывали местом доставки заказа город, принадлежащий Великобритании или Канаде. Дополнительным критерием выборки является 
--значение средней стоимости фрахта заказа - больше или равно 100, или меньше 10. Результирующая таблица должна иметь колонки 
--CustomerID и FreightAvg, значение средней стоимости должно быть округлено до целого значения, строки должны быть отсортированы 
--в обратном порядке по значению среднего значения фрахта.
--Select avg(Orders.Freight)
--from Orders join Customers on Orders.CustomerID = Customers.CustomerID
--where Orders.ShipCountry in ('Canada',
Select *
from Orders;


--Задание 10
--Напишите запрос, который выводит EmployeeID предпоследнего нанятого компанией сотрудника. Используйте подзапрос для 
--исключения последнего нанятого сотрудника.
Select top 1 Employees.EmployeeID
from Employees
where HireDate <(select max(HireDate) from Employees)
order by Employees.HireDate desc


--Задание 11
--Напишите запрос, который выводит EmployeeID предпоследнего нанятого компанией сотрудника. Используйте ключевые слова 
--OFFSET и FETCH.
Select Employees.EmployeeID
from Employees
order by Employees.HireDate desc
OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;

--Задание 12
--Напишите запрос, который выводит общую сумму фрахтов заказов для компаний-заказчиков для заказов, стоимость фрахта 
--которых больше или равна средней величине стоимости фрахта всех заказов, а также дата отгрузки заказа должна находится во 
--второй половине июля 1996 года. Результирующая таблица должна иметь колонки CustomerID и FreightSum, строки 
--которой должны быть отсортированы по сумме фрахтов заказов.

--Задание 13
--Напишите запрос, который выводит 3 заказа с наибольшей стоимостью, которые были созданы после 1 сентября 1997 года 
--включительно и были доставлены в страны Южной Америки. Общая стоимость рассчитывается как сумма стоимости деталей заказа 
--с учетом дисконта. Результирующая таблица должна иметь колонки CustomerID, ShipCountry и OrderPrice, строки которой должны 
--быть отсортированы по стоимости заказа в обратном порядке.

--Задание 14
--Перепишите запрос с использованием группировки:
--SELECT DISTINCT s.CompanyName,
--(SELECT min(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MinPrice,
--(SELECT max(t.UnitPrice) FROM dbo.Products as t WHERE t.SupplierID = p.SupplierID) as MaxPrice
--FROM dbo.Products AS p
--INNER JOIN dbo.Suppliers AS s ON p.SupplierID = s.SupplierID
--ORDER BY s.CompanyName

--Задание 15
--Напишите запрос, который выводит список компаний-заказчиков из Лондона, которые делали заказы у сотрудников лондонского офиса 
--и заказали доставку через службу Speedy Express. Результирующая таблица должна иметь колонки Customer и Employee, колонка 
--Employee должна содержать FirstName и LastName сотрудника.

--Задание 16
--Напишите запрос, который выводит список продуктов из категорий Beverages и Seafood, которые можно заказать у поставщиков 
--(Discontinued) и которые остались на складе в количестве меньше 20 штук. Результирующая таблица должна иметь колонки ProductName,
--UnitsInStock, ContactName и Phone поставщика. Строки таблицы должны быть отсортированы по значению складского запаса.
