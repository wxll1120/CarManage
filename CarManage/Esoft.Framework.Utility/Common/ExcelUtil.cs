using System;
using System.Data.OleDb;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System.Web;
using System.Collections.Generic;

namespace Esoft.Framework.Utility.Common
{
    public class ExcelUtil
    {
        /// <summary>
        /// 2003Excel连接字符串
        /// </summary>
        private const string excel2003ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";

        /// <summary>
        /// 2007Excel连接字符串
        /// </summary>
        //private const string excel2007ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
        private const string excel2007ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source ={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";

        /// <summary>
        /// CSV文件连接字符串
        /// </summary>
        //private const string csvConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";
        //private const string csvConnectionString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=Extensions=asc,csv,tab,txt";
        private const string csvConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";

        /// <summary>
        /// 将DataTable数据导出到CSV文件
        /// </summary>
        /// <param name="table">DataTable数据源</param>
        /// <param name="file">导出文件完整路径</param>
        public static void ConvertToCSV(DataTable table, string file)
        {
            string title = string.Empty;

            using (FileStream fileStream = new FileStream(file, 
                FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(
                    new BufferedStream(fileStream), System.Text.Encoding.Default))
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        title += column.ColumnName + ",";
                    }

                    title = title.Substring(0, title.Length - 1) + "\n";

                    streamWriter.Write(title);

                    foreach (DataRow row in table.Rows)
                    {
                        string line = string.Empty;

                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            line += row[i].ToString().Trim() + ",";
                        }

                        line = line.Substring(0, line.Length - 1) + "\n";

                        streamWriter.Write(line);
                    }
                }
            }
        }

        /// <summary>
        /// 将DataTable数据导出到Excel文件
        /// </summary>
        /// <param name="table">table数据</param>
        /// <param name="file">文件名称</param>
        public static void ConvertToExcel(DataTable table, string file)
        {

            InitializeWorkbook();
            GenerateData1(table);
            WriteToStream(file);
        }

        /// <summary>
        /// 网页中导出Cxcel
        /// </summary>
        /// <param name="argResp">Http</param>
        /// <param name="table">table数据</param>
        public static void ConvertToExcel(HttpResponse argResp, DataTable table)
        {
            InitializeWorkbook();
            GenerateData1(table);
            argResp.BinaryWrite(WriteToStream().GetBuffer());
            argResp.End();
        }

        public static void ConvertToExcel(HttpResponse argResp, DataTable table, string imageColName, int imageColSpan)
        {
            InitializeWorkbook();
            GenerateData(table, imageColName, imageColSpan);
            argResp.BinaryWrite(WriteToStream().GetBuffer());
            argResp.End();
        }

        private static HSSFWorkbook hssfworkbook;

        private static void WriteToStream(string file)
        {
            //Write the stream data of workbook to the root directory

            FileStream streamWriter = new FileStream(file, FileMode.Create);
            hssfworkbook.Write(streamWriter);
            streamWriter.Close();
        }

        private static MemoryStream WriteToStream()
        {
            //Write the stream data of workbook to the root directory

            MemoryStream streamWriter = new MemoryStream();
            hssfworkbook.Write(streamWriter);
            return streamWriter;
            //streamWriter.Close();
        }

        private static void GenerateData(DataTable table)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IFont font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 15;
            
            font1.FontName = "宋体";
            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.SetFont(font1);
            style1.Alignment = HorizontalAlignment.CENTER;

            IFont font2 = hssfworkbook.CreateFont();
            font2.FontHeightInPoints = 12;
            font2.FontName = "宋体";
            ICellStyle style2 = hssfworkbook.CreateCellStyle();
            style2.SetFont(font2);
            style1.Alignment = HorizontalAlignment.CENTER;
            IDataFormat format = hssfworkbook.CreateDataFormat();
            List<int> dates = new List<int>();
            List<int> doubles = new List<int>();
            List<int> ints = new List<int>();
            IRow headRow = sheet1.CreateRow(0);
            for (int col = 0; col < table.Columns.Count; col++)
            {
                string name = table.Columns[col].ColumnName;
                if (name.Contains("日期"))
                    dates.Add(col);
                else if (name.Contains("价") || name.Contains("金额"))
                    doubles.Add(col);
                else if (name.Contains("数量"))
                    ints.Add(col);
                ICell headCell = headRow.CreateCell(col);
                headCell.SetCellValue(name);
                headCell.CellStyle = style1;
                sheet1.AutoSizeColumn(col);
            }
            int rowCount = table.Rows.Count;
            int columnCount = table.Columns.Count;
            for (int r = 0; r < rowCount; r++)
            {
                DataRow rowVal = table.Rows[r];
                IRow row = sheet1.CreateRow(r + 1);
                for (int c = 0; c < columnCount; c++)
                {
                    ICell cell = row.CreateCell(c);
                    DateTime time = DateTime.MinValue;
                    string dateType = string.Empty;
                    
                    if (DateTime.TryParse(rowVal[c].ToString(), out time))
                    {
                        cell.SetCellValue(Convert.ToDateTime(rowVal[c].ToString()));
                        dateType = "dateTime";
                    }
                    else
                        cell.SetCellValue(rowVal[c].ToString());
                    
                    cell.CellStyle = style2;
                    switch (dateType)
                    {
                        case "dateTime": cell.CellStyle.DataFormat = format.GetFormat("yyyy年M月dd日");
                            break;
                    }
                }
            }
        }

        private static void GenerateData1(DataTable table)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IFont font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 15;

            font1.FontName = "宋体";
            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.SetFont(font1);
            style1.Alignment = HorizontalAlignment.CENTER;
            //style1.DataFormat=

            IFont font2 = hssfworkbook.CreateFont();
            font2.FontHeightInPoints = 12;
            font2.FontName = "宋体";
            ICellStyle style2 = hssfworkbook.CreateCellStyle();
            style2.SetFont(font2);
            style2.Alignment = HorizontalAlignment.CENTER;

            ICellStyle styleDate = hssfworkbook.CreateCellStyle();
            styleDate.SetFont(font2);
            //styleDate.Alignment = HorizontalAlignment.CENTER;
            styleDate.DataFormat = HSSFDataFormat.GetBuiltinFormat("yyyy年M月dd日");

            ICellStyle styleDouble = hssfworkbook.CreateCellStyle();
            styleDouble.SetFont(font2);
            //styleDouble.Alignment = HorizontalAlignment.CENTER;
            styleDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

            ICellStyle styleInt = hssfworkbook.CreateCellStyle();
            styleInt.SetFont(font2);
            //styleInt.Alignment = HorizontalAlignment.CENTER;
            styleInt.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");

            IRow headRow = sheet1.CreateRow(0);

            List<int> dates = new List<int>();
            List<int> doubles = new List<int>();
            List<int> ints = new List<int>();
            for (int col = 0; col < table.Columns.Count; col++)
            {
                string name = table.Columns[col].ColumnName;
                if (name.Contains("期"))
                    dates.Add(col);
                else if (name.Contains("价") || name.Contains("金额"))
                    doubles.Add(col);
                else if (name.Contains("数量"))
                    ints.Add(col);
                ICell headCell = headRow.CreateCell(col);
                headCell.SetCellValue(name);
                headCell.CellStyle = style1;
                sheet1.AutoSizeColumn(col);
            }
            
            int rowCount = table.Rows.Count;
            int columnCount = table.Columns.Count;
            for (int r = 0; r < rowCount; r++)
            {
                DataRow rowVal = table.Rows[r];
                IRow row = sheet1.CreateRow(r + 1);
                for (int c = 0; c < columnCount; c++)
                {
                    ICell cell = row.CreateCell(c);
                    DateTime time = DateTime.MinValue;
                    double dAmount = 0;
                    int quantity = 0;
                    string dateType = string.Empty;
                    if (dates.Contains(c))
                    {
                        if (DateTime.TryParse(rowVal[c].ToString(), out time))
                        {
                            cell.SetCellValue(Convert.ToDateTime(rowVal[c].ToString()));
                            dateType = "dateTime";
                        }
                        else
                            cell.SetCellValue(rowVal[c].ToString());
                    }
                    else if (doubles.Contains(c))
                    {
                        if (double.TryParse(rowVal[c].ToString(), out dAmount))
                        {
                            cell.SetCellValue(dAmount);
                            dateType = "double";
                        }
                        else
                            cell.SetCellValue(rowVal[c].ToString());
                    }
                    else if (ints.Contains(c))
                    {
                        if (int.TryParse(rowVal[c].ToString(), out quantity))
                        {
                            cell.SetCellValue(quantity);
                            dateType = "ints";
                        }
                        else
                            cell.SetCellValue(rowVal[c].ToString());
                    }
                    else
                        cell.SetCellValue(rowVal[c].ToString());

                    //cell.SetCellValue(ConvertUtil.ToNullableDateTime(rowVal[c]) == null ?
                    //    null : Convert.ToDateTime(rowVal[c]));

                    IDataFormat format = hssfworkbook.CreateDataFormat();
                    
                    
                    //cell.SetValueAndFormat
                    
                    switch (dateType)
                    {
                        case "dateTime": //cell.CellStyle.DataFormat = format.GetFormat("yyyy年M月dd日");
                            cell.CellStyle = styleDate;
                            break;
                        //case "double": //cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                        //    cell.CellStyle = styleDouble;
                        //    break;
                        //case "ints": //cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("123456");
                        //    cell.CellStyle = styleInt;
                        //    break;
                        default:
                            cell.CellStyle = style2;
                            break;
                    }
                }
            }
            
            
        }

        static void SetValueAndFormat(IWorkbook workbook, ICell cell, int value, short formatId)
        {
            cell.SetCellValue(value);
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.DataFormat = formatId;
            cell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 导出Excel带图片
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="imageColName">图片所占的列名</param>
        /// <param name="imageColSpan">图片所跨列数</param>
        private static void GenerateData(DataTable table, string imageColName, int imageColSpan)
        {
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IFont font1 = hssfworkbook.CreateFont();
            font1.FontHeightInPoints = 15;

            font1.FontName = "宋体";
            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.SetFont(font1);
            style1.Alignment = HorizontalAlignment.CENTER;
            
            IFont font2 = hssfworkbook.CreateFont();
            font2.FontHeightInPoints = 12;
            font2.FontName = "宋体";
            ICellStyle style2 = hssfworkbook.CreateCellStyle();
            style2.SetFont(font2);
            style1.Alignment = HorizontalAlignment.CENTER;

            IDataFormat format = hssfworkbook.CreateDataFormat();
            IRow headRow = sheet1.CreateRow(0);
            int imageCol = 0;
            for (int col = 0; col < table.Columns.Count; col++)
            {
                string name = table.Columns[col].ColumnName;
                if (name == imageColName)
                    imageCol = col;
                ICell headCell = headRow.CreateCell(col);
                headCell.SetCellValue(name);
                headCell.CellStyle = style1;
                sheet1.AutoSizeColumn(col);
            }

            int rowCount = table.Rows.Count;
            int columnCount = table.Columns.Count;
            for (int r = 0; r < rowCount; r++)
            {
                DataRow rowVal = table.Rows[r];
                IRow row = sheet1.CreateRow(r + 1);
                row.Height = short.Parse("1200");
                sheet1.SetColumnWidth(0, 105 * 60);
                sheet1.SetColumnWidth(imageCol, 40 * 60);
                for (int c = 0; c < columnCount; c++)
                {
                    ICell cell = row.CreateCell(c);
                    if (c == imageCol)
                    {
                        try
                        {
                            if (rowVal[imageCol] == null || string.IsNullOrEmpty(rowVal[imageCol].ToString()))
                                continue;
                            System.Drawing.Image img = System.Drawing.Image.FromFile(rowVal[imageCol].ToString());
                            byte[] bytes = ConvertUtil.GetBytes(img);
                            int pictureIdx = hssfworkbook.AddPicture(bytes, PictureType.JPEG);

                            // Create the drawing patriarch.  This is the top level container for all shapes. 
                            IDrawing patriarch = sheet1.CreateDrawingPatriarch();

                            //IClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 0, 6, 3, 9);
                            IClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, imageCol, r + 1,
                                imageCol + imageColSpan, r + 2);
                            IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
                        }
                        catch { }
                    }
                    else
                    {
                        //cell.SetCellValue(rowVal[c].ToString());
                        DateTime time = DateTime.MinValue;
                        bool temp = false;
                        if (DateTime.TryParse(rowVal[c].ToString(), out time))
                        {
                            cell.SetCellValue(Convert.ToDateTime(rowVal[c].ToString()));
                            temp = true;
                        }
                        else
                            cell.SetCellValue(rowVal[c].ToString());
                        cell.CellStyle = style2;
                        if (temp)
                        {
                            cell.CellStyle.DataFormat = format.GetFormat("yyyy年M月dd日");
                        }
                    }
                }
            }
        }

        private static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filePath">Excel文件完整路径</param>
        /// <param name="tableName">Excel表名（sheet名称）</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ExportFromExcel(string filePath, string tableName)
        {
            DataTable dt = new DataTable();

            bool isExcel2003 = filePath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase);
            string connectionString = string.Empty;
            string commandText = string.Format("select * from [{0}$]", tableName);

            if (isExcel2003)
                connectionString = excel2003ConnectionString;
            else
                connectionString = excel2007ConnectionString;

            connectionString = string.Format(connectionString, filePath);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, connection);
                adapter.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 导出CSV文件数据
        /// </summary>
        /// <param name="filePath">CSV文件完整路径</param>
        /// <returns>返回DataTable数据</returns>
        public static DataTable ExportFromCSV(string filePath)
        {
            DataTable dt = new DataTable();


            string newPath = Path.GetDirectoryName(filePath) + "\\" + Guid.NewGuid().ToString().Replace("-", "") + ".csv";
            string connectionString = string.Format(csvConnectionString,
                Path.GetDirectoryName(newPath));

            string commandText = string.Format("select * from [{0}]",
                Path.GetFileName(newPath));

            File.Move(filePath, newPath);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, connection);
                adapter.Fill(dt);
            }
            File.Move(newPath, filePath);

            return dt;
        }
    }
}
