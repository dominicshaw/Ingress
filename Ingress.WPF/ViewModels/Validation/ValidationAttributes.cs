using System;
using System.Collections.Generic;

namespace Ingress.WPF.ViewModels.Validation
{
    public static class ValidationAttributes
    {
        private static readonly Dictionary<Type, ValidationAttributesByType> _types = new Dictionary<Type, ValidationAttributesByType>();

        public static ValidationAttributesByType TryGet(Type type)
        {
            if (_types.TryGetValue(type, out var vcac))
                return vcac;
            
            vcac = new ValidationAttributesByType(type);
            _types.Add(type, vcac);
            return vcac;
        }
    }
}