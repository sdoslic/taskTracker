using System;

namespace TaskTracker.Model
{
    public class Task : Entry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Person ResposiblePerson { get; set; }
        public Util.Type.Status Status { get; set; }

        public Task()
        {
            StartDate = DateTime.Today;
            DueDate = DateTime.Today;
            ResposiblePerson = new Person();
            Status = Util.Type.Status.Todo;
        }

        public Task(string name,
                    string desc,
                    DateTime startDate,
                    DateTime dueDate,
                    Person person,
                    Util.Type.Status status)
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
