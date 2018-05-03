using Ingress.WPF.ViewModels.Data;

namespace Ingress.WPF.ViewModels
{
    public enum Where
    {
        List,
        Activity
    }

    public class NavigationCommand
    {
        public Where GoWhere { get; }
        public ActivityViewModel Activity { get; }

        public NavigationCommand(Where where, ActivityViewModel activity = null)
        {
            GoWhere = where;
            Activity = activity;
        }
    }
}