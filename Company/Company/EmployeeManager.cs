using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Company
{
  internal class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
  {
    private List<T> employees = new List<T>();

    public void Add(T employee)
    {
      if (employee == null)
        throw new EmployeeAdditionException("Нельзя добавить пустого сотрудника");
      T? results = employees.SingleOrDefault(emp => emp.ID == employee.ID);
      if (results != null)
        throw new EmployeeAdditionException($"Сотрудник с таким id уже добавлен");
      //все проверки пройдены, можно добавлять
      employees.Add(employee);
    }

    public T Get(string name)
    {
      T? result = employees.FirstOrDefault(emp => emp.Name == name); //имя не уникально
      //я бы лучше возвращала список
      if (result == null) 
        throw new EmployeeNotFoundException("Сотрудник с таким именем не содержится в списке");
      return result;
    }

    public void Update(T employee)
    {
      T employeeToUpdateInfo = GetById (employee.ID);
      int index = employees.IndexOf(employeeToUpdateInfo);
      employees[index] = employee;
    }

    public void Delete(T employee)
    {
      Console.WriteLine(employee);
      if (!employees.Contains(employee)) 
        throw new EmployeeNotFoundException("Такой сотрудник не содержится в списке");
      employees.Remove(employee);
    }

    public T GetById(int id)
    {
      T? result = employees.SingleOrDefault(emp => emp.ID == id); //id должно быть уникально
      if (result == null) 
        throw new EmployeeNotFoundException("Сотрудник с таким id не содержится в списке");
      return result;
    }

    public List<T> getEmployees()
    {
      return employees;
    }
  }
}
