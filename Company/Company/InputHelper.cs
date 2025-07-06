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
  internal class InputHelper
  {

    public static string ReadString(string prompt)
    {
      Console.Write($"{prompt}: ");
      string? value = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException($"Введено недопустимое значение для ({prompt})");
      return value;
    }

    public static int ReadInt(string prompt)
    {
      Console.Write($"{prompt}: ");
      try
      {
        string? input = Console.ReadLine();
        int value = int.Parse(input);
        return value;
      }
      catch
      {
        throw new ArgumentException($"Введено недопустимое значение для ({prompt})");
      }
    }

    public static decimal ReadDecimal(string prompt)
    {
      Console.Write($"{prompt}: ");
      try
      {
        string? input = Console.ReadLine();
        decimal value = decimal.Parse(input);
        return value;
      }
      catch
      {
        throw new ArgumentException($"Введено недопустимое значение для ({prompt})");
      }
    }

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
      catch (ArgumentException ex)
      {
        throw new EmployeeCreationException(ex.Message);
      }
      catch
      {
        throw new EmployeeCreationException("Неизвестная ошибка");
      }
    }

    public static PartTimeEmployee InputPartTimeEmployee(int _id = -1)
    {
      try
      {
        int id = _id == -1 ? ReadInt("ID"): _id;
        string name = ReadString("Имя");
        decimal hourlyRate = ReadDecimal("Почасовая ставка");
        decimal workedHours = ReadDecimal("Отработанные часы");
        PartTimeEmployee partTimeEmployee = new PartTimeEmployee(id, name, hourlyRate, workedHours);
        return partTimeEmployee;
      }
      catch (ArgumentException ex)
      {
        throw new EmployeeCreationException(ex.Message);
      }
      catch
      {
        throw new EmployeeCreationException("Неизвестная ошибка");
      }
    }
  }
}
