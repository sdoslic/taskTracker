
namespace TaskTracker.Model
{
    public class Type
    {
        public enum Status
        {
            Todo,
            Ongoing,
            Done,
            Unknown
        }

        public static string ConvertStatusToString(Status s)
        {
            switch(s)
            {
                case Status.Todo:
                    return "todo";
                case Status.Ongoing:
                    return "ongoing";
                case Status.Done:
                    return "done";
                default:
                    return "unknown";
            }
        }

        public static Status ConvertStringToStatus(string s)
        {
            switch (s)
            {
                case "todo":
                    return Status.Todo;
                case "ongoing":
                    return Status.Ongoing;
                case "done":
                    return Status.Done;
                default:
                    return Status.Unknown;
            }
        }
    }
}
