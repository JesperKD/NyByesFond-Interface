using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    /// <summary>
    /// Object containing Location data
    /// </summary>
    public class Address : BaseEntity
    {
        private readonly string _roadName;
        private readonly int _houseNumber;
        private readonly string _zipNumber;
        private readonly string _city;

        /// <summary>
        /// instantiates object containing localisation data
        /// </summary>
        /// <param name="id"> entity identifier </param>
        /// <param name="roadName"> name of road/street </param>
        /// <param name="houseNumber"> housenumber </param>
        /// <param name="zipNumber"> postal code </param>
        /// <param name="city"> City name </param>
        public Address(int id, string roadName, int houseNumber, string zipNumber, string city) : base(id)
        {
            _roadName = roadName;
            _houseNumber = houseNumber;
            _zipNumber = zipNumber;
            _city = city;
        }

        public string Roadname { get { return _roadName; } }
        public int HouseNumber { get { return _houseNumber; } }
        public string ZipNumber{ get { return _zipNumber; } }
        public string City { get { return _city; } }

    }
}
