using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public abstract class UseCase
    {
        public abstract Task<string> ExecuteAsync();s

    }
}
