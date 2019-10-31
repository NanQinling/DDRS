using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class MoveItemService
    {

        /// <summary>
        /// 上下移动ListViewItem
        /// </summary>
        /// <param name="toListView">ListView对象</param>
        /// <param name="tnMoveStep">-2表示到首，-1表示上移，1表示下称，2表示到末</param>
        public void MoveListItemX(ListView toListView, int tnMoveStep)
        {
            if (toListView.SelectedItems.Count != 1)
            {
                return;
            }

            //得到当前选择的项
            ListViewItem objlistItem = toListView.SelectedItems[0];
            int intCurIndex = objlistItem.Index;
            int intNewIndex = 0;

            //取得新的位置
            if (tnMoveStep == -1)
            {
                intNewIndex = intCurIndex - 1;
            }
            else if (tnMoveStep == 1)
            {
                intNewIndex = intCurIndex + 1;
            }
            else if (tnMoveStep < -1)
            {
                intNewIndex = 0;
            }
            else if (tnMoveStep > 1)
            {
                intNewIndex = toListView.Items.Count - 1;
            }

            if (intNewIndex < 0)
            {
                intNewIndex = 0;
            }
            if (intNewIndex > toListView.Items.Count - 1)
            {
                intNewIndex = toListView.Items.Count - 1;
            }

            //复制数据项
            ListViewItem objNewItem = (ListViewItem)objlistItem.Clone();
            //移除原先项
            toListView.Items.Remove(objlistItem);
            //插入新的项
            toListView.Items.Insert(intNewIndex, objNewItem);
            objNewItem.Selected = true;
            objNewItem.EnsureVisible();
        }




        /// <summary>
        /// 按照ListView排序对数据表进行排序更新
        /// </summary>
        /// <param name="strTableName">要更新的表名称</param>
        /// <param name="strOderColumn">排序的字段名称</param>
        /// <param name="intNewIndex">新的序号</param>
        /// <param name="strByColumn">建立联系的字段名</param>
        /// <param name="strByID">建立联系的字段值</param>
        public void ModifySortID(string strTableName, string strOderColumn, int intNewIndex, string strByColumn, string strByID)
        {
            //1、编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update {0} set {1} = {2}");
            sqlBuilder.Append(" where {3} = {4}");
            //2、解析对象
            string sql = string.Format(sqlBuilder.ToString(), strTableName, strOderColumn, intNewIndex, strByColumn, strByID);
            //3、执行SQL语句，返回结果
            SQLHelper.Update(sql);
        }





        /// <summary>
        /// 上下移动DataGridView，要使用SelectedRows[0] ,就必须设置这个属性:dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        /// </summary>
        /// <param name="toDataGridView"></param>
        /// <param name="tnMoveStep">-2表示到首，-1表示上移，1表示下称，2表示到末</param>
        public void MoveDataGridViewX(DataGridView toDataGridView, int tnMoveStep)
        {
            if (toDataGridView.SelectedRows.Count != 1)
            {
                return;
            }

            int rowIndex = toDataGridView.SelectedRows[0].Index;  //得到当前选中行的索引

            List<string> list = new List<string>();
            for (int i = 0; i < toDataGridView.Columns.Count; i++)
            {
                list.Add(toDataGridView.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中 
            }

            //取得新的位置
            if (tnMoveStep == -1)
            {
                if (rowIndex == 0)  //判断是否到顶行
                {
                    return;
                }

                for (int j = 0; j < toDataGridView.Columns.Count; j++)
                {
                    toDataGridView.Rows[rowIndex].Cells[j].Value = toDataGridView.Rows[rowIndex - 1].Cells[j].Value;
                    toDataGridView.Rows[rowIndex - 1].Cells[j].Value = list[j].ToString();
                }
                toDataGridView.Rows[rowIndex].Selected = false;
                toDataGridView.Rows[rowIndex - 1].Selected = true;
            }
            else if (tnMoveStep == 1)
            {
                if (rowIndex == toDataGridView.Rows.Count - 1)   //判断是否到底行
                {
                    return;
                }

                for (int j = 0; j < toDataGridView.Columns.Count; j++)
                {
                    toDataGridView.Rows[rowIndex].Cells[j].Value = toDataGridView.Rows[rowIndex + 1].Cells[j].Value;
                    toDataGridView.Rows[rowIndex + 1].Cells[j].Value = list[j].ToString();
                }
                toDataGridView.Rows[rowIndex].Selected = false;
                toDataGridView.Rows[rowIndex + 1].Selected = true;
            }
            else if (tnMoveStep < -1)
            {
                for (int j = 0; j < toDataGridView.Columns.Count; j++)
                {
                    for (int k = rowIndex; k > 0; k--)
                    {
                        toDataGridView.Rows[k].Cells[j].Value = toDataGridView.Rows[k - 1].Cells[j].Value;
                    }
                    toDataGridView.Rows[0].Cells[j].Value = list[j].ToString();
                }
                toDataGridView.Rows[rowIndex].Selected = false;
                toDataGridView.Rows[0].Selected = true;
            }
            else if (tnMoveStep > 1)
            {
                for (int j = 0; j < toDataGridView.Columns.Count; j++)
                {
                    for (int k = rowIndex; k < toDataGridView.Rows.Count - 1; k++)
                    {
                        toDataGridView.Rows[k].Cells[j].Value = toDataGridView.Rows[k + 1].Cells[j].Value;

                    }
                    toDataGridView.Rows[toDataGridView.Rows.Count - 1].Cells[j].Value = list[j].ToString();
                }
                toDataGridView.Rows[rowIndex].Selected = false;
                toDataGridView.Rows[toDataGridView.Rows.Count - 1].Selected = true;
            }

        }



    }
}
