using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace organizer.Codes.Database {
    public class DatabaseTaskFolder {

        // Read all task folders structure into a list
        public static List<TaskFolder> ReadAll() {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            // read folders
            Dictionary<int, TaskFolder> allFolders = ReadTaskFolders();
            DatabaseTask.ReadAllTasks(allFolders);
            DatabaseNote.ReadAllNotes(allFolders);

            // convert to list
            List<TaskFolder> res = new List<TaskFolder>();
            foreach (KeyValuePair<int, TaskFolder> item in allFolders) {
                res.Add(item.Value);
            }

            db.CloseIfOpened();
            return res;
        }

        public static void CreateNewTaskFolder(string text) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "INSERT INTO `TaskFolder`(`text`,`status`) VALUES (@text,0)";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", text);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void UpdateTaskFolder(TaskFolder taskFolder, string newText, Status newStatus) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "UPDATE `TaskFolder` SET `text`=@text,`status`=@status WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", newText);
            command.Parameters.AddWithValue("@status", (int)newStatus);
            command.Parameters.AddWithValue("@id", taskFolder.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void DeleteTaskFolder(TaskFolder taskFolder) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();
            string query = "DELETE FROM `TaskFolder` WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@id", taskFolder.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void ReloadTaskFolder(TaskFolder folder) {
            List<TaskFolder> allFolders = ReadAll();
            TaskFolder found = null;
            foreach (var f in allFolders) {
                if(f.id == folder.id) {
                    found = f;
                    break;
                }
            }
            if(found==null) {
                throw new Exception("Cannot find this folder in database");
            }
            folder.id = found.id;
            folder.notes = found.notes;
            folder.status = found.status;
            folder.tasks = found.tasks;
            folder.text = found.text;
        }

        public static Dictionary<int, TaskFolder> ReadTaskFolders() {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            // query
            string query = "SELECT id,text,status FROM TaskFolder";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
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

            db.CloseIfOpened();
            return res;
        }
    }
}
