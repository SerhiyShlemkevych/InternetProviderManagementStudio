using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipms.UI.ViewModels.Entities;
using Ipms.Models;

namespace Ipms.UI.Models
{
    static class Mapper
    {
        public static TInto Map<TInto>(object from)
        {
            Type intoType = typeof(TInto);
            Type fromType = from.GetType();
            object into = Activator.CreateInstance(intoType);
            var properties = fromType.GetProperties();
            foreach (var property in properties)
            {
                var intoProperty = intoType.GetProperty(property.Name);
                if (intoProperty != null)
                {
                    if (property.PropertyType.IsAssignableFrom(intoProperty.PropertyType))
                    {
                        intoProperty.SetValue(into, property.GetValue(from));
                    }
                }
            }
            return (TInto)into;
        }

        public static TInto Map<TFrom, TInto>(object from, Action<TFrom, TInto> customCast)
        {
            TInto into = Map<TInto>(from);
            customCast((TFrom)from, into);
            return into;
        }

        internal static CustomerViewModel Map<T1, T2>(CustomerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
