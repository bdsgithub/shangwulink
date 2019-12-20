using System;
using System.IO;
using System.Data;

using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web;
using System.Collections.Generic;

namespace HF.Cloud.CommonDAL
{
    public class ExcelExportHelper
    {
        /// <summary>
        /// 创建一个Excel
        /// Yakecan
        /// </summary>
        /// <returns>返回一个空表格</returns>
        public HSSFWorkbook CreateNewExcel()
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();

            dsi.Company = "河南浩方科贸有限公司";
            dsi.Manager = "Office Word 2003/2007";

            si.Author = "www.xxx.com";
            si.Subject = "信息导出";
            si.Title = "查询统计";

            workBook.DocumentSummaryInformation = dsi;
            workBook.SummaryInformation = si;

            return workBook;
        }

        /// <summary>
        /// 读取Excel Yakecan
        /// </summary>
        /// <param name="filePath">Excel文件的绝对路径</param>
        /// <returns></returns>
        public HSSFWorkbook ReadNewExcel(string filePath)
        {
            HSSFWorkbook workBook;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                //    workBook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream);
                //else if (filePath.IndexOf(".xls") > 0) // 2003版本
                workBook = new HSSFWorkbook(stream);
            }
            return workBook;
        }
        /// <summary>
        /// 获取多个isheet
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<ISheet> GetSheet(string filePath)
        {
            List<ISheet> sheet1 = new List<ISheet>();
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    NPOI.XSSF.UserModel.XSSFWorkbook workBook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream);
                    for (int i = 0; i < workBook.NumberOfSheets; i++)
                    {
                        sheet1.Add(workBook.GetSheetAt(i));
                    }
                }
                else
                {
                    NPOI.HSSF.UserModel.HSSFWorkbook workBook = new NPOI.HSSF.UserModel.HSSFWorkbook(stream);
                    for (int i = 0; i < workBook.NumberOfSheets; i++)
                    {
                        sheet1.Add(workBook.GetSheetAt(i));
                    }
                }
            }
            return sheet1;
        }
        /// <summary>
        /// 根据模版导入ExcelToDataTable Yakecan
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();

            List<ISheet> sheet1 = GetSheet(filePath);
            #region 循环sheet集合
            for (int k = 0; k < sheet1.Count; k++)
            {
                //判断工作簿中是否有内容
                IRow rowHead = sheet1[k].GetRow(0);
                if (rowHead != null)
                {
                    #region 插入dt
                    int columnCount = rowHead.PhysicalNumberOfCells;
                    int rowCount = sheet1[k].PhysicalNumberOfRows;

                    for (int i = 0; i < columnCount; i++)
                    {
                        ICell cell = rowHead.GetCell(i);

                        if (cell != null && !dt.Columns.Contains(cell.ToString().Trim()))
                        {
                            dt.Columns.Add(cell.ToString().Trim(), typeof(String));
                        }
                    }
                    IRow row;
                    ICell celln;
                    DataRow dr;
                    for (int j = 1; j < rowCount; j++)
                    {
                        row = sheet1[k].GetRow(j);
                        dr = dt.NewRow();
                        for (int t = 0; t < columnCount; t++)
                        {
                            celln = row.GetCell(t);
                            if (celln != null)
                            {
                                dr[t] = celln.ToString();
                            }
                            else
                            {
                                dr[t] = null;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    #endregion
                }

            }
            #endregion

            return dt;
        }

        /// <summary>
        /// 导入默认的格式，第一行默认是COLUMN
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public DataTable DefaultExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook workBook = ReadNewExcel(filePath);

            ISheet sheet1 = workBook.GetSheetAt(0);

            IRow rowHead = sheet1.GetRow(0);
            int columnCount = rowHead.PhysicalNumberOfCells;
            int rowCount = sheet1.PhysicalNumberOfRows;

            for (int i = 0; i < columnCount; i++)
            {
                ICell cell = rowHead.GetCell(i);

                if (cell != null)
                {
                    dt.Columns.Add(cell.ToString().Trim(), typeof(String));
                }
            }
            IRow row;
            ICell celln;
            DataRow dr;
            for (int j = 1; j < rowCount; j++)
            {
                row = sheet1.GetRow(j);
                dr = dt.NewRow();
                for (int t = 0; t < columnCount; t++)
                {
                    celln = row.GetCell(t);
                    if (celln != null)
                    {
                        dr[t] = celln.ToString();
                    }
                    else
                    {
                        dr[t] = null;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 根据Stream流和文件名称导出Excel
        /// Yakecan
        /// </summary>
        /// <param name="stream">Stream 流</param>
        /// <param name="xlsName">文件名称</param>
        private void OutPutExcelStreamOnClient(Stream stream, string xlsName)
        {
            MemoryStream ms = stream as MemoryStream;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(xlsName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            ms.Close();
        }

        /// <summary>
        /// 把指定的DataTable导出Excel
        /// Yakecan
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="fileName">导出的文件名称</param>
        /// <param name="sheetTittle">Sheet的名称</param>
        public void Export(DataTable dt, string fileName, string sheetTittle)
        {
            HSSFWorkbook workbook = CreateNewExcel();
            ISheet sheet1 = workbook.CreateSheet(sheetTittle);

            IRow titleRow = sheet1.CreateRow(0);
            titleRow.Height = (short)20 * 25;

            ICellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = (short)16;
            titleStyle.SetFont(font);

            NPOI.SS.Util.CellRangeAddress region = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count);
            sheet1.AddMergedRegion(region); // 添加合并区域

            ICell titleCell = titleRow.CreateCell(0);
            titleCell.CellStyle = titleStyle;
            titleCell.SetCellValue(sheetTittle);


            IRow headerRow = sheet1.CreateRow(1);
            ICellStyle headerStyle = workbook.CreateCellStyle();
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            IFont titleFont = workbook.CreateFont();
            titleFont.FontHeightInPoints = (short)11;
            titleFont.FontName = "宋体";
            headerStyle.SetFont(titleFont);

            headerRow.CreateCell(0).SetCellValue("序号");
            headerRow.GetCell(0).CellStyle = headerStyle;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                headerRow.CreateCell(i + 1).SetCellValue(dt.Columns[i].ColumnName);
                headerRow.GetCell(i + 1).CellStyle = headerStyle;
                sheet1.SetColumnWidth(i, 256 * 18);
            }

            ICellStyle bodyStyle = workbook.CreateCellStyle();
            bodyStyle.BorderBottom = BorderStyle.Thin;
            bodyStyle.BorderLeft = BorderStyle.Thin;
            bodyStyle.BorderRight = BorderStyle.Thin;
            bodyStyle.BorderTop = BorderStyle.Thin;
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                IRow bodyRow = sheet1.CreateRow(r + 2);
                bodyRow.CreateCell(0).SetCellValue(r + 1);
                bodyRow.GetCell(0).CellStyle = bodyStyle;
                bodyRow.GetCell(0).CellStyle.Alignment = HorizontalAlignment.Center;

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    bodyRow.CreateCell(c + 1).SetCellValue(dt.Rows[r][c].ToString());
                    bodyRow.GetCell(c + 1).CellStyle = bodyStyle;
                }
            }

            sheet1.CreateFreezePane(1, 2);

            //FileStream fs = new FileStream(path, FileMode.Create);
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet1 = null;
            headerRow = null;
            workbook = null;
            OutPutExcelStreamOnClient(ms, fileName);
            ms.Dispose();
        }
    }
}