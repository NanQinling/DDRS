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

        public FrmMain()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;    //必加，不加也不会实现，设定工作区，不包括任务栏

            this.toolStripStatusLabel2.Text = Program.currentAdmin.dept;
            this.toolStripStatusLabel5.Text = Program.currentAdmin.username;
            this.toolStripStatusLabel8.Text = Program.salaryDate.current_year_month.Substring(0, 4) + "年" + Program.salaryDate.current_year_month.Substring(4, 2) + "月";
            this.toolStripStatusLabel12.Text = Program.version;

            JudgmentAuthority();//判断权限


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
        private void JudgmentAuthority()
        {
            if (Program.currentAdmin.attendance == true)
            {
                this.考勤管理ToolStripMenuItem.Enabled = true;
            }
            if (Program.currentAdmin.overtime == true)
            {
                this.加班管理JToolStripMenuItem.Enabled = true;
            }
            if (Program.currentAdmin.assessment == false)
            {

            }
            if (Program.currentAdmin.evaluation == false)
            {

            }

        }




        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd objModyfyPwd = new FrmModifyPwd();
            objModyfyPwd.ShowDialog();

        }

        private void TsmiManageJiaBan_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmJiaBan objForm = new FrmJiaBan();
            this.OpenForm(objForm);
        }

        private void tsmiManageKaoQin_Click(object sender, EventArgs e)
        {
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
            CloseForm();
            FrmKaoQinImport objForm = new FrmKaoQinImport();
            this.OpenForm(objForm);
        }

        private void tsmiImportJiaBan_Click(object sender, EventArgs e)
        {
            CloseForm();
            FrmJiaBanImport objForm = new FrmJiaBanImport();
            this.OpenForm(objForm);
        }

        private void tsmiExportPrintJiaBan_Click(object sender, EventArgs e)
        {
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


    }
}
