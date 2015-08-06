using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Data.OracleClient;
using GOMFrameWork.Utils;
using System.Threading;

namespace GOMFrameWork.DBContext
{
    public abstract class DBProvider
    {
        protected DbConnection Conn = null;
        protected DbCommand Cmd = null;

        protected void OpenConn()
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
        }

        #region 根据sql语句,paramerers参数执行数据库

        public abstract DataSet ExcuteProc(string procName, DbParameter[] parameters);

        public void ExecuteNonQuery(string sqlstr, DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sqlstr))
            {
                return;
            }
            OpenConn();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sqlstr;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddRange(parameters);
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                Cmd.Dispose();
            }
        }

        public int ExecuteScalar(string sqlstr, DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sqlstr))
            {
                return 0;
            }

            OpenConn();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sqlstr;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddRange(parameters);
            try
            {
                return Convert.ToInt32(Cmd.ExecuteScalar());
            }
            catch
            {
                throw;
            }
            finally
            {
                Cmd.Dispose();
            }
        }

        public DbDataReader ExcuteReader(string sqlstr, DbParameter[] parameters)
        {
            OpenConn();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sqlstr;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddRange(parameters);

            try
            {
                return Cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                Cmd.Dispose();
            }
        }

        #endregion

        internal void Close()
        {
            Conn.Close();
        }
    }

    internal class SqlProvider : DBProvider
    {
        public SqlProvider(string connStr)
        {
            base.Conn = new SqlConnection(connStr);
            base.Cmd = new SqlCommand() { Connection = base.Conn as SqlConnection, CommandType = CommandType.Text };
        }

        public override DataSet ExcuteProc(string procName, DbParameter[] parameters)
        {
            OpenConn();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = procName;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddRange(parameters);
            DataSet ds = new DataSet();
            DataAdapter adapter = null;
            try
            {
                adapter = new SqlDataAdapter(Cmd as SqlCommand);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                adapter.Dispose();
                ds.Dispose();
                Cmd.Dispose();
            }
            return ds;
        }
    }

    internal class OracleProvider : DBProvider
    {
        public OracleProvider(string connStr)
        {
            base.Conn = new OracleConnection(connStr);
            base.Cmd = new OracleCommand() { Connection = Conn as OracleConnection, CommandType = CommandType.Text };
        }

        public override DataSet ExcuteProc(string procName, DbParameter[] parameters)
        {
            OpenConn();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = procName;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddRange(parameters);
            DataSet ds = new DataSet();
            DataAdapter adapter = null;
            try
            {
                adapter = new OracleDataAdapter(Cmd as OracleCommand);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                adapter.Dispose();
                ds.Dispose();
                Cmd.Dispose();
            }
            return ds;
        }
    }
}
