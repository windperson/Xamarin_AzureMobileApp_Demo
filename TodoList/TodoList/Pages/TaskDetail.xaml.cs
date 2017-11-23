using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetail : ContentPage
    {
        public TaskDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(item);
        }
    }
}