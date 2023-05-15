using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    public enum FilterMode
    {
        empty = 1 ,

        notEmpty = 2,

        equal = 3,

        different = 4,

        contain = 5,
        
        notContain = 6,

        startWith = 7,

        endWith = 8,

        less = 9,

        lessOrEqual = 10,

        bigger = 11,

        biggerOrEqual = 12
    }
}
