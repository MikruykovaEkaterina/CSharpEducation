using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
  internal class Phonebook
  {
    private static Phonebook instance;
    private List<Abonent> abonents = new List<Abonent>();
    private Phonebook()
    { }

    public static Phonebook Instance()
    {
      if (instance == null)
        instance = new Phonebook();
      return instance;
    }

    public void AddAbonent(Abonent abonent) //добавляем абонента
    {
      if(GetAbonentByName(abonent.Name) != null)
        throw new Exception($"Имя {abonent.Name} уже используется");
      if (GetAbonentByNumber(abonent.Number) != null)
        throw new Exception($"Номер {abonent.Number} уже используется");
      abonents.Add(abonent);
    }

    public Abonent GetAbonentByNumber(string number) //получаем абонента по номеру
    {
      for (int i = 0; i<abonents.Count; i++)
      {
        if(Equals(abonents[i].Number, number)) 
        {
          return abonents[i];
        }
      }
      return null;
    }

    public Abonent GetAbonentByName(string name) //получаем абонента по имени
    {
      for (int i = 0; i < abonents.Count; i++)
      {
        if (Equals(abonents[i].Name, name))
        {
          return abonents[i];
        }
      }
      return null;
    }

    public void DeleteAbonentByAbonent(Abonent abonent, string filePath) //удалить абонента по абоненту
    {
      abonents.Remove(abonent);
      DeleteAbonentFromFile(abonent, filePath);
    }

    public void DeleteAbonentFromList(int number, string filePath) //удалить абонента из списка
    {
      if (number > abonents.Count || number < 1)
        throw new Exception("Не допустимый номер из списка");
      else
      {
        Abonent abonentToDelete = abonents[number - 1];
        DeleteAbonentFromFile(abonentToDelete, filePath);
        abonents.RemoveAt(number - 1);
      }

    }

    public void DeleteAbonentFromFile(Abonent abonent, string filePath) //удалить абонента из файла
    {
      List<string> lines = new List<string>();
      lines.Add(abonent.Name);
      lines.Add(abonent.Number);
      File.WriteAllLines(filePath, File.ReadLines(filePath).Where(l => !lines.Contains(l)).ToList());
    }

    public List<Abonent> GetAbonents() // получить абонентов
    {
      return abonents;
    }

    public void GetAbonentsFromFile(string filePath) //получить абонентов из файла
    {
      using (StreamReader sr = new StreamReader(filePath))
      {
        string name, number;
        while ((name = sr.ReadLine()) != null && (number = sr.ReadLine()) != null)
        {
          var abonent = new Abonent(name, number);
          try
          {
            AddAbonent(abonent);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.ToString());
          }
        }
      }
    }

    public void PutAbonentInFile(string filePath, Abonent abonent) //добавить абонента в файл
    {
      using (StreamWriter writer = new StreamWriter(filePath, true))
      {
        writer.WriteLine(abonent.Name);
        writer.WriteLine(abonent.Number);
      }
    }
  }

}
