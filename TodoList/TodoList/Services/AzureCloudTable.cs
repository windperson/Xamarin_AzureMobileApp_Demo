using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TodoList.Abstractions;

namespace TodoList.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        private MobileServiceClient _client;
        private IMobileServiceTable<T> _table;

        public AzureCloudTable(MobileServiceClient client)
        {
            _client = client;
            _table = client.GetTable<T>();
        }


        #region ICloudTable implementation

        public async Task<T> CreateItemAsync(T item)
        {
            await _table.InsertAsync(item);
            return item;
        }

        public async Task<T> ReadItemAsync(string id) => await _table.LookupAsync(id);

        public async Task<T> UpdateItemAsync(T item)
        {
            await _table.UpdateAsync(item);
            return item;
        }

        public async Task DeleteItemAsync(T item) => await _table.DeleteAsync(item);

        public async Task<ICollection<T>> ReadAllItemAsync() => await _table.ToListAsync();

        #endregion
    }
}