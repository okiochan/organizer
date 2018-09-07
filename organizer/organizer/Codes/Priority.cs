namespace organizer.Codes {
    public enum Priority {
        LOW,MID,HIGH
    }

    public static class Sorter {
        public static int SortByPriority(Task x, Task y) {
            if (x.prio != y.prio) return x.prio - y.prio;
            if (x.id != y.id) return x.id - y.id;
            return 0;
        }
    }
}
