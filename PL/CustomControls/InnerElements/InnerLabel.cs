using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class InnerLabel : InnerControl
    {
        private Point textLocation = Point.Empty;
        private bool textHorizontalPadding = false;
        private bool textVerticalPadding = false;
        private string text = "";

        private int horizontalPaddingAbsolute = 0; // valor em pixels para Absolute
        private int verticalPaddingAbsolute = 0;

        private TextVerticalAlignmentEnum verticalTextAlignment = TextVerticalAlignmentEnum.Center;
        private TextHorizontalAlignmentEnum horizontalTextAlignment = TextHorizontalAlignmentEnum.Left;

        private TextHorizontalPaddingEnum horizontalPaddingMode = TextHorizontalPaddingEnum.None;
        private TextVerticalPaddingEnum verticalPaddingMode = TextVerticalPaddingEnum.None;
        public string Text
        {
            get => text;
            set { text = value; AdjustControlSize(); }
        }        
        public bool TextVerticalPadding
        {
            get => textVerticalPadding;
            set { textVerticalPadding = value; AdjustTextLocation(); }
        }
        
        public bool TextHorizontalPadding
        {
            get => textHorizontalPadding;
            set { textHorizontalPadding = value; AdjustTextLocation(); }
        }
        public TextVerticalAlignmentEnum VerticalTextAlignment
        {
            get => verticalTextAlignment;
            set { verticalTextAlignment = value; AdjustTextLocation(); }
        }

        public TextHorizontalAlignmentEnum HorizontalTextAlignment
        {
            get => horizontalTextAlignment;
            set { horizontalTextAlignment = value; AdjustTextLocation(); }
        }
        public TextHorizontalPaddingEnum HorizontalPaddingMode
        {
            get => horizontalPaddingMode;
            set{ horizontalPaddingMode = value;AdjustTextLocation(); }
        }

        public TextVerticalPaddingEnum VerticalPaddingMode
        {
            get => verticalPaddingMode;
            set{ verticalPaddingMode = value; AdjustTextLocation(); }
        }

        public int HorizontalPaddingAbsolute
        {
            get => horizontalPaddingAbsolute;
            set{ 
                horizontalPaddingAbsolute = value;
                if (horizontalPaddingMode == TextHorizontalPaddingEnum.Absolute)
                    AdjustTextLocation();
            }
        }

        public int VerticalPaddingAbsolute
        {
            get => verticalPaddingAbsolute;
            set
            {
                verticalPaddingAbsolute = value;
                if (verticalPaddingMode == TextVerticalPaddingEnum.Absolute)
                    AdjustTextLocation();
            }
        }
        public enum TextVerticalAlignmentEnum
        {
            Top,
            Center,
            Bottom,
        }
        public enum TextHorizontalAlignmentEnum
        {
            Left,
            Center,
            Right
        }

        public enum TextHorizontalPaddingEnum
        {
            None,
            HalfFontWidth,
            OneFourthFontWidth,
            Absolute
        }
        public enum TextVerticalPaddingEnum
        {
            None,
            HalfFontHeight,
            OneFourthFontHeight,
            Absolute
        }
        
        public InnerLabel()
        {
            AdjustControlSize();
        }
  
        private void AdjustControlSize()
        {
            Size = NhegazSizeMethods.textExactSize(Text, Font);
            
        }
        public void RefreshLayout()
        {
            AdjustTextLocation(); // recalcula o ponto do texto baseado na altura atual
        }
        private void AdjustTextLocation()
        {
            Size textSize = NhegazSizeMethods.textExactSize(Text, Font);

            int textX = 0;
            int textY = 0;

            int fontUnitWidth = NhegazSizeMethods.textExactSize("0", Font).Width;
            int fontUnitHeight = NhegazSizeMethods.textExactSize("0", Font).Height;

            int horizontalPadding = 0;
            int verticalPadding = 0;

            switch (HorizontalPaddingMode)
            {
                case TextHorizontalPaddingEnum.None:
                    horizontalPadding = 0;
                    break;
                case TextHorizontalPaddingEnum.HalfFontWidth:
                    horizontalPadding = fontUnitWidth / 2;
                    break;
                case TextHorizontalPaddingEnum.OneFourthFontWidth:
                    horizontalPadding = fontUnitWidth / 4;
                    break;
                case TextHorizontalPaddingEnum.Absolute:
                    horizontalPadding = horizontalPaddingAbsolute;
                    break;
            }

            switch (VerticalPaddingMode)
            {
                case TextVerticalPaddingEnum.None:
                    verticalPadding = 0;
                    break;
                case TextVerticalPaddingEnum.HalfFontHeight: // provavelmente deveria ser HalfFontHeight
                    verticalPadding = fontUnitHeight / 2;
                    break;
                case TextVerticalPaddingEnum.OneFourthFontHeight:
                    verticalPadding = fontUnitHeight / 4;
                    break;
                case TextVerticalPaddingEnum.Absolute:
                    verticalPadding = verticalPaddingAbsolute;
                    break;
            }

            switch (HorizontalTextAlignment)
            {
                case TextHorizontalAlignmentEnum.Left:
                    textX = horizontalPadding;
                    break;
                case TextHorizontalAlignmentEnum.Center:
                    textX = (Size.Width - textSize.Width) / 2;
                    break;
                case TextHorizontalAlignmentEnum.Right:
                    textX = Size.Width - (textSize.Width + horizontalPadding);
                    break;
            }

            switch (VerticalTextAlignment)
            {
                case TextVerticalAlignmentEnum.Top:
                    textY = verticalPadding;
                    break;
                case TextVerticalAlignmentEnum.Center:
                    textY = (Size.Height - textSize.Height) / 2;
                    break;
                case TextVerticalAlignmentEnum.Bottom:
                    textY = Size.Height - (textSize.Height + verticalPadding);
                    break;
            }                           
            textLocation = new Point(textX, textY);
        }

        public override void OnPaint(CustomControl parent, PaintEventArgs e)
        {
            if (!Visible) return;

            using (SolidBrush brush = new SolidBrush(BackgroundColor))
            {
                Rectangle rect = new(Location, Size);
                e.Graphics.FillRectangle(brush, rect);
            }

            int textY = Location.Y + (Size.Height - NhegazSizeMethods.textExactSize(Text, Font).Height) / 2;

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                new Rectangle(Location.X + textLocation.X, Location.Y + textLocation.Y, Width - textLocation.X, Height - textLocation.Y),
                ForeColor,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.WordEllipsis
            );
        }
        
    }
}
