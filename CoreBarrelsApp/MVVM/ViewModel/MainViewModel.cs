using CoreBarrelsApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand KompasViewCommand { get; set; }
        public RelayCommand CalcViewCommand { get; set; }
        public RelayCommand CalcPoreViewCommand { get; set; }
        public HomeViewModel HomeViewModel { get; set; }
        public KompasViewModel KompasViewModel { get; set; }
        public CalcViewModel CalcViewModel { get; set; }
        public CalcPoreViewModel CalcPoreViewModel { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            KompasViewModel = new KompasViewModel();
            CalcViewModel = new CalcViewModel();
            CalcPoreViewModel = new CalcPoreViewModel();
            CurrentView = CalcViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            KompasViewCommand = new RelayCommand(o =>
            {
                CurrentView = KompasViewModel;
            });

            CalcViewCommand = new RelayCommand(o =>
            {
                CurrentView = CalcViewModel;
            });

            CalcPoreViewCommand = new RelayCommand(o =>
            {
                CurrentView = CalcPoreViewModel;
            });
        }
    }
}
