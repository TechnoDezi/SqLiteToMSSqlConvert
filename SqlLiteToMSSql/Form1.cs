using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace SqlLiteToMSSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseSqlLite_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSqlLiteFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnTestMSSql_Click(object sender, EventArgs e)
        {
            if (TestMSSqlConn())
            {
                txtOutput.Text = "Connection Successful";
            }
        }

        private bool TestMSSqlConn()
        {
            bool returnValue = false;

            try
            {
                var msSqlCon = GetSqlConnection();
                msSqlCon.Open();

                returnValue = true;

                msSqlCon.Close();
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }

            return returnValue;
        }

        private SQLiteConnection GetSqlLiteConnection()
        {
            return new SQLiteConnection($"Data Source={txtSqlLiteFile.Text};Version=3;");
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(txtMSSqlConn.Text);
        }

        private bool TestSqlLiteConn()
        {
            bool returnValue = false;

            try
            {
                var sqlCon = GetSqlLiteConnection();
                sqlCon.Open();

                returnValue = true;

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }

            return returnValue;
        }

        private void btnTestSqlLiteConn_Click(object sender, EventArgs e)
        {
            if (TestSqlLiteConn())
            {
                txtOutput.Text = "Connection Successful";
            }
        }

        private List<string> GetTableList()
        {
            List<string> returnValue = new List<string>();

            try
            {
                
                string sql = "SELECT * FROM sqlite_master WHERE type='table'";

                var conn = GetSqlLiteConnection();
                conn.Open();

                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                
                // Loop through all tables
                while (reader.Read())
                {
                    string tablename = reader["name"].ToString();
                    string sqlstr = reader["sql"].ToString();

                    returnValue.Add(tablename);
                }

                conn.Close();
            }
            catch(Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }

            return returnValue;
        }

        private void btnListTables_Click(object sender, EventArgs e)
        {
            var tables = GetTableList();

            txtOutput.Text = "";

            foreach (var item in tables)
            {
                txtOutput.Text += item + Environment.NewLine;
            }
        }

        private string ReplaceCaseInsensitive(string str, string oldValue, string newValue)
        {
            int prevPos = 0;
            string retval = str;

            // find the first occurence of oldValue
            int pos = retval.IndexOf(oldValue, StringComparison.InvariantCultureIgnoreCase);

            while (pos > -1)
            {
                // remove oldValue from the string
                retval = retval.Remove(pos, oldValue.Length);

                // insert newValue in its place
                retval = retval.Insert(pos, newValue);

                // check if oldValue is found further down
                prevPos = pos + newValue.Length;
                pos = retval.IndexOf(oldValue, prevPos, StringComparison.InvariantCultureIgnoreCase);
            }

            return retval;
        }

        private void TransferSchema()
        {
            txtOutput.Text = "";

            try
            {
                var sqlconn = GetSqlConnection();
                sqlconn.Open();

                var liteConn = GetSqlLiteConnection();
                liteConn.Open();

                string sql = "SELECT * FROM sqlite_master WHERE type='table'";

                SQLiteCommand command = new SQLiteCommand(sql, liteConn);
                SQLiteDataReader reader = command.ExecuteReader();
                
                // Loop through all tables
                while (reader.Read())
                {
                    string tablename = reader["name"].ToString();
                    string sqlstr = reader["sql"].ToString();

                    // Only create and import table if it does not exist
                    if (!SQLTableExists(tablename))
                    {
                        txtOutput.Text += "Creating table: " + tablename + Environment.NewLine;

                        sqlstr = ReplaceSql(sqlstr);

                        SqlCommand sqlcmd = new SqlCommand(sqlstr, sqlconn);
                        try
                        {                            
                            sqlcmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
                        {
                            txtOutput.Text += ex.ToString() + Environment.NewLine;
                        }

                        sqlcmd.Dispose();
                    }
                    else
                    {
                        txtOutput.Text += "Table already exists: " + tablename + Environment.NewLine;
                    }
                }

                sqlconn.Close();
                liteConn.Close();
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }
        }

        private void TransferData()
        {
            txtOutput.Text = "";

            try
            {
                var sqlconn = GetSqlConnection();
                sqlconn.Open();

                var liteConn = GetSqlLiteConnection();
                liteConn.Open();

                string sql = "SELECT * FROM sqlite_master WHERE type='table'";

                SQLiteCommand command = new SQLiteCommand(sql, liteConn);
                SQLiteDataReader reader = command.ExecuteReader();
                
                // Loop through all tables
                while (reader.Read())
                {
                    string tablename = reader["name"].ToString();
                    string sqlstr = reader["sql"].ToString();
                    
                    if (SQLTableExists(tablename))
                    {
                        txtOutput.Text += "Transferring data for table: " + tablename + Environment.NewLine;

                        List<KeyValuePair<string, string>> columns = GetSQLiteSchema(tablename);

                        using (SQLiteCommand cmd = new SQLiteCommand("select * from " + tablename, liteConn))
                        {
                            using (SQLiteDataReader readerT = cmd.ExecuteReader())
                            {
                                while (readerT.Read())
                                {
                                    StringBuilder sqlS = new StringBuilder();

                                    if (sqlstr.ToLower().Contains("primary key autoincrement"))
                                    {
                                        sqlS.Append("set identity_insert " + tablename + " on;");
                                    }
                                    sqlS.Append("insert into " + tablename + " (");
                                    bool first = true;
                                    foreach (KeyValuePair<string, string> column in columns)
                                    {
                                        if (first)
                                            first = false;
                                        else
                                            sqlS.Append(",");
                                        sqlS.Append("[" + column.Key + "]");
                                    }
                                    sqlS.Append(") Values(");
                                    first = true;
                                    foreach (KeyValuePair<string, string> column in columns)
                                    {
                                        if (first)
                                            first = false;
                                        else
                                            sqlS.Append(",");
                                        sqlS.Append("@");
                                        sqlS.Append(column.Key);
                                    }
                                    sqlS.Append(");");
                                    if (sqlstr.ToLower().Contains("primary key autoincrement"))
                                    {
                                        sqlS.Append("set identity_insert " + tablename + " off;");
                                    }
                                    try
                                    {
                                        using (SqlCommand sqlcmd = new SqlCommand(sqlS.ToString(), sqlconn))
                                        {
                                            int colmnIndex = 0;
                                            foreach (KeyValuePair<string, string> column in columns)
                                            {
                                                if (column.Value.ToLower() == "blob")
                                                {
                                                    sqlcmd.Parameters.Add(new SqlParameter()
                                                    {
                                                        ParameterName = "@" + column.Key,
                                                        SqlDbType = SqlDbType.VarBinary,
                                                        Size = -1,
                                                        Value = readerT[column.Key]
                                                    });
                                                }
                                                else if (column.Value.ToLower() == "datetime")
                                                {
                                                    var timestamp = readerT.GetDouble(colmnIndex);

                                                    var datetime = DateTime.FromOADate(timestamp - 2415018.5);

                                                    sqlcmd.Parameters.AddWithValue("@" + column.Key, datetime);
                                                }
                                                else
                                                {
                                                    sqlcmd.Parameters.AddWithValue("@" + column.Key, readerT[column.Key]);
                                                }

                                                colmnIndex++;
                                            }
                                            int count = sqlcmd.ExecuteNonQuery();
                                            if (count == 0)
                                                txtOutput.Text += "Unable to insert row!" + Environment.NewLine;
                                        }
                                    }
                                    catch (Exception Exception)
                                    {
                                        string message = Exception.Message;
                                        int idx = message.IndexOf("Violation of PRIMARY KEY");
                                        if (idx < 0)
                                            txtOutput.Text += Exception.ToString() + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        txtOutput.Text += "Table does not exists: " + tablename + Environment.NewLine;
                    }
                }

                sqlconn.Close();
                liteConn.Close();
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }
        }

        private List<KeyValuePair<string, string>> GetSQLiteSchema(string tablename)
        {
            var liteConn = GetSqlLiteConnection();
            liteConn.Open();

            try
            {
                using (var cmd = new SQLiteCommand("PRAGMA table_info(" + tablename + ");", liteConn))
                {
                    var table = new DataTable();

                    SQLiteDataAdapter adp = null;
                    try
                    {
                        adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(table);
                        List<KeyValuePair<string, string>> res = new List<KeyValuePair<string, string>>();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            string key = table.Rows[i]["name"].ToString();
                            string value = table.Rows[i]["type"].ToString();

                            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key, value);

                            res.Add(kvp);
                        }
                        return res;
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }

            liteConn.Close();

            return null;
        }

        private string ReplaceSql(string sqlstr)
        {
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] BOOLEAN", "] bit");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] BLOB", "] varbinary(max)"); // Note, maks 2 GB i varbinary(max) kolonner
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] VARCHAR", "] nvarchar");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] nvarchar,", "] nvarchar(max),");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] nvarchar\r", "] nvarchar(max)\r"); // Case windiows
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] nvarchar\n", "] nvarchar(max)\n"); // Case linux
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] INTEGER", "] int");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] TEXT", "] nvarchar(max)");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "] NUMERIC", "] numeric(18,5)");

            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" BOOLEAN", "\" bit");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" BLOB", "\" varbinary(max)"); // Note, maks 2 GB i varbinary(max) kolonner
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" VARCHAR", "\" nvarchar");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" nvarchar,", "\" nvarchar(max),");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" nvarchar\r", "\" nvarchar(max)\r"); // Case windiows
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" nvarchar\n", "\" nvarchar(max)\n"); // Case linux
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" INTEGER", "\" int");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" TEXT", "\" nvarchar(max)");
            sqlstr = ReplaceCaseInsensitive(sqlstr, "\" NUMERIC", "\" numeric(18,5)");

            sqlstr = ReplaceCaseInsensitive(sqlstr, "primary key autoincrement", "IDENTITY(1,1)");

            //IDENTITY(1,1)

            //Replace double quotes with square brackets
            var reg = new Regex("\".*?\"");
            var matches = reg.Matches(sqlstr);
            foreach (var item in matches)
            {
                sqlstr = ReplaceCaseInsensitive(sqlstr, item.ToString(), "[" + item.ToString().TrimStart('"').TrimEnd('"') + "]");
            }

            return sqlstr;
        }

        private bool SQLTableExists(string tablename)
        {
            bool returnValue = false;

            try
            {
                var conn = GetSqlConnection();
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            returnValue = true;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }

            return returnValue;
        }

        private void btnTransferSchema_Click(object sender, EventArgs e)
        {
            TransferSchema();
        }

        private void btnTransferData_Click(object sender, EventArgs e)
        {
            TransferData();
        }
    }
}
