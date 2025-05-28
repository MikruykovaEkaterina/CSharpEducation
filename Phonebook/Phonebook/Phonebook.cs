using System;
using System.Collections.Generic;
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
    public bool AddAbonent(Abonent abonent)
    {
      if (abonent?.Name == null || abonent?.Number == null) return false;
      for (int i = 0; i < abonents.Count; i++)
      {
        if (Equals(abonents[i].Name, abonent.Name))
        {
          return false;
        }
        if(Equals(abonents[i].Number, abonent.Number)) 
        {
          return false;
        }
      }
      abonents.Add(abonent);
      return true;
    }
    public Abonent GetAbonentByNumber(string number)
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
    public Abonent GetAbonentByName(string name)
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
    public void DeleteAbonentByAbonent(Abonent abonent)
    {
      abonents.Remove(abonent);
    }
    public bool DeleteAbonentFromList(int number)
    {
      if (number > abonents.Count || number < 1)
        return false;
      else
      {
        abonents.RemoveAt(number - 1);
        return true;
      }
    }
    public List<Abonent> GetAbonents()
    {
      return abonents;
    }
    public void GetAbonentsFromFile(string filePath)
    {
      using (StreamReader sr = new StreamReader(filePath))
      {
        string name, number;
        while ((name = sr.ReadLine()) != null && (number = sr.ReadLine()) != null)
        {
          var abonent = new Abonent(name, number);
          AddAbonent(abonent);
        }
      }
    }
    public void PutAbonentsInFile(string filePath)
    {
      File.WriteAllText(filePath, string.Empty);
      using (StreamWriter writer = new StreamWriter(filePath))
      {
        foreach (Abonent abonent in abonents)
        {
          writer.WriteLine(abonent.Name);
          writer.WriteLine(abonent.Number);
        }
      }
    }
    public void PutAbonentInFile(string filePath, Abonent abonent)
    {
      using (StreamWriter writer = new StreamWriter(filePath, true))
      {
        writer.WriteLine(abonent.Name);
        writer.WriteLine(abonent.Number);
      }
    }
  }

}
