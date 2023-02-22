using System;

namespace TaskTracker.Model
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Person ResposiblePerson { get; set; }
        public Type.Status Status { get; set; }

        public Task(string name,
                    string desc,
                    DateTime startDate,
                    DateTime dueDate,
                    Person person,
                    Type.Status status)
        {
            Name = name;
            Description = desc;
            StartDate = startDate;
            DueDate = dueDate;
            ResposiblePerson = person;
            Status = status;
        }
    }
}
