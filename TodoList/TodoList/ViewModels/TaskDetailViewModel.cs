using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Abstractions;
using TodoList.Models;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private SharedInterface.ITodoItemService<TodoItem> _table = App.CloudService.GetTable<TodoItem>();
        public TodoItem Item { get; set; }

        public TaskDetailViewModel(TodoItem item = null)
        {
            if (item != null)
            {
                Item = item;
                Title = item.Text;
            }
            else
            {
                Item = new TodoItem { Text = "New Item", Complete = false };
                Title = "New Item";
            }
        }

        private Command _saveCmd;
        public Command SaveCommand => _saveCmd ?? (_saveCmd = new Command(async () => await ExecuteSaveCommand()));

        private async Task ExecuteSaveCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            try
            {
                if (Item.Id == null)
                {
                    await _table.CreateItemAsync(Item);
                }
                else
                {
                    await _table.UpdateItemAsync(Item);
                }
                MessagingCenter.Send(this, MsgCenterTag.ItemsChanged);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Command _deleteCmd;
        public Command DeleteCommand => _deleteCmd ?? (_deleteCmd = new Command(async () => await ExecuteDeleteCommand()));

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            try
            {
                if (Item.Id != null)
                {
                    await _table.DeleteItemAsync(Item);
                }
                MessagingCenter.Send(this, MsgCenterTag.ItemsChanged);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskDetail] Delete error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}