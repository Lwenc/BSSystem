using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;
using System.IO;
using System.Windows;

namespace HASystem.StaticClass
{
    public static class DataGridExtensions
    {
        public static void WriteExcel(DataGrid dg)
        {
            try
            {
                DataTable dt = ((DataView)dg.ItemsSource).Table;
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.xlsx)|*.xls|All File(*.*)|*.*";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Export Excel File To";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath;
                    filePath = saveFileDialog.FileName;
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet(dt.TableName);
                    NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dg.Columns.Count; i++)//使用GataGrid创建表头
                    {
                        row.CreateCell(i).SetCellValue(dg.Columns[i].Header.ToString());
                    }
                    //for (int i = 0; i < dt.Columns.Count; i++)//使用table创建表头
                    //{
                    //    row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                    //}
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                        }
                    }
                    // 写入到客户端  
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        book.Write(ms);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            byte[] data = ms.ToArray();
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                        book = null;
                        MessageBox.Show("导出成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("数据为空，导出失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
