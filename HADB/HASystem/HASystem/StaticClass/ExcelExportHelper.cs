using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HASystem.StaticClass
{
    static class ExcelExportHelper
    {
        /// <summary>
        /// 导出数据为Excel文件
        /// </summary>
        /// <param name="data">数据源表格</param>
        /// <param name="path">保存的路径</param>
        /// <param name="sheetName">表单名字</param>
        //全选导出
        public static void Export(DataGrid data, string path, string sheetName = "default")
        {
            if (data.Items.IsEmpty)
            {
                
                MessageBox.Show("数据为空，导出失败！","提示",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            Export(data, path, sheetName, data.Items);
        }
        //多选导出
        public static async void Export(DataGrid data, string path, string sheetName, IList results)
        {
            if (data.Items.IsEmpty)
            {
                MessageBox.Show("数据为空，导出失败！","提示",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
               

            Microsoft.Office.Interop.Excel.Application excel = null;
            Workbooks books = null;
            Workbook book = null;
            Worksheet sheet = null;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                books = excel.Workbooks;
                book = books.Add(true);
                sheet = book.ActiveSheet;

                excel.Visible = false;
                excel.DisplayAlerts = false;

                Range headerRange = sheet.Range[sheet.Cells[2, 1], sheet.Cells[2, data.Columns.Count]];
                headerRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                headerRange.Font.Bold = true;

                sheet.Name = string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName;
                sheet.Columns.EntireColumn.NumberFormatLocal = "@";

                //写入标题
                for (int i = 0; i < data.Columns.Count; i++)
                    sheet.Cells[2, i + 1] = data.Columns[i].Header;

                //由于无法得知结果的属性，因此考虑使用反射解决
                Type bindingType = data.Items[0].GetType();
                //储存各列分别绑定的名字
                string[] bindingPropertyName = new string[data.Columns.Count];

                //从Binding的Path里获取绑定的路径
                for (int i = 0; i < bindingPropertyName.Length; i++)
                    bindingPropertyName[i] = ((data.Columns[i] as DataGridBoundColumn).Binding as Binding).Path.Path;

                //写入数据
                //for (int row = 0; row < data.Items.Count; row++)
                //    for (int column = 0; column < data.Columns.Count; column++)
                //        //通过反射获取各列的值，性能上大概有点堪忧。。不过现在数据量不大应该能接受
                //        sheet.Cells[row + 3, column + 1] = bindingType.GetProperty(bindingPropertyName[column]).GetValue(data.Items[row]);
                await Task.Run(() =>
                {
                    for (int row = 0; row < results.Count; row++)
                    {
                        for (int column = 0; column < bindingPropertyName.Length; column++)
                        {
                            sheet.Cells[row + 3, column + 1] = bindingType.GetProperty(bindingPropertyName[column]).GetValue(results[row]);
                        }
                    }

                    sheet.Columns.AutoFit();

                    book.SaveAs(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange);
                    MessageBox.Show("数据保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                book?.Close();
                excel?.Quit();
                excel = null;

                //在方法返回前用GC无论如何都无法清除excel实例。。包括Marshal.FinalReleaseComObject也试过无效
                new Task(() => GC.Collect()).Start();
            }
        }
    }
}
