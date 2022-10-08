using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using storeManagement.Core;

namespace storeManagement.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TransactionViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }

        public TransactionViewModel TransactionVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            TransactionVM = new TransactionViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });
            
            TransactionViewCommand = new RelayCommand(o =>
            {
                CurrentView = TransactionVM;
            });

        }

    }
}
