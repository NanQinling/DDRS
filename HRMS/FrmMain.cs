using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace DDRS
{
    public partial class FrmMain : Form
    {
        private KaoQinService objKaoQinService = new DAL.KaoQinService();//创建数据访问类对象
        private JiaBanService objJiaBanService = new DAL.JiaBanService();//创建数据访问类对象
        private KaoPingService objKaoPingService = new DAL.KaoPingService();//创建数据访问类对象
        private AdminService objAdminService = new DAL.AdminService();//创建数据访问类对象

        private LoginLogService objLoginLogService = new DAL.LoginLogService();//创建数据访问类对象

        public FrmMain()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;    //必加，不加也不会实现，设定工作区，不包括任务栏

            this.toolStripStatusLabel2.Text = Program.currentAdmin.dept;
            this.toolStripStatusLabel5.Text = Program.currentAdmin.username;
            this.toolStripStatusLabel8.Text = Program.salaryDate.current_year_month.Substring(0, 4) + "年" + Program.salaryDate.current_year_month.Substring(4, 2) + "月";
            this.toolStripStatusLabel12.Text = Program.version;

            //JudgmentAuthority();//判断权限


        }





        ///// <summary>
        ///// 创建一个方法，用于让子窗口修改父窗口的控件
        ///// </summary>
        ///// <param name="err"></param>
        //public void SetToolStripErr(string err)
        //{
        //    toolStripStatusLabel4.Visible = true;
        //    toolStripStatusLabel4.Text = err;
        //}


        /// <summary>
        /// 打开窗体方法
        /// </summary>
        /// <param name="objForm"></param>
        private void OpenForm(Form objForm)
        {
            objForm.TopLevel = false;//将当前子窗体设置成非顶级控件
            objForm.WindowState = FormWindowState.Maximized;//设置窗体最大化
            objForm.FormBorderStyle = FormBorderStyle.None;//去掉窗体边框
            objForm.Parent = this.panelForm;//指示当前子窗体显示的容器
            objForm.Show();
        }

        /// <summary>
        /// 关闭窗体方法
        /// </summary>
        private void CloseForm()
        {
            foreach (Control item in this.panelForm.Controls)
            {
                if (item is Form)
                {
                    Form objControl = (Form)item;
                    objControl.Close();
                    this.panelForm.Controls.Remove(item);
                }
            }
        }




        //根据登录用户检测操作权限
        private bool JudgmentAuthority(string strShiWuLeiXing)
        {
            //封装用户信息到用户对象
            Admin objAdmin = new Admin()
            {
                userid = Program.currentAdmin.userid,
                pwd = Program.currentAdmin.pwd,
                dept = Program.currentAdmin.dept,
            };

            //重新获取用户权限
            objAdmin = objAdminService.AdminLogin(objAdmin, Program.salaryDate.loginDate);
            if (objAdmin == null)
            {
                MessageBox.Show("登录账号或密码错误！", "提示");
            }
            else
            {
                //（1）保存用户信息到全局变量
                Program.currentAdmin = objAdmin; //保存用户对象
            }

            bool b = true;
            if (strShiWuLeiXing == "Attendance")
            {
                if (Program.currentAdmin.Attendance == false)
                {
                    b = false;
                }
            }

            if (strShiWuLeiXing == "Overtime")
            {
                if (Program.currentAdmin.Overtime == false)
                {
                    b = false;
                }
            }
            if (strShiWuLeiXing == "Assessment")
            {
                if (Program.currentAdmin.Assessment == false)
                {
                    b = false;
                }
            }
            if (strShiWuLeiXing == "Evaluation")
            {
                if (Program.currentAdmin.Evaluation == false)
                {
                    b = false;
                }
            }
            return b;
        }




        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                DateTime dt = objLoginLogService.GetDBServerTime();
                objLoginLogService.WriteExitLog(Program.currentLoginLog.LoginLogId, dt);
            }
        }

        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd objModyfyPwd = new FrmModifyPwd();
            objModyfyPwd.ShowDialog();

        }

        private void TsmiManageJiaBan_Click(object sender, EventArgs e)
        {
            if (JudgmentAuthority("OverTime") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseForm();
            FrmJiaBan objForm = new FrmJiaBan();
            this.OpenForm(objForm);
        }

        private void tsmiManageKaoQin_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("OverTime") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseForm();
            FrmKaoQin objForm = new FrmKaoQin();
            this.OpenForm(objForm);
        }

        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiImportAttendance_Click(object sender, EventArgs e)
        {
            if (JudgmentAuthority("Attendance") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseForm();
            FrmKaoQinImport objForm = new FrmKaoQinImport();
            this.OpenForm(objForm);
        }

        private void tsmiImportJiaBan_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("Attendance") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseForm();
            FrmJiaBanImport objForm = new FrmJiaBanImport();
            this.OpenForm(objForm);
        }

        private void tsmiExportPrintJiaBan_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("OverTime") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseForm();
            DataTable dt_export = objJiaBanService.ExportJiaBanPrint(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            var filePath = string.Empty;
            //判断dt_export是否为空
            if (dt_export.Rows.Count != 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel文件|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    ExcelHelper excelHelper = new ExcelHelper(filePath);
                    excelHelper.DataTableToExcel_JiaBan(dt_export, "Sheet1", true, 3, true);
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsmiExportPrintKaoQin_Click(object sender, EventArgs e)
        {
            if (JudgmentAuthority("Attendance") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (objKaoQinService.GetNotSubmitKaoQinRenShu(Program.salaryDate.last_year_month, Program.currentAdmin.dept) > 0)
            {
                MessageBox.Show("存在未保存数据，请检查修改后再打印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CloseForm();
            DataTable dt_export = objKaoQinService.ExportKaoQinPrint(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            var filePath = string.Empty;
            //判断dt_export是否为空
            if (dt_export.Rows.Count != 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel文件|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    ExcelHelper excelHelper = new ExcelHelper(filePath);
                    excelHelper.DataTableToExcel_KaoQin(dt_export, "Sheet1", true, 4, true);
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 维护考评数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("Evaluation") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CloseForm();
            FrmKaoPing objForm = new FrmKaoPing();
            this.OpenForm(objForm);
        }

        private void 批量导入考评数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("Evaluation") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CloseForm();
            FrmKaoPingImport objForm = new FrmKaoPingImport();
            this.OpenForm(objForm);
        }

        private void 导出并打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (JudgmentAuthority("Evaluation") == false)
            {
                MessageBox.Show("您无权操作该事务，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (objKaoPingService.GetNotSubmitKaoPingRenShu(Program.salaryDate.last_year_month, Program.currentAdmin.dept) > 0)
            {
                MessageBox.Show("存在未考评数据，请检查修改后再打印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            CloseForm();
            DataTable dt_export = objKaoPingService.ExportKaoPingPrint(Program.salaryDate.last_year_month, Program.currentAdmin.dept, Convert.ToDouble(objKaoPingService.GetGetCurrentKaoPingYouLiangShu(Program.salaryDate.last_year_month, Program.currentAdmin.dept)) / Convert.ToDouble(objKaoPingService.GetCurrentKaoQinRenShu(Program.salaryDate.last_year_month, Program.currentAdmin.dept)));
            var filePath = string.Empty;
            //判断dt_export是否为空
            if (dt_export.Rows.Count != 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel文件|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    ExcelHelper excelHelper = new ExcelHelper(filePath);
                    excelHelper.DataTableToExcel_KaoPing(dt_export, "Sheet1", true, 3, true);
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
