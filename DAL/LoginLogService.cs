using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Net;
using System.Data.SqlClient;


namespace DAL
{
    public class LoginLogService
    {
        /// <summary>
        /// 添加登录日志，返回记录编号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int WriteLoginLog(LoginLog info)
        {
            string sql = "insert into LoginLogs(LoginId,SPName,ServerName,LoginAddr,LoginDept)";
            sql += " values (@LoginId,@SPName,@ServerName,@LoginAddr,@LoginDept);select @@identity";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LoginId",info.LoginId),
                new SqlParameter("@SPName",info.SPName),
                new SqlParameter("@ServerName",info.ServerName),
                new SqlParameter("@LoginAddr",info.LoginAddr),
                new SqlParameter("@LoginDept",info.LoginDept),

            };
            return Convert.ToInt32(SQLHelper.GetSingleResult(sql, param));
        }


        /// <summary>
        /// 将用户退出的时间保存在日志中
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int WriteExitLog(int logId, DateTime dt)
        {
            string sql = "update LoginLogs set ExitTime=@ExitTime where LogId=@LogId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ExitTime",dt),
                new SqlParameter("@LogId",logId),
            };
            return SQLHelper.Update(sql, param);

        }


        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <returns></returns>
        public string GetLocalIp()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <param name="bIsV4">True为IPV4地址,Flase为IPV6地址。</param>
        /// <returns></returns>
        public string GetLocalIp(bool bIsV4)
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (bIsV4 == true)
                {
                    if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        AddressIP = _IPAddress.ToString();
                    }
                }
                else
                {
                    if (_IPAddress.AddressFamily.ToString() == "InterNetworkV6")
                    {
                        AddressIP = _IPAddress.ToString();
                    }
                }
            }
            return AddressIP;
        }


        /// <summary>
        /// 获取服务器的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetDBServerTime()
        {
            return SQLHelper.GetDBServerTime();
        }








    }
}
