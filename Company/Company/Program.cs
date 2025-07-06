using Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practice4
{
  internal class Program
  {
    static EmployeeManager<Employee> employeeManager = new EmployeeManager<Employee>();
    public static void Main(string[] args)
    {
      while (true)
      {
        Console.WriteLine("Меню:" + "\n" +
                           "1. Добавить полного сотрудника" + "\n" +
                           "2. Добавить частичного сотрудника" + "\n" +
                           "3. Получить информацию о сотруднике по имени" + "\n" +
                           "4. Обновить данные сотрудника по ID\n" +
                           "5. Удалить сотрудника по ID\n" +
                           "6. Получить информацию по всем сотрудникам\n" +
                           "7. Выйти");

        try
        {
          Console.Write("Введите ");
          int number = InputHelper.ReadInt("пункт меню");
          switch (number)
          {
            case 1:
              AddFullTimeEmployee();
              break;
            case 2:
              AddPartTimeEmployee();
              break;
            case 3:
              GetEmployeeByName();
              break;
            case 4:
              UpdateEmployeeById();
              break;
            case 5:
              RemoveEmployeeById();
              break;
            case 6:
              GetAllEmployees();
              break;
            case 7:
              return;
            default:
              Console.Clear();
              Console.WriteLine("Некорректный выбор, попробуйте снова.");
              break;
          }
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine($"{ex.Message}");
          Console.WriteLine("Попробуйте снова");
        }
        catch (Exception ex)
        {
          Console.WriteLine("Неизвестная ошибка");
        }
      }
    }

    static void AddFullTimeEmployee()
    {
      Console.Clear();
      Console.WriteLine("1. Добавить полного сотрудника.");
      Console.Write("Введите данные для сотрудника\n");

      try
      {
        var fullTimeEmployee = InputHelper.InputFullTimeEmployee();
        employeeManager.Add(fullTimeEmployee);
        Console.WriteLine($"Сотрудник {fullTimeEmployee} успешно добавлен!");
      }
      catch (EmployeeCreationException ex) // если были проблемы при вводе данных соторудника
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeAdditionException ex) // если были проблемы при добавлении нового сотрудника
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Создание сотрудника было отменено");
      }
    }

    static void AddPartTimeEmployee()
    {
      Console.Clear();
      Console.WriteLine("2. Добавить частичного сотрудника.");
      Console.Write("Введите данные для сотрудника\n");
      try
      {
        var partTimeEmployee = InputHelper.InputPartTimeEmployee();
        employeeManager.Add(partTimeEmployee);
        Console.WriteLine($"Сотрудник {partTimeEmployee} успешно добавлен!");
      }
      catch (EmployeeCreationException ex) // если были проблемы при вводе данных соторудника
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeAdditionException ex) // если были проблемы при добавлении нового сотрудника
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Создание сотрудника было отменено");
      }
    }

    static void GetEmployeeByName()
    {
      Console.Clear();
      Console.WriteLine("3. Получить информацию о сотруднике по имени.");
      Console.Write("Введите ");
      try
      {
        string name = InputHelper.ReadString("Имя"); 
        var employee = employeeManager.Get(name);
        Console.WriteLine("Найденный сотрудник:");
        Console.WriteLine(employee);
        Console.WriteLine($"Итоговая зарплата: {employee.CalculateSalary()}");
      }
      catch (ArgumentException ex) // если были проблемы при вводе значений
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника был отменён");
      }
      catch (EmployeeNotFoundException ex) // если не найден сотрудник
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника был отменён");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Поиск сотрудника был отменён");
      }

    }

    static void UpdateEmployeeById()
    {
      Console.Clear();
      Console.WriteLine("4. Обновить данные сотрудника по ID.");
      Console.Write("Введите ");
      try
      {
        int id = InputHelper.ReadInt("ID");
        Employee employeeToUpdateInfo = employeeManager.GetById(id);
        Console.WriteLine($"Данные сотрудника:\n {employeeToUpdateInfo}");

        Console.WriteLine("Введите новые значения:");
        Employee employeeToUpdateFrom;
        if (employeeToUpdateInfo as FullTimeEmployee != null)
          employeeToUpdateFrom = InputHelper.InputFullTimeEmployee(id);
        else if (employeeToUpdateInfo as PartTimeEmployee != null)
          employeeToUpdateFrom = InputHelper.InputPartTimeEmployee(id);
        else
          throw new Exception("Неизвестный сотрудник на обновление...");

        employeeManager.Update(employeeToUpdateFrom);
        Console.WriteLine("Сотрудник был успешно обновлён!");
        Console.WriteLine($"Новые данные:\n {employeeToUpdateFrom}");
      }
      catch (EmployeeCreationException ex) // если были проблемы при вводе данных соторудника
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (ArgumentException ex) // если были проблемы при вводе значений
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (EmployeeNotFoundException ex) // если не найден сотрудник
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (InvalidOperationException ex) // если сотрудников с таким id больше одного
      {
        Console.WriteLine($"Сотрудников с таким ID оказалось несколько");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Обновление сотрудника было отменено");
      }
    }

    static void RemoveEmployeeById()
    {
      Console.Clear();
      Console.WriteLine("5. Удалить сотрудника по ID.");
      Console.Write("Введите ");
      try
      {
        int id = InputHelper.ReadInt("ID");
        Employee employee = employeeManager.GetById(id);
        Console.WriteLine(employee);
        employeeManager.Delete(employee);
        Console.WriteLine("Сотрудник был успешно удалён!");
      }
      catch (ArgumentException ex) // если были проблемы при вводе значений
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (EmployeeNotFoundException ex) // если не найден сотрудник
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (InvalidOperationException ex)  // если сотрудников с таким id больше одного
      {
        Console.WriteLine($"Сотрудников с таким ID оказалось несколько");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Удаление сотрудника было отменено");
      }
    }

    static void GetAllEmployees()
    {
      Console.Clear();
      Console.WriteLine("6. Получить информацию по всем сотрудникам.");
      List<Employee> employees = employeeManager.getEmployees();
      PrintEmployees(employees);
    }

    static void PrintEmployees(List<Employee> employees)
    {
      if (employees.Count == 0)
      {
        Console.WriteLine("Список сотрудников пуст.");
        return;
      }
      Console.WriteLine("Список сотрудников:");
      foreach (Employee employee in employees)
        Console.WriteLine(employee);
    }
  }
}

