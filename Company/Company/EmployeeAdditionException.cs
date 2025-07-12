using Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Исключение, возникающее при ошибке добавления сотрудника в систему
  /// </summary>
  /// <remarks>
  /// <para>
  /// Это исключение генерируется при нарушении бизнес-правил или возникновении системных ошибок
  /// в процессе добавления нового сотрудника в систему. Типичные ситуации:
  /// <list type="bullet">
  ///   <item><description>Попытка добавить null-объект сотрудника</description></item>
  ///   <item><description>Нарушение уникальности идентификатора сотрудника</description></item>
  /// </list>
  /// </para>
  /// <para>
  /// Ошибки валидации данных сотрудника (некорректный ID, имя или зарплата) вызывают <see cref="ArgumentException"/> 
  /// на этапе создания объекта и обрабатываются через <see cref="EmployeeCreationException"/>.
  /// </para>
  /// <example>
  /// <b>Пример генерации исключения:</b>
  /// <code>
  /// // В методе добавления сотрудника
  /// public void Add(T employee)
  /// {
  ///   if (employee == null)
  ///     throw new EmployeeAdditionException("Нельзя добавить пустого сотрудника");
  ///   T? results = employees.SingleOrDefault(emp => emp.ID == employee.ID);
  ///   if (results != null)
  ///     throw new EmployeeAdditionException($"Сотрудник с id = {employee.ID} уже добавлен");
  ///   employees.Add(employee);
  /// }
  /// </code>
  /// 
  /// <b>Пример обработки исключения:</b>
  /// <code>
  /// try
  /// {
  ///   var fullTimeEmployee = InputHelper.InputFullTimeEmployee();
  ///   employeeManager.Add(fullTimeEmployee);
  ///   Console.WriteLine($"Сотрудник {fullTimeEmployee} успешно добавлен!");
  /// }
  /// catch (EmployeeAdditionException ex) // если были проблемы при добавлении нового сотрудника
  /// {
  ///   Console.WriteLine($"{ex.Message}");
  ///   Console.WriteLine("Создание сотрудника было отменено");
  /// }
  /// </code>
  /// </example>
  /// </remarks>
  public class EmployeeAdditionException : Exception
  {
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeAdditionException"/>
    /// </summary>
    /// <remarks>
    /// Создает исключение по умолчанию
    /// </remarks>
    public EmployeeAdditionException() { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeAdditionException"/> 
    /// с указанным сообщением об ошибке
    /// </summary>
    /// <param name="message">
    /// Сообщение, описывающее ошибку. Рекомендуется включать:
    /// <list type="bullet">
    ///   <item><description>Идентификатор сотрудника</description></item>
    ///   <item><description>Тип операции добавления</description></item>
    ///   <item><description>Конкретную причину ошибки</description></item>
    /// </list>
    /// </param>
    /// <example>
    /// throw new EmployeeAdditionException($"Сотрудник с id = {employee.ID} уже добавлен");
    /// </example>
    public EmployeeAdditionException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeAdditionException"/> 
    /// с указанным сообщением об ошибке и ссылкой на внутреннее исключение
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="innerException">
    /// Исключение, вызвавшее текущее исключение. Используется для сохранения stack trace
    /// и контекста исходной ошибки.
    /// </param>
    /// <remarks>
    /// Этот конструктор следует использовать при обработке других исключений
    /// </remarks>
    public EmployeeAdditionException(string message, Exception innerException) : base(message, innerException) { }
  }
}
