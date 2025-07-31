using Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Company
{
  /// <summary>
  /// Статический класс, предоставляющий методы для ввода данных с консоли и создания экземпляров сотрудников.
  /// </summary>
  internal static class InputHelper
  {
    /// <summary>
    /// Считывает значение указанного типа с консоли.
    /// </summary>
    /// <typeparam name="T">Тип значения для считывания.</typeparam>
    /// <param name="prompt">Подсказка для ввода.</param>
    /// <param name="parser">Функция для парсинга строки в нужный тип.</param>
    /// <returns>Введенное значение указанного типа.</returns>
    /// <exception cref="ArgumentException">Вызывается, если введено недопустимое значение.</exception>
    public static T ReadValue<T>(string prompt, Func<string, T> parser)
    {
      Console.Write($"{prompt}: ");
      string? input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input))
        throw new ArgumentException($"Введено недопустимое значение для ({prompt}) = '{input}'");
      
      try
      {
        return parser(input);
      }
      catch
      {
        throw new ArgumentException($"Введено недопустимое значение для ({prompt}) = '{input}'");
      }
    }

    /// <summary>
    /// Считывает строковое значение с консоли.
    /// </summary>
    /// <param name="prompt">Подсказка для ввода.</param>
    /// <returns>Введенное строковое значение.</returns>
    /// <exception cref="ArgumentException">Вызывается, если введено недопустимое значение (пустая строка).</exception>
    public static string ReadString(string prompt)
    {
      return ReadValue(prompt, input => input);
    }

    /// <summary>
    /// Считывает целочисленное значение с консоли.
    /// </summary>
    /// <param name="prompt">Подсказка для ввода.</param>
    /// <returns>Введенное целочисленное значение.</returns>
    /// <exception cref="ArgumentException">Вызывается, если введено недопустимое значение.</exception>
    public static int ReadInt(string prompt)
    {
      return ReadValue(prompt, int.Parse);
    }

    /// <summary>
    /// Считывает десятичное значение с консоли.
    /// </summary>
    /// <param name="prompt">Подсказка для ввода.</param>
    /// <returns>Введенное десятичное значение.</returns>
    /// <exception cref="ArgumentException">Вызывается, если введено недопустимое значение.</exception>
    public static decimal ReadDecimal(string prompt)
    {
      return ReadValue(prompt, decimal.Parse);
    }

    /// <summary>
    /// Создает экземпляр сотрудника с полной занятостью на основе введенных данных.
    /// </summary>
    /// <param name="_id">Идентификатор сотрудника (по умолчанию -1 для создания нового сотрудника, иначе для обновления существующего сотрудника).</param>
    /// <returns>Экземпляр сотрудника с полной занятостью.</returns>
    /// <exception cref="EmployeeCreationException">Вызывается, если возникли проблемы при создании сотрудника.</exception>
    /// <exception cref="ArgumentException">Вызывается, если при создании сотрудника было введено недопустимое значение.</exception>
    public static FullTimeEmployee InputFullTimeEmployee(int _id = -1)
    {
      try
      {
        int id = _id == -1 ? ReadInt("ID") : _id;
        string name = ReadString("Имя");
        decimal baseSalary = ReadDecimal("Зарплата");

        FullTimeEmployee fullTimeEmployee = new FullTimeEmployee(id, name, baseSalary);
        return fullTimeEmployee;
      }
      catch (ArgumentException)
      {
        throw;
      }
      catch
      {
        throw new EmployeeCreationException("Неизвестная ошибка");
      }
    }

    /// <summary>
    /// Создает экземпляр сотрудника с частичной занятостью на основе введенных данных.
    /// </summary>
    /// <param name="_id">Идентификатор сотрудника (по умолчанию -1 для создания нового сотрудника, иначе для обновления существующего сотрудника).</param>
    /// <returns>Экземпляр сотрудника с полной занятостью.</returns>
    /// <exception cref="EmployeeCreationException">Вызывается, если возникли проблемы при создании сотрудника.</exception>
    /// <exception cref="ArgumentException">Вызывается, если при создании сотрудника было введено недопустимое значение.</exception>
    public static PartTimeEmployee InputPartTimeEmployee(int _id = -1)
    {
      try
      {
        int id = _id == -1 ? ReadInt("ID") : _id;
        string name = ReadString("Имя");
        decimal hourlyRate = ReadDecimal("Почасовая ставка");
        decimal workedHours = ReadDecimal("Отработанные часы");

        PartTimeEmployee partTimeEmployee = new PartTimeEmployee(id, name, hourlyRate, workedHours);
        return partTimeEmployee;
      }
      catch (ArgumentException)
      {
        throw;
      }
      catch
      {
        throw new EmployeeCreationException("Неизвестная ошибка");
      }
    }
  }
}
