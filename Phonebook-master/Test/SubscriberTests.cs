using Phonebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
  public class SubscriberTests
  {
    [Test]
    public void Create_SubscriberWithNameAndNumbers_FillCorrectly()
    {
      var name = "Egor";
      var phoneNumber = new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) };

      var subscriber = new Subscriber(name, phoneNumber);

      Assert.That(subscriber.Name, Is.EqualTo(name));
      Assert.That(subscriber.PhoneNumbers, Is.EqualTo(phoneNumber));
      Assert.That(subscriber.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void Create_SubscriberWithIdNameAndNumbers_FillCorrectly()
    {
      var id = Guid.NewGuid();
      var name = "Egor";
      var phoneNumber = new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) };

      var subscriber = new Subscriber(id, name, phoneNumber);

      Assert.That(subscriber.Name, Is.EqualTo(name));
      Assert.That(subscriber.PhoneNumbers, Is.EqualTo(phoneNumber));
      Assert.That(subscriber.Id, Is.EqualTo(id));
    }

    [Test]
    public void Equals_WithSameId_ShouldReturnTrue()
    {
      var id = Guid.NewGuid();
      
      var subscriber1 = new Subscriber(id, "Egor", 
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) });

      var subscriber2 = new Subscriber(id, "Kate",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Work) });

      Assert.IsTrue(subscriber1.Equals(subscriber2));
    }

    [Test]
    public void Equals_WithDifferentId_ShouldReturnFalse()
    {
      var name = "Egor";
      var phoneNumber = new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) };

      var subscriber1 = new Subscriber(name, phoneNumber);
      var subscriber2 = new Subscriber(name, phoneNumber);

      Assert.IsFalse(subscriber1.Equals(subscriber2));
    }

    [Test]
    public void Equals_WithNull_ShouldReturnFalse()
    {
      var subscriber = new Subscriber("Egor",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) });

      Assert.IsFalse(subscriber.Equals(null));
    }

    [Test]
    public void GetHashCode_ForSameId_ShouldReturnSameValue()
    {
      var id = Guid.NewGuid();

      var subscriber1 = new Subscriber(id, "Egor",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) });

      var subscriber2 = new Subscriber(id, "Kate",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Work) });

      Assert.That(subscriber1.GetHashCode(), Is.EqualTo(subscriber2.GetHashCode()));
    }
  }
}
