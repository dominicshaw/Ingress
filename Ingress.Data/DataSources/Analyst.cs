using System.Data;
using JetBrains.Annotations;

namespace Ingress.Data.DataSources
{
    public class Analyst
    {
        [UsedImplicitly]
        public string Name { get; }

        public Analyst(string nm)
        {
            Name = nm;
        }

        public Analyst(IDataRecord rdr)
        {
            Name = (string) rdr["analyst"];
        }
    }
}