using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Phonebook
{
  class Program
  {
    static Phonebook phonebook = Phonebook.Instance(); //ссылка на объект
    static string filePath = "phonebook.txt"; //путь к файлу
    static void Main(string[] args)
    {
      GetAbonentsFromFile(); // получаем всех абонентов из файла
      bool repeat = true;
      while (repeat)
      {
        Console.Write("Список действий:\n" +
          "1. Добавить абонента.\n" +
          "2. Получить абонента по номеру телефона.\n" +
          "3. Получить абонента по имени.\n" +
          "4. Удалить абонента. \n" +
          "5. Выйти из программы. \n" +
          "Введите номер выбранного действия: ");
        if (int.TryParse(Console.ReadLine(), out int number))
        {
          switch (number)
          {
            case 1:
              AddAbonent();
              break;
            case 2:
              GetAbonentByNumber();
              break;
            case 3:
              GetAbonentByName();
              break;
            case 4:
              SelectMethodDeleteAbonent();
              break;
            case 5:
              PutAbonentsInFile();
              repeat = false;
              break;
            default:
              Console.WriteLine("Ошибка! Вы ввели не допустимое число.");
              break;
          }
        }
        else
        {
          Console.WriteLine("Ошибка! Вы ввели не число.");
        }
      }
    }

    static void AddAbonent() //добавить абонента
    {
      Console.Write("Введите имя абонента: ");
      string name = Console.ReadLine();
      Console.Write("Введите номер абонента: ");
      string number = Console.ReadLine();
      Abonent abonent = new Abonent(name, number);
      if (phonebook.AddAbonent(abonent))
      {
        Console.WriteLine($"Абонент с именем: {name} и номером: {number} успешно добавлен!!");
        PutAbonentInFile(abonent);
      }

      else
        Console.WriteLine($"Ошибка. Абонент с именем: {name} или номером: {number} уже добавлен или null");
    }
    static void GetAbonentByNumber()//получить абонента по номеру
    {
      Console.Write("Введите номер абонента: ");
      string number = Console.ReadLine();
      Abonent abonent = phonebook.GetAbonentByNumber(number);
      if (abonent == null)
        Console.WriteLine($"Ошибка! Абонент с номером: {number} не зарегистрирован.");
      else
        Console.WriteLine($"Абонент с номером: {number} зарегистрирован c именем: {abonent.Name}.");
    }
    static void GetAbonentByName()//получить абонента по имени
    {
      Console.Write("Введите имя абонента: ");
      string name = Console.ReadLine();
      Abonent abonent = phonebook.GetAbonentByName(name);
      if (abonent == null)
        Console.WriteLine($"Ошибка! Абонент с именем: {name} не зарегистрирован.");
      else
        Console.WriteLine($"Абонент с именем: {name} зарегистрирован c номером: {abonent.Number}.");
    }
    static void SelectMethodDeleteAbonent()//выбрать метод удаления абонента
    {
      Console.WriteLine("Способы удаления номера:\n" +
        "1. Удалить по номеру телефона.\n" +
        "2. Удалить по имени.\n" +
        "3. Удалить по номеру из списка абонентов.\n" +
        "Выберите способ удаления номера: ");
      if (int.TryParse(Console.ReadLine(), out int number))
      {
        switch (number)
        {
          case 1:
            DeleteAbonentByNumber();
            break;
          case 2:
            DeleteAbonentByName();
            break;
          case 3:
            DeleteAbonentFromList();
            break;
          default:
            Console.WriteLine("Ошибка! Вы ввели не допустимое число.");
            break;
        }
      }
      else
      {
        Console.WriteLine("Ошибка! Вы ввели не число.");
      }
    }
    static void DeleteAbonentByNumber()//удалить абонента по номеру
    {
      Console.Write("Введите номер абонента: ");
      string number = Console.ReadLine();
      DeleteAbonent("number", number);
    }

    static void DeleteAbonentByName()//удалить абонента по имени
    {
      Console.Write("Введите имя абонента: ");
      string name = Console.ReadLine();
      DeleteAbonent("name", name);
    }
    static void DeleteAbonent(string searchCriteria, string searchValue)//удалить абонента по номеру или имени
    {
      Abonent abonent = null;

      if (searchCriteria == "number")
      {
        abonent = phonebook.GetAbonentByNumber(searchValue);
      }
      else
      {
        abonent = phonebook.GetAbonentByName(searchValue);
      }

      if (abonent == null)
      {
        Console.WriteLine($"Ошибка! Абонент с {searchCriteria} '{searchValue}' не зарегистрирован.");
      }
      else
      {
        phonebook.DeleteAbonentByAbonent(abonent);
        Console.WriteLine("Абонент успешно удалён!!");
      }
    }
    static void DeleteAbonentFromList()//удалить абонента из списка
    {
      PrintAllAbonent();
      Console.Write("Введите номер выбранного действия: ");
      if (int.TryParse(Console.ReadLine(), out int number))
      {
        if (phonebook.DeleteAbonentFromList(number))
          Console.WriteLine("Абонент успешно удалён!!");
        else
          Console.WriteLine($"Ошибка! Вы ввели не допустимое число.");
      }
      else
        Console.WriteLine("Ошибка! Вы ввели не число.");
    }
    static void PrintAllAbonent() //вывести всех абонентов
    {
      Console.WriteLine("Список всех абонентов:");
      int count = 1;
      List<Abonent> abonents = phonebook.GetAbonents();
      foreach (var abonent in abonents)
      {
        Console.WriteLine($"{count++}. Имя: {abonent.Name}. Номер: {abonent.Number}");
      }
    }
    static void GetAbonentsFromFile() //получить абонентов из файла
    {
      phonebook.GetAbonentsFromFile(filePath);
    }
    static void PutAbonentsInFile() //записать абонентов в файл
    { 
      phonebook.PutAbonentsInFile(filePath);
    }
    static void PutAbonentInFile(Abonent abonent) //записать абонентов в файл
    {
      // какой смысл в записи абонентов в файл при добавлении??
      // мы всё равно очищаем файл перед вводом абонентов при завершении программы
      // а если бы и надо было реализовать запись абонента при добавление отдельно (например, без завершения программы)
      // то и удаление бы тоже надо было реализовывать, а в тз этого нет
      phonebook.PutAbonentInFile(filePath, abonent);
    }
  }
}