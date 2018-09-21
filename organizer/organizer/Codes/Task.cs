using System;

namespace organizer.Codes {
    public class Task {
        public int id;
        public String text;
        public Priority prio;
        public Status status;
        public DateTime deadline; // DateTime.MinValue means no deadline
        public DateTime startdate;
        public int timeSpent; // in minutes

        public Task() {
            id = -1;
            text = "title...";
            prio = Priority.LOW;
            status = Status.TODO;
            deadline = DateTime.MaxValue;
            startdate = DateTime.Now;
            timeSpent = 0;
        }
    }
}
