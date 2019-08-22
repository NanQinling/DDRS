using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DAL;

namespace DDRS
{
    public partial class FrmModifyPwd : Form
    {
        private AdminService objAdminService = new AdminService();

        public FrmModifyPwd()
        {
            InitializeComponent();
        }
        //修改密码
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //校验用户输入的旧密码是否正确
            if (this.txtOldPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入原密码！", "提示");
                this.txtOldPwd.Focus();
                return;
            }
            //判断原密码是否正确
            if (this.txtOldPwd.Text.Trim() != Program.currentAdmin.pwd)
            {
                MessageBox.Show("原密码不正确！", "提示");
                this.txtOldPwd.Focus();
                this.txtOldPwd.SelectAll();
                return;
            }

            //校验用户输入的新密码是否正确
            if (this.txtNewPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入新密码！", "提示");
                this.txtNewPwd.Focus();
                return;
            }
            if (this.txtNewPwd.Text.Trim().Length < 6 || this.txtNewPwd.Text.Trim().Length > 18)
            {
                MessageBox.Show("密码长度必须在6-18位之间！", "提示");
                this.txtNewPwd.Focus();
                this.txtNewPwd.SelectAll();
            }

            if (this.txtNewPwd.Text.Trim() != this.txtNewPwdConfirm.Text.Trim())
            {
                MessageBox.Show("两次输入的密码不一致！", "提示");
                this.txtNewPwd.Focus();
                return;
            }

            //将新密码提交到数据库
            int result = objAdminService.ModifyPwd(Program.currentAdmin.userid, this.txtNewPwd.Text.Trim());
            if (result >= 1)
            {
                MessageBox.Show("新密码修改成功，请妥善保存！", "提示");
                Program.currentAdmin.pwd = this.txtNewPwdConfirm.Text.Trim();
                this.Close();
            }





        }
    }
}
