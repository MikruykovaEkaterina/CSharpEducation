using Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practice4
{
  /// <summary>
  /// Главный класс приложения, содержащий точку входа и логику пользовательского интерфейса
  /// </summary>
  /// <remarks>
  /// <para>
  /// Класс реализует консольное меню для управления сотрудниками через единый экземпляр <see cref="EmployeeManager{T}"/>.
  /// Особенности реализации:
  /// <list type="bullet">
  ///   <item><description>Использует глобальный менеджер сотрудников для всех операций</description></item>
  ///   <item><description>Обрабатывает все исключения бизнес-логики с выводом понятных сообщений</description></item>
  ///   <item><description>Предоставляет отдельные методы для каждого типа операций</description></item>
  ///   <item><description>Автоматически очищает консоль между операциями</description></item>
  /// </list>
  /// </para>
  /// </remarks>
  internal class Program
  {
    /// <summary>
    /// Статический экземпляр менеджера сотрудников для глобального доступа
    /// </summary>
    /// <remarks>
    /// <para>
    /// Предоставляет единую точку доступа к менеджеру сотрудников в рамках приложения.
    /// Инициализируется при первом обращении.
    /// </para>
    /// </remarks>
    static EmployeeManager<Employee> employeeManager = new EmployeeManager<Employee>();

    /// <summary>
    /// Точка входа в приложение
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    /// <remarks>
    /// <para>
    /// Реализует бесконечный цикл меню до выбора опции выхода (7).
    /// Обрабатывает все некритичные исключения с выводом сообщений.
    /// </para>
    /// <para>
    /// Логика работы:
    /// <list type="number">
    ///   <item><description>Отображает меню с опциями</description></item>
    ///   <item><description>Ожидает ввод пользователя</description></item>
    ///   <item><description>Выполняет выбранную операцию</description></item>
    ///   <item><description>Обрабатывает ошибки и возвращает в главное меню</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static void Main(string[] args)
    {
      while (true)
      {
        Console.WriteLine("Меню:" + "\n" +
                           "1. Добавить полного сотрудника" + "\n" +
                           "2. Добавить частичного сотрудника" + "\n" +
                           "3. Получить информацию о сотруднике по ID" + "\n" +
                           "4. Обновить данные сотрудника по ID\n" +
                           "5. Удалить сотрудника по ID\n" +
                           "6. Получить информацию по всем сотрудникам\n" +
                           "7. Выйти");
        Console.Write("Введите ");
        try
        {
          int number = InputHelper.ReadValue("пункт меню", int.Parse);
          switch (number)
          {
            case 1:
              AddFullTimeEmployee();
              break;
            case 2:
              AddPartTimeEmployee();
              break;
            case 3:
              GetEmployeeById();
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

    /// <summary>
    /// Добавляет сотрудника с полной занятостью
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Ввод данных через <see cref="InputHelper.InputFullTimeEmployee"/></description></item>
    ///   <item><description>Добавление в менеджер сотрудников</description></item>
    ///   <item><description>Обработка возможных ошибок ввода/добавления</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Перехватываемые исключения:
    /// <list type="bullet">
    ///   <item><description><see cref="EmployeeCreationException"/> - ошибка создания объекта</description></item>
    ///   <item><description><see cref="EmployeeAdditionException"/> - ошибка добавления</description></item>
    ///   <item><description><see cref="Exception"/> - неизвестные ошибки</description></item>
    /// </list>
    /// </para>
    /// </remarks>
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
      catch (ArgumentException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeCreationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeAdditionException ex)
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

    /// <summary>
    /// Добавляет сотрудника с частичной занятостью через консольный ввод
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Ввод данных через <see cref="InputHelper.InputPartTimeEmployee"/></description></item>
    ///   <item><description>Добавление сотрудника в менеджер</description></item>
    ///   <item><description>Отображение результата операции</description></item>
    ///   <item><description>Обработка возможных ошибок</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Перехватываемые исключения:
    /// <list type="bullet">
    ///   <item><description><see cref="EmployeeCreationException"/> - ошибка создания объекта сотрудника</description></item>
    ///   <item><description><see cref="EmployeeAdditionException"/> - ошибка добавления сотрудника</description></item>
    ///   <item><description><see cref="Exception"/> - неизвестные ошибки</description></item>
    /// </list>
    /// </para>
    /// </remarks>
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
      catch (ArgumentException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeCreationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (EmployeeAdditionException ex)
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

    /// <summary>
    /// Поиск и отображение сотрудника по ID
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Ввод ID сотрудника</description></item>
    ///   <item><description>Поиск через <see cref="EmployeeManager{T}.GetById"/></description></item>
    ///   <item><description>Вывод информации о сотруднике и его зарплате</description></item>
    ///   <item><description>Обработка возможных ошибок</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Перехватываемые исключения:
    /// <list type="bullet">
    ///   <item><description><see cref="EmployeeCreationException"/> - ошибка ввода</description></item>
    ///   <item><description><see cref="ArgumentException"/> - неверный формат данных</description></item>
    ///   <item><description><see cref="EmployeeNotFoundException"/> - сотрудник не найден</description></item>
    ///   <item><description><see cref="InvalidOperationException"/> - нарушение уникальности ID</description></item>
    ///   <item><description><see cref="Exception"/> - неизвестные ошибки</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    static void GetEmployeeById()
    {
      Console.Clear();
      Console.WriteLine("3. Получить информацию о сотруднике по ID.");
      Console.Write("Введите ");
      try
      {
        int id = InputHelper.ReadInt("ID");
        Employee employee = employeeManager.GetById(id);
        Console.WriteLine("Найденный сотрудник:");
        Console.WriteLine(employee);
        Console.WriteLine($"Итоговая зарплата: {employee.CalculateSalary()}");
      }
      catch (EmployeeCreationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника было отменено");
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника было отменено");
      }
      catch (EmployeeNotFoundException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника было отменено");
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Поиск сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Поиск сотрудника было отменено");
      }
    }

    /// <summary>
    /// Обновление данных сотрудника по его ID
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Ввод ID сотрудника</description></item>
    ///   <item><description>Получение текущих данных сотрудника</description></item>
    ///   <item><description>Определение типа сотрудника</description></item>
    ///   <item><description>Ввод новых данных через метод
    ///     <see cref="InputHelper.InputFullTimeEmployee(int)"/> или
    ///     <see cref="InputHelper.InputPartTimeEmployee(int)"/>.
    ///     Параметр<paramref name="_id"/> передаётся для сохранения существующего идентификатора сотрудника.
    ///   </description></item>
    ///   <item><description>Обновление через <see cref="EmployeeManager{T}.Update"/></description></item>
    ///   <item><description>Обработка возможных ошибок</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Перехватываемые исключения:
    /// <list type="bullet">
    ///   <item><description><see cref="EmployeeCreationException"/> - ошибка ввода новых данных</description></item>
    ///   <item><description><see cref="ArgumentException"/> - неверный формат данных</description></item>
    ///   <item><description><see cref="EmployeeNotFoundException"/> - сотрудник не найден</description></item>
    ///   <item><description><see cref="InvalidOperationException"/> - нарушение уникальности ID</description></item>
    ///   <item><description><see cref="Exception"/> - неизвестные ошибки</description></item>
    /// </list>
    /// </para>
    /// </remarks>
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
      catch (EmployeeCreationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Создание сотрудника было отменено");
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (EmployeeNotFoundException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Обновление сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Обновление сотрудника было отменено");
      }
    }

    /// <summary>
    /// Удаление сотрудника по ID
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Ввод ID сотрудника</description></item>
    ///   <item><description>Поиск сотрудника через <see cref="EmployeeManager{T}.GetById"/></description></item>
    ///   <item><description>Удаление через <see cref="EmployeeManager{T}.Delete"/></description></item>
    ///   <item><description>Отображение результата операции</description></item>
    ///   <item><description>Обработка возможных ошибок</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Перехватываемые исключения:
    /// <list type="bullet">
    ///   <item><description><see cref="ArgumentException"/> - неверный формат данных</description></item>
    ///   <item><description><see cref="EmployeeNotFoundException"/> - сотрудник не найден</description></item>
    ///   <item><description><see cref="InvalidOperationException"/> - нарушение уникальности ID</description></item>
    ///   <item><description><see cref="Exception"/> - неизвестные ошибки</description></item>
    /// </list>
    /// </para>
    /// </remarks>
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
      catch (ArgumentException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (EmployeeNotFoundException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine("Удаление сотрудника было отменено");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Удаление сотрудника было отменено");
      }
    }

    /// <summary>
    /// Получение и отображение списка всех сотрудников
    /// </summary>
    /// <remarks>
    /// <para>
    /// Последовательность операций:
    /// <list type="number">
    ///   <item><description>Очистка консоли и вывод заголовка</description></item>
    ///   <item><description>Получение списка сотрудников через <see cref="EmployeeManager{T}.GetEmployees"/></description></item>
    ///   <item><description>Вывод списка через <see cref="PrintEmployees"/></description></item>
    /// </list>
    /// </para>
    /// </remarks>
    static void GetAllEmployees()
    {
      Console.Clear();
      Console.WriteLine("6. Получить информацию по всем сотрудникам.");

      List<Employee> employees = employeeManager.getEmployees();
      PrintEmployees(employees);
    }

    /// <summary>
    /// Выводит список сотрудников в консоль
    /// </summary>
    /// <param name="employees">Список сотрудников для отображения</param>
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

