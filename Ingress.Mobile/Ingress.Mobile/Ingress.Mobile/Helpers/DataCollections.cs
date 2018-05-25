using System.Collections.Generic;

namespace Ingress.Mobile.Helpers
{
    public static class DataCollections
    {
        public static List<int> Hours { get; } = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static List<int> Minutes { get; } = new List<int>() { 0, 15, 30, 45 };
        public static List<int> Ratings { get; } = new List<int>() { 1, 2, 3, 4, 5 };
        public static List<string> PushPull { get; } = new List<string>() { "Push", "Pull" };
    }
}