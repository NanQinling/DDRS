using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;//引入读取配置文件类所在的命名空间


namespace DAL
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    public class SQLHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// 执行增、删、改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                //写入系统日志

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 获取单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                //写入系统日志

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 返回一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //写入系统日志
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询返回一个DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);//创建数据适配器对象
            DataSet ds = new DataSet();//创建一个内存数据集
            try
            {
                conn.Open();
                da.Fill(ds);//使用数据适配器填充数据集
                return ds;
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }









        #region 参数化查询类

        /// <summary>
        /// 执行insert、update、delete类型的SQL语句
        /// </summary>
        /// <param name="sql">提交的SQL语句，可以根据需要添加参数</param>
        /// <param name="param">参数数组（如果没有参数，请传递null）</param>
        /// <returns>返回受影响的行数</returns>
        public static int Update(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (param != null)
            {
                cmd.Parameters.AddRange(param);//添加参数组
            }
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                string info = "执行 public int Update(string sql,SqlParameter[] param)方法时发生异常：" + ex.Message;
                //在这里写入日志
                throw new Exception(info);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行返回单一结果的查询
        /// </summary>
        /// <param name="sql">提交的SQL语句，可以根据需要添加参数</param>
        /// <param name="param">参数数组（如果没有参数，请传递null）</param>
        /// <returns>返回Object类型</returns>
        public static object GetSingleResult(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (param != null)
            {
                cmd.Parameters.AddRange(param);//添加参数组
            }
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                string info = "执行 public object GetSingleResult(string sql,SqlParameter[] param)方法时发生异常：" + ex.Message;
                //在这里写入日志
                throw new Exception(info);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行返回一个结果集的查询
        /// </summary>
        /// <param name="sql">提交的SQL语句，可以根据需要添加参数</param>
        /// <param name="param">参数数组（如果没有参数，请传递null）</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader GetReader(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (param != null)
            {
                cmd.Parameters.AddRange(param);//添加参数组
            }
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            catch (Exception ex)
            {
                string info = "执行 public SqlDataReader GetReader(string sql,SqlParameter[] param)方法时发生异常：" + ex.Message;
                //在这里写入日志
                throw new Exception(info);
            }
        }

        /// <summary>
        /// 获取数据库服务器的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDBServerTime()
        {
            string sql = "select getdate()";
            return Convert.ToDateTime(GetSingleResult(sql, null));
        }


        #endregion



    }
}
