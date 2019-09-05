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
            string sql = "SELECT Max(发放日期) AS 当前期间,getdate() as 服务器时间 FROM ffb WHERE ffb.工资范围='VN' AND ffb.发放类型='工资' and 是否发放= 1";
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objMydate.last_year_month = ((DateTime)objReader["当前期间"]).ToString("yyyyMMdd").Substring(0, 6);
                    objMydate.current_date_gz = ((DateTime)objReader["当前期间"]).AddMonths(1);
                    objMydate.current_date_jj = ((DateTime)objReader["当前期间"]).AddMonths(1).AddDays(10);
                    objMydate.current_year_month = objMydate.current_date_gz.ToString("yyyyMMdd").Substring(0, 6);
                    objMydate.loginDate = (DateTime)objReader["服务器时间"];
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
