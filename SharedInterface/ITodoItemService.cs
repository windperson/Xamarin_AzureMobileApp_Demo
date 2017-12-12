using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterface
{
    public interface ITodoItemService<T> where T : ITodoItem
    {
        Task<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);

        Task<ICollection<T>> ReadAllItemAsync();
    }
}
