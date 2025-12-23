using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Magazine_Conveyor.Model;
using Magazine_Conveyor.Service;

namespace Magazine_Conveyor.ViewModel
{
    /// <summary>
    /// ViewModel for the Magazine application.
    /// Mediates between the View (MainWindow) and the Model (Magazine).
    /// Implements INotifyPropertyChanged for two-way data binding.
    /// </summary>
    public class MagazineViewModel : INotifyPropertyChanged
    {
        private Magazine magazine;
        private FindFreePlaceService findFreePlaceService;
        private int positionCount;
        private bool isRotary;
        private int neededPlaces;
        private int lastFoundPosition;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MagazineViewModel()
        {
            // Initialize default values
            positionCount = Magazine.PLACES_AVAILABLE_DEFAULT;
            isRotary = false;
            neededPlaces = Magazine.NEEDED_PLACES_DEFAULT;
            lastFoundPosition = -1;

            // Create model and service
            magazine = Magazine.GetInstance(positionCount, isRotary, true);
            findFreePlaceService = new FindFreePlaceService(magazine);
            
            magazine.UpdatePositionsVisibility();

            // Initialize commands
            UpdateMagazineCommand = new RelayCommand(_ => ExecuteUpdateMagazine());
            FindFreePlaceCommand = new RelayCommand(_ => ExecuteFindFreePlace());
        }

        #region Properties for Binding

        public ObservableCollection<Position> Positions => 
            new ObservableCollection<Position>(magazine.Positions);

        public int PositionCount
        {
            get => positionCount;
            set
            {
                if (positionCount != value)
                {
                    positionCount = value;
                    OnPropertyChanged(nameof(PositionCount));
                }
            }
        }

        public bool IsRotary
        {
            get => isRotary;
            set
            {
                if (isRotary != value)
                {
                    isRotary = value;
                    OnPropertyChanged(nameof(IsRotary));
                }
            }
        }

        public int NeededPlaces
        {
            get => neededPlaces;
            set
            {
                if (neededPlaces != value)
                {
                    neededPlaces = value;
                    OnPropertyChanged(nameof(NeededPlaces));
                }
            }
        }

        public int LastFoundPosition
        {
            get => lastFoundPosition;
            private set
            {
                if (lastFoundPosition != value)
                {
                    lastFoundPosition = value;
                    OnPropertyChanged(nameof(LastFoundPosition));
                }
            }
        }

        #endregion

        #region Commands

        public ICommand UpdateMagazineCommand { get; private set; }

        public ICommand FindFreePlaceCommand { get; private set; }

        #endregion

        #region Command Implementations

        private void ExecuteUpdateMagazine()
        {
            // Create a new magazine instance with current settings
            magazine = Magazine.GetInstance(PositionCount, IsRotary, true);
            findFreePlaceService = new FindFreePlaceService(magazine);
            magazine.UpdatePositionsVisibility();
            
            // Update view
            OnPropertyChanged(nameof(Positions));
        }

        private void ExecuteFindFreePlace()
        {
            findFreePlaceService.Execute();
            LastFoundPosition = findFreePlaceService.FreePlace;

            if (LastFoundPosition == -1)
            {
                MessageBox.Show($"There are not enough free positions for {NeededPlaces} needed places.");
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
