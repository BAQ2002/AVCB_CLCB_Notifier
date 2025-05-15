using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class InnerLabel : Control
    {
        private string text;
        private Color backgroundColor;

        public override string Text
        {
            get => text;
            set
            {
                text = value ?? "";
                AdjustControlSize();
                Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; Invalidate(); }
        }

        public InnerLabel()
        {
            AdjustControlSize();
        }

        private void AdjustControlSize()
        {
            Size = TextRenderer.MeasureText(
                text,
                Font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            int textX = (Width - textExactSize(text, Font).Width) / 2;

            TextRenderer.DrawText(
                g,
                text,
                Font, 
                new Point(textX, 0), 
                this.ForeColor,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine
            );
        }

        private Size textExactSize(string text, Font font)
        {
            Size size = TextRenderer.MeasureText(
                text,
                font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine
            );

            return size;
        }
    }
}
