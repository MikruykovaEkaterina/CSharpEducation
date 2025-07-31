using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Интерфейс, определяющий основные операции управления сотрудниками.
  /// </summary>
  /// <typeparam name="T">Тип сотрудника</typeparam>
  internal interface IEmployeeManager<T>
  {
    /// <summary>
    /// Добавляет нового сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник для добавления.</param>
    void Add(T employee);

    /// <summary>
    /// Возвращает сотрудника по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns>Сотрудник с указанным идентификатором.</returns>
    T GetById(int id);

    /// <summary>
    /// Обновляет информацию о сотруднике.
    /// </summary>
    /// <param name="employee">Сотрудник для обновления.</param>
    void Update(T employee);

    /// <summary>
    /// Удаляет сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник для удаления.</param>
    void Delete(T employee);

    /// <summary>
    /// Возвращает список всех сотрудников.
    /// </summary>
    /// <returns>Список всех сотрудников.</returns>
    List<T> getEmployees();
  }
}
