﻿using SQLite;
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

        public async Task<List<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Database.Table<ItemModel>().ToListAsync();
        }

        public Task<bool> CreateAsync(ItemModel item)
        {
            Database.InsertAsync(item);
            return Task.FromResult(true);
        }

        public Task<ItemModel> ReadAsync(string id)
        {
            return Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();

        }

        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var data = await ReadAsync(item.Id);
            if(data == null)
            {
                return false;
            }
            var result = await Database.UpdateAsync(item);
            return (result == 1);
        }

        public  async Task<bool> DeleteAsync(string id)
        {
            var item = await ReadAsync(id);
            if(item == null)
            {
                return false;
            }

            var result = await Database.DeleteAsync(item);
            return (result == 1);
        }

        
      
    }
}
