using System;

namespace organizer.Codes {
    public class Task {
        public int id;
        public String text;
        public Priority prio;
        public Status status;
        public DateTime deadline; // DateTime.MinValue means no deadline
    }
}
