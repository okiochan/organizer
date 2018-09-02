using System.Collections.Generic;

namespace organizer {
    public class TaskFolder {
        public int id;
        public List<Task> tasks;
        public List<Note> notes;
        public Status status;
        public string name;
    }
}
