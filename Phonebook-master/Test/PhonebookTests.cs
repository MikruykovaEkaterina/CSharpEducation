using Phonebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
  public class PhonebookTests
  {
    private Subscriber correctSubscriber;
    private Subscriber wrongNumberSubscriber;

    [SetUp]
    public void Setup()
    {
      correctSubscriber = new Subscriber("Egor",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 000-0000", PhoneNumberType.Personal) });

      wrongNumberSubscriber = new Subscriber("Kate",
        new List<PhoneNumber> { new PhoneNumber("1234", PhoneNumberType.Personal) });
    }

    [Test]
    public void Constructor_WithoutSubscriberList_InitializesPhonebook()
    {
      var phonebook = new Phonebook.Phonebook();
      Assert.That(phonebook.GetAll().Count(), Is.EqualTo(0));
    }

    [Test]
    public void Constructor_WithCorrectSubscriberList_InitializesPhonebook()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var subscribers = phonebook.GetAll().ToList();

      Assert.Contains(correctSubscriber, subscribers);
      Assert.That(phonebook.GetAll().Count(), Is.EqualTo(1));
    }

    [Test]
    public void Constructor_WithDuplicateSubscriberInList_ThrowException()
    {
      Assert.Throws<InvalidOperationException>(() =>
      {
        var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber, correctSubscriber });
      }, "Unable to add subscriber. Subscriber exists");
    }

    [Test]
    public void Constructor_WithWrongNumberSubscriberInList_ThrowException()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber, wrongNumberSubscriber });
      }, "Phone number is invalid");
    }

    [Test]
    public void GetSubscriber_SubscriberInList_GetsSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var subscriber = phonebook.GetSubscriber(correctSubscriber.Id);

      Assert.That(subscriber.Id, Is.EqualTo(correctSubscriber.Id));
    }

    [Test]
    public void GetSubscriber_SubscriberNotInList_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var guid = Guid.NewGuid();
      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {guid} not found."),
        () => phonebook.GetSubscriber(guid));
    }

    [Test]
    public void GetSubscriber_DefaultGuid_ThrowsNotFoundException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {default(Guid)} not found."), 
        () => phonebook.GetSubscriber(default(Guid)));
    }

    [Test]
    public void AddSubscriber_ValidSubscriber_AddsSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook();
      phonebook.AddSubscriber(correctSubscriber);

      var subscribers = phonebook.GetAll().ToList();
      Assert.Contains(correctSubscriber, subscribers);
      Assert.That(subscribers.Count(), Is.EqualTo(1));
    }

    [Test]
    public void AddSubscriber_WrongNumberSubscriber_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook();

      Assert.Throws(Is.TypeOf<ArgumentException>().
        And.Message.EqualTo("Phone number is invalid"),
        () => phonebook.AddSubscriber(wrongNumberSubscriber));
    }

    [Test]
    public void AddSubscriber_DuplicateSubscriber_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo("Unable to add subscriber. Subscriber exists"),
        () => phonebook.AddSubscriber(correctSubscriber));
    }

    [Test]
    public void AddSubscriber_Null_ThrowsException()
    {
      var phonebook = new Phonebook.Phonebook();
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("Subscriber to add cannot be null. (Parameter 'subscriber')"),
        () => phonebook.AddSubscriber(null));
    }

    [Test]
    public void AddNumberToSubscriber_SubscriberInListValidNumber_AddsSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var newPhoneNumber = new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Personal);

      phonebook.AddNumberToSubscriber(correctSubscriber, newPhoneNumber);

      var subscriber = phonebook.GetSubscriber(correctSubscriber.Id);
      Assert.Contains(newPhoneNumber, subscriber.PhoneNumbers);
      Assert.That(subscriber.PhoneNumbers.Count(), Is.EqualTo(2));
    }

    [Test]
    public void AddNumberToSubscriber_SubscriberInListInvalidNumber_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var newPhoneNumber = new PhoneNumber("+1111", PhoneNumberType.Personal);

      Assert.Throws(Is.TypeOf<ArgumentException>().
        And.Message.EqualTo("Phone number is invalid"),
        () => phonebook.AddNumberToSubscriber(correctSubscriber, newPhoneNumber));
    }

    public void AddNumberToSubscriber_SubscriberNotInList_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook();
      var newPhoneNumber = new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Personal);

      Assert.Throws(Is.TypeOf<InvalidOperationException>()
        .And.Message.EqualTo(($"Subscriber with id {correctSubscriber.Id} not found.")),
        () => phonebook.AddNumberToSubscriber(correctSubscriber, newPhoneNumber));
    }

    [Test]
    public void AddNumberToSubscriber_NullSubscriber_ThrowsException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var number = new PhoneNumber("+7 (912) 345-6789", PhoneNumberType.Work);
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("Subscriber to add number cannot be null. (Parameter 'subscriber')"),
        () => phonebook.AddNumberToSubscriber(null, number));
    }

    [Test]
    public void RenameSubscriber_SubscriberInList_RenamesSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var newName = "Kate";
      phonebook.RenameSubscriber(correctSubscriber, newName);

      var subscriber = phonebook.GetSubscriber(correctSubscriber.Id);
      Assert.That(subscriber.Name, Is.EqualTo(newName));
    }

    [Test]
    public void RenameSubscriber_SubscriberNotInList_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var newName = "Kate";

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {wrongNumberSubscriber.Id} not found."),
        () => phonebook.RenameSubscriber(wrongNumberSubscriber, newName));
    }

    [Test]
    public void RenameSubscriber_NullSubscriber_ThrowsException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("Subscriber to rename cannot be null. (Parameter 'subscriber')"),
        () => phonebook.RenameSubscriber(null, "NewName"));
    }

    [Test]
    public void UpdateSubscriber_OldSubscriberInListValidNewSubscriber_UpdatesSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      var newCorrectSubscriber = new Subscriber("Kate",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Work) });

      phonebook.UpdateSubscriber(correctSubscriber, newCorrectSubscriber);

      var subscribers = phonebook.GetAll().ToList();
      Assert.Contains(newCorrectSubscriber, subscribers);
      Assert.That(subscribers, Is.Not.Contains(correctSubscriber));
    }

    [Test]
    public void UpdateSubscriber_OldSubscriberInListInvalidNewSubscriber_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });

      Assert.Throws(Is.TypeOf<ArgumentException>().
        And.Message.EqualTo("Phone number is invalid"),
        () => phonebook.UpdateSubscriber(correctSubscriber, wrongNumberSubscriber));
    }

    [TestCase("+7 (912) 111-1111")]
    [TestCase("1234")]
    public void UpdateSubscriber_ValidOldSubscriberNotInList_ThrowException(string number)
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { });
      var newSubscriber = new Subscriber("Kate",
        new List<PhoneNumber> { new PhoneNumber(number, PhoneNumberType.Work) });

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {correctSubscriber.Id} not found."),
        () => phonebook.UpdateSubscriber(correctSubscriber, newSubscriber));
    }

    [TestCase("+7 (912) 111-1111")]
    [TestCase("1234")]
    public void UpdateSubscriber_InValidOldSubscriberNotInList_ThrowException(string number)
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { });
      var newSubscriber = new Subscriber("Kate",
        new List<PhoneNumber> { new PhoneNumber(number, PhoneNumberType.Work) });

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {wrongNumberSubscriber.Id} not found."),
        () => phonebook.UpdateSubscriber(wrongNumberSubscriber, newSubscriber));
    }

    [Test]
    public void UpdateSubscriber_ExistingIdForNewSubscriber_ThrowException()
    {
      var subscriberWithSameId = new Subscriber("Kate",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Work) });

      var phonebook = new Phonebook.Phonebook(new List<Subscriber>
      { correctSubscriber, subscriberWithSameId });

      var newSubscriberWithSameId = new Subscriber(subscriberWithSameId.Id, "Kate",
        new List<PhoneNumber> { new PhoneNumber("+7 (912) 111-1111", PhoneNumberType.Personal) });

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Unable to update subscriber. Subscriber with the ID {subscriberWithSameId.Id} already exists."),
        () => phonebook.UpdateSubscriber(correctSubscriber, newSubscriberWithSameId));
    }

    [Test]
    public void UpdateSubscriber_NullOldSubscriber_ThrowsException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("Old subscriber cannot be null. (Parameter 'oldSubscriber')"),
        () => phonebook.UpdateSubscriber(null, correctSubscriber));
    }

    [Test]
    public void UpdateSubscriber_NullNewSubscriber_ThrowsException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("New subscriber cannot be null. (Parameter 'newSubscriber')"),
        () => phonebook.UpdateSubscriber(correctSubscriber, null));
    }

    [Test]
    public void DeleteSubscriber_SubscriberInList_DeletesSuccessfully()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });
      phonebook.DeleteSubscriber(correctSubscriber);

      var subscribers = phonebook.GetAll().ToList();
      Assert.That(subscribers, Is.Not.Contains(correctSubscriber));
    }

    [Test]
    public void DeleteSubscriber_SubscriberNotInList_ThrowException()
    {
      var phonebook = new Phonebook.Phonebook(new List<Subscriber> { correctSubscriber });

      Assert.Throws(Is.TypeOf<InvalidOperationException>().
        And.Message.EqualTo($"Subscriber with id {wrongNumberSubscriber.Id} not found."),
        () => phonebook.DeleteSubscriber(wrongNumberSubscriber));
    }

    [Test]
    public void DeleteSubscriber_Null_ThrowsArgumentNullException()
    {
      var phonebook = new Phonebook.Phonebook();
      Assert.Throws(Is.TypeOf<ArgumentNullException>().
        And.Message.EqualTo("Subscriber to delete cannot be null. (Parameter 'subscriberToDelete')"), 
        () => phonebook.DeleteSubscriber(null));
    }
  }
}
