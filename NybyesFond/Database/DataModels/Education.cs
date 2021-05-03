using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    /// <summary>
    /// Object for educational information
    /// </summary>
    public class Education : BaseEntity
    {
        private readonly string _youthEducation;
        private readonly string _ongoinEducation;
        private readonly string _placeOfEducation;

        /// <summary>
        /// instantiates new object with education data
        /// </summary>
        /// <param name="id"> identifier for this entity </param>
        /// <param name="youthEducation"> previous education</param>
        /// <param name="ongoinEducation">current education</param>
        /// <param name="placeOfEducation">name of current educational institute</param>
        public Education(int id, string youthEducation, string ongoinEducation, string placeOfEducation) : base(id)
        {
            _youthEducation = youthEducation;
            _ongoinEducation = ongoinEducation;
            _placeOfEducation = placeOfEducation;
        }

        public string YouthEducation { get { return _youthEducation; } }
        public string OngoingEducation { get { return _ongoinEducation; } }
        public string PlaceOfEducation { get { return _placeOfEducation; } }

    }
}
