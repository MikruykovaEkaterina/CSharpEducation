using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  /// <summary>
  /// Представляет сотрудника с почасовой оплатой труда
  /// </summary>
  /// <remarks>
  /// Наследуется от абстрактного класса <see cref="Employee"/>.
  /// Зарплата рассчитывается как произведение отработанных часов на часовую ставку.
  /// </remarks>
  internal class PartTimeEmployee : Employee
  {
    #region Поля и свойства

    private decimal hourlyRate;
    /// <summary>
    /// Базовый оклад сотрудника
    /// </summary>
    /// <value>
    /// Положительное десятичное число, представляющее почасовую ставку.
    /// </value>
    /// <exception cref="ArgumentException">
    /// Выбрасывается при попытке установить не положительное значение.
    /// </exception>
    public decimal HourlyRate
    {
      get
      {
        return hourlyRate;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (Почасовая ставка = '{value}'). " +
            $"Почасовая ставка должна быть положительной");
        hourlyRate = value;
      }
    }

    private decimal workedHours;
    /// <summary>
    /// Базовый оклад сотрудника
    /// </summary>
    /// <value>
    /// Положительное десятичное число, представляющее отработанные часы.
    /// </value>
    /// <exception cref="ArgumentException">
    /// Выбрасывается при попытке установить не положительное значение.
    /// </exception>
    public decimal WorkedHours
    {
      get
      {
        return workedHours;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (Отработанные часы = '{value}'). " +
            $"Отработанные часы должны быть положительными");
        workedHours = value;
      }
    }

    #endregion

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
          throw new ArgumentException($"Введено недопустимое значение для (ID = '{value}'). " +
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

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">
    /// Выбрасывается при попытке установить или получить значение.
    /// </exception>
    public override decimal BaseSalary
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public override decimal CalculateSalary()
    {
      return WorkedHours * HourlyRate;
    }

    public override string ToString()
    {
      return $"ID: {ID}; Имя: {Name}; Почасовая ставка: {HourlyRate}; Отработанные часы: {workedHours}";
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PartTimeEmployee"/>
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника</param>
    /// <param name="name">Полное имя сотрудника</param>
    /// <param name="hourlyRate"> Почасовая ставка сотрудника</param>
    /// <param name="workedHours">Отработанные часы сотрудника</param>
    /// <exception cref="ArgumentException">
    /// Возникает если любой из параметров не соответствует бизнес-правилам
    /// </exception>
    public PartTimeEmployee(int id, string name, decimal hourlyRate, decimal workedHours)
    {
      ID = id;
      Name = name;
      HourlyRate = hourlyRate;
      WorkedHours = workedHours;
    }

    #endregion
  }
}
