using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomidoros.Controller;
using Pomidoros.Model;
using Pomidoros.View;
using SQLite;

namespace Pomidoros.ViewModel
{
    public class UserItemDatabase
    {
        public Dictionary<string, string> user_data = LoginPage.user_data;

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public UserItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TodoItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TodoItem)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }


        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

    

        public Task<int> SaveItemAsync(TodoItem item)
        {
          
                return Database.UpdateAsync(item);
           
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
