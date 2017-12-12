using TodoList.Abstractions;

namespace TodoList.Models
{
    public class TodoItem : SyncableData, SharedInterface.ITodoItem
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}