using System;
using System.ComponentModel;
using System.Windows;

namespace Timer.ViewModels
{
    /// <summary>
    /// ViewModelBase is the base class for view model classes to inherit from, 
    /// containing NotifyPropertyChanged to enable ViewModels to update UI while data changed 
    /// </summary>
    class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DependencyObject Context { get; set; }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            if (Context != null)
            {
                Context.Dispatcher.BeginInvoke(new Action(() =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }));
            }
            else
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
