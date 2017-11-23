using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Abstractions
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _propTitle = string.Empty;
        public string Title
        {
            get => _propTitle;
            set => SetProperty(ref _propTitle, value, "Title");
        }

        private bool _propIsBusy;

        public bool Isbusy
        {
            get => _propIsBusy;
            set => SetProperty(ref _propIsBusy, value, "IsBusy");
        }

        protected void SetProperty<T>(ref T store,T value, string propName, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(store, value))
            {
                return;
            }
            store = value;
            onChanged?.Invoke();
            OnPropertyChanged(propName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
