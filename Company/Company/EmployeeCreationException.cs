using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Исключение, возникающее при ошибке создания объекта сотрудника
  /// </summary>
  /// <remarks>
  /// <para>
  /// Это исключение генерируется при ошибках в процессе создания экземпляра сотрудника. Типичные ситуации:
  /// <list type="bullet">
  ///   <item><description>Некорректные входные данные (ID, имя, зарплата)</description></item>
  ///   <item><description>Ошибки формата при вводе данных</description></item>
  /// </list>
  /// </para>
  /// <example>
  /// <b>Пример генерации исключения:</b>
  /// <code>
  /// public static FullTimeEmployee InputFullTimeEmployee(int _id = -1)
  /// {
  ///   try
  ///   {
  ///     int id = _id == -1 ? ReadInt("ID") : _id;
  ///     string name = ReadString("Имя");
  ///     decimal baseSalary = ReadDecimal("Зарплата");
  ///     return new FullTimeEmployee(id, name, baseSalary);
  ///   }
  ///   catch (ArgumentException ex)
  ///   {
  ///     throw new EmployeeCreationException(ex.Message, ex);
  ///   }
  ///   catch (Exception ex)
  ///   {
  ///     throw new EmployeeCreationException("Неизвестная ошибка при создании сотрудника", ex);
  ///   }
  /// }
  /// </code>
  /// 
  /// <b>Пример обработки исключения:</b>
  /// <code>
  /// static void AddFullTimeEmployee()
  /// {
  ///   Console.Clear();
  ///   Console.WriteLine("1. Добавить полного сотрудника.");
  ///   Console.Write("Введите данные для сотрудника\n");
  /// 
  ///   try
  ///   {
  ///     var fullTimeEmployee = InputHelper.InputFullTimeEmployee();
  ///     employeeManager.Add(fullTimeEmployee);
  ///     Console.WriteLine($"Сотрудник {fullTimeEmployee} успешно добавлен!");
  ///   }
  ///   catch (EmployeeCreationException ex)
  ///   {
  ///     Console.WriteLine($"Ошибка создания сотрудника: {ex.Message}");
  ///     Console.WriteLine("Создание сотрудника было отменено");
  ///   }
  /// }
  /// </code>
  /// </example>
  /// </remarks>
  public class EmployeeCreationException : Exception
  {
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeCreationException"/>
    /// </summary>
    /// <remarks>
    /// Создает исключение по умолчанию
    /// </remarks>
    public EmployeeCreationException() { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeCreationException"/> 
    /// с указанным сообщением об ошибке
    /// </summary>
    /// <param name="message">
    /// Сообщение, описывающее ошибку. Рекомендуется включать:
    /// <list type="bullet">
    ///   <item><description>Конкретное поле с ошибкой</description></item>
    ///   <item><description>Причину ошибки</description></item>
    /// </list>
    /// </param>
    /// <example>
    /// new EmployeeCreationException("Некорректное значение зарплаты: -1500. Зарплата должна быть положительной")
    /// </example>
    public EmployeeCreationException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeCreationException"/> 
    /// с указанным сообщением об ошибке и ссылкой на внутреннее исключение
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="innerException">
    /// Исключение, вызвавшее текущее исключение. Используется для сохранения stack trace
    /// и исходного контекста ошибки.
    /// </param>
    /// <remarks>
    /// Этот конструктор следует использовать при обработке других исключений
    /// </remarks>
    public EmployeeCreationException(string message, Exception innerException) : base(message, innerException) { }
  }
}
