using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace Business
{
    public class AccessHelper
    {
        private readonly string _connStr;
        private OleDbConnection _oleConnection;
        private OleDbCommand _oleCommand;
        private OleDbDataReader _oleReader;
        private DataTable _dt;

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        public AccessHelper()
        {
            //conn_str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + Environment.CurrentDirectory + "\\yxdain.accdb'";  
            var data = ConfigurationSettings.AppSettings["Default"].ToString();
            _connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Environment.CurrentDirectory + "\\'" + data; 
            InitDb();
        }

        private void InitDb()
        {
            _oleConnection = new OleDbConnection(_connStr);//创建实例  
            _oleCommand = new OleDbCommand();
        }

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="dbPath">数据库路径</param>  
        public AccessHelper(string dbPath)
        {
            //conn_str ="Provider=Microsoft.Jet.OLEDB.4.0;Data Source='"+ db_path + "'";  
            _connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + dbPath + "'";
            InitDb();
        }

        /// <summary>  
        /// 转换数据格式  
        /// </summary>  
        /// <param name="reader">数据源</param>  
        /// <returns>数据列表</returns>  
        private DataTable ConvertOleDbReaderToDataTable(ref OleDbDataReader reader)
        {
            var dataColumnCount = reader.FieldCount;
            var dtTmp = BuildAndInitDataTable(dataColumnCount);
            if (dtTmp == null)
            {
                return null;
            } 
            while (reader.Read())
            {
                var dr = dtTmp.NewRow();
                for (var i = 0; i < dataColumnCount; ++i)
                {
                    dr[i] = reader[i];
                } 
                dtTmp.Rows.Add(dr);
            }

            return dtTmp;
        }

        /// <summary>  
        /// 创建并初始化数据列表  
        /// </summary>  
        /// <param name="fieldCount">列的个数</param>  
        /// <returns>数据列表</returns>  
        private DataTable BuildAndInitDataTable(int fieldCount)
        {
            if (fieldCount <= 0)
            {
                return null;
            }
            var dtTmp = new DataTable();
            for (var i = 0; i < fieldCount; ++i)
            {
                var dc = new DataColumn(i.ToString());
                dtTmp.Columns.Add(dc);
            }

            return dtTmp;
        }

        /// <summary>  
        /// 从数据库里面获取数据  
        /// </summary>  
        /// <param name="strSql">查询语句</param>  
        /// <returns>数据列表</returns>  
        public DataTable GetDataTableFromDb(string strSql)
        {
            if (_connStr == null)
            {
                return null;
            }

            try
            {
                _oleConnection.Open();//打开连接  
                if (_oleConnection.State == ConnectionState.Closed)
                {
                    return null;
                }
                _oleCommand.CommandText = strSql;
                _oleCommand.Connection = _oleConnection;
                _oleReader = _oleCommand.ExecuteReader(CommandBehavior.Default);
                _dt = ConvertOleDbReaderToDataTable(ref _oleReader);
                _oleReader.Close();
                _oleReader.Dispose();
            }
            catch (Exception e)
            {  
                throw new  Exception(e.Message);
            }
            finally
            {
                if (_oleConnection.State != ConnectionState.Closed)
                {
                    _oleConnection.Close();
                }
            }

            return _dt;
        }

        /// <summary>  
        /// 执行sql语句  
        /// </summary>  
        /// <param name="strSql">sql语句</param>  
        /// <returns>返回结果</returns>  
        public int ExcuteSql(string strSql)
        {
            var nResult = 0;

            try
            {
                _oleConnection.Open();//打开数据库连接  
                if (_oleConnection.State == ConnectionState.Closed)
                {
                    return nResult;
                }
                _oleCommand.Connection = _oleConnection;
                _oleCommand.CommandText = strSql;
                nResult = _oleCommand.ExecuteNonQuery();
            }
            catch  
            {
                //Console.WriteLine(e.ToString());  
                //throw new Exception(e.Message);
                return nResult;
            }
            finally
            {
                if (_oleConnection.State != ConnectionState.Closed)
                {
                    _oleConnection.Close();
                }
            }

            return nResult;
        }
    }
}