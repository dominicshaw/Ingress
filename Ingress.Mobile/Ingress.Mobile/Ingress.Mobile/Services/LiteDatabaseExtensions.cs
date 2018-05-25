using System.Collections.Generic;
using Ingress.Mobile.Helpers;
using LiteDB;

namespace Ingress.Mobile.Services
{
    public static class LiteDatabaseExtensions
    {
        public static void ReplaceAll<T>(this LiteDatabase db, IEnumerable<T> items)
        {
            var col = db.GetCollection<T>();

            if (col.Count() > 0)
                db.DropCollection(col.Name);

            col = db.GetCollection<T>();
            col.Insert(items);
        }

        public static void SaveAll<T>(this LiteDatabase db, IEnumerable<T> items)
        {
            var sdb = db.GetCollection<T>();
            sdb.Upsert(items);
        }

        public static void Save<T>(this LiteDatabase db, T item)
        {
            var col = db.GetCollection<T>();
            
            var result = col.Update(item);
            
            if (!result)
            {
                Reporter.TrackError("Save Failure", new Dictionary<string, string>
                {
                    {"Type", typeof(T).ToString()},
                    {"Object", item.ToString()}
                });
            }
        }

        public static bool Has<T>(this LiteDatabase db)
        {
            var adb = db.GetCollection<T>();
            return adb.Count() > 0;
        }
    }
}