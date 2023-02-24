
using System;
using System.Collections.Generic;

namespace TaskTracker.Model
{
    public class Person : Entry
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public List<Task> Tasks { get; set; }

        public Person()
        {
            Birthday = DateTime.Today;
            Tasks = new List<Task>();
        }
        public Person(string name, DateTime birthday, string email)
        {
            Name = name;
            Birthday = birthday;
            Email = email;

            Tasks = new List<Task>();
        }
    }
}
