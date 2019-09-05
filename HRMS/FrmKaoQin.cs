using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace DDRS
{
    public partial class FrmKaoQin : Form
    {
        private KaoQinService objKaoQinService = new DAL.KaoQinService();//创建数据访问类对象

        /// <summary>
        /// 加班表格初始化
        /// </summary>
        private void init_dgvKaoQin()
        {
            List<KaoQin> list = objKaoQinService.GetKaoQinByDept(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            SetDgvKaoQinFormat(list);
        }

        /// <summary>
        /// 设置表格显示格式
        /// </summary>
        /// <param name="list"></param>
        private void SetDgvKaoQinFormat(List<KaoQin> list)
        {
            this.dgvKaoQin.DataSource = list;
            this.dgvKaoQin.AllowUserToAddRows = false;
            this.dgvKaoQin.AllowUserToDeleteRows = false;
            this.dgvKaoQin.ReadOnly = true;
            this.dgvKaoQin.MultiSelect = false;
            this.dgvKaoQin.Columns[3].Frozen = true;

            //禁止 DataGridView 点击 列标题 排序
            for (int i = 0; i < this.dgvKaoQin.Columns.Count; i++)
            {
                dgvKaoQin.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //隐藏不必要的列
            for (int i = 0; i < dgvKaoQin.Columns.Count; i++)
            {
                if (i == 0 || i == dgvKaoQin.Columns.Count - 1 || i == dgvKaoQin.Columns.Count - 2 || i == dgvKaoQin.Columns.Count - 3 || i == dgvKaoQin.Columns.Count - 4)
                {
                    dgvKaoQin.Columns[i].Visible = false;
                }
                if (dgvKaoQin.Columns[i].Name == "部门")
                {
                    dgvKaoQin.Columns[i].Visible = false;
                }
            }
            //调整列宽
            for (int i = 0; i < dgvKaoQin.Columns.Count; i++)
            {
                if (i == 1 || i == 2 || i == 3 || i == 25)
                {
                    dgvKaoQin.Columns[i].Width = 80;
                }

                else if (i == 0 || (i >= 4 && i < 25))
                {
                    dgvKaoQin.Columns[i].Width = 40;
                }
            }

        }

        /// <summary>
        /// 清空当前文本框
        /// </summary>
        private void init_gbKaoQin()
        {
            foreach (Control item in this.gbKaoQin.Controls)
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
        private KaoQin FengZhuangDuiXiang()
        {
            KaoQin objKaoQin = new KaoQin()
            {
                考勤年月 = Program.salaryDate.last_year_month,
                部门 = Program.currentAdmin.dept,
                班组 = this.txtBanZu.Text.Trim(),
                人员编号 = this.txtHnbh.Text.Trim(),
                姓名 = this.txtName.Text.Trim(),
                应出勤 = double.Parse(this.txtYingChuQin.Text),
                实际出勤 = double.Parse(this.txtShiJiChuQin.Text),
                出差 = double.Parse(this.txtChuChai.Text),
                旷工 = double.Parse(this.txtKuangGong.Text),
                年假 = double.Parse(this.txtNianJia.Text),
                事假 = double.Parse(this.txtShiJia.Text),
                病假 = double.Parse(this.txtBingJia.Text),
                正常调休 = double.Parse(this.txtZhengChangTiaoXiu.Text),
                产假 = double.Parse(this.txtChanJia.Text),
                陪产假 = double.Parse(this.txtPeiChanJia.Text),
                婚假 = double.Parse(this.txtHunJian.Text),
                丧假 = double.Parse(this.txtSangJia.Text),
                迟到早退次数 = double.Parse(this.txtChiDaoZaoTuiCiShu.Text),
                缺卡次数 = double.Parse(this.txtQueKaCiShu.Text),
                工作日加班次数 = double.Parse(this.txtGongZuoRiJiaBanCiShu.Text),
                休息日加班 = double.Parse(this.txtXiuXiRiJiaBan.Text),
                节假日加班 = double.Parse(this.txtJieJiaRiJiaBan.Text),
                休息日出差 = double.Parse(this.txtXiuXiRiChuChai.Text),
                夜间值班次数 = double.Parse(this.txtYeJianZhiBanCiShu.Text),
                夜间值班调休次数 = double.Parse(this.txtYeJianZhiBanTiaoXiuCiShu.Text),
                打卡签到次数 = double.Parse(this.txtDaKaQianDaoCiShu.Text),
                工作时长 = double.Parse(this.txtGongZuoShiChang.Text),
                备注 = this.txtBeiZhu.Text.Trim(),
                更改者 = Program.currentAdmin.username,
                更改日期 = DateTime.Now,
                IsSubmit = true

            };
            return objKaoQin;
        }



        public FrmKaoQin()
        {
            InitializeComponent();
            init_dgvKaoQin();
            #region 将txtbox控件同时添加一个KeyPress事件
            this.txtYingChuQin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtShiJiChuQin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtChuChai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtKuangGong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtNianJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtShiJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtBingJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtZhengChangTiaoXiu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtChanJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtPeiChanJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtHunJian.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtSangJia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtChiDaoZaoTuiCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtQueKaCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtGongZuoRiJiaBanCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtXiuXiRiJiaBan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtJieJiaRiJiaBan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtXiuXiRiChuChai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtYeJianZhiBanCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtYeJianZhiBanTiaoXiuCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtDaKaQianDaoCiShu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtGongZuoShiChang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            #endregion
            this.txtBeiZhu.MaxLength = 30;//设置备注txtBox最大字符为30
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 验证表单输入数据

            if (this.txtYingChuQin.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入应出勤天数！", "提示");
                return;
            }
            if (this.txtShiJiChuQin.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入实际出勤天数！", "提示");
                return;
            }
            if (this.txtChuChai.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入出差天数！", "提示");
                return;
            }
            if (this.txtKuangGong.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入旷工天数！", "提示");
                return;
            }
            if (this.txtNianJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入年假天数！", "提示");
                return;
            }
            if (this.txtShiJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入事假天数！", "提示");
                return;
            }
            if (this.txtBingJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入病假天数！", "提示");
                return;
            }
            if (this.txtZhengChangTiaoXiu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入正常调休天数！", "提示");
                return;
            }
            if (this.txtChanJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入产假天数！", "提示");
                return;
            }
            if (this.txtPeiChanJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入陪产假天数！", "提示");
                return;
            }
            if (this.txtHunJian.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入婚假天数！", "提示");
                return;
            }
            if (this.txtSangJia.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入丧假天数！", "提示");
                return;
            }
            if (this.txtChiDaoZaoTuiCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入迟到早退次数！", "提示");
                return;
            }
            if (this.txtQueKaCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入缺卡次数！", "提示");
                return;
            }
            if (this.txtGongZuoRiJiaBanCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入工作日加班次数！", "提示");
                return;
            }
            if (this.txtXiuXiRiChuChai.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入休息日加班天数！", "提示");
                return;
            }
            if (this.txtJieJiaRiJiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入节假日加班天数！", "提示");
                return;
            }
            if (this.txtXiuXiRiChuChai.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入休息日出差天数！", "提示");
                return;
            }
            if (this.txtYeJianZhiBanCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入夜间值班次数！", "提示");
                return;
            }
            if (this.txtYeJianZhiBanTiaoXiuCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入夜间值班调休次数！", "提示");
                return;
            }
            if (this.txtDaKaQianDaoCiShu.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入打卡签到次数！", "提示");
                return;
            }
            if (this.txtGongZuoShiChang.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入工作时长！", "提示");
                return;
            }
            if (this.txtBeiZhu.Text.Trim().Length > 30)
            {
                MessageBox.Show("备注信息不能超过20个字符，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtBeiZhu.Focus();
                return;
            }
            #endregion

            #region 封装对象
            KaoQin objKaoQin = FengZhuangDuiXiang();
            #endregion

            #region 考勤表间数据校验
            string err = objKaoQinService.CheckKaoQin(objKaoQin);
            if (err.Length > 0)
            {
                //((FrmMain)ParentForm).SetToolStripErr(err);       //子窗口修改父窗口的控件，对应frmMain表单中的SetToolStripErr方法。
                txterr.Text = err;
                MessageBox.Show("数据有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region 修改对象
            try
            {
                if (objKaoQinService.ModifyKaoQin(objKaoQin) == 1)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    init_dgvKaoQin();
                    init_gbKaoQin();
                    txterr.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion 

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKaoQin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            #region 点击表格中的一行时，把内容送到编辑区
            string userid = this.dgvKaoQin.CurrentRow.Cells["人员编号"].Value.ToString();
            KaoQin objKaoQin = objKaoQinService.GetKaoQinByUserId(Program.salaryDate.last_year_month, userid);
            this.txtBuMen.Text = objKaoQin.部门.ToString();
            this.txtBanZu.Text = objKaoQin.班组.ToString();
            this.txtHnbh.Text = objKaoQin.人员编号.ToString();
            this.txtName.Text = objKaoQin.姓名.ToString();
            this.txtYingChuQin.Text = objKaoQin.应出勤.ToString();
            this.txtShiJiChuQin.Text = objKaoQin.实际出勤.ToString();
            this.txtChuChai.Text = objKaoQin.出差.ToString();
            this.txtKuangGong.Text = objKaoQin.旷工.ToString();
            this.txtNianJia.Text = objKaoQin.年假.ToString();
            this.txtShiJia.Text = objKaoQin.事假.ToString();
            this.txtBingJia.Text = objKaoQin.病假.ToString();
            this.txtZhengChangTiaoXiu.Text = objKaoQin.正常调休.ToString();
            this.txtChanJia.Text = objKaoQin.产假.ToString();
            this.txtPeiChanJia.Text = objKaoQin.陪产假.ToString();
            this.txtHunJian.Text = objKaoQin.婚假.ToString();
            this.txtSangJia.Text = objKaoQin.丧假.ToString();
            this.txtChiDaoZaoTuiCiShu.Text = objKaoQin.迟到早退次数.ToString();
            this.txtQueKaCiShu.Text = objKaoQin.缺卡次数.ToString();
            this.txtGongZuoRiJiaBanCiShu.Text = objKaoQin.工作日加班次数.ToString();
            this.txtXiuXiRiJiaBan.Text = objKaoQin.休息日加班.ToString();
            this.txtJieJiaRiJiaBan.Text = objKaoQin.节假日加班.ToString();
            this.txtXiuXiRiChuChai.Text = objKaoQin.休息日出差.ToString();
            this.txtYeJianZhiBanCiShu.Text = objKaoQin.夜间值班次数.ToString();
            this.txtYeJianZhiBanTiaoXiuCiShu.Text = objKaoQin.夜间值班调休次数.ToString();
            this.txtDaKaQianDaoCiShu.Text = objKaoQin.打卡签到次数.ToString();
            this.txtGongZuoShiChang.Text = objKaoQin.工作时长.ToString();
            this.txtBeiZhu.Text = objKaoQin.备注.ToString();
            #endregion

        }

        #region 验证txtBox输入

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataValidate.NumberDotTextbox_KeyPress(sender, e) == true)
            {
                e.Handled = true;
            }
        }

        #endregion

        private void DgvKaoQin_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvKaoQin, e);
        }

        private void BtnNotSubmit_Click(object sender, EventArgs e)
        {
            List<KaoQin> list = objKaoQinService.GetNotSubmitKaoQin(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            SetDgvKaoQinFormat(list);
        }
    }
}
