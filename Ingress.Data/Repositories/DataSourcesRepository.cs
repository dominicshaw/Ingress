using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Ingress.Data.DataSources;
using Ingress.Data.Interfaces;

namespace Ingress.Data.Repositories
{
    public class DataSourcesRepository : IDataSourcesRepository
    {
        public async Task<List<string>> GetAnalysts()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT analyst FROM pb_dashboard.dbo.vote WHERE ISNULL(analyst, '') != ''");
            sql.AppendLine("UNION");
            sql.AppendLine("SELECT DISTINCT analyst FROM analyst_meeting WHERE ISNULL(analyst, '') != ''"); // todo - update to new db
            sql.AppendLine("ORDER BY analyst");

            var results = new List<string>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Skrybe"].ConnectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(sql.ToString(), con) { CommandType = CommandType.Text })
                {
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            if(rdr["analyst"] == DBNull.Value || rdr["analyst"].ToString() == string.Empty)
                                continue;

                            results.Add(rdr["analyst"].ToString());
                        }
                    }
                }
            }

            return results;
        }

        public async Task<List<Broker>> GetBrokers()
        {
            return await GetBrokers(false);
        }

        public async Task<List<Broker>> GetBrokers(bool includeDeleted)
        {
            var brokers = new List<Broker>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Brokers"].ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = new SqlCommand("SELECT * FROM broker WHERE @include_deleted = 1 OR (@include_deleted = 0 AND Deleted = 0) ORDER BY RecipientName", con) { CommandType = CommandType.Text })
                {
                    cmd.Parameters.Add("@include_deleted", SqlDbType.Bit).Value = includeDeleted;

                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                            brokers.Add(new Broker(rdr));
                    }
                }
            }

            return brokers;
        }
    }
}
