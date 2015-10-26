using ExcelLibrary.SpreadSheet;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using GOMFrameWork.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace G.Util.Tool
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 读取Excel到DataTable
        /// </summary>
        /// <param name="excelPath">excel文件路径</param>
        /// <param name="hps">表头对应的属性名</param>
        /// <param name="offset">行偏移量(除去表头行)</param>
        /// <returns>DataTable</returns>
        public static DataTable Read(string excelPath, string[] hps, int offset = 1)
        {
            if (!File.Exists(excelPath))
            {
                return null;
            }

            DataTable dt = new DataTable();
            var exName = Path.GetExtension(excelPath);
            if (exName.Equals(".xls", StringComparison.CurrentCultureIgnoreCase))
            {
                #region .xls文件处理:HSSFWorkbook

                HSSFWorkbook hssfworkbook = null;
                try
                {
                    using (FileStream file = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = new HSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                ISheet sheet = hssfworkbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数
                var lastCellNum = hps.Length > headerRow.LastCellNum ? headerRow.LastCellNum : hps.Length;
                for (int j = 0; j < lastCellNum; j++)
                {
                    //SET EVERY COLUMN NAME
                    dt.Columns.Add(hps[j]);
                }

                while (rows.MoveNext())
                {
                    IRow row = (HSSFRow)rows.Current;
                    if (row.RowNum < offset)
                    {
                        //The firt row is title,no need import
                        continue;
                    }

                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        if (i >= dt.Columns.Count)//cell count>column count,then break //每条记录的单元格数量不能大于表格栏位数量 20140213
                        {
                            break;
                        }

                        ICell cell = row.GetCell(i);

                        if ((i == 0) && (string.IsNullOrEmpty(cell.ToString()) == true))//每行第一个cell为空,break
                        {
                            break;
                        }

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }

                    dt.Rows.Add(dr);
                }

                #endregion
            }
            else if (exName.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                #region .xlsx文件处理:XSSFWorkbook

                XSSFWorkbook hssfworkbook = null;

                try
                {
                    using (FileStream file = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = new XSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                ISheet sheet = hssfworkbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                XSSFRow headerRow = (XSSFRow)sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数 
                var lastCellNum = hps.Length > headerRow.LastCellNum ? headerRow.LastCellNum : hps.Length;
                for (int j = 0; j < lastCellNum; j++)
                {
                    dt.Columns.Add(hps[j]);
                }

                while (rows.MoveNext())
                {
                    IRow row = (XSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    if (row.RowNum < offset)
                    {
                        //The firt row is title,no need import
                        continue;
                    }

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        if (i >= dt.Columns.Count)//cell count>column count,then break //每条记录的单元格数量不能大于表格栏位数量 20140213
                        {
                            break;
                        }

                        ICell cell = row.GetCell(i);

                        if ((i == 0) && (string.IsNullOrEmpty(cell.ToString()) == true))//每行第一个cell为空,break
                        {
                            break;
                        }

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }

                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 读取Excel到List
        /// </summary>
        /// <typeparam name="T">读取到的实体类型</typeparam>
        /// <param name="excelPath">excel文件路径</param>
        /// <param name="hps">表头对应的属性名</param>
        /// <param name="offset">行偏移量(除去表头行)</param>
        /// <returns>List</returns>
        public static List<T> Read<T>(string excelPath, string[] hps, int offset = 1) where T : EntityBase, new()
        {
            var dt = Read(excelPath, hps, offset);
            if (dt.Rows.Count > 0)
            {
                return dt.ConvertToList<T>();
            }
            return null;
        }

        /// <summary>
        /// 导出数据到Excel文件
        /// </summary>
        /// <typeparam name="T">要导出数据的类型</typeparam>
        /// <param name="data">要导出的数据</param>
        /// <param name="hps">导出Excel文件Header描述，0：HeaderName，1：PropertyName</param>
        /// <param name="outpath">输出文件路径</param>
        public static void ExportExcel<T>(List<T> data, string[,] hps, string outpath) where T : EntityBase
        {
            Worksheet worksheet = new Worksheet("Sheet1");

            //Init Excel Header
            for (int i = 0; i < hps.Length / 2; ++i)
            {
                worksheet.Cells[0, i] = new Cell(hps[i, 0]) { Style = new CellStyle() { BackColor = Color.Gray } };
            }

            //Insert Data To Table
            for (int r = 0; r < data.Count; ++r)
            {
                for (int i = 0; i < hps.Length / 2; ++i)
                {
                    if (hps[i, 1] != null && hps[i, 1].Length > 0)
                    {
                        var v = data[r][hps[i, 1]];
                        if (v != null)
                        {
                            worksheet.Cells[r + 1, i] = new Cell(v);
                        }
                    }
                }
            }

            Workbook workbook = new Workbook();
            workbook.Worksheets.Add(worksheet);
            string filepath = outpath.Substring(0, outpath.LastIndexOf("\\"));
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            workbook.Save(outpath);
        }
    }
}