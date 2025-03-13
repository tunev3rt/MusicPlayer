using MusicPlayer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore navigationStore;
        public BaseViewModel CurrentViewModel { get => navigationStore.CurrentViewModel; }

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
