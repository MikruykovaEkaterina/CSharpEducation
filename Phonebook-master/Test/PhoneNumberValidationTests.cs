using System.ComponentModel.DataAnnotations;
using Phonebook;
using System.Collections.Generic;
using System;

namespace Test
{
  public class PhoneNumberValidationTests
  {
    [Test]
    public void Validate_InvalidPhoneNumber_ThrowException()
    {
      var number = new PhoneNumber("1234", PhoneNumberType.Work);

      Assert.Throws(Is.TypeOf<ArgumentException>()
        .And.Message.EqualTo("Phone number is invalid"),
        () => PhoneNumberValidator.Validate(number));
    }

    [Test]
    public void Validate_ValidPhoneNumber_NotThrowException()
    {
      var number = new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Work);

      Assert.Throws(Is.Null, () => PhoneNumberValidator.Validate(number));
    }

    [Test]
    public void ValidateList_OneInvalidPhoneNumberInList_ThrowException()
    {
      List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
      {
        new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Work),
        new PhoneNumber("1234", PhoneNumberType.Work)
      };

      Assert.Throws(Is.TypeOf<ArgumentException>()
        .And.Message.EqualTo("Phone number is invalid"),
        () => PhoneNumberValidator.ValidateList(phoneNumbers));
    }

    [Test]
    public void ValidateList_AllInvalidPhoneNumberInList_ThrowException()
    {
      List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
      {
        new PhoneNumber("aaaaaaaaaaaaaaaaa", PhoneNumberType.Work),
        new PhoneNumber("1234", PhoneNumberType.Work)
      };

      Assert.Throws(Is.TypeOf<ArgumentException>()
        .And.Message.EqualTo("Phone number is invalid"),
        () => PhoneNumberValidator.ValidateList(phoneNumbers));
    }

    [Test]
    public void ValidateList_AllValidPhoneNumberInList_NotThrowException()
    {
      List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
      {
        new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Work),
        new PhoneNumber("+7 (156) 938-0000", PhoneNumberType.Work),
      };

      Assert.Throws(Is.Null, () => PhoneNumberValidator.ValidateList(phoneNumbers));
    }
  }
}