using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace DAL
{
    public class ExcelHelper
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        string passWord = "password";       //锁定表格的密码

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 根据标题层级设置数据格式
        /// </summary>
        /// <param name="titleLevel">标题层级</param>
        /// <returns></returns>
        public ICellStyle SetStyle(int titleLevel)
        {
            if (titleLevel == 1)
            {
                // 创建一级标题字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "微软雅黑";
                font.FontHeightInPoints = 18;
                font.IsBold = true;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.IsLocked = true;     //锁定列
                return style;
            }
            else if (titleLevel == 2)
            {
                // 创建二级标题字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 10;
                font.IsBold = true;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.IsLocked = true;     //锁定列
                return style;
            }
            else if (titleLevel == 3)
            {
                // 创建列名字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 10;
                font.IsBold = true;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.WrapText = true;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.IsLocked = true;     //锁定列
                return style;
            }
            else if (titleLevel == 4)
            {
                // 创建数据字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 9;
                font.IsBold = false;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.ShrinkToFit = true;      //缩小字体填充
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.IsLocked = true;     //锁定列
                return style;
            }
            else if (titleLevel == 5)
            {
                // 创建数据字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 9;
                font.IsBold = false;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.ShrinkToFit = true;      //缩小字体填充
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.IsLocked = false;     //锁定列
                return style;
            }
            else if (titleLevel == 6)
            {
                // 创建数据字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 9;
                font.IsBold = false;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.WrapText = true;          //自动换行
                style.ShrinkToFit = false;      //缩小字体填充
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.IsLocked = false;     //锁定列
                return style;
            }
            else if (titleLevel == 7)
            {
                // 创建二级标题字体样式（右对齐）：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 10;
                font.IsBold = true;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.IsLocked = true;     //锁定列
                return style;
            }
            else
            {
                // 创建二级标题字体样式：
                IFont font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 10;
                font.IsBold = true;
                // 绑定字体样式到样式对象上
                ICellStyle style = workbook.CreateCellStyle();
                style.SetFont(font);
                return style;
            }
        }







        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name = "data" > 要导入的数据 </ param >
        /// < param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name = "sheetName" > 要导入的excel的sheet的名称 </ param >
        /// < returns > 导入数据行数(包含列名那一行) </ returns >
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                ICellStyle style3 = SetStyle(3);        //表头
                ICellStyle style4 = SetStyle(4);        //锁定列
                ICellStyle style5 = SetStyle(5);        //需要编辑的数据

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                        row.GetCell(j).CellStyle = style3;
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                #region 原始代码
                //for (i = 0; i < data.Rows.Count; ++i)
                //{
                //    IRow row = sheet.CreateRow(count);
                //    for (j = 0; j < data.Columns.Count; ++j)
                //    {
                //        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                //    }
                //    ++count;
                //}
                #endregion

                #region 将字符型转为数值型，并将0替换为空值。
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                        if (b == true)
                        {
                            if (double.Parse(data.Rows[i][j].ToString()) != 0)
                            {
                                row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue("");
                            }
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }

                        if (data.Columns[j].ColumnName == "部门" || data.Columns[j].ColumnName == "班组" || data.Columns[j].ColumnName == "人员编号" || data.Columns[j].ColumnName == "姓名")
                        {
                            row.GetCell(j).CellStyle = style4;
                        }
                        else
                        {
                            row.GetCell(j).CellStyle = style5;
                        }
                    }
                    ++count;
                }
                #endregion

                sheet.ProtectSheet(passWord);     //锁定密码

                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }


        /// <summary>
        /// 将考勤数据导出到excel表，并设置好打印格式
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="yincanglie">要隐藏的列数</param>
        /// <param name="isPrint">是否是生成打印报表</param>
        /// <returns></returns>
        public int DataTableToExcel_KaoQin(DataTable data, string sheetName, bool isColumnWritten, int yincanglie, bool isPrint)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                ICellStyle style1 = SetStyle(1);       //一级标题
                ICellStyle style2 = SetStyle(2);       //二级标题
                ICellStyle style3 = SetStyle(3);       //表头
                ICellStyle style4 = SetStyle(4);       //数据

                if (isPrint == true)
                {


                    DateTime GengGaiRiQi = new DateTime(2017, 1, 1, 0, 0, 0);//创建一个日期，用来判断当前月最后一条导入数据的日期。
                    for (int i_date = 0; i_date < data.Rows.Count; i_date++)
                    {
                        for (int j_date = 0; j_date < data.Columns.Count; j_date++)
                        {
                            if (data.Columns[j_date].ColumnName == ("更改日期"))
                            {
                                if (DateTime.Parse(data.Rows[i_date][j_date].ToString()) > GengGaiRiQi)
                                {
                                    GengGaiRiQi = DateTime.Parse(data.Rows[i_date][j_date].ToString());
                                }
                            }
                        }
                    }

                    // 数据时间赋值：
                    IRow row = sheet.CreateRow(count);
                    row.CreateCell(data.Columns.Count - 4 - yincanglie).SetCellValue($"数据时间：{GengGaiRiQi}");
                    row.GetCell(data.Columns.Count - 4 - yincanglie).CellStyle = style2;

                    // 一级标题赋值：
                    row = sheet.CreateRow(count + 1);
                    row.CreateCell(0).SetCellValue("华能陕西渭南热电有限公司考勤确认表");
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, data.Columns.Count - yincanglie - 1));       //合并单元格第一行
                    row.GetCell(0).CellStyle = style1;//绑定样式对象到单元格上

                    // 二级标题赋值：
                    row = sheet.CreateRow(count + 2);
                    row.CreateCell(0).SetCellValue($"部门（盖章）：{data.Rows[i]["部门"].ToString()}");
                    row.GetCell(0).CellStyle = style2;
                    row.CreateCell(6).SetCellValue($"班组：{data.Rows[i]["班组"].ToString()}");
                    row.GetCell(6).CellStyle = style2;
                    row.CreateCell(data.Columns.Count - 3 - yincanglie).SetCellValue($"考勤年月：{data.Rows[i]["考勤年月"].ToString().Substring(0, 4)}年{data.Rows[i]["考勤年月"].ToString().Substring(4, 2)}月");
                    row.GetCell(data.Columns.Count - 3 - yincanglie).CellStyle = style2;

                    row = sheet.CreateRow(count + 3);       //空一行

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        row = sheet.CreateRow(count + 4);
                        row.Height = 300 * 4;       //设置行高
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        row = sheet.CreateRow(count + 4);
                        row.Height = 100 * 4;       //设置行高
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                                row.GetCell(j).CellStyle = style4;
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                                row.GetCell(j).CellStyle = style4;
                            }
                        }
                        ++count;
                    }

                    count += 4;
                    row = sheet.CreateRow(count);
                    count++;
                    row = sheet.CreateRow(count);
                    row.CreateCell(3).SetCellValue($"部门负责人：");
                    row.GetCell(3).CellStyle = style2;
                    row.CreateCell(13).SetCellValue($"班（组）长：");
                    row.GetCell(13).CellStyle = style2;
                    row.CreateCell(22).SetCellValue($"考勤员：");
                    row.GetCell(22).CellStyle = style2;

                    sheet.PrintSetup.Landscape = true;      //横向打印
                    sheet.PrintSetup.Scale = 90;        //缩放
                    sheet.PrintSetup.PaperSize = 9;     //A4 210*297mm
                    sheet.RepeatingRows = new CellRangeAddress(0, 4, 0, 4);     //设置打印标题

                    //设置列宽
                    for (int lie = 0; lie < data.Columns.Count; lie++)
                    {
                        if (lie == 0)
                        {
                            sheet.SetColumnWidth(lie, 4 * 256);
                        }
                        else if (lie == 1 || lie == 2 || lie == 24 || lie == 25)
                        {
                            sheet.SetColumnWidth(lie, 8 * 256 - 80);
                        }
                        else if (lie == 26)
                        {
                            sheet.SetColumnWidth(lie, 12 * 256);
                        }
                        else
                        {
                            sheet.SetColumnWidth(lie, 5 * 256 - 120);
                        }
                    }

                }

                else
                {
                    ICellStyle style5 = SetStyle(5);       //需要编辑的数据

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    #region 原始代码
                    //for (i = 0; i < data.Rows.Count; ++i)
                    //{
                    //    IRow row = sheet.CreateRow(count);
                    //    for (j = 0; j < data.Columns.Count; ++j)
                    //    {
                    //        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    //    }
                    //    ++count;
                    //}
                    #endregion

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }

                            if (data.Columns[j].ColumnName == "考勤年月" || data.Columns[j].ColumnName == "部门" || data.Columns[j].ColumnName == "班组" || data.Columns[j].ColumnName == "人员编号" || data.Columns[j].ColumnName == "姓名" || data.Columns[j].ColumnName == "应出勤" || data.Columns[j].ColumnName == "年假" || data.Columns[j].ColumnName == "事假" || data.Columns[j].ColumnName == "病假" || data.Columns[j].ColumnName == "产假" || data.Columns[j].ColumnName == "陪产假" || data.Columns[j].ColumnName == "婚假" || data.Columns[j].ColumnName == "丧假" || data.Columns[j].ColumnName == "迟到早退次数" || data.Columns[j].ColumnName == "缺卡次数")
                            {
                                row.GetCell(j).CellStyle = style4;
                            }
                            else
                            {
                                row.GetCell(j).CellStyle = style5;
                            }
                        }
                        ++count;
                    }

                    //设置列宽
                    for (int lie = 0; lie < data.Columns.Count; lie++)
                    {
                        if (lie == 0)
                        {
                            sheet.SetColumnWidth(lie, 4 * 256);
                        }
                        else if ((lie >= 1 && lie <= 5) || lie == 27 || lie == 28)
                        {
                            sheet.SetColumnWidth(lie, 8 * 256 - 80);
                        }
                        else if (lie == 29)
                        {
                            sheet.SetColumnWidth(lie, 12 * 256);
                        }
                        else
                        {
                            sheet.SetColumnWidth(lie, 5 * 256 - 120);
                        }
                    }
                }

                sheet.ProtectSheet(passWord);     //锁定密码
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }



        /// <summary>
        /// 将加班数据导出到excel表，并设置好打印格式
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="yincanglie">要隐藏的列数</param>
        /// <param name="isPrint">是否是生成打印报表</param>
        /// <returns></returns>
        public int DataTableToExcel_JiaBan(DataTable data, string sheetName, bool isColumnWritten, int yincanglie, bool isPrint)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                ICellStyle style1 = SetStyle(1);       //一级标题
                ICellStyle style2 = SetStyle(2);       //二级标题
                ICellStyle style3 = SetStyle(3);       //表头
                ICellStyle style4 = SetStyle(4);       //数据

                if (isPrint == true)
                {
                    DateTime GengGaiRiQi = new DateTime(2017, 1, 1, 0, 0, 0);//创建一个日期，用来判断当前月最后一条导入数据的日期。
                    for (int i_date = 0; i_date < data.Rows.Count; i_date++)
                    {
                        for (int j_date = 0; j_date < data.Columns.Count; j_date++)
                        {
                            if (data.Columns[j_date].ColumnName == ("更改日期"))
                            {
                                if (DateTime.Parse(data.Rows[i_date][j_date].ToString()) > GengGaiRiQi)
                                {
                                    GengGaiRiQi = DateTime.Parse(data.Rows[i_date][j_date].ToString());
                                }
                            }
                        }
                    }

                    // 数据时间赋值：
                    IRow row = sheet.CreateRow(count);
                    row.CreateCell(data.Columns.Count - 3 - yincanglie).SetCellValue($"数据时间：{GengGaiRiQi}");
                    row.GetCell(data.Columns.Count - 3 - yincanglie).CellStyle = style2;


                    // 一级标题赋值：
                    row = sheet.CreateRow(count + 1);
                    row.CreateCell(0).SetCellValue("华能陕西渭南热电有限公司加班费审批表");
                    row.Height = 250 * 4;       //设置行高
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, data.Columns.Count - yincanglie - 1));       //合并单元格第一行
                    row.GetCell(0).CellStyle = style1;//绑定样式对象到单元格上


                    row = sheet.CreateRow(count + 2);
                    row.CreateCell(0).SetCellValue($"部门（盖章）：{data.Rows[i]["部门"].ToString()}");
                    row.GetCell(0).CellStyle = style2;
                    row.CreateCell(data.Columns.Count - 2 - yincanglie).SetCellValue($"考勤年月：{data.Rows[i]["考勤年月"].ToString().Substring(0, 4)}年{data.Rows[i]["考勤年月"].ToString().Substring(4, 2)}月");
                    row.GetCell(data.Columns.Count - 2 - yincanglie).CellStyle = style2;

                    row = sheet.CreateRow(count + 3);       //空一行


                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        row = sheet.CreateRow(count + 4);
                        row.Height = 200 * 4;       //设置行高
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    double sum = 0;

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        row = sheet.CreateRow(count + 4);
                        row.Height = 100 * 4;       //设置行高
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                    if (data.Columns[j].ColumnName == "金额")
                                    {
                                        sum += double.Parse(data.Rows[i][j].ToString());
                                    }
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                                row.GetCell(j).CellStyle = style4;
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                                row.GetCell(j).CellStyle = style4;
                            }
                        }
                        ++count;
                    }


                    count += 4;
                    row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                    {
                        row.CreateCell(j).SetCellValue("");
                        row.GetCell(j).CellStyle = style4;
                    }
                    row.Height = 100 * 4;
                    row.CreateCell(9).SetCellValue(sum.ToString("0.00"));
                    row.GetCell(9).CellStyle = style3;

                    sheet.AddMergedRegion(new CellRangeAddress(count, count, 0, 2));       //合并单元格第一行
                    row.CreateCell(0).SetCellValue("合计");
                    row.GetCell(0).CellStyle = style3;

                    count++;
                    row = sheet.CreateRow(count);
                    count++;
                    row = sheet.CreateRow(count);
                    row.CreateCell(2).SetCellValue($"分管领导：");
                    row.GetCell(2).CellStyle = style2;
                    row.CreateCell(5).SetCellValue($"部门负责人：");
                    row.GetCell(5).CellStyle = style2;
                    row.CreateCell(9).SetCellValue($"填报人：");
                    row.GetCell(9).CellStyle = style2;

                    sheet.PrintSetup.Landscape = false;      //横向打印
                    sheet.PrintSetup.Scale = 100;        //缩放
                    sheet.PrintSetup.PaperSize = 9;     //A4 210*297mm
                    sheet.RepeatingRows = new CellRangeAddress(0, 4, 0, 4);     //设置打印标题


                    //设置列宽
                    for (int lie = 0; lie < data.Columns.Count; lie++)
                    {
                        if (lie == 0)
                        {
                            sheet.SetColumnWidth(lie, 5 * 256);
                        }
                        else if (lie == 1 || lie == 2 || lie == 3 || lie == 4 || lie == 8)
                        {
                            sheet.SetColumnWidth(lie, 8 * 256);
                        }
                        else if (lie == 5 || lie == 6 || lie == 7)
                        {
                            sheet.SetColumnWidth(lie, 6 * 256);
                        }
                        else if (lie == 9)//金额列
                        {
                            sheet.SetColumnWidth(lie, 10 * 256);
                        }
                        else if (lie == 10)//备注列
                        {
                            sheet.SetColumnWidth(lie, 12 * 256);
                        }
                    }
                }
                else
                {
                    ICellStyle style5 = SetStyle(5);       //需要编辑的数据

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    #region 原始代码
                    //for (i = 0; i < data.Rows.Count; ++i)
                    //{
                    //    IRow row = sheet.CreateRow(count);
                    //    for (j = 0; j < data.Columns.Count; ++j)
                    //    {
                    //        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    //    }
                    //    ++count;
                    //}
                    #endregion

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }

                            if (data.Columns[j].ColumnName == "金额" || data.Columns[j].ColumnName == "备注")
                            {
                                row.GetCell(j).CellStyle = style5;
                            }
                            else
                            {
                                row.GetCell(j).CellStyle = style4;
                            }
                        }
                        ++count;
                    }

                    //设置列宽
                    //设置列宽
                    for (int lie = 0; lie < data.Columns.Count; lie++)
                    {
                        if (lie == 0)
                        {
                            sheet.SetColumnWidth(lie, 5 * 256);
                        }
                        else if (lie == 1 || lie == 3 || lie == 4 || lie == 5 || lie == 6 || lie == 10)
                        {
                            sheet.SetColumnWidth(lie, 8 * 256);
                        }
                        else if (lie == 7 || lie == 8 || lie == 9)
                        {
                            sheet.SetColumnWidth(lie, 6 * 256);
                        }
                        else if (lie == 2 || lie == 11)//部门、金额列
                        {
                            sheet.SetColumnWidth(lie, 10 * 256);
                        }
                        else if (lie == 12)//备注列
                        {
                            sheet.SetColumnWidth(lie, 12 * 256);
                        }
                    }
                }

                sheet.ProtectSheet(passWord);     //锁定密码
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// 将考勤数据导出到excel表，并设置好打印格式
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="yincanglie">要隐藏的列数</param>
        /// <param name="isPrint">是否是生成打印报表</param>
        /// <returns></returns>
        public int DataTableToExcel_KaoPing(DataTable data, string sheetName, bool isColumnWritten, int yincanglie, bool isPrint)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                ICellStyle style1 = SetStyle(1);       //一级标题
                ICellStyle style2 = SetStyle(2);       //二级标题
                ICellStyle style7 = SetStyle(7);       //二级标题（右对齐）
                ICellStyle style3 = SetStyle(3);       //表头
                ICellStyle style4 = SetStyle(4);       //数据

                if (isPrint == true)        //打印数据要生成格式
                {
                    ICellStyle style5 = SetStyle(5);       //需要编辑的数据
                    ICellStyle style6 = SetStyle(6);       //需要编辑的数据

                    DateTime GengGaiRiQi = new DateTime(2017, 1, 1, 0, 0, 0);//创建一个日期，用来判断当前月最后一条导入数据的日期。
                    for (int i_date = 0; i_date < data.Rows.Count - 1; i_date++)
                    {
                        for (int j_date = 0; j_date < data.Columns.Count; j_date++)
                        {
                            if (data.Columns[j_date].ColumnName == ("更改日期"))
                            {
                                if (DateTime.Parse(data.Rows[i_date][j_date].ToString()) > GengGaiRiQi)
                                {
                                    GengGaiRiQi = DateTime.Parse(data.Rows[i_date][j_date].ToString());
                                }
                            }
                        }
                    }

                    // 数据时间赋值：
                    IRow row = sheet.CreateRow(count);
                    row.CreateCell(data.Columns.Count - 2 - yincanglie).SetCellValue($"数据时间：{GengGaiRiQi}");
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, data.Columns.Count - yincanglie - 2, data.Columns.Count - yincanglie - 1));
                    row.GetCell(data.Columns.Count - 2 - yincanglie).CellStyle = style7;

                    // 一级标题赋值：
                    row = sheet.CreateRow(count + 1);
                    row.CreateCell(0).SetCellValue("华能陕西渭南热电有限公司员工月度考评表");
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, data.Columns.Count - yincanglie - 1));       //合并单元格第一行
                    row.GetCell(0).CellStyle = style1;//绑定样式对象到单元格上

                    // 二级标题赋值：
                    row = sheet.CreateRow(count + 2);
                    row.CreateCell(0).SetCellValue($"部门（盖章）：{data.Rows[i]["部门"].ToString()}");
                    row.GetCell(0).CellStyle = style2;
                    //row.CreateCell(6).SetCellValue($"班组：{data.Rows[i]["班组"].ToString()}");
                    //row.GetCell(6).CellStyle = style2;
                    row.CreateCell(data.Columns.Count - 1 - yincanglie).SetCellValue($"考评年月：{data.Rows[i]["考评年月"].ToString().Substring(0, 4)}年{data.Rows[i]["考评年月"].ToString().Substring(4, 2)}月");
                    row.GetCell(data.Columns.Count - 1 - yincanglie).CellStyle = style2;

                    row = sheet.CreateRow(count + 3);       //空一行

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        row = sheet.CreateRow(count + 4);
                        row.Height = 200 * 4;       //设置行高
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        row = sheet.CreateRow(count + 4);
                        if (data.Rows[i]["考评意见"].ToString() == "优良")
                        {
                            row.Height = 100 * 4 * 4;       //设置优良人员行高
                        }
                        else
                        {
                            row.Height = 100 * 4;       //设置行高
                        }
                        for (j = 0; j < data.Columns.Count - yincanglie; ++j)
                        {
                            #region 将表中为0的值设置为空单元格
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }
                            #endregion

                            #region 设定各单元格的字体格式
                            if (data.Columns[j].ColumnName == "序号" || data.Columns[j].ColumnName == "考评年月" || data.Columns[j].ColumnName == "部门" || data.Columns[j].ColumnName == "人员编号" || data.Columns[j].ColumnName == "姓名")
                            {
                                row.GetCell(j).CellStyle = style4;
                            }
                            else
                            {
                                row.GetCell(j).CellStyle = style6;
                            }

                            //设定优良率行的字体格式
                            if (i == data.Rows.Count - 1)
                            {
                                row.GetCell(j).CellStyle = style3;
                            }
                            #endregion
                        }
                        ++count;
                    }


                    count += 4;

                    sheet.AddMergedRegion(new CellRangeAddress(count - 1, count - 1, 0, 2));       //合并单元格第一行
                    row.CreateCell(0).SetCellValue("优良率：");
                    row.GetCell(0).CellStyle = style3;

                    count++;
                    row = sheet.CreateRow(count);
                    row.CreateCell(1).SetCellValue($"分管领导：");
                    row.GetCell(1).CellStyle = style2;
                    row.CreateCell(9).SetCellValue($"部门负责人：");
                    row.GetCell(9).CellStyle = style2;
                    row.CreateCell(10).SetCellValue($"填报人：");
                    row.GetCell(10).CellStyle = style2;

                    sheet.PrintSetup.Landscape = true;      //横向打印
                    sheet.PrintSetup.Scale = 90;        //缩放
                    sheet.PrintSetup.PaperSize = 9;     //A4 210*297mm
                    sheet.RepeatingRows = new CellRangeAddress(0, 4, 0, 4);     //设置打印标题


                }

                else            //不打印数据直接生成导入模板
                {
                    ICellStyle style5 = SetStyle(5);       //需要编辑的数据
                    ICellStyle style6 = SetStyle(6);       //需要编辑的数据

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                            row.GetCell(j).CellStyle = style3;
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    #region 原始代码
                    //for (i = 0; i < data.Rows.Count; ++i)
                    //{
                    //    IRow row = sheet.CreateRow(count);
                    //    for (j = 0; j < data.Columns.Count; ++j)
                    //    {
                    //        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    //    }
                    //    ++count;
                    //}
                    #endregion

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        if (data.Rows[i]["考评意见"].ToString() == "优良")
                        {
                            row.Height = 100 * 4 * 4;       //设置优良人员行高
                        }
                        else
                        {
                            row.Height = 100 * 4;       //设置行高
                        }
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            bool b = double.TryParse(data.Rows[i][j].ToString(), out double result);
                            if (b == true)
                            {
                                if (double.Parse(data.Rows[i][j].ToString()) != 0)
                                {
                                    row.CreateCell(j).SetCellValue(double.Parse(data.Rows[i][j].ToString()));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue("");
                                }
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                            }

                            if (data.Columns[j].ColumnName == "序号" || data.Columns[j].ColumnName == "考评年月" || data.Columns[j].ColumnName == "部门" || data.Columns[j].ColumnName == "人员编号" || data.Columns[j].ColumnName == "姓名")
                            {
                                row.GetCell(j).CellStyle = style4;
                            }
                            else
                            {
                                row.GetCell(j).CellStyle = style6;
                            }
                        }
                        ++count;
                    }

                }


                //设置列宽（注意：打印、不打印都有）
                for (int lie = 0; lie < data.Columns.Count; lie++)
                {
                    if (data.Columns[lie].ColumnName == "序号")
                    {
                        sheet.SetColumnWidth(lie, 4 * 256);
                    }
                    else if (data.Columns[lie].ColumnName == "部门" || data.Columns[lie].ColumnName == "考评年月" || data.Columns[lie].ColumnName == "人员编号" || data.Columns[lie].ColumnName == "姓名" || data.Columns[lie].ColumnName == "考评意见" || data.Columns[lie].ColumnName == "考评得分")
                    {
                        sheet.SetColumnWidth(lie, 8 * 256 - 80);
                    }
                    else if (data.Columns[lie].ColumnName == "备注")
                    {
                        sheet.SetColumnWidth(lie, 20 * 256);
                    }
                    else if (data.Columns[lie].ColumnName == "具体解释说明")
                    {
                        sheet.SetColumnWidth(lie, 65 * 256);
                    }
                    else
                    {
                        sheet.SetColumnWidth(lie, 5 * 256 - 120);
                    }
                }





                sheet.ProtectSheet(passWord);     //锁定密码
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }





    }
}