using System;
using System.Collections.Generic;
using System.IO;

namespace TaskTracker.Model
{
    public class PersonDAO
    {
        private static string fileName = "person.csv";

        public static List<Person> GetAll()
        {
            List<Person> list = new List<Person>();
            if (File.Exists(fileName))
            {
                StreamReader reader = File.OpenText(fileName);
                string s = string.Empty;
                while ((s = reader.ReadLine()) != null)
                {
                    string[] personStr = s.Split(',');
                    Person p = new Person(personStr[0], 
                                          DateTime.ParseExact(personStr[1], "dd/MM/yyyy", null),
                                          personStr[2]);
                    list.Add(p);
                }
                reader.Close();
            }
            else
            {

                throw new FileNotFoundException("Could not find file " + fileName);
            }
            return list;
        }

        public static Person GetByName(string name)
        {
            Person p = null;
            StreamReader reader = File.OpenText(fileName);
            string s = string.Empty;
            while ((s = reader.ReadLine()) != null)
            {
                string[] personStr = s.Split(',');
                if(personStr[0] == name)
                {
                    p = new Person(personStr[0],
                                   DateTime.ParseExact(personStr[1], "dd/MM/yyyy", null),
                                   personStr[2]);
                    break;
                }
            }
            reader.Close();
            return p;
        }

        public static void Add(Person p)
        {
            string[] s = new string[] { p.Name, p.Birthday.ToString("dd/MM/yyyy"), p.Email};

            File.AppendAllLines(fileName, new List<string> { string.Join(",", s) });
        }

        public static void Update(string name, Person p)
        {
            DeleteByName(name);
            Add(p);
        }

        public static void DeleteByName(string name)
        {
            List<Person> all = GetAll();
            all.RemoveAll(p => p.Name == name);
            File.Delete(fileName);
            foreach (Person p in all)
            {
                Add(p);
            }
        }

        public static List<Person> FilterPerson(string str)
        {
            List<Person> list = GetAll();
            list.RemoveAll(t => !(t.Name.ToLower().Contains(str.ToLower())));
            return list;
        }
    }
}
