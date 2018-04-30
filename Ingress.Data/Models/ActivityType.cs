namespace Ingress.Data.Models
{
    public class ActivityType
    {
        public ActivityType(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; }
        public string Name { get; }
    }
}