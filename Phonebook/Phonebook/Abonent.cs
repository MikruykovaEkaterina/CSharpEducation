using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
  internal class Abonent
  {
    public readonly string Name;
    public readonly string Number;

    public Abonent(string name, string number)
    {
      this.Name = name;
      this.Number = number;
    }
  }
}
