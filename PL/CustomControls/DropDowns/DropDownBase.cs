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
    
    public class DropDownMonth : DropDownDateBase
    {
        class MonthItem
        {
            public int Month { get; set; }
            public string MonthText { get; set; }
        }

        List<MonthItem> MonthList = new List<MonthItem>();

        int NumberOfRows;
        int ItemsPerRow;
        public DropDownMonth(CustomControl control) : base(control)
        {
            NumberOfRows = 4;
            ItemsPerRow = 3;            

            MonthList.Add(new MonthItem { Month = 1, MonthText = "Jan" }); MonthList.Add(new MonthItem { Month = 2, MonthText = "Fev" });
            MonthList.Add(new MonthItem { Month = 3, MonthText = "Mar" }); MonthList.Add(new MonthItem { Month = 4, MonthText = "Abri" });
            MonthList.Add(new MonthItem { Month = 5, MonthText = "Mai" }); MonthList.Add(new MonthItem { Month = 6, MonthText = "Jun" });
            MonthList.Add(new MonthItem { Month = 7, MonthText = "Jul" }); MonthList.Add(new MonthItem { Month = 8, MonthText = "Ago" });
            MonthList.Add(new MonthItem { Month = 9, MonthText = "Set" }); MonthList.Add(new MonthItem { Month = 10, MonthText = "Out" });
            MonthList.Add(new MonthItem { Month = 11, MonthText = "Nov" }); MonthList.Add(new MonthItem { Month = 12, MonthText = "Dez" });

            AdjustControlSize();
        }

        protected override void OnLabelClick(int index)
        {
            var item = MonthList[index];

            if (parentControl is CustomDatePicker dp)
                dp.selectedMonth.Text = item.Month.ToString("D2");

            this.Parent?.Controls.Remove(this);
        }

        protected override void AdjustControlSize()
        {
            base.AdjustControlSize();

            this.Controls.Clear();

            int itemsPerRow = ItemsPerRow;
            if (MonthList == null || MonthList.Count == 0 || itemsPerRow <= 0)
                return;

            
            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int itemHeight = textExactSize("000", this.Font).Height;
            int itemWidth = textExactSize("000", this.Font).Width;

            int totalItems = MonthList.Count;
            int numRows = (int)Math.Ceiling((double)totalItems / itemsPerRow);

            Width = xPadding + (itemsPerRow * (itemWidth + xPadding));
            Height = yPadding + (numRows * (itemHeight + yPadding));

            for (int i = 0; i < totalItems; i++)
            {
                int row = i / itemsPerRow;
                int column = i % itemsPerRow;

                int x = xPadding + (column * (itemWidth + xPadding));
                int y = itemHeight + (2 * yPadding) + (row * (itemHeight + yPadding));

                CustomLabel lbl = CreateDateLabel(i, MonthList[i].MonthText, x, y, itemWidth, itemHeight);
                //lbl.ForeColor = DayItemList[i].IsCurrentMonth ? ForeColor : SecondaryForeColor;
                //lbl.Location = new Point();
                this.Controls.Add(lbl);
            }
        }
    }
    
    


}
