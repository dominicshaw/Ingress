using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ingress.DTOs;
using LiteDB;

namespace Ingress.Mobile.Services
{
    public class LocalDb : IDisposable
    {
        private static readonly object _lock = new object();

        private static readonly string _databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), @"Ingress.db");

        private readonly LiteDatabase _database;

        public LocalDb()
        {
            _database = new LiteDatabase(_databaseName);
        }

        public IList<BrokerDTO> SearchBrokers(string searchText)
        {
            lock (_lock)
            {
                var sdb = _database.GetCollection<BrokerDTO>();

                if (sdb == null)
                    return new List<BrokerDTO>();

                if (string.IsNullOrWhiteSpace(searchText))
                    return sdb.Find(Query.All("Name"), 0, 30).ToList();

                searchText = searchText.ToUpper();

                var results = sdb.Find(f => f.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.Name).ToList();

                if (results.Count == 0)
                    results = sdb.Find(f => f.Name.Contains(searchText)).OrderBy(x => x.Name).ToList();

                return results;
            }
        }

        public void SaveBrokers(IEnumerable<BrokerDTO> brokers)
        {
            lock (_lock)
            {
                _database.SaveAll(brokers);

                var col = _database.GetCollection<BrokerDTO>();
                col.EnsureIndex(x => x.Name);
            }
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}