namespace RiderbikeZone.Servicios
{
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Windows.Forms;

    public class SCrearXLS
    {
        #region Métodos
        public void ExportarDataGridViewExcel(DataGridView _grd, string _dirr)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < _grd.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < _grd.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 1, j + 1] = _grd.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs( _dirr + ".xls",
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
        }

        public void EscribirExcel(DataGridView _dataGridView, string _nombreArchivo)
        {
            Microsoft.Office.Interop.Excel.Application aplicacion;
            Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
            Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
            aplicacion = new Microsoft.Office.Interop.Excel.Application();
            libros_trabajo = aplicacion.Workbooks.Add();
            hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
            hoja_trabajo.Cells.Font.Name = "Century Gothic";
            hoja_trabajo.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            int c = 2;
            for (int i = 0; i < _dataGridView.Columns.Count; i++)
            {
                hoja_trabajo.Cells[2, c + 1] = _dataGridView.Columns[i].HeaderCell.Value.ToString();
                hoja_trabajo.Cells[2, c + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
                hoja_trabajo.Cells[2, c + 1].Font.Size = 11;
                hoja_trabajo.Cells[2, c + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                c++;
            }
            //Recorremos el DataGridView rellenando la hoja de trabajo
            int x = 2, y = 2;
            for (int i = 0; i < _dataGridView.Rows.Count - 1; i++)
            {
                y = 2;
                for (int j = 0; j < _dataGridView.Columns.Count; j++)
                {
                    if (y >= 8 && y <= 12)
                    {
                        var tt = _dataGridView.Rows[i].Cells[j].Value.ToString().Split('.');
                        string _num = string.Empty;
                        foreach (var ii in tt) _num += ii;
                        hoja_trabajo.Cells[x + 1, y + 1] = Convert.ToInt32(_num);
                    }
                    else hoja_trabajo.Cells[x + 1, y + 1] = _dataGridView.Rows[i].Cells[j].Value.ToString();
                    hoja_trabajo.Cells[x + 1, y + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
                    hoja_trabajo.Cells[x + 1, y + 1].Font.Size = 10;
                    y++;
                }
                x++;
            }
            libros_trabajo.SaveAs(_nombreArchivo + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
            libros_trabajo.Close(true);
            aplicacion.Quit();
        }

        public void EscribirExcelClientes(DataGridView _dataGridView, string _nombreArchivo)
        {
            Microsoft.Office.Interop.Excel.Application aplicacion;
            Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
            Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
            aplicacion = new Microsoft.Office.Interop.Excel.Application();
            libros_trabajo = aplicacion.Workbooks.Add();
            hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
            hoja_trabajo.Cells.Font.Name = "Century Gothic";
            hoja_trabajo.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            int c = 2;
            for (int i = 0; i < _dataGridView.Columns.Count; i++)
            {
                hoja_trabajo.Cells[2, c + 1] = _dataGridView.Columns[i].HeaderCell.Value.ToString();
                hoja_trabajo.Cells[2, c + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
                hoja_trabajo.Cells[2, c + 1].Font.Size = 11;
                hoja_trabajo.Cells[2, c + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                c++;
            }
            //Recorremos el DataGridView rellenando la hoja de trabajo
            int x = 2, y = 2;
            for (int i = 0; i < _dataGridView.Rows.Count - 1; i++)
            {
                y = 2;
                for (int j = 0; j < _dataGridView.Columns.Count; j++)
                {
                    hoja_trabajo.Cells[x + 1, y + 1] = _dataGridView.Rows[i].Cells[j].Value.ToString();
                    hoja_trabajo.Cells[x + 1, y + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
                    hoja_trabajo.Cells[x + 1, y + 1].Font.Size = 10;
                    y++;

                    //if (y >= 8 && y <= 12)
                    //{
                    //    var tt = _dataGridView.Rows[i].Cells[j].Value.ToString().Split('.');
                    //    string _num = string.Empty;
                    //    foreach (var ii in tt) _num += ii;
                    //    hoja_trabajo.Cells[x + 1, y + 1] = Convert.ToInt32(_num);
                    //}
                    //else hoja_trabajo.Cells[x + 1, y + 1] = _dataGridView.Rows[i].Cells[j].Value.ToString();
                    //hoja_trabajo.Cells[x + 1, y + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
                    //hoja_trabajo.Cells[x + 1, y + 1].Font.Size = 10;
                    //y++;
                }
                x++;
            }
            libros_trabajo.SaveAs(_nombreArchivo + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
            libros_trabajo.Close(true);
            aplicacion.Quit();
        }
        #endregion
    }
}
