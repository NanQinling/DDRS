using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
    public class KaoPingService
    {
        /// <summary>
        /// 保存时判断优良率是否超过20%
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <param name="strScore">德、能、勤、绩的得分</param>
        /// <param name="HNBH">当前人员编号</param>
        /// <returns></returns>
        public double GetCurrentYouLiangLv(string last_year_month, string dept, string[] strScore, string HNBH)
        {
            //优良率不超过20%
            double dYouLiangLv;
            double dScore = Convert.ToDouble(strScore[0]) + Convert.ToDouble(strScore[1]) + Convert.ToDouble(strScore[2]) + Convert.ToDouble(strScore[3]);

            if (dScore >= 80)
            {
                //获取当前月优良人员名单（数组）
                string[] strYouLiangRenYuanHNBH = GetCurrentKaoPingYouLiangRenYuanHNBH(last_year_month, dept);
                if (!strYouLiangRenYuanHNBH.Contains(HNBH))
                {
                    dYouLiangLv = (Convert.ToDouble(GetGetCurrentKaoPingYouLiangShu(last_year_month, dept)) + 1) / Convert.ToDouble(GetCurrentKaoQinRenShu(last_year_month, dept));
                }
                else
                {
                    dYouLiangLv = Convert.ToDouble(GetGetCurrentKaoPingYouLiangShu(last_year_month, dept)) / Convert.ToDouble(GetCurrentKaoQinRenShu(last_year_month, dept));
                }
            }
            else
            {
                //获取当前月优良人员名单（数组）
                string[] strYouLiangMingDan = GetCurrentKaoPingYouLiangRenYuanHNBH(last_year_month, dept);
                if (!strYouLiangMingDan.Contains(HNBH))
                {
                    dYouLiangLv = Convert.ToDouble(GetGetCurrentKaoPingYouLiangShu(last_year_month, dept)) / Convert.ToDouble(GetCurrentKaoQinRenShu(last_year_month, dept));
                }
                else
                {
                    dYouLiangLv = (Convert.ToDouble(GetGetCurrentKaoPingYouLiangShu(last_year_month, dept)) - 1) / Convert.ToDouble(GetCurrentKaoQinRenShu(last_year_month, dept));
                }
            }

            return dYouLiangLv;
        }



        /// <summary>
        /// 查询当前考评年月是否存在数据
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int GetCurrentKaoPingRenShu(string last_year_month, string dept)
        {
            string sql = "select count(*) from imp_evaluation where 考评年月 = '{0}' and 部门 = '{1}'";
            sql = string.Format(sql, last_year_month, dept);
            return (int)SQLHelper.GetSingleResult(sql);
        }


        /// <summary>
        /// 查询当月部门考勤人数
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int GetCurrentKaoQinRenShu(string last_year_month, string dept)
        {
            string sql = "select count(*) from imp_attendance where 考勤年月 = '{0}' and 部门 = '{1}'";
            sql = string.Format(sql, last_year_month, dept);
            //return (int)SQLHelper.GetSingleResult(sql);

            //不数少于5人的，就按5人算。
            return (int)SQLHelper.GetSingleResult(sql) < 5 ? 5 : (int)SQLHelper.GetSingleResult(sql);
        }

        /// <summary>
        /// 查询当前考评年月优良率
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>e
        /// <returns></returns>
        public int GetGetCurrentKaoPingYouLiangShu(string last_year_month, string dept)
        {
            string sql = "select count(*) from imp_evaluation where 考评年月 = '{0}' and 部门 = '{1}' and 考评得分 >= 80";
            sql = string.Format(sql, last_year_month, dept);
            //sql = string.Format(sql, last_year_month, dept);
            return (int)SQLHelper.GetSingleResult(sql);
        }

        /// <summary>
        /// 查询当前考评年月优良率人员编号
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>e
        /// <returns></returns>
        public string[] GetCurrentKaoPingYouLiangRenYuanHNBH(string last_year_month, string dept)
        {
            string sql = "select 人员编号 from imp_evaluation where 考评年月 = '{0}' and 部门 = '{1}' and 考评得分 >= 80";
            sql = string.Format(sql, last_year_month, dept);

            DataTable dt = SQLHelper.GetDataSet(sql).Tables[0];
            string[] strName = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strName[i] = dt.Rows[i]["人员编号"].ToString();
            }
            return strName;
        }

        /// <summary>
        /// 根据考评表，生成部门人员考评对象
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int CreatKaoPingByKaoPing(string last_year_month, string dept)
        {
            //1、编写SQL语句
            string sql = "insert into imp_evaluation(考评年月, 部门, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明,备注, 更改者, 更改日期, IsSubmit, 排序)";
            sql += " select 考勤年月 as 考评年月, 部门, a.人员编号, 姓名, 0 as 德, 0 as 能, 0 as 勤, 0 as 绩, 0 as 考评得分, '' as 考评意见, '' as 具体解释说明, '' as 备注, '' as 更改者, getdate() as 更改日期, 0 as IsSubmit, a.排序 from imp_attendance a";
            sql += " inner join emp_org b on b.人员编号 = a.人员编号";

            sql += $" where a.考勤年月 = '{last_year_month}' and a.部门 = '{dept}' and b.员工子组 > '12'";

            ////2、解析对象
            //sql = string.Format(sql, last_year_month, dept);

            //3、执行SQL语句，返回结果
            return SQLHelper.Update(sql);
        }


        /// <summary>
        /// 根据部门名称查询考评对象
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<KaoPing> GetKaoPingByDept(string last_year_month, string dept)
        {
            string whereSql = $" where 考评年月 = '{last_year_month}' and 部门 = '{dept}'";
            whereSql += " order by 排序";

            return this.GetKaoPingBySql(whereSql);
        }



        /// <summary>
        /// 根据SQL语句查询对象
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        private List<KaoPing> GetKaoPingBySql(string whereSql)
        {
            string sql = "select id, 考评年月, 部门, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明, 备注, 更改者, 更改日期, IsSubmit, 排序 from imp_evaluation";
            sql += whereSql;

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<KaoPing> list = new List<KaoPing>();
            while (objReader.Read())
            {
                list.Add(new KaoPing()
                {
                    id = Convert.ToInt32(objReader["id"].ToString()),
                    考评年月 = objReader["考评年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    //班组 = objReader["班组"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    德 = Convert.ToDouble(objReader["德"]),
                    能 = Convert.ToDouble(objReader["能"]),
                    勤 = Convert.ToDouble(objReader["勤"]),
                    绩 = Convert.ToDouble(objReader["绩"]),
                    考评得分 = Convert.ToDouble(objReader["考评得分"]),
                    考评意见 = objReader["考评意见"].ToString(),
                    具体解释说明 = objReader["具体解释说明"].ToString(),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["更改者"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"]),
                    IsSubmit = objReader["IsSubmit"] is DBNull ? false : (bool)objReader["IsSubmit"],
                    排序 = objReader["排序"] is DBNull ? 0 : Convert.ToInt32(objReader["排序"].ToString()),

                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 根据人员编号查询考评对象
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public KaoPing GetKaoPingByUserId(string last_year_month, string userid)
        {
            string sql = "select id, 考评年月, 部门, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明, 备注, 更改者, 更改日期, IsSubmit, 排序 from imp_evaluation";
            sql += " where 考评年月 = '{0}' and 人员编号 = '{1}'";
            sql = string.Format(sql, last_year_month, userid);

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KaoPing objKaoPing = null;
            if (objReader.Read())
            {
                objKaoPing = new KaoPing()
                {
                    id = Convert.ToInt32(objReader["id"].ToString()),
                    考评年月 = objReader["考评年月"].ToString(),
                    部门 = objReader["部门"].ToString(),
                    //班组 = objReader["班组"].ToString(),
                    人员编号 = objReader["人员编号"].ToString(),
                    姓名 = objReader["姓名"].ToString(),
                    德 = Convert.ToDouble(objReader["德"]),
                    能 = Convert.ToDouble(objReader["能"]),
                    勤 = Convert.ToDouble(objReader["勤"]),
                    绩 = Convert.ToDouble(objReader["绩"]),
                    考评得分 = Convert.ToDouble(objReader["考评得分"]),
                    考评意见 = objReader["考评意见"].ToString(),
                    具体解释说明 = objReader["具体解释说明"].ToString(),
                    备注 = objReader["备注"].ToString(),
                    更改者 = objReader["更改者"].ToString(),
                    更改日期 = Convert.ToDateTime(objReader["更改日期"]),
                    IsSubmit = objReader["IsSubmit"] is DBNull ? false : (bool)objReader["IsSubmit"],
                    排序 = objReader["排序"] is DBNull ? 0 : Convert.ToInt32(objReader["排序"].ToString()),
                };
            }
            objReader.Close();
            return objKaoPing;
        }


        /// <summary>
        /// 校验考评数据
        /// </summary>
        /// <param name="objKaoPing"></param>
        /// <returns></returns>
        public string CheckKaoPing(KaoPing objKaoPing)
        {
            StringBuilder err = new StringBuilder();

            if (objKaoPing.德 > 25)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【德】得分不应大于25！\r\n");
            }
            if (objKaoPing.能 > 25)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【能】得分不应大于25！\r\n");
            }
            if (objKaoPing.勤 > 25)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【勤】得分不应大于25！\r\n");
            }
            if (objKaoPing.绩 > 25)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【绩】得分不应大于25！\r\n");
            }

            if (objKaoPing.德 < 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【德】得分不应小于0！\r\n");
            }
            if (objKaoPing.能 < 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【能】得分不应小于0！\r\n");
            }
            if (objKaoPing.勤 < 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【勤】得分不应小于0！\r\n");
            }
            if (objKaoPing.绩 < 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】【绩】得分不应小于0！\r\n");
            }
            if (objKaoPing.备注.Length > 30)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】备注字符长度不应大于30！\r\n");
            }
            if (objKaoPing.具体解释说明.Length > 200)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】具体解释说明字符长度不应大于200！\r\n");
            }
            if (objKaoPing.考评得分 >= 80 && objKaoPing.具体解释说明.Length == 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】优良人员请填写具体解释说明！\r\n");
            }
            if (objKaoPing.考评得分 < 60 && objKaoPing.具体解释说明.Length == 0)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】不合格人员请填写具体解释说明！\r\n");
            }
            if ((objKaoPing.德 + objKaoPing.能 + objKaoPing.勤 + objKaoPing.绩) != objKaoPing.考评得分)
            {
                err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】各项得分合计与考评得分不一致！\r\n");
            }

            if (objKaoPing.考评得分 >= 80)
            {
                if (objKaoPing.考评意见 != "优良")
                {
                    err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】考评意见与考评得分不一致！\r\n");
                }
            }
            else if (objKaoPing.考评得分 >= 60)
            {
                if (objKaoPing.考评意见 != "合格")
                {
                    err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】考评意见与考评得分不一致！\r\n");
                }
            }
            else
            {
                if (objKaoPing.考评意见 != "不合格")
                {
                    err.Append($"人员编号【{objKaoPing.人员编号}】，姓名【{objKaoPing.姓名}】考评意见与考评得分不一致！\r\n");
                }
            }
            return err.ToString();
        }



        /// <summary>
        /// 修改考评对象
        /// </summary>
        /// <param name="objKaoPing"></param>
        /// <returns></returns>
        public int ModifyKaoPing(KaoPing objKaoPing, string dept)
        {
            //1、编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update imp_evaluation set 德 = {0},能 = {1},勤 = {2},绩 = {3},考评得分 = {4},考评意见 = '{5}',具体解释说明 = '{6}',备注 = '{7}',更改者 = '{8}',更改日期 = '{9}',IsSubmit = '{10}'");
            sqlBuilder.Append(" where 考评年月 = '{11}' and 人员编号 = '{12}' and 部门 = '{13}'");
            //2、解析对象
            string sql = string.Format(sqlBuilder.ToString(), objKaoPing.德, objKaoPing.能, objKaoPing.勤, objKaoPing.绩, objKaoPing.考评得分, objKaoPing.考评意见, objKaoPing.具体解释说明, objKaoPing.备注, objKaoPing.更改者, objKaoPing.更改日期, objKaoPing.IsSubmit, objKaoPing.考评年月, objKaoPing.人员编号, dept);
            //3、执行SQL语句，返回结果
            return SQLHelper.Update(sql);

        }


        /// <summary>
        ///根据部门名称查询考评对象，返回DataTable，用于导出Excel。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportKaoPingByDept(string last_year_month, string dept)
        {
            string sql = "select ROW_NUMBER() over (order by id) as 序号, 考评年月, 部门, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明, 备注 from imp_evaluation";
            sql += " where 考评年月 = '{0}' and 部门 = '{1}'";
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
        /// 将DataTable转成List，便于调用CheckKaoPing方法。
        /// </summary>
        /// <param name="dt_import"></param>
        /// <returns></returns>
        public List<KaoPing> DataTableToList(DataTable dt_import)
        {
            List<KaoPing> list = new List<KaoPing>();
            for (int i = 0; i < dt_import.Rows.Count; i++)
            {
                list.Add(new KaoPing()
                {
                    id = Convert.ToInt32(dt_import.Rows[i]["序号"]),
                    考评年月 = dt_import.Rows[i]["考评年月"].ToString(),
                    部门 = dt_import.Rows[i]["部门"].ToString(),
                    //班组 = dt_import.Rows[i]["班组"].ToString(),
                    人员编号 = dt_import.Rows[i]["人员编号"].ToString(),
                    姓名 = dt_import.Rows[i]["姓名"].ToString(),
                    德 = Convert.ToDouble(dt_import.Rows[i]["德"]),
                    能 = Convert.ToDouble(dt_import.Rows[i]["能"]),
                    勤 = Convert.ToDouble(dt_import.Rows[i]["勤"]),
                    绩 = Convert.ToDouble(dt_import.Rows[i]["绩"]),
                    考评得分 = Convert.ToDouble(dt_import.Rows[i]["考评得分"]),
                    考评意见 = dt_import.Rows[i]["考评意见"].ToString(),
                    具体解释说明 = dt_import.Rows[i]["具体解释说明"].ToString(),
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
        /// 查询未保存数据，如果有，则不能打印。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int GetNotSubmitKaoPingRenShu(string last_year_month, string dept)
        {
            string sql = "select count(*) from imp_evaluation  where 考评年月 = '{0}' and 部门 = '{1}' and (issubmit = 0 or issubmit is null)";
            sql = string.Format(sql, last_year_month, dept);
            return (int)SQLHelper.GetSingleResult(sql);
        }


        /// <summary>
        /// 查询未报送考评对象
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<KaoPing> GetNotSubmitKaoPingRenYuan(string last_year_month, string dept)
        {
            string whereSql = string.Format(" where 考评年月 = '{0}' and 部门 = '{1}' and (issubmit = 0 or issubmit is null) order by 排序", last_year_month, dept);
            return this.GetKaoPingBySql(whereSql);
        }


        /// <summary>
        ///根据部门名称查询考评对象，返回DataTable，用于导出Excel打印。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public DataTable ExportKaoPingPrint(string last_year_month, string dept, double dYouLiangLv)
        {
            string sql = "select ROW_NUMBER() over (order by id) as 序号, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明, 备注, 考评年月, 部门, 更改日期 from imp_evaluation";
            sql += " where 考评年月 = '{0}' and 部门 = '{1}'";
            sql += " order by 排序";
            sql = string.Format(sql, last_year_month, dept);
            DataTable dt_export = SQLHelper.GetDataSet(sql).Tables[0];

            DataRow row = dt_export.NewRow();
            //row["人员编号"] = "优良率";      //此处不用写，NPOI合并单元格后会将值清空。
            row["考评意见"] = dYouLiangLv.ToString("0.00%");

            dt_export.Rows.Add(row);

            for (int i = 0; i < dt_export.Rows.Count - 1; i++)
            {
                dt_export.Rows[i]["序号"] = i + 1;
            }

            return dt_export;
        }









    }
}
