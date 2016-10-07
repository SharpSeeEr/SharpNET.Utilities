using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.SimpleExcel
{
    public class Row
    {
        protected IXLRow _row;

        protected int _startingColNum = 1;
        protected int _currentColNum = 1;
        protected Cell _currentCell;

        internal Row(IXLRow row)
        {
            _row = row;
            _currentCell = new Cell(_row.Cell(_currentColNum));
        }

        public IXLRow Internal
        {
            get
            {
                return _row;
            }
        }

        protected Cell UpdateCurrentCell()
        {
            if (_currentColNum < 1) _currentColNum = 1;
            _currentCell = new SimpleExcel.Cell(_row.Cell(_currentColNum));
            return _currentCell;
        }

        public Cell CurrentCell()
        {
            return _currentCell;
        }

        public Cell NextCell()
        {
            _currentColNum += 1;
            return UpdateCurrentCell();
        }

        public Cell PrevCell()
        {
            _currentColNum -= 1;
            return UpdateCurrentCell();
        }

        public Cell FirstCell()
        {
            return Cell(1);
        }

        public Cell Cell(int colNum)
        {
            _currentColNum = colNum;
            return UpdateCurrentCell();
        }

        public Row Bold(bool state = true)
        {
            _row.Style.Font.Bold = state;
            return this;
        }

        public Row Italic(bool state = true)
        {
            _row.Style.Font.Italic = state;
            return this;
        }

        public Row Currency()
        {
            _row.Style.NumberFormat.SetNumberFormatId(7);
            return this;
        }

        public Row Font(string fontName)
        {
            _row.Style.Font.FontName = fontName;
            return this;
        }

        public Row FontSize(double size)
        {
            _row.Style.Font.FontSize = size;
            return this;
        }

        public Row Color(XLColor color)
        {
            _row.Style.Font.FontColor = color;
            return this;
        }

        public Row BgColor(XLColor color)
        {
            _row.Style.Fill.BackgroundColor = color;
            return this;
        }

        public Row WrapText(bool state = true)
        {
            _row.Style.Alignment.WrapText = state;
            return this;
        }

        public Row AdjustToContents()
        {
            _row.AdjustToContents();
            return this;
        }

        public Row Border(XLBorderStyleValues style, XLColor color)
        {
            _row.Style
                .Border.SetOutsideBorder(style)
                .Border.SetOutsideBorderColor(color);
            return this;
        }

        public Row BorderTop(XLBorderStyleValues style, XLColor color)
        {
            _row.Style
                .Border.SetTopBorder(style)
                .Border.SetTopBorderColor(color);
            return this;
        }

        public Row BorderRight(XLBorderStyleValues style, XLColor color)
        {
            _row.Style
                .Border.SetRightBorder(style)
                .Border.SetRightBorderColor(color);
            return this;
        }

        public Row BorderBottom(XLBorderStyleValues style, XLColor color)
        {
            _row.Style
                .Border.SetBottomBorder(style)
                .Border.SetBottomBorderColor(color);
            return this;
        }

        public Row BorderLeft(XLBorderStyleValues style, XLColor color)
        {
            _row.Style
                .Border.SetLeftBorder(style)
                .Border.SetLeftBorderColor(color);
            return this;
        }
    }
}
