using System;
using System.Collections.Generic;

namespace organizer.Codes {
    public class TaskFolder {
        public int id;
        public string text;
        public List<Task> tasks;
        public List<Note> notes;
        public Status status;

        public void SortTasksByPriority() {
            Comparison<Task> compare = (Task x, Task y) => {
                return -((int)x.prio - (int)y.prio);
            };
            tasks.Sort(compare);
        }
    }
}
