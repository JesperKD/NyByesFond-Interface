using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    /// <summary>
    /// object for person including all person data
    /// </summary>
    public class Person
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _eMail;
        private readonly Address _address;
        private readonly Education _education;

        /// <summary>
        /// instantiates a person
        /// </summary>
        /// <param name="id">entity idenfier </param>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="eMail">Email</param>
        /// <param name="address">address object</param>
        /// <param name="education">education object</param>
        public Person(string firstName, string lastName, string eMail, Address address, Education education)
        {
            _firstName = firstName;
            _lastName = lastName;
            _eMail = eMail;
            _address = address;
            _education = education;
        }

        public string FullName { get { return $"{_firstName} {_lastName}"; } }
        public string FirstName { get { return _firstName; } }
        public string LastName { get { return _lastName; } }
        public string eMail { get { return _eMail; } }
        public Address Address { get { return _address; } }
        public Education Education { get { return _education; } }

    }
}
