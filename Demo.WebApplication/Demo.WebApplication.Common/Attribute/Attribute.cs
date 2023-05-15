using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute{ }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name) 
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicate : Attribute{ }

    [AttributeUsage(AttributeTargets.Property)]
    public class TimeMalformed : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length20 : Attribute{ }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length25 : Attribute{ }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length50 : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length100 : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length255 : Attribute { }
}
