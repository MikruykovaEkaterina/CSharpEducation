using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  internal class PartTimeEmployee : Employee
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
    
    private decimal hourlyRate;
    public decimal HourlyRate
    {
      get { return hourlyRate; }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (Почасовая ставка). Почасовая ставка должна быть положительной");
        hourlyRate = value;
      }
    }

    private decimal workedHours;
    public decimal WorkedHours
    {
      get { return workedHours; }
      set
      {
        if (value <= 0)
          throw new ArgumentException($"Введено недопустимое значение для (Отработанные часы). Отработанные часы должны быть положительными");
        workedHours = value;
      }
    }

    public override decimal BaseSalary 
    { 
      get => throw new NotImplementedException(); 
      set => throw new NotImplementedException(); 
    }

    public override decimal CalculateSalary()
    {
      return workedHours * hourlyRate;
    }

    public PartTimeEmployee(int id, string name, decimal hourlyRate, decimal workedHours)
    {
      ID = id;
      Name = name;
      HourlyRate = hourlyRate;
      WorkedHours = workedHours;
    }

    public override string ToString()
    {
      return $"ID: {ID}; Имя: {Name}; Почасовая ставка: {HourlyRate}; Отработанные часы: {workedHours}";
    }
  }
}
