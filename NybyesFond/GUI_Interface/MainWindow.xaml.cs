using GUI_Interface.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace GUI_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LegatViewModel _legatViewModel;
        private readonly DataGrid _legatDataGrid;

        public MainWindow(LegatViewModel legatViewModel)
        {
            InitializeComponent();

            _legatViewModel = legatViewModel;
            _legatDataGrid = LegatDG;

            InitializeLegatDataGridProperties();
        }

        private static MessageBoxResult DisplayMessage(string message, string title, MessageBoxImage messageType, MessageBoxButton buttonLayout = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, title, buttonLayout, messageType);
        }

        private void InitializeLegatDataGridProperties()
        {
            _legatDataGrid.ItemsSource = _legatViewModel.ObservableLegats;
            _legatDataGrid.AlternationCount = 2;
            _legatDataGrid.IsReadOnly = true;
            _legatDataGrid.Columns[0].SortDirection = System.ComponentModel.ListSortDirection.Ascending;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LegatDG.ItemsSource = _legatViewModel.ObservableLegats;

                await _legatViewModel.GetAllLegats();
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke hente data", "FEJL", MessageBoxImage.Error);
            }
            finally
            {
                Debug.WriteLine($"Windows <{this}> was loaded.");
            }
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            var defaultContent = RefreshButton.Content;
            RefreshButton.IsEnabled = false;
            RefreshButton.Content = "Opdatere Ansøgninger..";

            try
            {
                await _legatViewModel.GetAllLegats();
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke opdatere data", "FEJL", MessageBoxImage.Error);
            }
            finally
            {
                RefreshButton.IsEnabled = true;
                RefreshButton.Content = defaultContent;
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var defaultContent = DeleteButton.Content;
            DeleteButton.IsEnabled = false;

            MessageBoxResult messageBoxResult = DisplayMessage(
                                   "Godkend sletning af ALT data?",
                                   "Godkendelses Vindue",
                                   MessageBoxImage.Warning,
                                   MessageBoxButton.YesNo);

            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeleteButton.Content = "Sletter Oplysninger..";
                    await _legatViewModel.TruncateData();
                }
            }
            catch (Exception)
            {
                DisplayMessage("Kunne ikke slette data.", "FEJL", MessageBoxImage.Error);
            }
            finally
            {
                DeleteButton.IsEnabled = true;
                DeleteButton.Content = defaultContent;
            }
        }

        private async void CreateTestData_Click(object sender, RoutedEventArgs e)
        {
            string defaultButtonContent = (string)TestDataButton.Content;
            TestDataButton.IsEnabled = false;

            try
            {
                TestDataButton.Content = "Opretter Test data..";

                await _legatViewModel.CreateTestDataAsync(100);
            }
            finally
            {
                TestDataButton.IsEnabled = true;
                TestDataButton.Content = defaultButtonContent;
            }
        }

        //private async void CreateMockData_Click(object sender, RoutedEventArgs e)
        //{
        //    string defaultContent = (string)MockDataButton.Content;

        //    try
        //    {
        //        MockDataButton.Content = "Creating Mock Data...";

        //        await _legatViewModel.CreateMockDataAsync(100);
        //    }
        //    finally
        //    {                
        //        MockDataButton.IsEnabled = true;
        //        MockDataButton.Content = defaultContent;
        //    }
        //}
    }
}
