using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace organizer.Codes.Database {
    public class DatabaseTask {

        public static void CreateNewTask(string text, Priority prio, Status status, DateTime deadline, TaskFolder owner) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "INSERT INTO `Task`(`text`,`prio`,`status`,`deadline`,`owner`) VALUES (@text,@prio,@status,@deadline,@owner)";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", text);
            command.Parameters.AddWithValue("@prio", (int)prio);
            command.Parameters.AddWithValue("@status", (int)status);
            if (DateTime.MinValue.Equals(deadline)) {
                command.Parameters.AddWithValue("@deadline", null);
            } else {
                command.Parameters.AddWithValue("@deadline", DateTimeHelper.ToString(deadline));
            }
            command.Parameters.AddWithValue("@owner", owner.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void UpdateTask(Task task, string newText, Priority newPrio, Status newStatus, DateTime newDeadline) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "UPDATE `Task` SET `text`=@text,`prio`=@prio,`status`=@status,`deadline`=@deadline WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", newText);
            command.Parameters.AddWithValue("@prio", (int)newPrio);
            command.Parameters.AddWithValue("@status", (int)newStatus);
            if (DateTime.MinValue.Equals(newDeadline)) {
                command.Parameters.AddWithValue("@deadline", null);
            } else {
                command.Parameters.AddWithValue("@deadline", DateTimeHelper.ToString(newDeadline));
            }
            command.Parameters.AddWithValue("@id", task.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void DeleteTask(Task task) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "DELETE FROM `Task` WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@id", task.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        // reads tasks into folder
        public static void ReadAllTasks(Dictionary<int, TaskFolder> taskFolder) { // read into folders
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            // query
            string query = "SELECT id,text,prio,status,deadline,owner FROM Task";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            SQLiteDataReader reader = command.ExecuteReader();

            // read into folders
            while (reader.Read()) {
                Task task = new Task();
                task.id = reader.GetInt32(0);
                task.text = reader.GetString(1);
                task.prio = (Priority)reader.GetInt32(2);
                task.status = (Status)reader.GetInt32(3);
                if (reader.IsDBNull(4)) {
                    task.deadline = DateTime.MinValue;
                } else {
                    task.deadline = DateTimeHelper.FromString(reader.GetString(4));
                }
                int owner = reader.GetInt32(5);
                taskFolder[owner].tasks.Add(task);
            }

            db.CloseIfOpened();
        }
    }
}
