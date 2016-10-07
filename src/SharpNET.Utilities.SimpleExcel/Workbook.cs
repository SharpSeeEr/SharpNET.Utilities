using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.SimpleExcel
{
    public class Workbook
    {
        public static readonly string WorkbookMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        protected XLWorkbook _wb;

        protected int _currentWorksheetIndex;
        protected Worksheet _currentWorksheet;

        protected int _autoNameIndex = 1;

        public Workbook()
        {
            _wb = new XLWorkbook();
            
        }

        public XLWorkbook Internal
        {
            get
            {
                return _wb;
            }
        }

        public string MimeType
        {
            get
            {
                return WorkbookMimeType;
            }
        }

        protected Worksheet UpdateCurrentWorksheet()
        {
            if (_currentWorksheetIndex < 1) _currentWorksheetIndex = 1;
            else if (_wb.Worksheets.Count > _currentWorksheetIndex)
            {
                _wb.AddWorksheet(NextAutoName());
                _currentWorksheetIndex = _wb.Worksheets.Count;
            }
            _currentWorksheet = new Worksheet(_wb.Worksheet(_currentWorksheetIndex));
            return _currentWorksheet;
        }

        public Worksheet CurrentSheet()
        {
            if (_currentWorksheet == null) AddSheet("Sheet" + _currentWorksheetIndex); 
            return _currentWorksheet;
        }

        protected string NextAutoName()
        {
            return "Sheet" + _autoNameIndex++;
        }

        public Worksheet AddSheet(string name = null)
        {
            _wb.AddWorksheet(name ?? NextAutoName());
            _currentWorksheetIndex = _wb.Worksheets.Count;
            return UpdateCurrentWorksheet();
        }

        public Worksheet NextSheet()
        {
            _currentWorksheetIndex += 1;
            return UpdateCurrentWorksheet();
        }

        public Worksheet PrevSheet()
        {
            _currentWorksheetIndex -= 1;
            return UpdateCurrentWorksheet();
        }

        public Worksheet FirstSheet()
        {
            _currentWorksheetIndex = 1;
            return UpdateCurrentWorksheet();
        }

        public Worksheet LastSheet()
        {
            _currentWorksheetIndex = _wb.Worksheets.Count;
            return UpdateCurrentWorksheet();
        }

        public void Save()
        {
            _wb.Save();
        }

        public void SaveAs(string file)
        {
            _wb.SaveAs(file);
        }

        public void SaveAs(System.IO.Stream stream)
        {
            _wb.SaveAs(stream);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                _wb.SaveAs(ms);
                return ms.ToArray();
            }
        }
    }
}
