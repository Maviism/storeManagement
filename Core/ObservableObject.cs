using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;

namespace storeManagement.Core
{
    class ObservableObject : INotifyPropertyChanged
    {
        private ISeries[] _series;

        public ISeries[] Series
        {
            get => _series;
            set
            {
                if (_series == value) return;
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
