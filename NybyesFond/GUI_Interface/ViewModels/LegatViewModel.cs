using DataAccess.DataModels;
using DataAccess.Repositories;
using NybyesFond.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GUI_Interface.ViewModels
{
    public class LegatViewModel : ViewModelBase
    {
        private readonly ILegatRepository _legatRepository;
        private ObservableCollection<Legat> _observableLegats;

        /// <summary>
        /// For View Injection Purpose.
        /// </summary>
        public LegatViewModel() { }

        public LegatViewModel(ILegatRepository legatRepository)
        {
            _legatRepository = legatRepository;
            _observableLegats = new();
        }

        public ObservableCollection<Legat> ObservableLegats
        {
            get { return _observableLegats; }
            set
            {
                _observableLegats = value;
                OnPropertyChanged("ObservableLegats");
            }
        }

        internal async Task GetAllLegats()
        {
            ClearDataCollection();
            var result = await Task.Run(() => _legatRepository.GetAllAsync());

            foreach (Legat legat in result)
            {
                ObservableLegats.Add(legat);
            }
        }

        internal async Task TruncateData()
        {
            await Task.Run(() => _legatRepository.TruncateData());
            ClearDataCollection();
        }

        internal async Task CreateTestDataAsync(int maxCount)
        {
            Education brrrrruh = new("STKX", "HTKX", "ZBQ");
            Address ladnuv = new("lolgade", "12", "1212", "BruhTown");
            Person spassssserperson = new("Bruhtias", "bruhsilicys", "bruh@bruhmail.bruh", ladnuv, brrrrruh);
            Legat legat = new(spassssserperson, "det nice", "121212", "bruh", DateTime.Now, DateTime.Now, "lol", "bruh", "lalalalalalalala", DateTime.Now);

            for (int i = 0; i < maxCount; i++)
            {
                await Task.Run(() => _legatRepository.CreateAsync(legat));
            }

            await GetAllLegats();
        }

        private void ClearDataCollection()
        {
            _observableLegats.Clear();
        }

        //internal async Task CreateMockDataAsync(int maxCount)
        //{
        //    Education brrrrruh = new("STKX", "HTKX", "ZBQ");
        //    Address ladnuv = new("lolgade", "12", "1212", "BruhTown");
        //    Person spassssserperson = new("Bruhtias", "bruhsilicys", "bruh@bruhmail.bruh", ladnuv, brrrrruh);


        //    for (int i = 0; i < maxCount; i++)
        //    {
        //        Legat legat = new(spassssserperson, "det nice", "121212", "bruh", DateTime.Now, DateTime.Now, "lol", "bruh", "lalalalalalalala", DateTime.Now, i + 1);
        //        await Task.Delay(25);
        //        ObservableLegats.Add(legat);
        //    }
        //}

        //internal async Task DeleteMockDataAsync()
        //{
        //    await Task.Delay(25);

        //    ObservableLegats.Clear();
        //}
    }
}
