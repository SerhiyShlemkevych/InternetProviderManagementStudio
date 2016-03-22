using System;

namespace Ipms.UI.Models
{
    static class Mapper
    {
        public static TInto Map<TInto>(object from)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

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
            if (customCast == null)
            {
                throw new ArgumentNullException("customCast");
            }

            TInto into = Map<TInto>(from);
            customCast((TFrom)from, into);
            return into;
        }
    }
}
