using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Company
{
  /// <summary>
  /// Класс, отвечающий за управление сотрудниками.
  /// </summary>
  /// <typeparam name="T">Тип сотрудника.</typeparam>
  internal class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
  {
    #region Поля и свойства

    /// <summary>
    /// Внутренняя коллекция для хранения сотрудников
    /// </summary>
    /// <remarks>
    /// <para>
    /// Использует обобщенный список для хранения объектов типа <typeparamref name="T"/> в памяти.
    /// Доступ к коллекции осуществляется исключительно через публичные методы менеджера.
    /// </para>
    /// </remarks>
    private List<T> employees = new List<T>();

    #endregion

    #region <IEmployeeManager<T>>

    /// <inheritdoc/>
    /// <exception cref="EmployeeAdditionException">Вызывается, если сотрудник уже добавлен или передано значение null.</exception>
    public void Add(T employee)
    {
      if (employee == null)
        throw new EmployeeAdditionException("Нельзя добавить пустого сотрудника");
      T? results = employees.SingleOrDefault(emp => emp.ID == employee.ID);
      if (results != null)
        throw new EmployeeAdditionException($"Сотрудник с id = {employee.ID} уже добавлен");
      employees.Add(employee);
    }

    /// <inheritdoc/>
    /// <exception cref="EmployeeNotFoundException">Вызывается, если сотрудник не найден в списке.</exception>
    /// <exception cref="InvalidOperationException">Вызывается, если найдено несколько сотрудников с одним и тем же ID.</exception>
    public void Update(T employee)
    {
      T employeeToUpdateInfo = GetById(employee.ID);
      int index = employees.IndexOf(employeeToUpdateInfo);
      employees[index] = employee;
    }

    /// <inheritdoc/>
    /// <exception cref="EmployeeNotFoundException">Вызывается, если сотрудник не найден в списке.</exception>
    public void Delete(T employee)
    {
      if (!employees.Contains(employee))
        throw new EmployeeNotFoundException($"Cотрудник: Имя = {employee.Name}, ID = {employee.ID} не содержится в списке");
      employees.Remove(employee);
    }

    /// <inheritdoc/>
    /// <exception cref="EmployeeNotFoundException">Вызывается, если сотрудник не найден в списке.</exception>
    /// <exception cref="InvalidOperationException">Вызывается, если найдено несколько сотрудников с одним и тем же ID.</exception>
    public T GetById(int id)
    {
      try
      {
        T? result = employees.SingleOrDefault(emp => emp.ID == id);
        if (result == null)
          throw new EmployeeNotFoundException($"Сотрудник с id = {id} не содержится в списке");
        return result;
      }
      catch (InvalidOperationException ex)
      {
        throw new InvalidOperationException($"Найдено несколько сотрудников с ID = {id}");
      }
    }

    public List<T> getEmployees()
    {
      return employees;
    }

    #endregion
  }
}
