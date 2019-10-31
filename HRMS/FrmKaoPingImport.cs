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
    public partial class FrmKaoPingImport : Form
    {
        private KaoPingService objKaoPingService = new DAL.KaoPingService();//创建数据访问类对象

        private void init_dgvImport()
        {
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //禁止 DataGridView 点击 列标题 排序
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //调整列宽
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].Name == "序号" || dataGridView1.Columns[i].Name == "德" || dataGridView1.Columns[i].Name == "能" || dataGridView1.Columns[i].Name == "勤" || dataGridView1.Columns[i].Name == "绩")
                {
                    dataGridView1.Columns[i].Width = 40;
                }
                else if (dataGridView1.Columns[i].Name == "考评年月" || dataGridView1.Columns[i].Name == "人员编号" || dataGridView1.Columns[i].Name == "姓名" || dataGridView1.Columns[i].Name == "考评意见" || dataGridView1.Columns[i].Name == "考评得分")
                {
                    dataGridView1.Columns[i].Width = 80;
                }
                else if (dataGridView1.Columns[i].Name == "部门")
                {
                    dataGridView1.Columns[i].Width = 120;
                }
                else if (dataGridView1.Columns[i].Name == "具体解释说明")
                {
                    dataGridView1.Columns[i].Width = 500;
                }
                else if (dataGridView1.Columns[i].Name == "备注")
                {
                    dataGridView1.Columns[i].Width = 180;
                }
            }

            //设置优良人员行高
            DataTable dt_import = (DataTable)dataGridView1.DataSource;
            for (int i = 0; i < dt_import.Rows.Count; i++)
            {

                if (dt_import.Rows[i]["考评意见"].ToString() == "优良")
                {
                    dataGridView1.Rows[i].Height = 100;
                }
            }





            //前5列不能编辑
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].Name == "序号" || dataGridView1.Columns[i].Name == "考评年月" || dataGridView1.Columns[i].Name == "部门" || dataGridView1.Columns[i].Name == "人员编号" || dataGridView1.Columns[i].Name == "姓名")
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
            }

        }
        public FrmKaoPingImport()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt_export = objKaoPingService.ExportKaoPingByDept(Program.salaryDate.last_year_month, Program.currentAdmin.dept);
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
                    excelHelper.DataTableToExcel_KaoPing(dt_export, "Sheet1", true, 4, false);
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            dataGridView1.DataSource = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel文件|*.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName.Length > 0)
                {
                    textBox1.Text = openFileDialog.FileName;
                    filePath = openFileDialog.FileName;

                    ExcelHelper excelHelper = new ExcelHelper(filePath);
                    DataTable dt_import = excelHelper.ExcelToDataTable(filePath, true);

                    //导入Excel模板验证
                    if (!dt_import.Columns.Contains("考评年月"))
                    {
                        MessageBox.Show("要导入的Excel文件人员不是考评文件，请确认！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //给dt里面的空值赋值为0，否则遇到空值会报错。
                    for (int i = 0; i < dt_import.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt_import.Columns.Count - 1; j++)
                        {
                            if (dt_import.Rows[i]["德"].ToString() == "")
                            {
                                dt_import.Rows[i]["德"] = 0;
                            }
                            if (dt_import.Rows[i]["能"].ToString() == "")
                            {
                                dt_import.Rows[i]["能"] = 0;
                            }
                            if (dt_import.Rows[i]["勤"].ToString() == "")
                            {
                                dt_import.Rows[i]["勤"] = 0;
                            }
                            if (dt_import.Rows[i]["绩"].ToString() == "")
                            {
                                dt_import.Rows[i]["绩"] = 0;
                            }
                            if (dt_import.Rows[i]["考评得分"].ToString() == "")
                            {
                                dt_import.Rows[i]["考评得分"] = 0;
                            }
                        }
                    }

                    dataGridView1.DataSource = dt_import;
                    init_dgvImport();
                    btnImport.Enabled = true;
                }
            }
            else
            {
                return; //未选中文件
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("请选择要导入的表格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt_import = (DataTable)dataGridView1.DataSource;

            #region 校验导入的Excel数据是否为数值型以及备注字符长度大于30，有误数据标记为红色，通过数据恢复为白色。

            int IsWrong = 0;
            for (int i = 0; i < dt_import.Rows.Count; i++)
            {
                for (int j = 0; j < dt_import.Columns.Count - 1; j++)//判断是否为数值型
                {
                    if (dt_import.Columns[j].ColumnName == "德" || dt_import.Columns[j].ColumnName == "能" || dt_import.Columns[j].ColumnName == "勤" || dt_import.Columns[j].ColumnName == "绩" || dt_import.Columns[j].ColumnName == "考评得分")
                    {
                        bool b = double.TryParse(dt_import.Rows[i][j].ToString(), out double result);
                        if (b == false)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            IsWrong++;
                        }

                    }

                    //if (dt_import.Rows[i]["备注"].ToString().Trim().Length > 30)//判断字符长度是否大于30
                    //{
                    //    dataGridView1.Rows[i].Cells["备注"].Style.BackColor = Color.Red;
                    //    IsWrong++;
                    //}

                    //if (dt_import.Rows[i]["具体解释说明"].ToString().Trim().Length > 200)//判断字符长度是否大于200
                    //{
                    //    dataGridView1.Rows[i].Cells["具体解释说明"].Style.BackColor = Color.Red;
                    //    IsWrong++;
                    //}
                }
            }
            if (IsWrong > 0)
            {
                MessageBox.Show("数据格式应为数值型，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < dt_import.Rows.Count; i++)
                {
                    for (int j = 0; j < dt_import.Columns.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }

            }
            #endregion

            List<KaoPing> objKaoPing = objKaoPingService.DataTableToList(dt_import);

            string err = "";
            int importCount = 0;
            foreach (var item in objKaoPing)
            {

                item.更改者 = Program.currentAdmin.username;
                item.更改日期 = DateTime.Now;

                //考勤表间数据校验
                err = objKaoPingService.CheckKaoPing(item);
                if (err.Length > 0)
                {
                    txterr.Text = err;
                    MessageBox.Show("导入数据有误，请修改后重新提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //导入数据
                    if (objKaoPingService.ModifyKaoPing(item, Program.currentAdmin.dept) == 1)
                    {
                        importCount++;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.dataGridView1.DataSource = null;
            MessageBox.Show($"{importCount.ToString()}条数据导入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txterr.Text = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
