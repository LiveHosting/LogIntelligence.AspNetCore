using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore
{
    internal static class ObjectExtensions
    {
        internal static bool IsValidForItems(this object obj)
        {
            if (obj == null) return false;
            var valueType = obj.GetType();
            return valueType.IsPrimitive
                || valueType.Equals(typeof(string))
                || valueType.Equals(typeof(Version))
                || valueType.Equals(typeof(IPAddress));
        }
    }
}
