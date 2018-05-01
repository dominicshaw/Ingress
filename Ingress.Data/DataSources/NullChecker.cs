using System;

namespace Ingress.Data.DataSources
{
    internal static class NullChecker<T>
    {
        internal static T Check(object field, T retVal)
        {
            if (field == null || Convert.IsDBNull(field))
                return retVal;

            try
            {
                return (T) field;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(string.Format("Could not cast '{0}' to {1}", field, typeof (T).Name));
            }
        }

        internal static T Check(object field)
        {
            if (Convert.IsDBNull(field))
                return default(T);

            try
            {
                return (T) field;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(string.Format("Could not cast '{0}' to {1}", field.GetType().Name, typeof (T).Name));
            }
        }
    }
}