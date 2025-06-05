using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AVBC_CLCB_Notifier.PL.CustomControls.InnerLabel;
using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System.Drawing.Drawing2D;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class InnerLabel : InnerControl
    {
        private Point textLocation = Point.Empty;

        private string text = "";
    
        private TextVerticalAlignment textVerticalAlignment = TextVerticalAlignment.Center;
        private TextHorizontalAlignment textHorizontalAlignment = TextHorizontalAlignment.Left;

        private HorizontalPaddingMode horizontalPaddingMode = HorizontalPaddingMode.None;
        private VerticalPaddingMode verticalPaddingMode = VerticalPaddingMode.None;
        public bool ApplyHorizontalPaddingWhenCentered { get; set; } = false;
        public bool ApplyVerticalPaddingWhenCentered { get; set; } = false;
        public string Text
        {
            get => text;
            set { text = value; Size = NhegazSizeMethods.TextExactSize(Text, Font); }
        }
        public override Font Font
        {
            get => base.Font;
            set
            { base.Font = value; Size = NhegazSizeMethods.TextExactSize(Text, Font); }
        }
        public TextHorizontalAlignment TextHorizontalAlignment
        {
            get => textHorizontalAlignment;
            set { textHorizontalAlignment = value; AdjustTextLocation(); }
        }
        public TextVerticalAlignment TextVerticalAlignment
        {
            get => textVerticalAlignment;
            set { textVerticalAlignment = value; AdjustTextLocation(); }
        }     
        public HorizontalPaddingMode HorizontalPaddingMode
        {
            get => horizontalPaddingMode;
            set{ horizontalPaddingMode = value; AdjustTextLocation(); }
        }
        public VerticalPaddingMode VerticalPaddingMode
        {
            get => verticalPaddingMode;
            set{ verticalPaddingMode = value; AdjustTextLocation(); }
        }
      
        public InnerLabel() : base()
        {
        }
  
        protected override void AdjustControlSize()
        {           
            base.AdjustControlSize();
        }

        protected override void SymmetricalCircleAdjust()
        {
            base.SymmetricalCircleAdjust();

            TextHorizontalAlignment = TextHorizontalAlignment.Center;
            TextVerticalAlignment = TextVerticalAlignment.Center;
        }
        public override void Update()
        {
            AdjustTextLocation(); // recalcula o ponto do texto baseado na altura atual
        }

        private (int horizontalPadding, int verticalPadding) PaddingModeCaser()        
        {
            int horizontalPadding = 0;
            int verticalPadding = 0;

            int fontUnitWidth = NhegazSizeMethods.TextExactSize("0", Font).Width;
            int fontUnitHeight = NhegazSizeMethods.TextExactSize("0", Font).Height;

            switch (HorizontalPaddingMode)
            {
                case HorizontalPaddingMode.None:
                    horizontalPadding = 0;
                    break;
                case HorizontalPaddingMode.HalfFontWidth:
                    horizontalPadding = fontUnitWidth / 2;
                    break;
                case HorizontalPaddingMode.OneFourthFontWidth:
                    horizontalPadding = fontUnitWidth / 4;
                    break;
                case HorizontalPaddingMode.Absolute:
                    horizontalPadding = (TextHorizontalAlignment == TextHorizontalAlignment.Left) ? Padding.Left : Padding.Right;
                    break;
            }

            switch (VerticalPaddingMode)
            {
                case VerticalPaddingMode.None:
                    verticalPadding = 0;
                    break;
                case VerticalPaddingMode.HalfFontHeight: // provavelmente deveria ser HalfFontHeight
                    verticalPadding = fontUnitHeight / 2;
                    break;
                case VerticalPaddingMode.OneFourthFontHeight:
                    verticalPadding = fontUnitHeight / 4;
                    break;
                case VerticalPaddingMode.Absolute:
                    verticalPadding = (TextVerticalAlignment == TextVerticalAlignment.Top) ? Padding.Top : Padding.Bottom;
                    break;
            }
            return (horizontalPadding, verticalPadding);
        }
        private void AdjustTextLocation()
        {
            Size textSize = NhegazSizeMethods.TextExactSize(Text, Font);

            int textX = 0; int horizontalPadding = 0;
            int textY = 0; int verticalPadding = 0;

            int fontUnitWidth = NhegazSizeMethods.TextExactSize("0", Font).Width;
            int fontUnitHeight = NhegazSizeMethods.TextExactSize("0", Font).Height;
           
            switch (HorizontalPaddingMode)
            {
                case HorizontalPaddingMode.None:
                    horizontalPadding = 0;
                    break;
                case HorizontalPaddingMode.HalfFontWidth:
                    horizontalPadding = fontUnitWidth / 2;
                    break;
                case HorizontalPaddingMode.OneFourthFontWidth:
                    horizontalPadding = fontUnitWidth / 4;
                    break;
                case HorizontalPaddingMode.Absolute:
                    horizontalPadding = (TextHorizontalAlignment == TextHorizontalAlignment.Left) ? Padding.Left : Padding.Right;
                    break;
            }

            switch (VerticalPaddingMode)
            {
                case VerticalPaddingMode.None:
                    verticalPadding = 0;
                    break;
                case VerticalPaddingMode.HalfFontHeight:
                    verticalPadding = fontUnitHeight / 2;
                    break;
                case VerticalPaddingMode.OneFourthFontHeight:
                    verticalPadding = fontUnitHeight / 4;
                    break;
                case VerticalPaddingMode.Absolute:
                    verticalPadding = (TextVerticalAlignment == TextVerticalAlignment.Top) ? Padding.Top : Padding.Bottom;
                    break;
            }

            switch (TextHorizontalAlignment)
            {
                case TextHorizontalAlignment.Left:
                    textX = horizontalPadding;
                    break;
                case TextHorizontalAlignment.Center:
                    textX = (Size.Width - textSize.Width) / 2;
                    if (ApplyHorizontalPaddingWhenCentered)
                        textX += (Padding.Left - Padding.Right) / 2;
                    break;
                case TextHorizontalAlignment.Right:
                    textX = Size.Width - (textSize.Width + horizontalPadding);
                    break;
            }

            switch (TextVerticalAlignment)
            {
                case TextVerticalAlignment.Top:
                    textY = verticalPadding;
                    break;
                case TextVerticalAlignment.Center:
                    textY = (Size.Height - textSize.Height) / 2;
                    if (ApplyVerticalPaddingWhenCentered)
                        textY += (Padding.Top - Padding.Bottom) / 2;
                    break;
                case TextVerticalAlignment.Bottom:
                    textY = Size.Height - (textSize.Height + verticalPadding);
                    break;
            }                           
            textLocation = new Point(textX, textY);
        }

        public override void OnPaint(CustomControl parent, PaintEventArgs e)
        {
            base.OnPaint(parent, e);

            int textY = Location.Y + (Size.Height - NhegazSizeMethods.TextExactSize(Text, Font).Height) / 2;
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
