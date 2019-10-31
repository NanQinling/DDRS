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
    /// 考勤数据访问类
    /// </summary>
    public class KaoQinService
    {

        /// <summary>
        /// 根据部门名称查询考勤对象
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<KaoQin> GetKaoQinByDept(string last_year_month, string dept)
        {
            string whereSql = $" where 考勤年月 = '{last_year_month}' and 部门 = '{dept}'";
            whereSql += " order by 排序";

            return this.GetKaoQinBySql(whereSql);
        }

        /// <summary>
        /// 查询未报送考勤对象
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<KaoQin> GetNotSubmitKaoQinRenYuan(string last_year_month, string dept)
        {
            string whereSql = string.Format(" where 考勤年月 = '{0}' and 部门 = '{1}' and (issubmit = 0 or issubmit is null) order by 排序", last_year_month, dept);
            return this.GetKaoQinBySql(whereSql);
        }


        /// <summary>
        /// 根据SQL语句查询考勤对象
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        private List<KaoQin> GetKaoQinBySql(string whereSql)
        {
            string sql = "select id,考勤年月,部门,班组,人员编号,姓名,应出勤,实际出勤,出差,旷工,年假,事假,病假,正常调休,产假,陪产假,婚假,丧假,迟到早退次数,缺卡次数,前夜班次数,休息日加班,节假日加班,休息日出差,后夜班次数,后夜班调休次数,打卡签到次数,工作时长,备注,更改者,更改日期,IsSubmit,排序 from imp_attendance";
            sql += whereSql;

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<KaoQin> list = new List<KaoQin>();
            while (objReader.Read())
            {
                list.Add(new KaoQin()
                {
                    id = Convert.ToInt32(objReader["id"].ToString()),
                    考勤年月 = objReader["考勤年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    班组 = objReader["班组"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    应出勤 = Convert.ToDouble(objReader["应出勤"].ToString()),
                    实际出勤 = Convert.ToDouble(objReader["实际出勤"].ToString()),
                    出差 = Convert.ToDouble(objReader["出差"].ToString()),
                    旷工 = Convert.ToDouble(objReader["旷工"].ToString()),
                    年假 = Convert.ToDouble(objReader["年假"].ToString()),
                    事假 = Convert.ToDouble(objReader["事假"].ToString()),
                    病假 = Convert.ToDouble(objReader["病假"].ToString()),
                    正常调休 = Convert.ToDouble(objReader["正常调休"].ToString()),
                    产假 = Convert.ToDouble(objReader["产假"].ToString()),
                    陪产假 = Convert.ToDouble(objReader["陪产假"].ToString()),
                    婚假 = Convert.ToDouble(objReader["婚假"].ToString()),
                    丧假 = Convert.ToDouble(objReader["丧假"].ToString()),
                    迟到早退次数 = Convert.ToDouble(objReader["迟到早退次数"].ToString()),
                    缺卡次数 = Convert.ToDouble(objReader["缺卡次数"].ToString()),
                    前夜班次数 = Convert.ToDouble(objReader["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(objReader["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(objReader["节假日加班"].ToString()),
                    休息日出差 = Convert.ToDouble(objReader["休息日出差"].ToString()),
                    后夜班次数 = Convert.ToDouble(objReader["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(objReader["后夜班调休次数"].ToString()),
                    打卡签到次数 = Convert.ToDouble(objReader["打卡签到次数"].ToString()),
                    工作时长 = Convert.ToDouble(objReader["工作时长"].ToString()),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["更改者"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"].ToString()),
                    IsSubmit = objReader["IsSubmit"] is DBNull ? false : (bool)objReader["IsSubmit"],
                    排序 = objReader["排序"] is DBNull ? 0 : Convert.ToInt32(objReader["排序"].ToString()),

                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 根据人员编号查询考勤对象
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public KaoQin GetKaoQinByUserId(string last_year_month, string userid)
        {
            string sql = "select id,考勤年月,部门,班组,人员编号,姓名,应出勤,实际出勤,出差,旷工,年假,事假,病假,正常调休,产假,陪产假,婚假,丧假,迟到早退次数,缺卡次数,前夜班次数,休息日加班,节假日加班,休息日出差,后夜班次数,后夜班调休次数,打卡签到次数,工作时长,备注,更改者,更改日期,IsSubmit,排序 from imp_attendance where 考勤年月 = '{0}' and 人员编号 = '{1}'";
            sql = string.Format(sql, last_year_month, userid);

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KaoQin objKaoQin = null;
            if (objReader.Read())
            {
                objKaoQin = new KaoQin()
                {
                    id = Convert.ToInt32(objReader["id"].ToString()),
                    考勤年月 = objReader["考勤年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    班组 = objReader["班组"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    应出勤 = Convert.ToDouble(objReader["应出勤"].ToString()),
                    实际出勤 = Convert.ToDouble(objReader["实际出勤"].ToString()),
                    出差 = Convert.ToDouble(objReader["出差"].ToString()),
                    旷工 = Convert.ToDouble(objReader["旷工"].ToString()),
                    年假 = Convert.ToDouble(objReader["年假"].ToString()),
                    事假 = Convert.ToDouble(objReader["事假"].ToString()),
                    病假 = Convert.ToDouble(objReader["病假"].ToString()),
                    正常调休 = Convert.ToDouble(objReader["正常调休"].ToString()),
                    产假 = Convert.ToDouble(objReader["产假"].ToString()),
                    陪产假 = Convert.ToDouble(objReader["陪产假"].ToString()),
                    婚假 = Convert.ToDouble(objReader["婚假"].ToString()),
                    丧假 = Convert.ToDouble(objReader["丧假"].ToString()),
                    迟到早退次数 = Convert.ToDouble(objReader["迟到早退次数"].ToString()),
                    缺卡次数 = Convert.ToDouble(objReader["缺卡次数"].ToString()),
                    前夜班次数 = Convert.ToDouble(objReader["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(objReader["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(objReader["节假日加班"].ToString()),
                    休息日出差 = Convert.ToDouble(objReader["休息日出差"].ToString()),
                    后夜班次数 = Convert.ToDouble(objReader["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(objReader["后夜班调休次数"].ToString()),
                    打卡签到次数 = Convert.ToDouble(objReader["打卡签到次数"].ToString()),
                    工作时长 = objReader["工作时长"] is DBNull ? 0 : Convert.ToDouble(objReader["工作时长"].ToString()),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["更改者"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"].ToString()),
                    IsSubmit = objReader["IsSubmit"] is DBNull ? false : (bool)objReader["IsSubmit"],
                    排序 = objReader["排序"] is DBNull ? 0 : Convert.ToInt32(objReader["排序"].ToString()),

                };
            }
            objReader.Close();
            return objKaoQin;
        }


        /// <summary>
        /// 校验考勤数据
        /// </summary>
        /// <param name="objKaoQin"></param>
        /// <returns></returns>
        public string CheckKaoQin(KaoQin objKaoQin)
        {
            StringBuilder err = new StringBuilder();

            if (objKaoQin.出差 > objKaoQin.实际出勤)
            {
                err.Append($"人员编号【{objKaoQin.人员编号}】，姓名【{objKaoQin.姓名}】出差天数不应大于实际出勤天数！\r\n");
            }
            if (objKaoQin.打卡签到次数 > objKaoQin.实际出勤)
            {
                err.Append($"人员编号【{objKaoQin.人员编号}】，姓名【{objKaoQin.姓名}】打卡签到次数不应大于实际出勤天数！\r\n");
            }
            if (objKaoQin.节假日加班 > 7)
            {
                err.Append($"人员编号【{objKaoQin.人员编号}】，姓名【{objKaoQin.姓名}】节假日加班不应大于7天！\r\n");
            }
            if (objKaoQin.正常调休 > objKaoQin.休息日加班 + objKaoQin.节假日加班 + objKaoQin.前夜班次数 / 2)
            {
                err.Append($"人员编号【{objKaoQin.人员编号}】，姓名【{objKaoQin.姓名}】正常调休天数不应大于加班天数！\r\n");
            }
            if (objKaoQin.实际出勤 < objKaoQin.应出勤 + objKaoQin.休息日加班 + objKaoQin.节假日加班 + objKaoQin.休息日出差 - objKaoQin.旷工 - objKaoQin.年假 - objKaoQin.事假 - objKaoQin.病假 - objKaoQin.正常调休 - objKaoQin.产假 - objKaoQin.陪产假 - objKaoQin.婚假 - objKaoQin.丧假)
            {
                err.Append($"人员编号【{objKaoQin.人员编号}】，姓名【{objKaoQin.姓名}】实际出勤不能小于【应出勤】+【休息日加班】+【节假日加班】+【休息日出差】-【休假合计】！\r\n");
            }

            return err.ToString();
        }



        /// <summary>
        /// 修改考勤对象
        /// </summary>
        /// <param name="objKaoQin"></param>
        /// <returns></returns>
        public int ModifyKaoQin(KaoQin objKaoQin, string dept)
        {
            //1、编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update imp_attendance set 应出勤 = {0},实际出勤 = {1},出差 = {2},旷工 = {3},年假 = {4},事假 = {5},病假 = {6},正常调休 = {7},产假 = {8},陪产假 = {9},婚假 = {10},丧假 = {11},迟到早退次数 = {12},缺卡次数 = {13},前夜班次数 = {14},休息日加班 = {15},节假日加班 = {16},休息日出差 = {17},后夜班次数 = {18},后夜班调休次数 = {19},打卡签到次数 = {20},工作时长 = {21},备注 = '{22}',更改者 = '{23}',更改日期 = '{24}',IsSubmit = '{25}',排序 = {26}");
            sqlBuilder.Append(" where 考勤年月 = '{27}' and 人员编号 = '{28}' and 部门 = '{29}'");
            //2、解析对象
            string sql = string.Format(sqlBuilder.ToString(), objKaoQin.应出勤, objKaoQin.实际出勤, objKaoQin.出差, objKaoQin.旷工, objKaoQin.年假, objKaoQin.事假, objKaoQin.病假, objKaoQin.正常调休, objKaoQin.产假, objKaoQin.陪产假, objKaoQin.婚假, objKaoQin.丧假, objKaoQin.迟到早退次数, objKaoQin.缺卡次数, objKaoQin.前夜班次数, objKaoQin.休息日加班, objKaoQin.节假日加班, objKaoQin.休息日出差, objKaoQin.后夜班次数, objKaoQin.后夜班调休次数, objKaoQin.打卡签到次数, objKaoQin.工作时长, objKaoQin.备注, objKaoQin.更改者, objKaoQin.更改日期, objKaoQin.IsSubmit, objKaoQin.排序, objKaoQin.考勤年月, objKaoQin.人员编号, dept);
            //3、执行SQL语句，返回结果
            return SQLHelper.Update(sql);

        }



        /// <summary>
        ///根据部门名称查询考勤对象，返回DataTable，用于导出Excel。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportKaoQinByDept(string last_year_month, string dept)
        {
            string sql = "select ROW_NUMBER() over (order by id) as 序号,考勤年月,部门,班组,人员编号,姓名,应出勤,实际出勤,出差,旷工,年假,事假,病假,正常调休,产假,陪产假,婚假,丧假,迟到早退次数,缺卡次数,前夜班次数,休息日加班,节假日加班,休息日出差,后夜班次数,后夜班调休次数,打卡签到次数,工作时长,备注 from imp_attendance";
            sql += " where 考勤年月 = '{0}' and 部门 = '{1}'";
            sql += " order by 排序";
            sql = string.Format(sql, last_year_month, dept);
            DataTable dt_export = SQLHelper.GetDataSet(sql).Tables[0];
            return dt_export;
        }


        /// <summary>
        /// 将DataTable转成List，便于调用CheckKaoQin方法。
        /// </summary>
        /// <param name="dt_import"></param>
        /// <returns></returns>
        public List<KaoQin> DataTableToList(DataTable dt_import)
        {
            List<KaoQin> list = new List<KaoQin>();
            for (int i = 0; i < dt_import.Rows.Count; i++)
            {
                list.Add(new KaoQin()
                {
                    id = Convert.ToInt32(dt_import.Rows[i]["序号"].ToString()),
                    考勤年月 = dt_import.Rows[i]["考勤年月"].ToString(),
                    部门 = dt_import.Rows[i]["部门"].ToString(),
                    班组 = dt_import.Rows[i]["班组"].ToString(),
                    人员编号 = dt_import.Rows[i]["人员编号"].ToString(),
                    姓名 = dt_import.Rows[i]["姓名"].ToString(),
                    应出勤 = Convert.ToDouble(dt_import.Rows[i]["应出勤"].ToString()),
                    实际出勤 = Convert.ToDouble(dt_import.Rows[i]["实际出勤"].ToString()),
                    出差 = Convert.ToDouble(dt_import.Rows[i]["出差"].ToString()),
                    旷工 = Convert.ToDouble(dt_import.Rows[i]["旷工"].ToString()),
                    年假 = Convert.ToDouble(dt_import.Rows[i]["年假"].ToString()),
                    事假 = Convert.ToDouble(dt_import.Rows[i]["事假"].ToString()),
                    病假 = Convert.ToDouble(dt_import.Rows[i]["病假"].ToString()),
                    正常调休 = Convert.ToDouble(dt_import.Rows[i]["正常调休"].ToString()),
                    产假 = Convert.ToDouble(dt_import.Rows[i]["产假"].ToString()),
                    陪产假 = Convert.ToDouble(dt_import.Rows[i]["陪产假"].ToString()),
                    婚假 = Convert.ToDouble(dt_import.Rows[i]["婚假"].ToString()),
                    丧假 = Convert.ToDouble(dt_import.Rows[i]["丧假"].ToString()),
                    迟到早退次数 = Convert.ToDouble(dt_import.Rows[i]["迟到早退次数"].ToString()),
                    缺卡次数 = Convert.ToDouble(dt_import.Rows[i]["缺卡次数"].ToString()),
                    前夜班次数 = Convert.ToDouble(dt_import.Rows[i]["前夜班次数"].ToString()),
                    休息日加班 = Convert.ToDouble(dt_import.Rows[i]["休息日加班"].ToString()),
                    节假日加班 = Convert.ToDouble(dt_import.Rows[i]["节假日加班"].ToString()),
                    休息日出差 = Convert.ToDouble(dt_import.Rows[i]["休息日出差"].ToString()),
                    后夜班次数 = Convert.ToDouble(dt_import.Rows[i]["后夜班次数"].ToString()),
                    后夜班调休次数 = Convert.ToDouble(dt_import.Rows[i]["后夜班调休次数"].ToString()),
                    打卡签到次数 = Convert.ToDouble(dt_import.Rows[i]["打卡签到次数"].ToString()),
                    工作时长 = Convert.ToDouble(dt_import.Rows[i]["工作时长"].ToString()),
                    备注 = dt_import.Rows[i]["备注"].ToString(),
                    //更改者 = dt_import.Rows[i]["更改者"].ToString(),
                    //更改日期 = Convert.ToDateTime(dt_import.Rows[i]["更改日期"].ToString()),
                    //IsSubmit = dt_import.Rows[i]["IsSubmit"] is DBNull ? false : (bool)dt_import.Rows[i]["IsSubmit"],
                    //排序 = Convert.ToInt32(dt_import.Rows[i]["排序"].ToString()),


                });
            }
            return list;
        }


        /// <summary>
        ///根据部门名称查询考勤对象，返回DataTable，用于导出Excel打印。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportKaoQinPrint(string last_year_month, string dept)
        {
            string sql = "select ROW_NUMBER() over (order by id) as 序号,人员编号,姓名,应出勤,实际出勤,出差,旷工,年假,事假,病假,正常调休,产假,陪产假,婚假,丧假,迟到早退次数,缺卡次数,前夜班次数,休息日加班,节假日加班,休息日出差,后夜班次数,后夜班调休次数,打卡签到次数,工作时长,'' as 本人签字,备注,考勤年月,部门,班组,更改日期 from imp_attendance";
            sql += " where 考勤年月 = '{0}' and 部门 = '{1}'";
            sql += " order by 排序";
            sql = string.Format(sql, last_year_month, dept);
            DataTable dt_export = SQLHelper.GetDataSet(sql).Tables[0];

            for (int i = 0; i < dt_export.Rows.Count; i++)
            {
                dt_export.Rows[i]["序号"] = i + 1;
            }
            return dt_export;
        }

        /// <summary>
        /// 查询未报送考勤人数
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int GetNotSubmitKaoQinRenShu(string last_year_month, string dept)
        {
            string sql = "select count(*) from imp_attendance  where 考勤年月 = '{0}' and 部门 = '{1}' and (issubmit = 0 or issubmit is null)";
            sql = string.Format(sql, last_year_month, dept);
            return (int)SQLHelper.GetSingleResult(sql);
        }


    }
}
