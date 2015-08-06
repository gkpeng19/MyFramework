using ExcelLibrary.SpreadSheet;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace G.Util
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 根据Excel文件读取数据
        /// </summary>
        /// <typeparam name="T">要读取到的实体类型</typeparam>
        /// <param name="excelPath">Excel文件绝对路径</param>
        /// <param name="hps">Excel文件Header描述，顺序的每一列对应到的PropertyName</param>
        /// <param name="offset">正文开始的偏移量(行)</param>
        /// <returns>数据列表</returns>
        public static List<T> Read<T>(string excelPath, string[] hps, int offset = 1) where T : EntityBase, new()
        {
            if (!File.Exists(excelPath))
            {
                return null;
            }

            Workbook book = Workbook.Open(excelPath);
            Worksheet sheet = book.Worksheets[0];

            List<T> list = new List<T>();
            for (int r = 0; r <= sheet.Cells.LastRowIndex; r++)
            {
                var obj = new T();
                for (int i = 0; i <= sheet.Cells.LastColIndex; i++)
                {
                    var v = sheet.Cells[r, i].Value;
                    if (v != null)
                    {
                        obj.SetUIValue(hps[i], v);
                    }
                    else
                    {
                        obj.SetUIValue(hps[i], string.Empty);
                    }
                }
                list.Add(obj);
            }

            return list;
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