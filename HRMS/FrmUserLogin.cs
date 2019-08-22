using DAL;
using Models;
using System;
using System.Deployment.Application;
using System.Windows.Forms;


namespace DDRS
{
    public partial class FrmUserLogin : Form
    {
        private AdminService objAdminService = new AdminService();//创建数据访问类对象
        private MyDateService objMyDateService = new DAL.MyDateService();//创建数据访问类对象


        public FrmUserLogin()
        {
            InitializeComponent();

            this.skinEngine1.SkinFile = "MSN.ssk";

            //初始化班级下拉框
            this.cboDept.DataSource = objAdminService.GetAllDepts();
            this.cboDept.DisplayMember = "dept";
            this.cboDept.ValueMember = "deptid";
            this.cboDept.SelectedIndex = 2;//默认选中

            this.txtLoginId.Text = "80118777";
            this.txtLoginPwd.Text = "1";

            Program.salaryDate = objMyDateService.GetDate(new MyDate()); //初始化工资日期

            //获取程序发布版本号，并赋值给全局变量
            //ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
            //Program.version = ad.CurrentVersion.ToString();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //数据验证
            if (this.txtLoginId.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录账号！", "提示");
                this.txtLoginId.Focus();
                return;
            }
            if (!DataValidate.IsInteger(this.txtLoginId.Text.Trim()))
            {
                MessageBox.Show("登录账号必须是正整数！", "登录提示");
                this.txtLoginId.Focus();
                this.txtLoginId.SelectAll();
                return;
            }
            if (this.txtLoginPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录密码！", "提示");
                this.txtLoginPwd.Focus();
                return;
            }
            //封装用户信息到用户对象
            Admin objAdmin = new Admin()
            {
                userid = this.txtLoginId.Text.Trim(),
                pwd = this.txtLoginPwd.Text.Trim(),
                dept = this.cboDept.Text.Trim()
            };
            try
            {
                //提交用户信息
                objAdmin = objAdminService.AdminLogin(objAdmin);
                if (objAdmin == null)
                {
                    MessageBox.Show("登录账号或密码错误！", "提示");
                }
                else
                {
                    Program.currentAdmin = objAdmin; //保存用户对象
                    this.DialogResult = DialogResult.OK;//设置登录成功信息提示
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "登录失败！");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //改进用户体验
        private void txtLoginId_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtLoginId.Text.Trim().Length == 0) return;
            if (e.KeyValue == 13)
            {
                this.txtLoginPwd.Focus();
            }
        }

        private void txtLoginPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtLoginPwd.Text.Trim().Length == 0) return;
            if (e.KeyValue == 13)
            {
                btnLogin_Click(null, null);
            }
        }

    }
}
