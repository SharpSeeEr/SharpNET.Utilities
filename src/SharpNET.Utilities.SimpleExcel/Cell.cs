using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.SimpleExcel
{
    public class Cell
    {
        protected IXLCell _cell;

        internal Cell(IXLCell cell)
        {
            _cell = cell;
        }

        public IXLCell Internal
        {
            get
            {
                return _cell;
            }
        }

        public Cell Value<T>(T value)
        {
            _cell.SetValue(value);
            return this;
        }

        public Cell Left()
        {
            _cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            return this;
        }

        public Cell Center()
        {
            _cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            return this;
        }

        public Cell Right()
        {
            _cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            return this;
        }

        public Cell Bold(bool state = true)
        {
            _cell.Style.Font.Bold = state;
            return this;
        }

        public Cell Italic(bool state = true)
        {
            _cell.Style.Font.Italic = state;
            return this;
        }

        public Cell Currency()
        {
            _cell.Style.NumberFormat.SetNumberFormatId(8);
            return this;
        }

        public Cell Font(string fontName)
        {
            _cell.Style.Font.FontName = fontName;
            return this;
        }

        public Cell FontSize(double size)
        {
            _cell.Style.Font.FontSize = size;
            return this;
        }

        public Cell Color(XLColor color)
        {
            _cell.Style.Font.FontColor = color;
            return this;
        }

        public Cell BgColor(XLColor color)
        {
            _cell.Style.Fill.BackgroundColor = color;
            return this;
        }

        public Cell WrapText(bool state = true)
        {
            _cell.Style.Alignment.WrapText = state;
            return this;
        }

        public Cell Border(XLBorderStyleValues style, XLColor color)
        {
            _cell.Style
                .Border.SetOutsideBorder(style)
                .Border.SetOutsideBorderColor(color);
            return this;
        }

        public Cell BorderTop(XLBorderStyleValues style, XLColor color)
        {
            _cell.Style
                .Border.SetTopBorder(style)
                .Border.SetTopBorderColor(color);
            return this;
        }

        public Cell BorderRight(XLBorderStyleValues style, XLColor color)
        {
            _cell.Style
                .Border.SetRightBorder(style)
                .Border.SetRightBorderColor(color);
            return this;
        }

        public Cell BorderBottom(XLBorderStyleValues style, XLColor color)
        {
            _cell.Style
                .Border.SetBottomBorder(style)
                .Border.SetBottomBorderColor(color);
            return this;
        }

        public Cell BorderLeft(XLBorderStyleValues style, XLColor color)
        {
            _cell.Style
                .Border.SetLeftBorder(style)
                .Border.SetLeftBorderColor(color);
            return this;
        }
    }
}
