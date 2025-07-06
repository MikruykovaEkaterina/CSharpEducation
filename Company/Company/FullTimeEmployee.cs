using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  internal class FullTimeEmployee: Employee
  {
    private int id;
    public override int ID
    {
      get { return id; }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (ID). ID должен быть положительным");
        id = value;
      }
    }

    private string name;
    public override string Name
    {
      get { return name; }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new ArgumentException($"Введено недопустимое значение для (Имя). Имя не может быть пустым");
        name = value;
      }
    }

    private decimal baseSalary;
    public override decimal BaseSalary
    {
      get { return baseSalary; }
      set
      {
        if (value<=0)
          throw new ArgumentException($"Введено недопустимое значение для (Зарплата). Зарплата должна быть положительной");
        baseSalary = value;
      }
    }

    public override decimal CalculateSalary()
    {
      return BaseSalary;
    }

    public FullTimeEmployee(int id, string name, decimal baseSalary)
    {
      ID = id;
      Name = name;
      BaseSalary = baseSalary;
    }

    public override string ToString()
    {
      return $"ID: {ID}; Имя: {Name}; Зарплата: {BaseSalary}";
    }
  }
}
