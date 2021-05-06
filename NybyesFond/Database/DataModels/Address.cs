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
    public class Address
    {
        private readonly string _roadName;
        private readonly string _houseNumber;
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
        public Address(string roadName, string houseNumber, string zipNumber, string city)
        {
            _roadName = roadName;
            _houseNumber = houseNumber;
            _zipNumber = zipNumber;
            _city = city;
        }

        public string Roadname { get { return _roadName; } }
        public string HouseNumber { get { return _houseNumber; } }
        public string ZipNumber{ get { return _zipNumber; } }
        public string City { get { return _city; } }
        public string FullAddress { get { return $"{Roadname} {HouseNumber}, {ZipNumber} {City}"; } }

    }
}
