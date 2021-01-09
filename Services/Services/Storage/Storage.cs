using System;
using System.IO;
using SQLite;

namespace Services.Storage
{
    public class Storage : IStorage
    {
        private readonly SQLiteConnection sqLiteConnection;

        public Storage()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "pomidoros.db");

            sqLiteConnection = new SQLiteConnection(databasePath);
        }

        public void RemoveAll()
        {
        }
    }
}
