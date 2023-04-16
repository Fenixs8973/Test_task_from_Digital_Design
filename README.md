# Задание 1
## 1. Сотрудник с максимальной заработной платой:
```SQL
SELECT NAME
FROM EMPLOYEE
WHERE (NAME, SALARY) 
IN 
 (SELECT NAME, MAX(SALARY) FROM EMPLOYEE);
```

## 2. Вывести одно число: максимальную длину цепочки руководителей по таблице сотрудников (вычислитьглубину дерева).
```SQL
WITH RECURSIVE EmployeeHierarchy AS 
(SELECT id, chief_id, 1 AS depth
  FROM EMPLOYEE
  WHERE chief_id IS NULL
  UNION ALL
  SELECT e.id, e.chief_id, eh.depth + 1
  FROM EMPLOYEE AS e
  INNER JOIN EmployeeHierarchy AS eh ON e.chief_id = eh.id)
SELECT MAX(depth) AS max_depth
FROM EmployeeHierarchy;
```

## 3. Отдел, с максимальной суммарной зарплатой сотрудников.
```SQL
WITH dep_salary AS 
	(SELECT DEPARTMENT_Id, sum(SALARY) AS SALARY
    FROM EMPLOYEE 
	GROUP BY DEPARTMENT_Id)
SELECT DEPARTMENT_Id
FROM dep_salary
WHERE dep_salary.salary = (SELECT max(salary) FROM dep_salary);
```

## 4. Сотрудник, чье имя начинается на «Р» и заканчивается на «н».
```SQL
SELECT DISTINCT name
FROM EMPLOYEE
WHERE name LIKE 'Р%н';
```


# Задание 2

Входной файл должен называться "War_and_peace.txt"

Так как файл изначально был в .fb2 из него нужно было удалить все лишнее. Этим занимается класс TextProcessingFB2. Результат обработки записывается в буферный файл(который в конце удаляется).

Подсчет и сортировка слов выполняется в классе TextStatistics. Результат сохраняется в файл Result.txt
