using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DDRS
{
    public partial class FrmJiaBan : Form
    {
        private JiaBanService objJiaBanService = new DAL.JiaBanService();//创建数据访问类对象

        /// <summary>
        /// 加班表格初始化
        /// </summary>
        private void init_dgvJiaBan()
        {
            List<JiaBan> list = objJiaBanService.GetJiaBanByDept(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            this.dgvJiaBan.DataSource = list;
            this.dgvJiaBan.AllowUserToAddRows = false;
            this.dgvJiaBan.AllowUserToDeleteRows = false;
            this.dgvJiaBan.ReadOnly = true;
            this.dgvJiaBan.MultiSelect = false;
            this.dgvJiaBan.Columns[3].Frozen = true;


            //禁止 DataGridView 点击 列标题 排序
            for (int i = 0; i < this.dgvJiaBan.Columns.Count; i++)
            {
                dgvJiaBan.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //隐藏不必要的列
            for (int i = 0; i < dgvJiaBan.Columns.Count; i++)
            {
                if (i == 0 || i == dgvJiaBan.Columns.Count - 1 || i == dgvJiaBan.Columns.Count - 2 || i == dgvJiaBan.Columns.Count - 3)
                {
                    dgvJiaBan.Columns[i].Visible = false;
                }
            }

            //调整列宽
            for (int i = 0; i < dgvJiaBan.Columns.Count; i++)
            {
                if (i == 1 || i == 2 || i == 3)
                {
                    dgvJiaBan.Columns[i].Width = 80;
                }

                else if (i == 11)
                {
                    dgvJiaBan.Columns[i].Width = 180;
                }

                else
                {
                    dgvJiaBan.Columns[i].Width = 120;
                }
            }

        }

        /// <summary>
        /// 清空当前文本框
        /// </summary>
        private void init_gbJiaBan()
        {
            foreach (Control item in this.gbJiaBan.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                else if (item is RadioButton)
                {
                    ((RadioButton)item).Checked = false;
                }
                else if (item is ComboBox)
                {
                    ((ComboBox)item).SelectedIndex = -1;//不选择
                }
            }
        }

        /// <summary>
        /// 封装对象
        /// </summary>
        /// <returns></returns>
        private JiaBan FengZhuangDuiXiang()
        {
            JiaBan objJiaBan = new JiaBan()
            {
                考勤年月 = Program.salaryDate.last_year_month,
                部门 = Program.currentAdmin.dept,
                人员编号 = this.txtHnbh.Text.Trim(),
                姓名 = this.txtName.Text.Trim(),
                工作日加班次数 = double.Parse(this.txtGongZuoRiJiaBanCiShu.Text),
                休息日加班 = double.Parse(this.txtXiuXiRiJiaBan.Text),
                节假日加班 = double.Parse(this.txtJieJiaRiJiaBan.Text),
                正常调休 = double.Parse(this.txtZhengChangTiaoXiu.Text),
                夜间值班次数 = double.Parse(this.txtYeJianZhiBanCiShu.Text),
                夜间值班调休次数 = double.Parse(this.txtYeJianZhiBanTiaoXiuCiShu.Text),
                金额 = double.Parse(this.txtMoney.Text),
                备注 = this.txtBeiZhu.Text.Trim(),
                更改者 = Program.currentAdmin.username,
                更改日期 = DateTime.Now
            };
            return objJiaBan;
        }

        public FrmJiaBan()
        {
            InitializeComponent();
            init_dgvJiaBan();
            this.txtBeiZhu.MaxLength = 30;//设置备注txtBox最大字符为30

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region 表单输入数据验证
            double number;
            if (double.TryParse(this.txtGongZuoRiJiaBanCiShu.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【工作日加班次数】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtGongZuoRiJiaBanCiShu.Focus();
                return;
            }
            if (double.TryParse(this.txtXiuXiRiJiaBan.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【休息日加班】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtXiuXiRiJiaBan.Focus();
                return;
            }
            if (double.TryParse(this.txtJieJiaRiJiaBan.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【节假日加班】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtJieJiaRiJiaBan.Focus();
                return;
            }
            if (double.TryParse(this.txtZhengChangTiaoXiu.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【正常调休】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtZhengChangTiaoXiu.Focus();
                return;
            }
            if (double.TryParse(this.txtYeJianZhiBanCiShu.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【夜间值班次数】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtYeJianZhiBanCiShu.Focus();
                return;
            }
            if (double.TryParse(this.txtYeJianZhiBanTiaoXiuCiShu.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【夜间值班调休次数】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtYeJianZhiBanTiaoXiuCiShu.Focus();
                return;
            }
            if (double.TryParse(this.txtMoney.Text.Trim(), out number) == false)
            {
                MessageBox.Show("【金额】数据格式有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtMoney.Focus();
                return;
            }
            if (this.txtBeiZhu.Text.Trim().Length > 20)
            {
                MessageBox.Show("备注信息不能超过20个字符，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtBeiZhu.Focus();
                return;
            }
            #endregion

            #region 封装对象
            JiaBan objJiaBan = FengZhuangDuiXiang();
            #endregion

            #region 和考勤表数据校验加班费

            JiaBan objJiaBanFromKaoQin = objJiaBanService.GetJiaBanByKaoQinUserId(Program.salaryDate.last_year_month, objJiaBan.人员编号);

            string err = objJiaBanService.CheckJiaBan(objJiaBan, objJiaBanFromKaoQin);
            if (err.Length > 0)
            {
                txterr.Text = err;
                MessageBox.Show("数据有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region 修改对象
            try
            {
                if (objJiaBan.金额 > 0)
                {
                    if (objJiaBanService.ModifyJiaBan(objJiaBan) == 1)
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        init_dgvJiaBan();
                        init_gbJiaBan();
                        txterr.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("金额为0时无需保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtMoney.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion 




        }


        private void dgvJiaBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            #region 点选表格中的一行时，把内容送到编辑区
            string userid = this.dgvJiaBan.CurrentRow.Cells["人员编号"].Value.ToString();
            JiaBan objJiaBan = objJiaBanService.GetJiaBanByUserId(Program.salaryDate.last_year_month, userid); //查询创建对象
            //给控件赋值
            this.txtBuMen.Text = objJiaBan.考勤年月.ToString();
            this.txtHnbh.Text = objJiaBan.人员编号.ToString();
            this.txtName.Text = objJiaBan.姓名.ToString();
            this.txtGongZuoRiJiaBanCiShu.Text = objJiaBan.工作日加班次数.ToString();
            this.txtXiuXiRiJiaBan.Text = objJiaBan.休息日加班.ToString();
            this.txtJieJiaRiJiaBan.Text = objJiaBan.节假日加班.ToString();
            this.txtZhengChangTiaoXiu.Text = objJiaBan.正常调休.ToString();
            this.txtYeJianZhiBanCiShu.Text = objJiaBan.夜间值班次数.ToString();
            this.txtYeJianZhiBanTiaoXiuCiShu.Text = objJiaBan.夜间值班调休次数.ToString();
            this.txtMoney.Text = objJiaBan.金额.ToString();
            this.txtBeiZhu.Text = objJiaBan.备注.ToString();
            #endregion
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataValidate.NumberDotTextbox_KeyPress(sender, e) == true)
            {
                e.Handled = true;
            }
        }

        private void DgvJiaBan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvJiaBan, e);
        }
    }
}
