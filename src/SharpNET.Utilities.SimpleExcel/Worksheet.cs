using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.SimpleExcel
{
    public class Worksheet
    {
        protected IXLWorksheet _ws;

        protected int _currentRowNum = 1;
        protected Row _currentRow;

        internal Worksheet(IXLWorksheet ws)
        {
            _ws = ws;
            UpdateCurrentRow();
        }

        public IXLWorksheet Internal
        {
            get
            {
                return _ws;
            }
        }

        protected Row UpdateCurrentRow()
        {
            _currentRow = new SimpleExcel.Row(_ws.Row(_currentRowNum));
            return _currentRow;
        }
        public Row CurrentRow()
        {
            return _currentRow;
        }

        public Row NextRow()
        {
            _currentRowNum += 1;
            return UpdateCurrentRow();
        }

        public Row PrevRow()
        {
            _currentRowNum -= 1;
            if (_currentRowNum < 1) _currentRowNum = 1;
            return UpdateCurrentRow();
        }

        public Row Row(int rowNum)
        {
            _currentRowNum = rowNum;
            return UpdateCurrentRow();
        }

        public Row FirstRow()
        {
            return Row(1);
        }
    }
}
