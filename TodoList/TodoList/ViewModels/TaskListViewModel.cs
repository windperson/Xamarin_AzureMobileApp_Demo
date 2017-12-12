using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Abstractions;
using TodoList.Models;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        public TaskListViewModel()
        {
            Title = "Task List";
            RefreshList();
        }

        ObservableCollection<TodoItem> _items = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value, "Items");
        }

        private TodoItem _selectedItem;

        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value, "SelectedItem");
                if (_selectedItem != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new Pages.TaskDetail(_selectedItem));
                    SelectedItem = null;
                }
            }
        }

        private Command _refreshCmd;
        public Command RefreshCommand =>
            _refreshCmd ?? (_refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
            {
                return; ;
            }
            IsBusy = true;

            try
            {
                var table = App.CloudService.GetTable<TodoItem>();
                var list = await table.ReadAllItemAsync();
                Items.Clear();
                foreach (var item in list)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Command _addNewCmd;

        public Command AddNewItemCommand =>
            _addNewCmd ?? (_addNewCmd = new Command(async () => await ExecuteAddNewItemCommand()));

        async Task ExecuteAddNewItemCommand()
        {
            if (IsBusy)
            {
                return; ;
            }
            IsBusy = true;

            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Pages.TaskDetail());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error in AddNewItem: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task RefreshList()
        {
            await ExecuteRefreshCommand();
            MessagingCenter.Subscribe<TaskDetailViewModel>(this, MsgCenterTag.ItemsChanged, async (sender) =>
                 {
                     await ExecuteRefreshCommand();
                 });
        }
    }
}