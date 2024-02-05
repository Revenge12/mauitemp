using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class GetRequest
    {
        public bool IsList { get; set; }

        public Expression WhereStatment { get; set; }

        public string TableToReturn { get; set; }
    }
}
