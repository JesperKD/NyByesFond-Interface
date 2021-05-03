using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    public abstract class BaseEntity
    {
        private readonly int _id;

        protected BaseEntity(int id = 0)
        {
            _id = id;
        }

        public int Id { get { return _id; } }
    }
}
