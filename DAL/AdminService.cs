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
        public Admin AdminLogin(Admin objAdmin)
        {
            string sql = "select tbl_user.id,tbl_user.deptid,org_dept.机构简称 as dept,tbl_user.UserId,emp_bas.姓名 as username,tbl_user.attendance,tbl_user.overtime,tbl_user.evaluation,tbl_user.assessment from tbl_user";
            sql += " inner join org_dept on org_dept.机构编号 = tbl_user.deptid";
            sql += " inner join emp_bas on emp_bas.人员编号 = tbl_user.UserId";
            sql += " where userid={0} and pwd='{1}' and org_dept.机构简称='{2}'";
            sql += " order by tbl_user.DeptID";
            sql = string.Format(sql, objAdmin.userid, objAdmin.pwd, objAdmin.dept);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objAdmin.id = Convert.ToInt32(objReader["id"].ToString());
                    objAdmin.deptid = objReader["deptid"].ToString();
                    objAdmin.username = objReader["username"].ToString();
                    objAdmin.attendance = bool.Parse(objReader["attendance"].ToString());
                    objAdmin.overtime = bool.Parse(objReader["overtime"].ToString());
                    objAdmin.evaluation = bool.Parse(objReader["evaluation"].ToString());
                    objAdmin.assessment = bool.Parse(objReader["assessment"].ToString());
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
        public int ModifyPwd(String newPwd, string loginId)
        {
            string sql = "update tbl_user set pwd = '{0}' where userid = '{1}'";
            sql = string.Format(sql, newPwd, loginId);
            return SQLHelper.Update(sql);
        }




        /// <summary>
        /// 获取所有的登录部门对象
        /// </summary>
        /// <returns></returns>
        public List<Admin> GetAllDepts()
        {
            string sql = "select distinct deptid,org_dept.机构简称 as dept,org_dept.排序 from tbl_user";
            sql += " inner join org_dept on tbl_user.deptid = org_dept.机构编号";
            sql += " order by 排序";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Admin> list = new List<Admin>();
            while (objReader.Read())
            {
                list.Add(new Admin()
                {
                    dept = objReader["dept"].ToString(),
                    deptid = objReader["deptid"].ToString()
                });
            }
            objReader.Close();
            return list;
        }





    }
}
