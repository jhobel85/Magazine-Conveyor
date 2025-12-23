using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_WPF.Model
{
    public class Position : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool isVisible;
        private bool isChecked;
        
        public Position(bool isVisible, bool isChecked = false) {
            this.isVisible = isVisible;
            this.isChecked = isChecked;
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    //Inform View                    
                    OnPropertyChanged(nameof(IsVisible));
                    OnVisibleChanged();
                }
            }
        }

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked == value)
                    return;

                isChecked = value;
                //Inform View
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        //Inform View
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnVisibleChanged()
        {
            if (!IsVisible && IsChecked)
            {
                IsChecked = false;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
    }
}
