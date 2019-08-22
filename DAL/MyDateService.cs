using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class MyDateService
    {
        public MyDate GetDate(MyDate objMydate)
        {
            string sql = "SELECT Max(发放日期) AS 当前期间 FROM ffb WHERE ffb.工资范围='VN' AND ffb.发放类型='工资' and 是否发放= 1";
            //sql = string.Format(sql, objAdmin.userid, objAdmin.pwd, objAdmin.dept);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objMydate.last_year_month = objReader["当前期间"].ToString().Substring(0, 6);
                    objMydate.current_date_gz = DateTime.Parse($"{ objMydate.last_year_month.Substring(0, 4)}-{ objMydate.last_year_month.Substring(4, 2)}-10").AddMonths(1).ToString("yyyyMMdd");
                    objMydate.current_date_jj = DateTime.Parse($"{ objMydate.last_year_month.Substring(0, 4)}-{ objMydate.last_year_month.Substring(4, 2)}-20").AddMonths(1).ToString("yyyyMMdd");
                    objMydate.current_year_month = objMydate.current_date_gz.Substring(0, 6);
                }
                else
                {
                    objMydate = null;
                }
                objReader.Close();
                return objMydate;
            }
            catch (Exception ex)
            {
                throw new Exception("数据访问发生异常：" + ex.Message);
            }
        }



    }
}
