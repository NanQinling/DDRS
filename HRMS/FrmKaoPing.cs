using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Models;

namespace DDRS
{
    public partial class FrmKaoPing : Form
    {
        private KaoPingService objKaoPingService = new DAL.KaoPingService();//创建数据访问类对象
        private MoveItemService objMoveItemService = new DAL.MoveItemService();

        /// <summary>
        /// 考评表格初始化
        /// </summary>
        private void init_dgvKaoPing()
        {
            int nKaoPingRenShu = objKaoPingService.GetCurrentKaoPingRenShu(Program.salaryDate.last_year_month, Program.currentAdmin.dept);

            if (nKaoPingRenShu == 0)
            {
                objKaoPingService.CreatKaoPingByKaoPing(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            }

            List<KaoPing> list = objKaoPingService.GetKaoPingByDept(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            SetDgvKaoPingFormat(list);
        }


        /// <summary>
        /// 设置表格显示格式
        /// </summary>
        /// <param name="list"></param>
        private void SetDgvKaoPingFormat(List<KaoPing> list)
        {
            this.dgvKaoPing.DataSource = list;
            this.dgvKaoPing.AllowUserToAddRows = false;
            this.dgvKaoPing.AllowUserToDeleteRows = false;
            this.dgvKaoPing.ReadOnly = true;
            this.dgvKaoPing.MultiSelect = false;
            this.dgvKaoPing.Columns[3].Frozen = true;
            this.dgvKaoPing.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //设置自动换行显示
            this.dgvKaoPing.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvKaoPing.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //禁止 DataGridView 点击 列标题 排序
            for (int i = 0; i < this.dgvKaoPing.Columns.Count; i++)
            {
                dgvKaoPing.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //隐藏不必要的列
            for (int i = 0; i < dgvKaoPing.Columns.Count; i++)
            {
                if (dgvKaoPing.Columns[i].Name == "id" || dgvKaoPing.Columns[i].Name == "部门" || dgvKaoPing.Columns[i].Name == "更改者" || dgvKaoPing.Columns[i].Name == "更改日期" || dgvKaoPing.Columns[i].Name == "IsSubmit" || dgvKaoPing.Columns[i].Name == "排序")
                {
                    dgvKaoPing.Columns[i].Visible = false;
                }
            }

            //调整列宽
            for (int i = 0; i < dgvKaoPing.Columns.Count; i++)
            {
                if (dgvKaoPing.Columns[i].Name == "序号" || dgvKaoPing.Columns[i].Name == "德" || dgvKaoPing.Columns[i].Name == "能" || dgvKaoPing.Columns[i].Name == "勤" || dgvKaoPing.Columns[i].Name == "绩")
                {
                    dgvKaoPing.Columns[i].Width = 40;
                }
                else if (dgvKaoPing.Columns[i].Name == "考评年月" || dgvKaoPing.Columns[i].Name == "人员编号" || dgvKaoPing.Columns[i].Name == "姓名" || dgvKaoPing.Columns[i].Name == "考评意见" || dgvKaoPing.Columns[i].Name == "考评得分")
                {
                    dgvKaoPing.Columns[i].Width = 80;
                }
                else if (dgvKaoPing.Columns[i].Name == "部门")
                {
                    dgvKaoPing.Columns[i].Width = 120;
                }
                else if (dgvKaoPing.Columns[i].Name == "具体解释说明")
                {
                    dgvKaoPing.Columns[i].Width = 500;
                }
                else if (dgvKaoPing.Columns[i].Name == "备注")
                {
                    dgvKaoPing.Columns[i].Width = 180;
                }
            }

        }





        /// <summary>
        /// 清空当前文本框
        /// </summary>
        private void init_gbKaoPing()
        {
            foreach (Control item in this.gbKaoPing.Controls)
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
        private KaoPing FengZhuangDuiXiang()
        {
            KaoPing objKaoPing = new KaoPing()
            {
                考评年月 = Program.salaryDate.last_year_month,
                部门 = Program.currentAdmin.dept,
                //班组 = this.txtBanZu.Text.Trim(),
                人员编号 = this.txtHnbh.Text.Trim(),
                姓名 = this.txtName.Text.Trim(),
                德 = Convert.ToDouble(this.txtDe.Text),
                能 = Convert.ToDouble(this.txtNeng.Text),
                勤 = Convert.ToDouble(this.txtQin.Text),
                绩 = Convert.ToDouble(this.txtJi.Text),
                考评意见 = this.txtKaoPingYiJian.Text.Trim(),
                考评得分 = Convert.ToDouble(this.txtDe.Text) + Convert.ToDouble(this.txtNeng.Text) + Convert.ToDouble(this.txtQin.Text) + Convert.ToDouble(this.txtJi.Text),
                具体解释说明 = this.txtJieShiShuoMing.Text.Trim(),
                备注 = this.txtBeiZhu.Text.Trim(),
                更改者 = Program.currentAdmin.username,
                更改日期 = DateTime.Now,
                IsSubmit = true,

            };
            return objKaoPing;
        }




        public FrmKaoPing()
        {
            InitializeComponent();
            init_dgvKaoPing();

            #region 将txtbox控件同时添加一个KeyPress事件

            this.txtDe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtNeng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtQin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            this.txtJi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);

            #endregion

            #region 将txtbox控件同时添加一个LostFocus事件

            this.txtDe.LostFocus += txtBox_LostFocus;
            this.txtDe.LostFocus += txtBox_LostFocus;
            this.txtNeng.LostFocus += txtBox_LostFocus;
            this.txtQin.LostFocus += txtBox_LostFocus;
            this.txtJi.LostFocus += txtBox_LostFocus;

            #endregion

        }


        #region 离开txt焦点时自动计算总分

        private void txtBox_LostFocus(object sender, EventArgs e)
        {
            UpdateScoreAndYiJian();
        }

        #endregion

        #region 验证txtBox输入

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataValidate.NumberDotTextbox_KeyPress(sender, e) == true)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 离开txt焦点时自动计算总分，并生成考评意见。

        private void UpdateScoreAndYiJian()
        {
            if (this.txtDe.Text.Trim().Length == 0)
            {
                this.txtDe.Text = "0";
            }
            if (this.txtNeng.Text.Trim().Length == 0)
            {
                this.txtNeng.Text = "0";
            }
            if (this.txtQin.Text.Trim().Length == 0)
            {
                this.txtQin.Text = "0";
            }
            if (this.txtJi.Text.Trim().Length == 0)
            {
                this.txtJi.Text = "0";
            }

            this.txtKaoPingDeFen.Text = (Convert.ToDouble(this.txtDe.Text) + Convert.ToDouble(this.txtNeng.Text) + Convert.ToDouble(this.txtQin.Text) + Convert.ToDouble(this.txtJi.Text)).ToString();
            if (Convert.ToDouble(this.txtKaoPingDeFen.Text) >= 80)
            {
                this.txtKaoPingYiJian.Text = "优良";
            }
            else if (Convert.ToDouble(this.txtKaoPingDeFen.Text) >= 60)
            {
                this.txtKaoPingYiJian.Text = "合格";
            }
            else
            {
                this.txtKaoPingYiJian.Text = "不合格";
            }
        }

        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 验证表单输入数据

            if (this.txtDe.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入【德】分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.txtNeng.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入【能】分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.txtQin.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入【勤】分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.txtJi.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填入【绩】分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToDouble(this.txtDe.Text.Trim()) > 25 || Convert.ToDouble(this.txtNeng.Text.Trim()) > 25 || Convert.ToDouble(this.txtQin.Text.Trim()) > 25 || Convert.ToDouble(this.txtJi.Text.Trim()) > 25)
            {
                MessageBox.Show("单项得分不得大于25分，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToDouble(this.txtDe.Text.Trim()) < 0 || Convert.ToDouble(this.txtNeng.Text.Trim()) < 0 || Convert.ToDouble(this.txtQin.Text.Trim()) < 0 || Convert.ToDouble(this.txtJi.Text.Trim()) < 0)
            {
                MessageBox.Show("单项得分不得小于0分，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.txtJieShiShuoMing.Text.Trim().Length > 200)
            {
                MessageBox.Show("具体解释说明不能超过200个字符，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtJieShiShuoMing.Focus();
                return;
            }
            if (Convert.ToDouble(this.txtKaoPingDeFen.Text.Trim()) >= 80 && this.txtJieShiShuoMing.Text.Trim().Length == 0)
            {
                MessageBox.Show("优良人员请填写具体解释说明！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtJieShiShuoMing.Focus();
                return;
            }
            if (Convert.ToDouble(this.txtKaoPingDeFen.Text.Trim()) < 60 && this.txtJieShiShuoMing.Text.Trim().Length == 0)
            {
                MessageBox.Show("不合格人员请填写具体解释说明！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtJieShiShuoMing.Focus();
                return;
            }
            if (this.txtBeiZhu.Text.Trim().Length > 30)
            {
                MessageBox.Show("备注信息不能超过30个字符，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtBeiZhu.Focus();
                return;
            }


            string[] strScore = { this.txtDe.Text.Trim(), this.txtNeng.Text.Trim(), this.txtQin.Text.Trim(), this.txtJi.Text.Trim() };
            double dYouLiangLv = objKaoPingService.GetCurrentYouLiangLv(Program.salaryDate.last_year_month, Program.currentAdmin.dept, strScore, this.txtHnbh.Text.Trim());

             if (dYouLiangLv > 0.2)
            {
                MessageBox.Show("优良人数不能超过部门人数的20%，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            #endregion

            #region 封装对象
            KaoPing objKaoPing = FengZhuangDuiXiang();
            #endregion

            #region 修改对象
            try
            {
                if (objKaoPingService.ModifyKaoPing(objKaoPing, Program.currentAdmin.dept) == 1)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    init_dgvKaoPing();
                    init_gbKaoPing();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion 
        }

        private void dgvKaoPing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvKaoPing.SelectedRows.Count == 0)
            {
                return;
            }
            #region 点击表格中的一行时，把内容送到编辑区
            string userid = this.dgvKaoPing.CurrentRow.Cells["人员编号"].Value.ToString();
            KaoPing objKaoPing = objKaoPingService.GetKaoPingByUserId(Program.salaryDate.last_year_month, userid);
            this.txtBuMen.Text = objKaoPing.部门.ToString();
            //this.txtBanZu.Text = objKaoPing.班组.ToString();
            this.txtHnbh.Text = objKaoPing.人员编号.ToString();
            this.txtName.Text = objKaoPing.姓名.ToString();
            this.txtDe.Text = objKaoPing.德.ToString();
            this.txtNeng.Text = objKaoPing.能.ToString();
            this.txtQin.Text = objKaoPing.勤.ToString();
            this.txtJi.Text = objKaoPing.绩.ToString();
            this.txtKaoPingDeFen.Text = objKaoPing.考评得分.ToString();
            this.txtKaoPingYiJian.Text = objKaoPing.考评意见.ToString();
            this.txtJieShiShuoMing.Text = objKaoPing.具体解释说明.ToString();
            this.txtBeiZhu.Text = objKaoPing.备注.ToString();
            #endregion
        }




        ////随机生成合格数
        //private void btnCreatRandom_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("自动生成合格数据会将所有人的得分覆盖，确认吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    if (result == DialogResult.Cancel)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        List<KaoPing> objKaoPing = objKaoPingService.GetKaoPingByDept(Program.salaryDate.last_year_month, Program.currentAdmin.dept);

        //        int importCount = 0;
        //        foreach (var item in objKaoPing)
        //        {
        //            #region 初始化对象

        //            Random rnd = new Random();
        //            int intRnd1 = rnd.Next(15, 20);
        //            int intRnd2 = rnd.Next(15, 20);
        //            int intRnd3 = rnd.Next(15, 20);
        //            int intRnd4 = rnd.Next(15, 20);
        //            item.德 = intRnd1;
        //            item.能 = intRnd2;
        //            item.勤 = intRnd3;
        //            item.绩 = intRnd4;
        //            item.考评得分 = item.德 + item.能 + item.勤 + item.绩;

        //            if (item.考评得分 >= 80)
        //            {
        //                item.考评意见 = "优良";
        //            }
        //            else if (item.考评得分 >= 60)
        //            {
        //                item.考评意见 = "合格";
        //            }
        //            else
        //            {
        //                item.考评意见 = "不合格";
        //            }
        //            item.具体解释说明 = "";
        //            item.备注 = "";
        //            item.更改者 = Program.currentAdmin.username;
        //            item.更改日期 = DateTime.Now;
        //            item.IsSubmit = false;

        //            #endregion

        //            #region 修改对象
        //            try
        //            {
        //                //导入数据
        //                if (objKaoPingService.ModifyKaoPing(item, Program.currentAdmin.dept) == 1)
        //                {
        //                    importCount++;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //            #endregion
        //        }
        //        MessageBox.Show($"{importCount.ToString()}条数据生成成功，请在此数据基础上进行修改后报送！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        init_dgvKaoPing();
        //    }
        //}


        private void btnMoveTop_Click(object sender, EventArgs e)
        {
            objMoveItemService.MoveDataGridViewX(dgvKaoPing, -2);

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            objMoveItemService.MoveDataGridViewX(dgvKaoPing, -1);

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            objMoveItemService.MoveDataGridViewX(dgvKaoPing, 1);

        }

        private void btnMoveBott_Click(object sender, EventArgs e)
        {
            objMoveItemService.MoveDataGridViewX(dgvKaoPing, 2);

        }

        private void btnMoveSave_Click(object sender, EventArgs e)
        {
            List<KaoPing> list = new List<KaoPing>();
            list = (List<KaoPing>)dgvKaoPing.DataSource;

            foreach (var item in list)
            {
                item.排序 = list.IndexOf(item);
                objMoveItemService.ModifySortID("imp_evaluation", "排序", item.排序, "id", item.id.ToString());
            }
            MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);

        }

        private void btnNotSubmit_Click(object sender, EventArgs e)
        {
            List<KaoPing> list = objKaoPingService.GetNotSubmitKaoPingRenYuan(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
            SetDgvKaoPingFormat(list);
        }
    }
}
