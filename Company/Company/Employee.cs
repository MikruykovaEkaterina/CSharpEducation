using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Представляет абстрактного сотрудника компании
  /// </summary>
  /// <remarks>
  /// Этот класс служит базовым для всех типов сотрудников.
  /// Реализация должна быть предоставлена в производных классах.
  /// </remarks>
  internal abstract class Employee
  {
    /// <summary>
    /// Уникальный идентификатор сотрудника
    /// </summary>
    /// <value>
    /// Целочисленное значение, гарантированно уникальное в рамках компании
    /// </value>
    public abstract int ID { get; set; }

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    /// <value>
    /// Непустая строка
    /// </value>
    public abstract string Name { get; set; }

    /// <summary>
    /// Базовый оклад сотрудника
    /// </summary>
    /// <value>
    /// Положительное десятичное число, представляющее базовую часть зарплаты
    /// </value>
    public abstract decimal BaseSalary { get; set; }

    /// <summary>
    /// Вычисляет итоговую зарплату сотрудника
    /// </summary>
    /// <returns>
    /// Рассчитанная сумма зарплаты в десятичном формате
    /// </returns>
    /// <remarks>
    /// Реализация разная, для разных видов сотрудников
    /// </remarks>
    /// /// <example>
    /// Примеры реализации для разных типов сотрудников:
    /// <code>
    /// // Для полного сотрудника:
    /// return BaseSalary;
    /// 
    /// // Для частичного сотрудника:
    /// return workedHours * hourlyRate;
    /// </code>
    /// </example>
    public abstract decimal CalculateSalary();

    /// <summary>
    /// Возвращает строковое представление сотрудника в формате, специфичном для типа сотрудника
    /// </summary>
    /// <returns>
    /// Строка, содержащая основные характеристики сотрудника
    /// </returns>
    public abstract override string ToString();
  }
}
