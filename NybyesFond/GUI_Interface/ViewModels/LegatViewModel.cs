using DataAccess.DataModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Interface.ViewModels
{
    public class LegatViewModel
    {
        private readonly ILegatRepository _legatRepository;
        private IEnumerable<Legat> _legats;

        public IEnumerable<Legat> Legats { get { return _legats; } set { _legats = value; } }


        public LegatViewModel(ILegatRepository legatRepository)
        {
            _legatRepository = legatRepository;
        }

        public async Task GetAllLegats()
        {
            _legats = await _legatRepository.GetAll();

        }

        public async void RemoveTest()
        {
            Legat legat = new(null, null, null, null, DateTime.Now, DateTime.Now, null, null, null, DateTime.Now, 6);
            await _legatRepository.Delete(legat);
        }

    }
}
