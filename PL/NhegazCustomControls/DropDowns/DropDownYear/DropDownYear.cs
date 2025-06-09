using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class DropDownYear : DropDownDateBase
    {
        class YearItemLabel : InnerLabel
        {
            private int year;
            public int Year
            {
                get => year;
                set
                {
                    year = value;
                    Text = year.ToString(); // sem chamar Size de novo
                }
            }
        }

        private InnerLabel DecadeLabel = new();
        private InnerButton BackwardIcon = new(ButtonIcon.Backward, BackGroundShape.SymmetricCircle);
        private InnerLabel ForwardIcon = new();
        private YearItemLabel[,] YearItemsLabels;

        private int CurrentDecade;
        private int DecadeLastYear;
        
        private int NumberOfRows = 2;
        private int NumberOfColumns = 5;

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                BackwardIcon.Font = value;
                ForwardIcon.Font = value;
                DecadeLabel.Font = value;
                AdjustControlSize();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                BackwardIcon.ForeColor = value;
                ForwardIcon.ForeColor = value;
                DecadeLabel.ForeColor = value;
                Invalidate();
            }
        }

        public DropDownYear(CustomControl control) : base(control)
        {           
            if(parentControl is CustomDatePicker dp)
            {
                CurrentDecade = (DateTime.Now.Year / 10) * 10;
                DecadeLastYear = CurrentDecade + 9;

                ForwardIcon.Text = "▶";
                DecadeLabel.Text = $"{CurrentDecade} - {DecadeLastYear}";
                DecadeLabel.ReSizeBasedOnText = false;

                BackwardIcon.Click += (s, e) => { ChangeDecade(-10); Invalidate(); };
                ForwardIcon.Click += (s, e) => { ChangeDecade(10); Invalidate(); };
                BackwardIcon.DoubleClick += (s, e) => { ChangeDecade(-20); Invalidate(); };
                ForwardIcon.DoubleClick += (s, e) => { ChangeDecade(20); Invalidate(); };

                InnerControls.Add(BackwardIcon);
                InnerControls.Add(ForwardIcon);
                InnerControls.Add(DecadeLabel);

                CreateYearLabels();
                AdjustControlSize();
            }           
        }

        private void CreateYearLabels()
        {
            YearItemsLabels = new YearItemLabel[NumberOfRows, NumberOfColumns];

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    var yearItemLabel = new YearItemLabel
                    {
                        Font = Font,
                        ForeColor = ForeColor,
                        BackgroundColor = BackgroundColor,
                        BackGroundShape = BackGroundShape.SymmetricCircle,
                        ReSizeBasedOnText = false
                    };

                    int capturedRow = row;
                    int capturedCol = col;

                    yearItemLabel.MouseEnter += (s, e) =>
                    {
                        yearItemLabel.ForeColor = BackgroundColor;
                        yearItemLabel.BackgroundColor = OnFocusBorderColor;
                        Invalidate();
                    };
                    yearItemLabel.MouseLeave += (s, e) =>
                    {
                        yearItemLabel.ForeColor = ForeColor;
                        yearItemLabel.BackgroundColor = BackgroundColor;
                        Invalidate();
                    };
                    yearItemLabel.Click += (s, e) => OnYearLabelClick(capturedRow, capturedCol);

                    YearItemsLabels[row, col] = yearItemLabel;
                    InnerControls.Add(yearItemLabel);
                }
            }
            UpdateYearLabels(CurrentDecade);
        }

        private void ChangeDecade(int offset)
        {
            CurrentDecade += offset;
            DecadeLastYear = CurrentDecade + 9;
            DecadeLabel.Text = $"{CurrentDecade} - {DecadeLastYear}";
            UpdateYearLabels(CurrentDecade);
        }
        private void UpdateYearLabels(int currentDecade)
        {
            int index = 0;

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    int year = currentDecade + index;
                    YearItemLabel yearItemLabel = YearItemsLabels[row, col];                                    
                    yearItemLabel.Year = year;
                    index++;
                }
            }
        }
        protected void OnYearLabelClick(int row, int col)
        {
            if (parentControl is CustomDatePicker dp)
            {
                dp.selectedYear.Text = YearItemsLabels[row, col].Year.ToString();
                Parent?.Controls.Remove(this);
            }
        }

        protected override void AdjustControlSize()
        {
            AdjustPadding();

            if (YearItemsLabels == null || YearItemsLabels.Length == 0 || NumberOfColumns <= 0 || NumberOfRows <= 0)
                return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int yearItemSize = NhegazSizeMethods.TextProportionalSize("0000", Font, 1.3f).Width;
            int headerHeight = NhegazSizeMethods.TextExactSize("0000", Font).Height;

            int startY = yPadding + headerHeight + yPadding;

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    int x = xPadding + col * (yearItemSize + xPadding);
                    int y = startY + row * (yearItemSize + yPadding);

                    AdjustInnerSizes(row, col, yearItemSize);
                    AdjustInnerLocations(row, col, x, y);
                }
            }

            Width = xPadding + (NumberOfColumns * (yearItemSize + xPadding));
            Height = startY + (NumberOfRows * (yearItemSize + yPadding));
            AdjustInnerSizes();
            AdjustInnerLocations();
        }
        protected override void AdjustInnerSizes()
        {
            BackwardIcon.Width = NhegazSizeMethods.TextExactSize("00", Font).Height;
        }
        protected override void AdjustInnerSizes(int row, int col, int itemSize)
        {
            var label = YearItemsLabels[row, col];
            label.Width = itemSize;
            label.Height = itemSize;
        }
        protected override void AdjustInnerLocations()
        {
            BackwardIcon.Location = new Point(HorizontalPadding, VerticalPadding);
            ForwardIcon.Location = new Point(Width - (ForwardIcon.Width + HorizontalPadding), VerticalPadding);
            DecadeLabel.Location = new Point((Width - DecadeLabel.Width) / 2, VerticalPadding);
        }
        protected override void AdjustInnerLocations(int row, int col, int x, int y)
        {
            var label = YearItemsLabels[row, col];
            label.Location = new Point(x, y);
        }



    }
}
