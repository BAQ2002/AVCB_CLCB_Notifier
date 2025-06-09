using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers.Text;
using System.Drawing.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class DropDownDay : DropDownDateBase
    {
        class DayItemLabel : InnerLabel
        {
            private int day;
            public int Day
            {
                get => day;
                set
                {
                    day = value;
                    Text = day.ToString(); // Atualiza a visualização
                }
            }

            public bool IsCurrentMonth { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
        }

        private int NumberOfRows;
        private int NumberOfColumns;

        private int CurrentMonth;
        private int CurrentYear;

        private InnerLabel MonthLabel = new();
        private InnerButton BackwardIcon = new(ButtonIcon.Backward, BackGroundShape.SymmetricCircle); //Label&&Button para passar para a década anteriror
        private InnerButton ForwardIcon = new(ButtonIcon.Forward, BackGroundShape.SymmetricCircle);

        private DayItemLabel[,] DayItemLabels; //Matriz composta pelos Labels de dias.
        private InnerLabel[] HeaderLabels; //Matriz composta pelos Labels do cabecalho de dias da semana.

        private string[] MonthTexts =  {"null", "Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho",
                                         "Julho", "Agosto", "Setembro", "Outubro", "Novembro","Dezembro" };

        public override Font Font
        {
            get => base.Font;
            set 
            { 
                base.Font = value; 
                MonthLabel.Font = new Font(value, FontStyle.Bold); ForwardIcon.Font = value; 
                BackwardIcon.Font = value; 
                AdjustControlSize(); 
            }
        }

        public override Color ForeColor 
        {
            get => base.ForeColor;
            set 
            { 
                base.ForeColor = value; 
                MonthLabel.ForeColor = value; ForwardIcon.ForeColor = value; BackwardIcon.ForeColor = value; 
                Invalidate(); 
            }
        }
        public override Color HeaderBackgroundColor
        {
            get => base.HeaderBackgroundColor;
            set { base.HeaderBackgroundColor = value; MonthLabel.BackgroundColor = Color.Transparent; ForwardIcon.BackgroundColor = value; BackwardIcon.BackgroundColor = value; Invalidate(); }
        }

        public DropDownDay(CustomControl control) : base(control)
        {            
            if (parentControl is CustomDatePicker dp)
            {
                NumberOfRows = 6;
                NumberOfColumns = 7;

                CurrentMonth = int.Parse(dp.selectedMonth.Text);
                CurrentYear = int.Parse(dp.selectedYear.Text);
                
                InnerControls.Add(BackwardIcon);
                BackwardIcon.Click += (s, e) => { ChangeMonth(-1); Invalidate(); };
                BackwardIcon.DoubleClick += (s, e) => { ChangeMonth(-2); Invalidate(); };

                InnerControls.Add(ForwardIcon);
                ForwardIcon.Click += (s, e) => { ChangeMonth(1); Invalidate(); };
                ForwardIcon.DoubleClick += (s, e) => { ChangeMonth(2); Invalidate(); };

                InnerControls.Add(MonthLabel);
                MonthLabel.Text = MonthTexts[CurrentMonth];

                SecondaryForeColor = Color.FromArgb((ForeColor.R + 255) / 2, (ForeColor.G + 255) / 2, (ForeColor.B + 255) / 2);

                CreateHeaderLabels();
                CreateDayItems();               
                AdjustControlSize();
            }
        }

        /// <summary>
        /// Método responsavel por criar os HeaderLabels.
        /// </summary>
        protected void CreateHeaderLabels()
        {
            string[] weekDays = { "D", "S", "T", "Q", "Q", "S", "S" };
            HeaderLabels = new InnerLabel[weekDays.Length];

            for (int i = 0; i < weekDays.Length; i++)
            {
                var headerLabel = new InnerLabel
                {
                    Text = weekDays[i],
                    Font = new Font(Font, FontStyle.Bold),
                    BackgroundColor = BackgroundColor,
                    BackGroundShape = BackGroundShape.SymmetricCircle
                };
                InnerControls.Add(headerLabel);
                HeaderLabels[i] = headerLabel;
            }
        }

        /// <summary>
        /// Método responsavel por criar os DayItemsLabels. 
        /// </summary>
        protected void CreateDayItems()
        {
            DayItemLabels = new DayItemLabel[NumberOfRows, NumberOfColumns];

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    var dayItemLabel = new DayItemLabel
                    {
                        Font = Font,
                        BackgroundColor = BackgroundColor,
                        BackGroundShape = BackGroundShape.SymmetricCircle,
                        ReSizeBasedOnText = false
                    };

                    int capturedRow = row;
                    int capturedCol = col;

                    dayItemLabel.MouseEnter += (s, e) =>
                    {
                        dayItemLabel.ForeColor = BackgroundColor;
                        dayItemLabel.BackgroundColor = OnFocusBorderColor;
                        Invalidate();
                    };
                    dayItemLabel.MouseLeave += (s, e) =>
                    {
                        dayItemLabel.ForeColor = DayItemLabels[capturedRow, capturedCol].IsCurrentMonth ? ForeColor : SecondaryForeColor;
                        dayItemLabel.BackgroundColor = BackgroundColor;
                        Invalidate();
                    };
                    dayItemLabel.Click += (s, e) => OnDayLabelClick(capturedRow, capturedCol);

                    InnerControls.Add(dayItemLabel);
                    DayItemLabels[row, col] = dayItemLabel;
                }
                
            }
            UpdateDayLabels(CurrentYear, CurrentMonth);
        }

        /// <summary>
        /// Método que é invocado no Click/DoubleClick de Backward/Forward.
        /// Invoke => UpdateDayLabels; AdjustControlSize.
        /// </summary>
        /// <param name="offset"></param>
        private void ChangeMonth(int offset)
        {
            if (CurrentMonth + offset > 12)
            {
                CurrentMonth = 1;
                CurrentYear += 1;
            }
            else if (CurrentMonth + offset <= 0)
            {
                CurrentMonth = 12;
                CurrentYear -= 1;
            }
            else
            {
                CurrentMonth += offset;
            }
            MonthLabel.Text = MonthTexts[CurrentMonth];
            UpdateDayLabels(CurrentYear, CurrentMonth);
            AdjustInnerLocations();

        }

        /// <summary>
        /// Metodo responsavel por atualizar os DayItemsLabels
        /// ()
        /// </summary>
        private void UpdateDayLabels(int currentYear, int currentMonth)
        {
            DateTime firstDay = new DateTime(currentYear, currentMonth, 1);
            int firstDayOfWeek = (int)firstDay.DayOfWeek;

            int daysInCurrentMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            int previousMonth = (currentMonth == 1) ? 12 : currentMonth - 1;
            int previousYear = (currentMonth == 1) ? currentYear - 1 : currentYear;
            int daysInPrevMonth = DateTime.DaysInMonth(previousYear, previousMonth);

            int nextMonth = (currentMonth == 12) ? 1 : currentMonth + 1;
            int nextYear = (currentMonth == 12) ? currentYear + 1 : currentYear;

            int gridIndex = 0;

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    DayItemLabel dayItemLabel = DayItemLabels[row, col];

                    if (gridIndex < firstDayOfWeek)
                    {
                        // Dias do mês anterior
                        int day = daysInPrevMonth - firstDayOfWeek + gridIndex + 1;
                        dayItemLabel.Day = day;
                        dayItemLabel.Month = previousMonth;
                        dayItemLabel.Year = previousYear;
                        dayItemLabel.IsCurrentMonth = false;
                        dayItemLabel.ForeColor = SecondaryForeColor;
                    }
                    else if (gridIndex < firstDayOfWeek + daysInCurrentMonth)
                    {
                        // Dias do mês atual
                        int day = gridIndex - firstDayOfWeek + 1;
                        dayItemLabel.Day = day;
                        dayItemLabel.Month = currentMonth;
                        dayItemLabel.Year = currentYear;
                        dayItemLabel.IsCurrentMonth = true;
                        dayItemLabel.ForeColor = ForeColor;
                    }
                    else
                    {
                        // Dias do próximo mês
                        int day = gridIndex - (firstDayOfWeek + daysInCurrentMonth) + 1;
                        dayItemLabel.Day = day;
                        dayItemLabel.Month = nextMonth;
                        dayItemLabel.Year = nextYear;
                        dayItemLabel.IsCurrentMonth = false;
                        dayItemLabel.ForeColor = SecondaryForeColor;
                    }
                    gridIndex++;
                }
            }
        }

        /// <summary>
        /// Método que é invocado no Click de DayItemLabel.
        /// </summary>
        protected void OnDayLabelClick(int rowIndex, int colIndex)
        {
            var item = DayItemLabels[rowIndex, colIndex];

            if (parentControl is CustomDatePicker dp)
            {
                dp.selectedDay.Text = item.Day.ToString("D2");
                dp.selectedMonth.Text = item.Month.ToString("D2");
                dp.selectedYear.Text = item.Year.ToString();
            }

            this.Parent?.Controls.Remove(this);
        }
       
        protected override void AdjustControlSize()
        {           
            if (DayItemLabels == null || DayItemLabels.Length == 0 || NumberOfColumns <= 0 || NumberOfRows <= 0)
                return;

            AdjustPadding();
        
            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;
            int itemUniformSize = NhegazSizeMethods.TextProportionalSize("00", this.Font, 1.3f).Height;

            Width = xPadding + (NumberOfColumns * (itemUniformSize + xPadding));
            Height = yPadding + ((NumberOfRows + 2) * (itemUniformSize + yPadding));

            AdjustInnerSizes(); AdjustInnerLocations(); 

            int headerY = (2 * yPadding) + ForwardIcon.Height;
            int baseGridY = headerY + itemUniformSize + yPadding;

            for (int row = 0; row < NumberOfRows; row++)
            {
                int y = baseGridY + row * (itemUniformSize + yPadding);

                for (int col = 0; col < NumberOfColumns; col++)
                {
                    int x = xPadding + col * (itemUniformSize + xPadding);

                    if (row == 0)
                    {
                        AdjustInnerSizes(col, itemUniformSize);
                        AdjustInnerLocations(col, x, headerY);

                    }

                    AdjustInnerSizes(row, col, itemUniformSize);
                    AdjustInnerLocations(row, col, x, y);
                }
            }

        }
        protected override void AdjustInnerSizes()
        {
            BackwardIcon.Height = NhegazSizeMethods.TextExactSize("00", Font).Height;

            ForwardIcon.Height = NhegazSizeMethods.TextExactSize("00", Font).Height;
        }
        protected override void AdjustInnerSizes(int col, int itemSize)
        {
            var label = HeaderLabels[col];
            label.Width = itemSize;
            label.Height = itemSize;
        }
        protected override void AdjustInnerSizes(int row, int col, int itemSize)
        {
            var label = DayItemLabels[row, col];
            label.Width = itemSize;
            label.Height = itemSize;
        }
        
        protected override void AdjustInnerLocations()
        {
            BackwardIcon.Location = new Point(HorizontalPadding, VerticalPadding);
            ForwardIcon.Location = new Point(Width - (ForwardIcon.Width + HorizontalPadding), VerticalPadding);
            MonthLabel.Location = new Point((Width - MonthLabel.Width) / 2, VerticalPadding);
        }
        protected override void AdjustInnerLocations(int col, int x, int y)
        {
            var label = HeaderLabels[col];
            label.Location = new Point(x, y);
        }
        protected override void AdjustInnerLocations(int row, int col, int x, int y)
        {
            var label = DayItemLabels[row, col];
            label.Location = new Point(x, y);
        }      
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.DrawBackGround(e);
            Rectangle HeaderRectangle = new Rectangle(HorizontalPadding, VerticalPadding, Width - (2 * HorizontalPadding), HeaderLabels[0].Height);

            using (GraphicsPath headerBackgroundPath = NhegazDrawingMethods.RectBackgroundPath(HeaderRectangle, 6))//Define o GraphicsPath da area interna do Control
            {
                using (SolidBrush brush = new SolidBrush(HeaderBackgroundColor)) //Preenche a area com o BackgroundColor
                {
                    e.Graphics.FillPath(brush, headerBackgroundPath);
                }
                e.Graphics.DrawPath(new Pen(HeaderBackgroundColor, 1f), headerBackgroundPath);
            }
            base.DrawInnerControls(e);
            base.DrawBorder(e);
        }
    }
}
