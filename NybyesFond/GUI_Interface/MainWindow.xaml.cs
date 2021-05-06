using DataAccess.DataModels;
using DataAccess.Repositories;
using GUI_Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace GUI_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LegatViewModel _legatViewModel;

        public MainWindow(LegatViewModel legatViewModel)
        {
            _legatViewModel = legatViewModel;
            InitializeComponent();

        }

        private void SetDataGrid()
        {
            var result = _legatViewModel.Legats;
            LegatDG.ItemsSource = result;
            LegatDG.IsReadOnly = true;
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshButton.IsEnabled = false;

                await _legatViewModel.CreateTest();
                await _legatViewModel.GetAllLegats();
                SetDataGrid();

                RefreshButton.IsEnabled = true;
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke opdatere data", "FEJL", MessageBoxImage.Error);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _legatViewModel.GetAllLegats();
                SetDataGrid();
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke hente data", "FEJL", MessageBoxImage.Error);
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteButton.IsEnabled = false;
                MessageBoxResult messageBoxResult = DisplayMessage(
                                   "Godkend sletning af ALT data?",
                                   "Godkendelses Vindue",
                                   MessageBoxImage.Warning,
                                   MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    await _legatViewModel.TruncateData();
                }
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke hente data", "FEJL", MessageBoxImage.Error);
            }
            finally
            {
                DeleteButton.IsEnabled = true;
                await _legatViewModel.GetAllLegats();
                SetDataGrid();
            }
        }

        private static MessageBoxResult DisplayMessage(string message, string title, MessageBoxImage messageType, MessageBoxButton buttonLayout = MessageBoxButton.OK)
        {
            return MessageBox.Show( message, title, buttonLayout, messageType);
        }
    }
}
