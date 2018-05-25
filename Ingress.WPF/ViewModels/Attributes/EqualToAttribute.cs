using System;

namespace Ingress.WPF.ViewModels.Attributes
{
    public class EqualToAttribute : ComparisonAttribute
    {
        public EqualToAttribute(string otherProperty)
            : base(otherProperty, "{0} must be the same as {1}")
        {
        }

        protected override bool Compare(IComparable a, IComparable b)
        {
            return a.CompareTo(b) == 0;
        }
    }
}