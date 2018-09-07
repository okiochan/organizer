﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

/* usage example
db = new Database(@"..\..\..\db\tasks.db");
db.CreateNewTaskFolder("a new task");
var allFolders = db.ReadAll();
var lastTaskFolder = allFolders[allFolders.Count - 1]; // add note and task to last folder
db.CreateNewNote("a new note", lastTaskFolder);
db.CreateNewTask("a new task", Priority.LOW, Status.DONE, DateTime.Now, lastTaskFolder);
allFolders = db.ReadAll(); // update folders
*/

namespace organizer {
    public class Database : IDisposable {
        private SQLiteConnection db;
        private string path;
        public Database(string path) {
            this.path = path;
        }

        // Read all task folders structure into a list
        public List<TaskFolder> ReadAll() {
            OpenIfClosed();
            
            // read folders
            Dictionary<int, TaskFolder> allFolders = ReadTaskFolder();
            ReadAllTasks(allFolders);
            ReadAllNotes(allFolders);
            
            // convert to list
            List<TaskFolder> res = new List<TaskFolder>();
            foreach (KeyValuePair<int, TaskFolder> item in allFolders) {
                res.Add(item.Value);
            }

            CloseIfOpened();
            return res;
        }

        public void CreateNewTaskFolder(string text) {
            OpenIfClosed();
            string query = "INSERT INTO `TaskFolder`(`text`,`status`) VALUES (@text,0)";
            SQLiteCommand command = new SQLiteCommand(query, db);
            command.Parameters.AddWithValue("@text", text);
            command.ExecuteNonQuery();
            CloseIfOpened();
        }

        public void CreateNewTask(string text, Priority prio, Status status, DateTime deadline, TaskFolder owner) {
            OpenIfClosed();
            string query = "INSERT INTO `Task`(`text`,`prio`,`status`,`deadline`,`owner`) VALUES (@text,@prio,@status,@deadline,@owner)";
            SQLiteCommand command = new SQLiteCommand(query, db);
            command.Parameters.AddWithValue("@text", text);
            command.Parameters.AddWithValue("@prio", (int)prio);
            command.Parameters.AddWithValue("@status", (int)status);
            command.Parameters.AddWithValue("@deadline", null);
            command.Parameters.AddWithValue("@owner", owner.id);
            command.ExecuteNonQuery();
            CloseIfOpened();
        }

        public void CreateNewNote(string text, TaskFolder owner) {
            OpenIfClosed();
            string query = "INSERT INTO `Note`(`text`,`owner`) VALUES (@text,@owner)";
            SQLiteCommand command = new SQLiteCommand(query, db);
            command.Parameters.AddWithValue("@text", text);
            command.Parameters.AddWithValue("@owner", owner.id);
            command.ExecuteNonQuery();
            CloseIfOpened();
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

        private void OpenIfClosed() {
            if(db == null) {
                if (!File.Exists(path))
                    throw new Exception("Can't find the database at specified path");
                db = new SQLiteConnection("Data Source=" + path + ";");
                db.Open();
            }
        }
        private void CloseIfOpened() {
            if(db != null) {
                db.Close();
                db = null;
            }
        }

        public void Dispose() {
            CloseIfOpened();
        }
    }
}