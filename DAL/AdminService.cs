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
    /// 管理员数据访问类
    /// </summary>
    public class AdminService
    {
        /// <summary>
        /// 根据用户名或密码登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public Admin AdminLogin(Admin objAdmin, DateTime dateTime)
        {
            string sql = "select id,deptid,dept,UserId,username,attendance,overtime,evaluation,assessment,开始日期,结束日期,备注,更改者,更改日期 from tbl_user";
            sql += " where userid='{0}' and pwd='{1}' and dept='{2}' and '{3}' between 开始日期 and 结束日期";
            sql += " order by DeptID";
            sql = string.Format(sql, objAdmin.userid, objAdmin.pwd, objAdmin.dept, dateTime);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objAdmin.id = Convert.ToInt32(objReader["id"].ToString());
                    objAdmin.deptid = Convert.ToInt32(objReader["deptid"].ToString());
                    objAdmin.dept = objReader["dept"].ToString();
                    objAdmin.userid = objReader["userid"].ToString();
                    objAdmin.username = objReader["username"].ToString();
                    objAdmin.Attendance = (bool)objReader["Attendance"];
                    objAdmin.Overtime = (bool)objReader["Overtime"];
                    objAdmin.Evaluation = (bool)objReader["Evaluation"];
                    objAdmin.Assessment = (bool)objReader["Assessment"];
                    objAdmin.开始日期 = (DateTime)objReader["开始日期"];
                    objAdmin.结束日期 = (DateTime)objReader["结束日期"];
                    objAdmin.备注 = objReader["备注"].ToString();
                    objAdmin.更改者 = objReader["更改者"].ToString();
                    objAdmin.更改日期 = (DateTime)objReader["更改日期"];
                }
                else
                {
                    objAdmin = null;
                }
                objReader.Close();
                return objAdmin;
            }
            catch (Exception ex)
            {
                throw new Exception("数据访问发生异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据登录账号修改登录密码
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public int ModifyPwd(string newPwd, string loginId, string userName, DateTime dateTime)
        {
            string sql = "update tbl_user set pwd = '{0}',更改者 = '{1}',更改日期 = '{2}' where userid = '{3}'";
            sql = string.Format(sql, newPwd, userName, dateTime, loginId);
            return SQLHelper.Update(sql);
        }




        /// <summary>
        /// 获取所有的登录部门对象
        /// </summary>
        /// <returns></returns>
        public List<Admin> GetAllDepts()
        {
            string sql = "select distinct deptid,dept from tbl_user";
            sql += " order by deptid";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Admin> list = new List<Admin>();
            while (objReader.Read())
            {
                list.Add(new Admin()
                {
                    dept = objReader["dept"].ToString(),
                    deptid = (int)objReader["deptid"]
                });
            }
            objReader.Close();
            return list;
        }





    }
}
