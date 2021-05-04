using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    public abstract class BaseEntity
    {
        private readonly long _id;

        protected BaseEntity(long id = 0)
        {
            _id = id;
        }

        public long Id { get { return _id; } }
    }
}
