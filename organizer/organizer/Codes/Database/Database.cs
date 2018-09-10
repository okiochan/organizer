using System;
using System.Data.SQLite;
using System.IO;

namespace organizer.Codes.Database {
    public class Database : IDisposable {
        private static Database instance = null;
        public static Database GetInstance() {
            if(instance == null) {
                instance = OpenFromDefaultPath();
            }
            return instance;
        }

        private SQLiteConnection conn;
        private string path;
        public Database(string path) {
            this.path = path;
        }

        public SQLiteConnection GetConnection() {
            return conn;
        }

        public void OpenIfClosed() {
            if(conn == null) {
                if (!File.Exists(path))
                    throw new Exception("Can't find the database at specified path");
                conn = new SQLiteConnection("Data Source=" + path + ";");
                conn.Open();
            }
        }
        public void CloseIfOpened() {
            if(conn != null) {
                conn.Close();
                conn = null;
            }
        }

        public void Dispose() {
            CloseIfOpened();
        }

        private static Database OpenFromDefaultPath() {
            string[] paths = {
                @"..\..\..\db\tasks.db",
                @".\tasks.db"
            };
            foreach (var path in paths) {
                if(File.Exists(path)) {
                    return new Database(path);
                }
            }
            throw new Exception("Cannot find database in default paths");
        }
    }
}
