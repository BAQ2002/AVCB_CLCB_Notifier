using AVBC_CLCB_Notifier.PL.Templates;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System.Diagnostics;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public abstract class DropDownDateBase : CustomControl
    {
        protected int hoveredIndex = -1;
        protected Color itemFocusColor;
        protected CustomControl parentControl;      
        protected StringCollection itemList = new StringCollection();

        public DropDownDateBase(CustomControl parentControl)
        {
            this.parentControl = parentControl;
            BorderRadius = parentControl.BorderRadius;
            BorderWidth = parentControl.BorderWidth;
            BorderColor = parentControl.BorderColor;
            itemFocusColor = parentControl.OnFocusBorderColor;
            BackgroundColor = parentControl.BackgroundColor;
            Width = parentControl.Width;
            HorizontalPadding = parentControl.HorizontalPadding;
            VerticalPadding = parentControl.VerticalPadding;
            HeaderBackgroundColor = parentControl.HeaderBackgroundColor;

            MinimumSize = new Size(5, 5);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            TabStop = true;
            ForeColor = parentControl.ForeColor;
            Font = parentControl.Font;
        }

        protected CustomLabel CreateDateLabel(int index, string text, int x, int y, int width, int height)
        {
            CustomLabel lbl = new CustomLabel()
            {
                Name = $"Item{index}",
                Text = text,
                Font = Font,
                Location = new Point(x, y),
                Width = width,
                Height = height,
                BackgroundColor = BackgroundColor,
                ForeColor = ForeColor,                
            };

            lbl.MouseEnter += (s, e) =>
            {
                hoveredIndex = index;
                lbl.ForeColor = BackgroundColor;
                lbl.BackgroundColor = OnFocusBorderColor;
                Invalidate();
            };

            lbl.MouseLeave += (s, e) =>
            {
                if (hoveredIndex == index) hoveredIndex = -1;
                lbl.ForeColor = ForeColor;
                lbl.BackgroundColor = BackgroundColor;
                Invalidate();
            };

            lbl.Click += (s, e) => OnLabelClick(index);
            return lbl;
        }
        protected Size textExactSize(string text, Font font)
        {
            Size size = TextRenderer.MeasureText(
            text,
            font,
            new Size(int.MaxValue, int.MaxValue),
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine
            );

            return size;
        }
        protected virtual void OnLabelClick(int index) { }
    }
    
    
    
    


}
