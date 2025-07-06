using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
  public class EmployeeCreationException : Exception
  {
    public EmployeeCreationException() { }

    public EmployeeCreationException(string message) : base(message) { }

    public EmployeeCreationException(string message, Exception innerException) : base(message, innerException) { }
  }
  public class EmployeeAdditionException : Exception
  {
    public EmployeeAdditionException() { }

    public EmployeeAdditionException(string message) : base(message) { }

    public EmployeeAdditionException(string message, Exception innerException) : base(message, innerException) { }
  }

  public class EmployeeNotFoundException : Exception
  {
    public EmployeeNotFoundException() { }

    public EmployeeNotFoundException(string message) : base(message) { }

    public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
  }
}
