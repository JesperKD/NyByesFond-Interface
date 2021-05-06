using DataAccess.DataModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Interface.ViewModels
{
    public class LegatViewModel : INotifyPropertyChanged
    {
        private readonly ILegatRepository _legatRepository;
        private IEnumerable<Legat> _legats = new List<Legat>();

        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<Legat> Legats { get { return _legats; } set { _legats = value; OnPropertyChanged(); } }

        public LegatViewModel(ILegatRepository legatRepository)
        {
            _legatRepository = legatRepository;
        }

        public async Task GetAllLegats()
        {
            Legats = await _legatRepository.GetAll();
        }

        public async Task<bool> RefreshCheck()
        {
            long nr = Legats.Count();

            if (await _legatRepository.CheckForNewRecords(nr) == true )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task TruncateData()
        {
            await _legatRepository.TruncateData();
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //public async void RemoveTest()
        //{
        //    Legat legat = new(null, null, null, null, DateTime.Now, DateTime.Now, null, null, null, DateTime.Now, 6);
        //    await _legatRepository.Delete(legat);
        //}
       
        public async Task CreateTest()
        {
            Education brrrrruh = new("STKX", "HTKX", "ZBQ");
            Address ladnuv = new("lolgade", "12", "1212", "BruhTown");
            Person spassssserperson = new("Bruhtias", "bruhsilicys", "bruh@bruhmail.bruh", ladnuv, brrrrruh);
            Legat legat = new(spassssserperson, "det nice", "121212", "bruh", DateTime.Now, DateTime.Now, "lol", "bruh", "lalalalalalalala", DateTime.Now);

            for (int i = 0; i < 101; i++)
            {
                await _legatRepository.Create(legat);

            }
        }

    }
}
