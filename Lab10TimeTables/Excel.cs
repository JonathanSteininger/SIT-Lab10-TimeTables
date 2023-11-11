using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.OpenXmlFormats.Dml;
using NPOI.SS.UserModel;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Lab10TimeTables
{
    static class Excel
    {
        public static IWorkbook ReadWorkBook(string filepath)
        {
            if (!File.Exists(filepath)) throw new FileNotFoundException();
            using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read)){
                IWorkbook book =  WorkbookFactory.Create(stream);
                stream.Close();
                return book;
            }
        }

        public static object GetValue<T>(IRow row, int CellPosition)
        {
            if(typeof(T) == typeof(string)) return GetString(row, CellPosition);
            if(typeof(T) == typeof(double)) return GetDouble(row, CellPosition);
            if(typeof(T) == typeof(DateTime)) return GetDate(row, CellPosition);
            if(typeof(T) == typeof(bool)) return GetBoolean(row, CellPosition);
            throw new Exception("invalid format: GetValeu<T>, T can only be (string, double, DateTime, bool)");
        }

        public static double GetDouble(IRow row, int CellPosition)
        {
            if (row == null || CellPosition > row.LastCellNum || CellPosition < row.FirstCellNum) return -1f;
            ICell cell = row.Cells[CellPosition];
            if (cell == null) return -1f;
            return cell.NumericCellValue;
        }
        public static string GetString(IRow row, int CellPosition)
        {
            if (row == null || CellPosition > row.LastCellNum || CellPosition < row.FirstCellNum) return null;
            ICell cell = row.Cells[CellPosition];
            if (cell == null) return null;
            return cell.StringCellValue;
        }
        public static DateTime GetDate(IRow row, int CellPosition)
        {
            if (row == null || CellPosition > row.LastCellNum || CellPosition < row.FirstCellNum) return DateTime.MinValue;
            ICell cell = row.Cells[CellPosition];
            if (cell == null) return DateTime.MinValue;
            return cell.DateCellValue;
        }
        public static bool GetBoolean(IRow row, int CellPosition)
        {
            if (row == null || CellPosition > row.LastCellNum || CellPosition < row.FirstCellNum) throw new Exception("Boolean Excel cell's row is null");
            ICell cell = row.Cells[CellPosition];
            if (cell == null) throw new Exception("Boolean Excel cell null");
            return cell.BooleanCellValue;
        }
    }
}
