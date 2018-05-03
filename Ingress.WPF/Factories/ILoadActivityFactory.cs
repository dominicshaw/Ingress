using Ingress.Data.Models;
using Ingress.WPF.ViewModels.Data;

namespace Ingress.WPF.Factories
{
    public interface ILoadActivityFactory
    {
        ActivityViewModel Load(Activity activity);
    }
}