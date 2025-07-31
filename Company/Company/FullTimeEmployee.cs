using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Представляет сотрудника с полной занятостью и фиксированным окладом
  /// </summary>
  /// <remarks>
  /// Наследуется от абстрактного класса <see cref="Employee"/>.
  /// Зарплата рассчитывается как фиксированный оклад без дополнительных начислений.
  /// </remarks>
  internal class FullTimeEmployee : Employee
  {
    #region Базовый класс

    private int id;
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Выбрасывается при попытке установить не положительное значение.
    /// </exception>
    public override int ID
    {
      get
      {
        return id;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (ID = {value}). " +
            $"ID должен быть положительным");
        id = value;
      }
    }

    private string name;
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Выбрасывается при попытке установить пустую строку.
    /// </exception>
    public override string Name
    {
      get
      {
        return name;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new ArgumentException($"Введено недопустимое значение для (Имя = '{value}'). " +
            $"Имя не может быть пустым");
        name = value;
      }
    }

    private decimal baseSalary;
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Выбрасывается при попытке установить не положительное значение.
    /// </exception>
    public override decimal BaseSalary
    {
      get
      {
        return baseSalary;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (Зарплата = {value}). " +
            $"Зарплата должна быть положительной");
        baseSalary = value;
      }
    }

    public override decimal CalculateSalary()
    {
      return BaseSalary;
    }

    public override string ToString()
    {
      return $"ID: {ID}; Имя: {Name}; Зарплата: {BaseSalary}";
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="FullTimeEmployee"/>
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника</param>
    /// <param name="name">Полное имя сотрудника</param>
    /// <param name="baseSalary">Фиксированный оклад сотрудника</param>
    /// <exception cref="ArgumentException">
    /// Возникает если любой из параметров не соответствует бизнес-правилам.
    /// </exception>
    public FullTimeEmployee(int id, string name, decimal baseSalary)
    {
      ID = id;
      Name = name;
      BaseSalary = baseSalary;
    }

    #endregion
  }
}
