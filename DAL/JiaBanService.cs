using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Models;



namespace DAL
{
    /// <summary>
    /// 加班费数据访问类
    /// </summary>
    public class JiaBanService
    {
        /// <summary>
        /// 根据部门编号查询加班费对象
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public List<JiaBan> GetJiaBanByDept(string last_year_month, string dept)
        {
            //string sql = "select 考勤年月,部门,人员编号,姓名,前夜班次数,休息日加班,节假日加班,正常调休,后夜班次数,后夜班调休次数,金额,备注,更改者,更改日期 from imp_overtime where 考勤年月 = '{0}' and 部门 = '{1}' and 金额 > 0 union all select 考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数, 后夜班调休次数,0 as 金额,'' as 备注,更改者,更改日期 from imp_attendance where 考勤年月 = '{2}' and 部门 = '{3}' and not EXISTS (select 考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数,后夜班调休次数, 金额, 备注, 更改者, 更改日期 from imp_overtime where 考勤年月 = '{4}' and 部门 = '{5}' and 金额 > 0 and imp_attendance.人员编号 = imp_overtime.人员编号)";
            //sql = string.Format(sql, last_year_month, dept, last_year_month, dept, last_year_month, dept);

            string sql = "select a.考勤年月,a.部门,a.人员编号,a.姓名,a.前夜班次数,a.休息日加班,a.节假日加班,a.正常调休,a.后夜班次数,a.后夜班调休次数,a.金额,a.备注,a.更改者,a.更改日期,b.排序 from imp_overtime a";
            sql += " inner join imp_attendance b on b.人员编号 = a.人员编号";
            sql += " where a.考勤年月 = '{0}' and a.部门 = '{1}' and 金额 > 0 and b.考勤年月 = '{2}'";
            sql += " UNION ALL select b.考勤年月,b.部门,b.人员编号,b.姓名,b.前夜班次数,b.休息日加班,b.节假日加班,b.正常调休,b.后夜班次数,b.后夜班调休次数,0 as 金额,'' as 备注,'' as 更改者,'' as 更改日期,b.排序 from imp_attendance b";
            sql += " where b.考勤年月 = '{3}' and b.部门 = '{4}' and not EXISTS (select a.考勤年月,a.部门,a.人员编号,a.姓名,a.前夜班次数,a.休息日加班,a.节假日加班,a.正常调休,a.后夜班次数,a.后夜班调休次数,a.金额,a.备注,a.更改者,a.更改日期,b.排序 from imp_overtime a where a.考勤年月 = '{5}' and a.部门 = '{6}' and a.金额 > 0 and b.人员编号 = a.人员编号) order by 排序";
            sql = string.Format(sql, last_year_month, dept, last_year_month, last_year_month, dept, last_year_month, dept);

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<JiaBan> list = new List<JiaBan>();
            while (objReader.Read())
            {
                list.Add(new JiaBan()
                {
                    考勤年月 = objReader["考勤年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    前夜班次数 = Convert.ToDouble(objReader["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(objReader["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(objReader["节假日加班"].ToString()),
                    正常调休 = Convert.ToDouble(objReader["正常调休"].ToString()),
                    后夜班次数 = Convert.ToDouble(objReader["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(objReader["后夜班调休次数"].ToString()),
                    金额 = Convert.ToDouble(objReader["金额"].ToString()),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["备注"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 根据人员编号查询加班费对象
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JiaBan GetJiaBanByUserId(string last_year_month, string userid)
        {
            string sql = "select 考勤年月,部门,人员编号,姓名,前夜班次数,休息日加班,节假日加班,正常调休,后夜班次数,后夜班调休次数,金额,备注,更改者,更改日期 from imp_overtime where 考勤年月 = '{0}' and 人员编号 = '{1}' and 金额 > 0 union all select 考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数, 后夜班调休次数,0 as 金额,'' as 备注,更改者,更改日期 from imp_attendance where 考勤年月 = '{2}' and 人员编号 = '{3}' and not EXISTS (select 考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数,后夜班调休次数, 金额, 备注, 更改者, 更改日期 from imp_overtime where 考勤年月 = '{4}' and 人员编号 = '{5}' and 金额 > 0 and imp_attendance.人员编号 = imp_overtime.人员编号)";
            sql = string.Format(sql, last_year_month, userid, last_year_month, userid, last_year_month, userid);

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            JiaBan objJiaBan = null;
            if (objReader.Read())
            {
                objJiaBan = new JiaBan()
                {
                    考勤年月 = objReader["考勤年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    前夜班次数 = Convert.ToDouble(objReader["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(objReader["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(objReader["节假日加班"].ToString()),
                    正常调休 = Convert.ToDouble(objReader["正常调休"].ToString()),
                    后夜班次数 = Convert.ToDouble(objReader["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(objReader["后夜班调休次数"].ToString()),
                    金额 = Convert.ToDouble(objReader["金额"].ToString()),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["备注"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"].ToString())
                };
            }
            objReader.Close();
            return objJiaBan;
        }


        /// <summary>
        /// 校验加班数据
        /// </summary>
        /// <param name="objJiaBan"></param>
        /// <returns></returns>
        public string CheckJiaBan(JiaBan objJiaBan, JiaBan objJiaBanFromKaoQin)
        {
            StringBuilder err = new StringBuilder();

            if (objJiaBan.前夜班次数 != objJiaBanFromKaoQin.前夜班次数)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】前夜班次数与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.休息日加班 != objJiaBanFromKaoQin.休息日加班)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】休息日加班与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.节假日加班 != objJiaBanFromKaoQin.节假日加班)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】节假日加班与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.正常调休 != objJiaBanFromKaoQin.正常调休)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】正常调休与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.后夜班次数 != objJiaBanFromKaoQin.后夜班次数)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】后夜班次数与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.后夜班调休次数 != objJiaBanFromKaoQin.后夜班调休次数)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】后夜班调休次数与考勤表数据不一致，请核对！\r\n");
            }
            if (objJiaBan.金额 != objJiaBanFromKaoQin.金额)
            {
                err.Append($"人员编号【{objJiaBan.人员编号}】，姓名【{objJiaBan.姓名}】加班金额有误，请核对！\r\n");
            }
            return err.ToString();
        }



        /// <summary>
        /// 修改加班对象
        /// </summary>
        /// <param name="objJiaBan"></param>
        /// <returns></returns>
        public int ModifyJiaBan(JiaBan objJiaBan, string dept)
        {
            string sql = "select count(*) from imp_overtime where 考勤年月 = '{0}' and 人员编号 = '{1}'";
            sql = string.Format(sql, objJiaBan.考勤年月, objJiaBan.人员编号);
            int num = (int)SQLHelper.GetSingleResult(sql);
            sql = string.Empty;

            if (num < 1)  //如果加班表没有数据，就用insert添加
            {
                //1、编写SQL语句
                sql = "insert into imp_overtime (考勤年月,部门,人员编号,姓名,前夜班次数,休息日加班,节假日加班,正常调休,后夜班次数,后夜班调休次数,金额,备注,更改者,更改日期) values ('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}')";
                //2、解析对象
                sql = string.Format(sql, objJiaBan.考勤年月, objJiaBan.部门, objJiaBan.人员编号, objJiaBan.姓名, objJiaBan.前夜班次数, objJiaBan.休息日加班, objJiaBan.节假日加班, objJiaBan.正常调休, objJiaBan.后夜班次数, objJiaBan.后夜班调休次数, objJiaBan.金额, objJiaBan.备注, objJiaBan.更改者, objJiaBan.更改日期);
            }
            else  //如果加班表有数据，就用update修改
            {
                //1、编写SQL语句
                sql = "update imp_overtime set 前夜班次数 = {0},休息日加班 = {1},节假日加班 = {2},正常调休 = {3},后夜班次数 = {4},后夜班调休次数 = {5},金额 = {6},备注 = '{7}',更改者 = '{8}',更改日期 = '{9}' where 考勤年月 = '{10}' and 人员编号 = '{11}' and 部门 = '{12}'";
                //2、解析对象
                sql = string.Format(sql, objJiaBan.前夜班次数, objJiaBan.休息日加班, objJiaBan.节假日加班, objJiaBan.正常调休, objJiaBan.后夜班次数, objJiaBan.后夜班调休次数, objJiaBan.金额, objJiaBan.备注, objJiaBan.更改者, objJiaBan.更改日期, objJiaBan.考勤年月, objJiaBan.人员编号, dept);
            }
            //3、执行SQL语句，返回结果
            return SQLHelper.Update(sql);
        }


        /// <summary>
        ///根据部门名称查询加班对象，返回DataTable，用于导出Excel。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportJiaBan(string last_year_month, string dept)
        {
            string sql = " select ROW_NUMBER() over (order by id) as 序号,考勤年月,部门,人员编号,姓名,前夜班次数,休息日加班,节假日加班,正常调休,后夜班次数,后夜班调休次数,金额,备注 from imp_overtime where 考勤年月 = '{0}' and 部门 = '{1}' and 金额 > 0 union all select ROW_NUMBER() over (order by id) as 序号,考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数, 后夜班调休次数,0 as 金额,'' as 备注 from imp_attendance where 考勤年月 = '{2}' and 部门 = '{3}' and not EXISTS (select ROW_NUMBER() over (order by id) as 序号,考勤年月, 部门, 人员编号, 姓名, 前夜班次数, 休息日加班, 节假日加班, 正常调休, 后夜班次数,后夜班调休次数, 金额, 备注 from imp_overtime where 考勤年月 = '{4}' and 部门 = '{5}' and 金额 > 0 and imp_attendance.人员编号 = imp_overtime.人员编号)";
            sql = string.Format(sql, last_year_month, dept, last_year_month, dept, last_year_month, dept);

            DataTable dt_export = SQLHelper.GetDataSet(sql).Tables[0];

            for (int i = 0; i < dt_export.Rows.Count; i++)
            {
                dt_export.Rows[i]["序号"] = i + 1;
            }

            return dt_export;
        }


        /// <summary>
        /// 将DataTable转成List，便于调用CheckJiaBan方法。
        /// </summary>
        /// <param name="dt_import"></param>
        /// <returns></returns>
        public List<JiaBan> DataTableToList(DataTable dt_import)
        {
            List<JiaBan> list = new List<JiaBan>();
            for (int i = 0; i < dt_import.Rows.Count; i++)
            {
                list.Add(new JiaBan()
                {
                    考勤年月 = dt_import.Rows[i]["考勤年月"].ToString(),
                    部门 = dt_import.Rows[i]["部门"].ToString(),
                    人员编号 = dt_import.Rows[i]["人员编号"].ToString(),
                    姓名 = dt_import.Rows[i]["姓名"].ToString(),
                    前夜班次数 = Convert.ToDouble(dt_import.Rows[i]["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(dt_import.Rows[i]["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(dt_import.Rows[i]["节假日加班"].ToString()),
                    正常调休 = Convert.ToDouble(dt_import.Rows[i]["正常调休"].ToString()),
                    后夜班次数 = Convert.ToDouble(dt_import.Rows[i]["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(dt_import.Rows[i]["后夜班调休次数"].ToString()),
                    金额 = Convert.ToDouble(dt_import.Rows[i]["金额"].ToString()),
                    备注 = dt_import.Rows[i]["备注"].ToString(),
                    //更改者 = dt_import.Rows[i]["更改者"].ToString(),
                    //更改日期 = Convert.ToDateTime(dt_import.Rows[i]["更改日期"].ToString())
                });
            }
            return list;
        }




        /// <summary>
        /// 根据考勤表人员编号查询加班费对象，用来校验加班费。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JiaBan GetJiaBanByKaoQinUserId(string last_year_month, string userid)
        {
            string sql = "select imp_attendance.考勤年月,imp_attendance.部门,imp_attendance.人员编号,imp_attendance.姓名,imp_attendance.前夜班次数,imp_attendance.休息日加班,imp_attendance.节假日加班,imp_attendance.正常调休,imp_attendance.后夜班次数,imp_attendance.后夜班调休次数,0 as 金额 from imp_attendance where 考勤年月 = '{0}' and 人员编号 = '{1}'";
            sql = string.Format(sql, last_year_month, userid);

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            JiaBan objJiaBanFromKaoQin = null;
            if (objReader.Read())
            {
                objJiaBanFromKaoQin = new JiaBan()
                {
                    考勤年月 = objReader["考勤年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    前夜班次数 = Convert.ToDouble(objReader["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(objReader["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(objReader["节假日加班"].ToString()),
                    正常调休 = Convert.ToDouble(objReader["正常调休"].ToString()),
                    后夜班次数 = Convert.ToDouble(objReader["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(objReader["后夜班调休次数"].ToString()),
                    //金额 = Convert.ToDouble(objReader["金额"].ToString())
                };
            }

            #region 不使用的代码

            //if (objJiaBanFromKaoQin.休息日加班 <= 4)
            //{
            //    if (objJiaBanFromKaoQin.正常调休 <= objJiaBanFromKaoQin.休息日加班 + objJiaBanFromKaoQin.节假日加班)
            //    {
            //        objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + objJiaBanFromKaoQin.休息日加班 * 80 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50 - objJiaBanFromKaoQin.正常调休 * 80;
            //    }
            //    else
            //    {
            //        objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + objJiaBanFromKaoQin.休息日加班 * 80 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50 - objJiaBanFromKaoQin.正常调休 * 100;
            //    }
            //}
            //if (objJiaBanFromKaoQin.休息日加班 > 4)
            //{
            //    if (objJiaBanFromKaoQin.正常调休 <= objJiaBanFromKaoQin.休息日加班 + objJiaBanFromKaoQin.节假日加班)
            //    {
            //        objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + 4 * 80 + (objJiaBanFromKaoQin.休息日加班 - 4) * 160 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50 - objJiaBanFromKaoQin.正常调休 * 80;
            //    }
            //    else
            //    {
            //        objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + 4 * 80 + (objJiaBanFromKaoQin.休息日加班 - 4) * 160 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50 - objJiaBanFromKaoQin.正常调休 * 100;
            //    }
            //}
            #endregion


            //先算应发
            if (objJiaBanFromKaoQin.休息日加班 <= 4)
            {
                objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + objJiaBanFromKaoQin.休息日加班 * 80 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50;
            }
            if (objJiaBanFromKaoQin.休息日加班 > 4)
            {
                objJiaBanFromKaoQin.金额 = objJiaBanFromKaoQin.前夜班次数 * 50 + 4 * 80 + (objJiaBanFromKaoQin.休息日加班 - 4) * 160 + objJiaBanFromKaoQin.节假日加班 * 80 * 3 + (objJiaBanFromKaoQin.后夜班次数 - objJiaBanFromKaoQin.后夜班调休次数) * 50;
            }

            //再算应扣

            objJiaBanFromKaoQin.金额 -= objJiaBanFromKaoQin.正常调休 * 80;
            if (objJiaBanFromKaoQin.金额 < 0)
            {
                objJiaBanFromKaoQin.金额 -= objJiaBanFromKaoQin.前夜班次数 * 50;
            }
            if (objJiaBanFromKaoQin.金额 < 0)
            {
                objJiaBanFromKaoQin.金额 -= objJiaBanFromKaoQin.节假日加班 * 240;
            }


            objReader.Close();
            return objJiaBanFromKaoQin;
        }


        /// <summary>
        ///根据部门名称查询加班对象，返回DataTable，用于导出Excel打印。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportJiaBanPrint(string last_year_month, string dept)
        {
            string sql = "select 0 as 序号,a.人员编号,a.姓名,b.前夜班次数,b.休息日加班,b.节假日加班,b.正常调休,b.后夜班次数,b.后夜班调休次数,b.金额,b.备注,b.考勤年月,b.部门,b.更改日期 from emp_bas a";
            sql += " left join imp_overtime b on a.人员编号 = b.人员编号";
            sql += " left join imp_attendance c on a.人员编号 = c.人员编号";
            sql += " where c.考勤年月 = '{0}' and c.部门 = '{1}' and b.考勤年月 = '{2}' and b.部门 = '{3}' order by c.排序";
            sql = string.Format(sql, last_year_month, dept, last_year_month, dept);
            DataTable dt_export = SQLHelper.GetDataSet(sql).Tables[0];

            for (int i = 0; i < dt_export.Rows.Count; i++)
            {
                dt_export.Rows[i]["序号"] = i + 1;
            }
            return dt_export;
        }






























    }
}
