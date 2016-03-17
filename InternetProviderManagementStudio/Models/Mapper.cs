using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models
{
    class Mapper
    {
        public static T Map<T>(object from)
        {
            Type intoType = typeof(T);
            Type fromType = from.GetType();
            object into = Activator.CreateInstance(intoType);
            var properties = fromType.GetProperties();
            foreach (var property in properties)
            {
                var intoProperty = intoType.GetProperty(property.Name);
                if (intoProperty != null)
                {
                    intoProperty.SetValue(into, property.GetValue(from));
                }
            }
            return (T)into;
        }
    }
}
