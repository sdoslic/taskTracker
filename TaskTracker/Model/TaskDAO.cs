using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskTracker.Model
{
    public class TaskDAO
    {
        private static string fileName = "task.csv";

        public static List<Task> GetAll()
        {
            List<Task> list = new List<Task>();
            if (File.Exists(fileName))
            {
                var lines = File.ReadLines(fileName);

                foreach (var line in lines)
                {
                    string[] taskStr = line.Split(',');
                    Task p = new Task(taskStr[0],
                                      taskStr[1],
                                      DateTime.ParseExact(taskStr[2], "dd/MM/yyyy", null),
                                      DateTime.ParseExact(taskStr[3], "dd/MM/yyyy", null),
                                      PersonDAO.GetByName(taskStr[4]),
                                      Util.Type.ConvertStringToStatus(taskStr[5]));
                    list.Add(p);
                }
            }
            else
            {
                throw new FileNotFoundException("Could not find file " + fileName);
            }
            return list;
        }

        public static void Add(Task t)
        {
            string[] s = new string[] { t.Name, t.Description,
                                        t.StartDate.ToString("dd/MM/yyyy"),
                                        t.DueDate.ToString("dd/MM/yyyy"),
                                        t.ResposiblePerson.Name,
                                        Util.Type.ConvertStatusToString(t.Status)};

            File.AppendAllLines(fileName, new List<string> { string.Join(",", s) });
        }

        public static void Update(string name, Task t)
        {
            DeleteByName(name);
            Add(t);
        }

        public static void DeleteByName(string name)
        {
            List<Task> all = GetAll();
            all.RemoveAll(t => t.Name == name);
            File.Delete(fileName);
            foreach (Task t in all)
            {
                Add(t);
            }
        }

        public static bool PersonHasTasks(string name)
        {
            List<Task> all = GetAll();
            return all.Any(t => t.ResposiblePerson.Name == name);
        }

        public static List<Task> FilterTasks(string str)
        {
            List<Task> list = GetAll();
            list.RemoveAll(t => !(t.ResposiblePerson.Name.ToLower().Contains(str.ToLower()) ||
                                  Util.Type.ConvertStatusToString(t.Status).ToLower().Contains(str.ToLower())));
            return list;
        }

        public static List<Task> GetByResponsiblePerson(string person)
        {
            List<Task> list = GetAll();
            list.RemoveAll(t => t.ResposiblePerson.Name != person);
            return list;
        }
    }
}
