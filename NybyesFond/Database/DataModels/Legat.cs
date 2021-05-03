using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    /// <summary>
    /// Object containing all legat data
    /// </summary>
    public class Legat : BaseEntity
    {
        private readonly Person _person;
        private readonly string _reasonForSearch;
        private readonly int _wishedAmount;
        private readonly string _budget;
        private readonly DateTime _dateFrom;
        private readonly DateTime _dateTo;
        private readonly string _isSearchedAlready;
        private readonly string _knowledgeOfOtherSearch;
        private readonly string _additionalInfo;
        private readonly DateTime _todaysDate;

        /// <summary>
        /// instantiates a legat object
        /// </summary>
        /// <param name="id">entitity identifier</param>
        /// <param name="person">person object</param>
        /// <param name="reasonForSearch">written reason for search</param>
        /// <param name="wishedAmount">amount of money wished</param>
        /// <param name="budget">detailed budget description</param>
        /// <param name="dateFrom">start date</param>
        /// <param name="dateTo">end date</param>
        /// <param name="isSearchedAlready">radio button answer</param>
        /// <param name="knowledgeOfOtherSearch">names of other fonds that have been searched from</param>
        /// <param name="additionalInfo"></param>
        /// <param name="todaysDate">Todays Date</param>
        public Legat(int id, Person person, string reasonForSearch, int wishedAmount, string budget, DateTime dateFrom, DateTime dateTo, string isSearchedAlready, string knowledgeOfOtherSearch, string additionalInfo, DateTime todaysDate) : base(id)
        {
            _person = person;
            _reasonForSearch = reasonForSearch;
            _wishedAmount = wishedAmount;
            _budget = budget;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            _isSearchedAlready = isSearchedAlready;
            _knowledgeOfOtherSearch = knowledgeOfOtherSearch;
            _additionalInfo = additionalInfo;
            _todaysDate = todaysDate;
        }

        public Person Person { get {return _person; } }

        public string ReasonForSearch { get {return _reasonForSearch; } }

        public int WishedAmount {
            get
            {
                return _wishedAmount;
            }
        }

        public string Budget {
            get
            {
                return _budget;
            }
        }

        public DateTime DateFrom {
            get
            {
                return _dateFrom;
            }
        }

        public DateTime DateTo {
            get
            {
                return _dateTo;
            }
        }

        public string IsSearchedAlready {
            get
            {
                return _isSearchedAlready;
            }
        }

        public string KnowledgeOfOtherSearch {
            get
            {
                return _knowledgeOfOtherSearch;
            }
        }

        public string AdditionalInfo {
            get
            {
                return _additionalInfo;
            }
        }

        public DateTime TodaysDate {
            get
            {
                return _todaysDate;
            }
        }
    }
}
