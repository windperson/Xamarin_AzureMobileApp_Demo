using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class TodoItem : EntityData, SharedInterface.ITodoItem
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}