using MusicPlayer.Services;
using MusicPlayer.Stores;
using MusicPlayer.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicPlayer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        NavigationStore navigationStore = new NavigationStore();

        UserService userService = new UserService();

        navigationStore.CurrentViewModel = new LoginViewModel(navigationStore, userService);

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}

