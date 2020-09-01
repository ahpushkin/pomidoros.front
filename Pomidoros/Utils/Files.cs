using System;
using System.IO;

namespace Pomidoros.Utils
{
    //class for files save
    //for save user data*
    public class Files
    {
        void SaveData(string url)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, url);

            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
            }
        }
    }
}
