using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  internal abstract class Employee
  {
    public abstract int ID { get; set; }
    public abstract string Name { get; set; }
    public abstract decimal BaseSalary { get; set; }
    public abstract decimal CalculateSalary();
  }
}
