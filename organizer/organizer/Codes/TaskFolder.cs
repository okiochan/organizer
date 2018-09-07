using System.Collections.Generic;

namespace organizer.Codes {
    public class TaskFolder {
        public int id;
        public string text;
        public List<Task> tasks;
        public List<Note> notes;
        public Status status;
    }
}
