using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Model
{
    public class PersonDAO
    {
        private static string fileName = "person.txt";

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
    }
}
