using DataAccess.DataModels;
using DataAccess.Repositories;
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

namespace GUI_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILegatRepository _legatRepository;

        public MainWindow(ILegatRepository legatRepository)
        {
            InitializeComponent();
            _legatRepository = legatRepository;


            try
            {
                Task.Run(async () =>
                    {
                        await CreateLegat();
                    });
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task CreateLegat()
        {
            await _legatRepository.Create(
                new DataAccess.DataModels.Legat(
                    new Person("Rasmus", "Hedeland", "DinMor@Gmail.COm", 
                                new Address("BruhGade", 2, "5960", "ringsted"),
                                new Education("stx", "htx", "MinMor")),
                    "jeg kan godt lide epneg", 23232323, "det dejligt med lkajg", DateTime.Now, DateTime.Now.Add(TimeSpan.FromDays(20)), "Ej søgt", "Nuuu", "true", DateTime.Now 
                    )
                );
        }

    } 
}
