namespace TodoList.Abstractions
{
    public interface ICloudService
    {
        SharedInterface.ITodoItemService<T> GetTable<T>() where T : SyncableData, SharedInterface.ITodoItem;
    }
}