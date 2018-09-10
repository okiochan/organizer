using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace organizer.Codes.Database {
    public class DatabaseNote {

        public static void CreateNewNote(string text, TaskFolder owner) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "INSERT INTO `Note`(`text`,`owner`) VALUES (@text,@owner)";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", text);
            command.Parameters.AddWithValue("@owner", owner.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }


        public static void UpdateNote(Note note, string newText) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "UPDATE `Note` SET `text`=@text WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@text", newText);
            command.Parameters.AddWithValue("@id", note.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        public static void DeleteNote(Note note) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "DELETE FROM `Note` WHERE `id`=@id";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            command.Parameters.AddWithValue("@id", note.id);
            command.ExecuteNonQuery();

            db.CloseIfOpened();
        }

        // reads notes into folders
        public static void ReadAllNotes(Dictionary<int, TaskFolder> taskFolder) {
            Database db = Database.GetInstance();
            db.OpenIfClosed();

            string query = "SELECT id,text,owner FROM Note";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            SQLiteDataReader reader = command.ExecuteReader();

            // read into folders
            while (reader.Read()) {
                Note note = new Note();
                note.id = reader.GetInt32(0);
                note.text = reader.GetString(1);
                int owner = reader.GetInt32(2);
                taskFolder[owner].notes.Add(note);
            }

            db.CloseIfOpened();
        }
    }
}
