using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;
using System.Collections.Generic;
using System.Text;

namespace Mine.Services
{
    class DatabaseService : IDataStore<ItemModel>
    {

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }


        public async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        /// <summary>
        /// created a function that would wipe the wole data list
        /// </summary>
        public void WipeDataList()
        {
            Database.DropTableAsync<ItemModel>().GetAwaiter().GetResult();
            Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #region CRUDI
        /// <summary>
        /// create function for the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<bool> CreateAsync(ItemModel item)
        {
            Database.InsertAsync(item);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Read function for actial database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            return Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();

        }


        /// <summary>
        /// Is an undate function to update the data 
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(ItemModel Data)
        {
            var myRead = ReadAsync(Data.Id).GetAwaiter().GetResult();
            if (myRead == null)
            {
                return Task.FromResult(false);

            }

            Database.UpdateAsync(Data);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(string id)
        {
            // Check if it exists...
            var myRead = ReadAsync(id).GetAwaiter().GetResult();
            if (myRead == null)
            {
                return Task.FromResult(false);

            }

            // Then delete...

            Database.DeleteAsync(myRead);
            return Task.FromResult(true);
        }

        #endregion CRUDI

        /// <summary>
        /// Is a list of all the items in the index list
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<List<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Database.Table<ItemModel>().ToListAsync();
        }

        // Delete the Datbase Tables by dropping them
        public async void DeleteTables()
        {
            await Database.DropTableAsync<ItemModel>();
        }

        // Create the Datbase Tables
        public async void CreateTables()
        {
            await Database.CreateTableAsync<ItemModel>();
        }


      
    }
}
