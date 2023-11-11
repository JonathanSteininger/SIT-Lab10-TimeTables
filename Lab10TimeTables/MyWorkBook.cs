using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Lab10TimeTables
{
    internal class MyWorkBook
    {
        public IWorkbook workbook;
        public List<ISheet> sheets;

        public string DefaultPath = "./";

        public MyWorkBook() {
            workbook = new XSSFWorkbook();
            sheets = new List<ISheet>();
        }
        public MyWorkBook(string FilePath)
        {
            workbook = Excel.ReadWorkBook(FilePath);
            sheets = new List<ISheet>();
        }
        public ISheet AddSheet(string Name)
        {
            ISheet sheet = workbook.CreateSheet(Name);
            sheets.Add(sheet);
            return sheet;
        }




        public void Save(string FileName, string Path, bool CancelIfFileExists)
        {
            if (FileName.Length == 0) throw new Exception("invalid filename. must be more thatn 0 characters");
            if (!FileName.Contains(".xlsx")) FileName += ".xlsx";
            FileName = Path + FileName;
            if (CancelIfFileExists && File.Exists(FileName)) throw new Exception("File already exists");
            if(!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            using (FileStream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
                stream.Close();
            }
        }
        public void Save(string FileName) => Save(FileName,DefaultPath, false); 
        public void Save(string FileName, string Path) => Save(FileName,Path, false); 
        public void Save(string FileName, bool CancelIfFileExists) => Save(FileName, DefaultPath, CancelIfFileExists); 
    }
}
