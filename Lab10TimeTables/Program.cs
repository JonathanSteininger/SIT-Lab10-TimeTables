using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using System.Diagnostics;

namespace Lab10TimeTables
{
    internal class Program
    {
        const string CONNECTION_STRING = "Server=localhost;Database=studenttimetable;Uid=student;Pwd=secret;";
        static DataBase _db; 

        static private MyWorkBook _workbook;
        static private Dictionary<int, Subject> _subjects;
        static void Main(string[] args)
        {
            _db = new DataBase(CONNECTION_STRING);
            _workbook = new MyWorkBook();
            _subjects = GetSubjects();
            List<Student> students = GetStudents();
            foreach(Student student in students)
            {
                CreateTimeTableSheet(ref _workbook, student);
            }
            string filename = "timetables3";
            _workbook.Save(filename);
        }


        private static Dictionary<int, Subject> GetSubjects()
        {
            List<object[]> temp = _db.Query("select * from tblsubjects;");
            Dictionary<int, Subject> subjects = new Dictionary<int, Subject>();
            foreach (object[] row in temp) {
                Subject subject = new Subject((int)row[0], (string)row[1]);
                subjects.Add(subject.Id, subject);
            }
            return subjects;
        }

        private static void CreateTimeTableSheet(ref MyWorkBook workbook, Student student)
        {
            ISheet sheet = workbook.AddSheet($"{student.FirstName} {student.LastName}");//adds new worksheet

            //queries for the subject ids the student takes.
            string SubjectQuery = $"select sid from tblenrolments where studentID = {student.ID}";
            int[] subjectIDs = Array.ConvertAll(_db.Query(SubjectQuery).ToArray(), (row) => (int)row[0]);

            //make the string for the subject query
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i <  subjectIDs.Length; i++)
            {
                sb.Append(subjectIDs[i]);
                if(i < subjectIDs.Length - 1) sb.Append(", ");
            }
            string timeTableQuery = $"select * from tbltimetable where sid IN ({sb});";
            List<object[]> timeTable = _db.Query(timeTableQuery);//queries the database
            //makes a list of all the fields for the new table.
            List<TimeTableField> fields = new List<TimeTableField>();
            foreach (object[] row in timeTable) fields.Add(new TimeTableField(_subjects[(int)row[1]], (int)row[2], int.Parse((string)row[3])));

            FillSheet(ref sheet, fields);
        }

        static private void FillSheet(ref ISheet sheet, List<TimeTableField> fields)
        {
            IRow row = sheet.CreateRow(0);
            FillRow(ref row, "", "Mon", "Tue", "Wed", "Thu", "Fri");
            for(int i = 1; i <= 5; i ++) FillTableRow(i, ref sheet, fields);
        }

        private static void FillTableRow(int period, ref ISheet sheet, List<TimeTableField> fields)
        {
            IRow row = sheet.CreateRow(period);
            ICell cell1 = row.CreateCell(0);
            cell1.SetCellValue(period);
            for(int day = 1; day <= 5; day++)
            {
                ICell cell = row.CreateCell(day);
                int index = fields.FindIndex((field) => field.Period == period && field.day == day);
                if (index < 0) { cell.SetBlank(); continue; }
                cell.SetCellValue(fields[index].ToString());
            }
        }

        static private void FillRow(ref IRow row, params string[] values)
        {
            for(int i = 0; i < values.Length; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(values[i]);
            }
        }

        static List<Student> GetStudents()
        {
            List<object[]> list = _db.Query("Select * from tblstudents;");
            List<Student> students = new List<Student>();
            foreach (object[] row in list)
            {
                students.Add(new Student((int)row[0], (string)row[1], (string)row[2]));
            }
            return students;
        }
    }
}
