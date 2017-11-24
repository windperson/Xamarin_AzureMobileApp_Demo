using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Abstractions;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class EntryPageViewModel : BaseViewModel
    {
        public EntryPageViewModel()
        {
            Title = "Task List";
        }

        private Command _loginCmd;

        public Command LoginCommand =>
            _loginCmd ?? (_loginCmd = new Command(async () => await ExecuteLoginCommand().ConfigureAwait(false)));

        private async Task ExecuteLoginCommand()
        {
            if(IsBusy) { return;}
            IsBusy = true;

            try
            {
                Application.Current.MainPage = new NavigationPage(new Pages.TaskList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Login Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}