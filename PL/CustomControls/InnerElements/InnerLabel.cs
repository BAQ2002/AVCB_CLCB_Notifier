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
        public string Text { get; set; } = "";
        public Font Font { get; set; }
        public Color ForeColor { get; set; } = SystemColors.ControlText;
        public Color BackgroundColor { get; set; } = SystemColors.Control;

        public enum TextAlignmentEnum
        {
            MiddleLeft,   
            MiddleCenter,
            MiddleRight,
            
        }

        public InnerLabel(CustomControl parent, string? text = "",
                                   Color? backgroundColor = null, Color? foreColor = null, Size? size = null)
        {
            if (text != "") Text = text;
            this.Font = parent.Font;
            this.ForeColor = parent.ForeColor;
            this.BackgroundColor = parent.BackgroundColor;
            
            if (backgroundColor.HasValue) BackgroundColor = backgroundColor.Value;
            if (foreColor.HasValue) ForeColor = foreColor.Value;

            if (size.HasValue) this.Size = size.Value;
            else AdjustControlSize();
        }

        private void AdjustControlSize()
        {
            Size = NhegazSizeMethods.textExactSize(Text, Font);
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
                new Point(Location.X, textY),
                ForeColor,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine
            );
        }
        
    }
}
