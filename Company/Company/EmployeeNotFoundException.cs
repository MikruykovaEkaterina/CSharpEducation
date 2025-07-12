using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Исключение, возникающее при попытке выполнить операцию с сотрудником, который не найден в системе
  /// </summary>
  /// <remarks>
  /// <para>
  /// Это исключение генерируется при операциях, требующих наличия сотрудника в системе, когда запрошенный
  /// сотрудник отсутствует. Типичные ситуации:
  /// <list type="bullet">
  ///   <item><description>Попытка удалить несуществующего сотрудника</description></item>
  ///   <item><description>Попытка получить данные сотрудника по несуществующему ID</description></item>
  ///   <item><description>Попытка обновить данные отсутствующего сотрудника</description></item>
  ///   <item><description>Любые другие операции, требующие наличия конкретного сотрудника</description></item>
  /// </list>
  /// </para>
  /// <para>
  /// Исключение содержит информацию о критериях поиска (ID, имя и т.д.), по которым сотрудник не был найден.
  /// </para>
  /// <example>
  /// <b>Пример генерации исключения:</b>
  /// <code>
  /// public void DeleteEmployee(Employee employee)
  /// {
  ///   if (!employees.Contains(employee))
  ///     throw new EmployeeNotFoundException($"Сотрудник: Имя = {employee.Name}, ID = {employee.ID} не найден");
  ///     
  ///   employees.Remove(employee);
  /// }
  /// 
  /// public Employee GetEmployeeById(int id)
  /// {
  ///   var employee = employees.FirstOrDefault(e => e.ID == id);
  ///   if (employee == null)
  ///     throw new EmployeeNotFoundException($"Сотрудник с ID={id} не найден");
  ///         
  ///   return employee;
  /// }
  /// </code>
  /// 
  /// <b>Пример обработки исключения:</b>
  /// <code>
  /// try
  /// {
  ///   int id = InputHelper.ReadInt("Введите ID сотрудника");
  ///   Employee employee = employeeManager.GetEmployeeById(id);
  ///   employeeManager.DeleteEmployee(employee);
  ///   Console.WriteLine("Сотрудник успешно удален!");
  /// }
  /// catch (EmployeeNotFoundException ex)
  /// {
  ///   Console.WriteLine($"Ошибка: {ex.Message}");
  ///   Console.WriteLine("Операция удаления отменена");
  /// }
  /// </code>
  /// </example>
  /// </remarks>
  public class EmployeeNotFoundException : Exception
  {
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeNotFoundException"/>
    /// </summary>
    /// <remarks>
    /// Создает исключение по умолчанию
    /// </remarks>
    public EmployeeNotFoundException() { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeNotFoundException"/> 
    /// с указанным сообщением об ошибке
    /// </summary>
    /// <param name="message">
    /// Сообщение, описывающее ошибку. Должно содержать:
    /// <list type="bullet">
    ///   <item><description>Критерии поиска (ID, имя и т.д.)</description></item>
    ///   <item><description>Дополнительную информацию для диагностики</description></item>
    /// </list>
    /// </param>
    /// <example>
    /// new EmployeeNotFoundException($"Сотрудник: Имя = {employee.Name}, ID = {employee.ID} не найден")
    /// </example>
    public EmployeeNotFoundException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeNotFoundException"/> 
    /// с указанным сообщением об ошибке и ссылкой на внутреннее исключение
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="innerException">
    /// Исключение, вызвавшее текущее исключение. Используется, когда отсутствие сотрудника
    /// является результатом другой ошибки (например, ошибки подключения к базе данных).
    /// </param>
    /// <remarks>
    /// Этот конструктор следует использовать, когда проверка существования сотрудника
    /// требует внешних операций, которые могут вызвать исключения:
    /// </remarks>     
    public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
  }
}
