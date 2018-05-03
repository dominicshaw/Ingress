using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace Ingress.WPF.Layouts
{
    public class SqlLayoutStrategy : LayoutStrategy
    {
        public SqlLayoutStrategy(ILog log) : base(log)
        {
        }

        private static string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["Layouts"] != null)
                    return ConfigurationManager.ConnectionStrings["Layouts"].ConnectionString;

                return @"Data Source=LONHSQL01\PROD01;Initial Catalog=user_layouts;User Id=layout_manager;Password=layout_manager;";
            }
        }

        public override string RestoreLayout(string username, string application, string controlId, string tabId)
        {
            try
            {
                var layout = string.Empty;
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    var cmd = new SqlCommand("p_layout_get", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = 5 };

                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@app_id", SqlDbType.VarChar, 50).Value = application;
                    cmd.Parameters.Add("@grid_id", SqlDbType.VarChar, 50).Value = controlId;

                    if (string.IsNullOrEmpty(tabId))
                        cmd.Parameters.Add("@tab_id", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@tab_id", SqlDbType.VarChar, 50).Value = tabId;

                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        if (rdr["layout_data"] != DBNull.Value)
                            layout = rdr["layout_data"].ToString();
                    }

                    rdr.Close();
                    rdr.Dispose();

                    cmd.Dispose();

                    con.Close();
                }
                return layout;
            }
            catch (Exception e)
            {
                Log.Warn("Could not restore layout from SQL: {0}", e);
                return null;
            }
        }

        protected override void SaveLayout(string username, string application, string controlId, string tabId, string layoutData)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    var cmd = new SqlCommand("p_layout_write", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = 30 };

                    cmd.Parameters.Add("@ret", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add("@layout_id", SqlDbType.Int).Value = 0;

                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@created_by", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@updated_at", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@updated_by", SqlDbType.VarChar, 50).Value = username;

                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@app_id", SqlDbType.VarChar, 50).Value = application;
                    cmd.Parameters.Add("@grid_id", SqlDbType.VarChar, 50).Value = controlId;
                    if (!string.IsNullOrEmpty(tabId))
                        cmd.Parameters.Add("@tab_id", SqlDbType.VarChar, 50).Value = tabId;
                    else
                        cmd.Parameters.Add("@tab_id", SqlDbType.VarChar, 50).Value = DBNull.Value;

                    cmd.Parameters.Add("@layout_data", SqlDbType.NVarChar, -1).Value = layoutData;

                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    con.Close();
                }
            }
            catch (Exception e)
            {
                Log.Warn("Could not save layout in SQL: {0}", e);
            }
        }

        public override string ToString()
        {
            return "Database";
        }
    }
}