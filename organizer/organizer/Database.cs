using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace organizer {
    public class Database {
        private SQLiteConnection db;
        public Database(string path) {
            if (!File.Exists(path))
                throw new Exception("Can't find the database at specified path");
            
            db = new SQLiteConnection("Data Source=" + path + ";");
            db.Open();
        }

        // Read all task folders structure into a list
        public List<TaskFolder> ReadAll() {
            Dictionary<int, TaskFolder> allFolders = ReadTaskFolder();
            ReadAllTasks(allFolders);
            ReadAllNotes(allFolders);
            List<TaskFolder> res = new List<TaskFolder>();
            foreach (KeyValuePair<int, TaskFolder> item in allFolders) {
                res.Add(item.Value);
            }
            return res;
        }

        private Dictionary<int, TaskFolder> ReadTaskFolder() {
            // query
            string query = "SELECT id,text,status FROM TaskFolder";
            SQLiteCommand command = new SQLiteCommand(query, db);
            SQLiteDataReader reader = command.ExecuteReader();

            // populate folder
            Dictionary<int, TaskFolder> res = new Dictionary<int, TaskFolder>();
            while (reader.Read()) {
                TaskFolder folder = new TaskFolder();
                folder.id = reader.GetInt32(0);
                folder.text = reader.GetString(1);
                folder.status = (Status)reader.GetInt32(2);
                folder.notes = new List<Note>();
                folder.tasks = new List<Task>();
                res.Add(folder.id, folder);
            }
            return res;
        }

        // reads tasks into folder
        private void ReadAllTasks(Dictionary<int, TaskFolder> taskFolder) { // read into folders
            // query
            string query = "SELECT id,text,prio,status,deadline,owner FROM Task";
            SQLiteCommand command = new SQLiteCommand(query, db);
            SQLiteDataReader reader = command.ExecuteReader();

            // read into folders
            while (reader.Read()) {
                Task task = new Task();
                task.id = reader.GetInt32(0);
                task.text = reader.GetString(1);
                task.prio = (Priority)reader.GetInt32(2);
                task.status = (Status)reader.GetInt32(3);
                task.deadline = DateTime.Now; // TODO: add deadline reading
                int owner = reader.GetInt32(5);
                taskFolder[owner].tasks.Add(task);
            }
        }

        // reads notes into folders
        private void ReadAllNotes(Dictionary<int, TaskFolder> taskFolder) {
            // query
            string query = "SELECT id,text,owner FROM Note";
            SQLiteCommand command = new SQLiteCommand(query, db);
            SQLiteDataReader reader = command.ExecuteReader();

            // read into folders
            while (reader.Read()) {
                Note note = new Note();
                note.id = reader.GetInt32(0);
                note.text = reader.GetString(1);
                int owner = reader.GetInt32(2);
                taskFolder[owner].notes.Add(note);
            }
        }
    }
}
