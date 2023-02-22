using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Model
{
    public class TaskDAO
    {
        private static string fileName = "task.txt";

        public static List<Task> GetAll()
        {
            List<Task> list = new List<Task>();
            if (File.Exists(fileName))
            {
                StreamReader reader = File.OpenText(fileName);
                string s = string.Empty;
                while ((s = reader.ReadLine()) != null)
                {
                    string[] taskStr = s.Split(',');
                    Task p = new Task(taskStr[0],
                                      taskStr[1],
                                      DateTime.ParseExact(taskStr[2], "dd/MM/yyyy", null),
                                      DateTime.ParseExact(taskStr[3], "dd/MM/yyyy", null),
                                      PersonDAO.GetByName(taskStr[4]),
                                      Type.ConvertStringToStatus(taskStr[5]));
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
    }
}
