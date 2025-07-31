using Phonebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
  public class PhoneNumberTests
  {
    [TestCase(PhoneNumberType.Work)]
    [TestCase(PhoneNumberType.Personal)]
    public void Create_PhoneNumberWithType_FillCorrectly(PhoneNumberType type)
    {
      var number = "+7 (912) 000-0000";
      var phoneNumber = new PhoneNumber(number, type);

      Assert.That(phoneNumber.Number, Is.EqualTo(number));
      Assert.That(phoneNumber.Type, Is.EqualTo(type));
    }

    [Test]
    public void Create_NullPhoneNumber_ThrowException()
    {
      Assert.Throws(Is.TypeOf<ArgumentNullException> (), 
        () => new PhoneNumber(null, PhoneNumberType.Work),
        "Phone number cannot be null.");
    }
  }
}
