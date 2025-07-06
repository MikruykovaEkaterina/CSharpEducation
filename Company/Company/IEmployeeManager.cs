using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  internal interface IEmployeeManager<T>
  {
    void Add(T employee);
    T Get(string name);
    void Update(T employee);
    void Delete(T employee);
  }
}
