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
            RefreshButton.IsEnabled = false;

            await _legatViewModel.GetAllLegats();
            SetDataGrid();

            RefreshButton.IsEnabled = true;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _legatViewModel.GetAllLegats();
            SetDataGrid();
        }

        //private void CreateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    _legatViewModel.CreateTest();
        //}
    }
}
