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
namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class DropDownDay : DropDownDateBase
    {
        class DayItemLabel : InnerLabel
        {
            private int _day;
            public int Day
            {
                get => _day;
                set
                {
                    _day = value;
                    Text = _day.ToString(); // Atualiza a visualização
                }
            }

            public bool IsCurrentMonth { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
        }

        private int NumberOfRows;
        private int ItemsPerRow;

        private int CurrentMonth;
        private int CurrentYear;

        private InnerLabel MonthLabel = new();
        private InnerLabel BackwardIcon = new (); //Label&&Button para passar para a década anteriror
        private InnerLabel ForwardIcon = new ();
        private DayItemLabel[,] DayGrid;
        private InnerLabel[] HeaderLabels;


        private List<DayItemLabel> DayItemList = new List<DayItemLabel>();
        private string[] MonthTexts =  {"null", "Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho",
                                         "Julho", "Agosto", "Setembro", "Outubro", "Novembro","Dezembro" };

        public override Font Font
        {
            get => base.Font;
            set { base.Font = value; MonthLabel.Font = value; ForwardIcon.Font = value; BackwardIcon.Font = value; AdjustControlSize(); }
        }
        public override Color ForeColor 
        {
            get => base.ForeColor;
            set { base.ForeColor = value; MonthLabel.ForeColor = value; ForwardIcon.ForeColor = value; BackwardIcon.ForeColor = value; Invalidate(); }
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
                ItemsPerRow = 7;

                CurrentMonth = int.Parse(dp.selectedMonth.Text);
                CurrentYear = int.Parse(dp.selectedYear.Text);
                
                InnerControls.Add(BackwardIcon);
                BackwardIcon.Text = "◀";
                BackwardIcon.Click += (s, e) => { ChangeMonth(-1); Invalidate(); };
                BackwardIcon.DoubleClick += (s, e) => { ChangeMonth(-2); Invalidate(); };

                InnerControls.Add(ForwardIcon);
                ForwardIcon.Text = "▶";
                ForwardIcon.Click += (s, e) => { ChangeMonth(1); Invalidate(); };
                ForwardIcon.DoubleClick += (s, e) => { ChangeMonth(2); Invalidate(); };

                InnerControls.Add(MonthLabel);
                MonthLabel.Text = MonthTexts[CurrentMonth];

                SecondaryForeColor = Color.FromArgb((ForeColor.R + 255) / 2, (ForeColor.G + 255) / 2, (ForeColor.B + 255) / 2);

                CreateDayItems();
                CreateHeaderLabels();
                AdjustControlSize();
            }
        }
        private void NewUpdateDayList(int year, int month)
        {
            DateTime firstDay = new DateTime(year, month, 1);
            int firstDayOfWeek = (int)firstDay.DayOfWeek; // domingo = 0, segunda = 1, ...

            int daysInCurrentMonth = DateTime.DaysInMonth(year, month);

            int prevMonth = (month == 1) ? 12 : month - 1;
            int prevYear = (month == 1) ? year - 1 : year;
            int daysInPrevMonth = DateTime.DaysInMonth(prevYear, prevMonth);

            int nextMonth = (month == 12) ? 1 : month + 1;
            int nextYear = (month == 12) ? year + 1 : year;

            int gridIndex = 0;

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < ItemsPerRow; col++)
                {
                    DayItemLabel label = DayGrid[row, col];

                    int flatIndex = gridIndex++;
                    if (flatIndex < firstDayOfWeek)
                    {
                        // Dias do mês anterior
                        int day = daysInPrevMonth - firstDayOfWeek + flatIndex + 1;
                        label.Day = day;
                        label.Month = prevMonth;
                        label.Year = prevYear;
                        label.IsCurrentMonth = false;
                        label.ForeColor = SecondaryForeColor;
                    }
                    else if (flatIndex < firstDayOfWeek + daysInCurrentMonth)
                    {
                        // Dias do mês atual
                        int day = flatIndex - firstDayOfWeek + 1;
                        label.Day = day;
                        label.Month = month;
                        label.Year = year;
                        label.IsCurrentMonth = true;
                        label.ForeColor = ForeColor;
                    }
                    else
                    {
                        // Dias do próximo mês
                        int day = flatIndex - (firstDayOfWeek + daysInCurrentMonth) + 1;
                        label.Day = day;
                        label.Month = nextMonth;
                        label.Year = nextYear;
                        label.IsCurrentMonth = false;
                        label.ForeColor = SecondaryForeColor;
                    }
                }
            }
        }

        private void UpdateDayList(int year, int month)
        {
            int DayItemListIndex = 0; //Indice que faz referencia a lista de DayItemLabel: a cada item que é adicionado aumenta 1(vai para o proximo indice)

            DateTime firstDay = new DateTime(year, month, 1); //DateTime do primeiro dia do mes                 
            int monthFirstDayOfWeek = (int)firstDay.DayOfWeek; //Int do Dia da semana do primeiro dia do mes
            int daysInMonth = DateTime.DaysInMonth(year, month); //Quantidade de dias do mes

            if (monthFirstDayOfWeek > 0) //Se o primeiro dia da semana do mes nao for domingo, adiciona os dias do mes anterior ate o primeiro domingo do mes
            {
                int prevMonth = (month == 1) ? 12 : month - 1; //Se o mes for 1: mes anterior sera 12, se nao: sera mes-1
                int prevYear = (month == 1) ? year - 1 : year; //Se mes for 1: ano anterior sera ano-1, se nao ano sera ==
                int daysInPreviousMonth = DateTime.DaysInMonth(prevYear, prevMonth); //Quantidade de dias no mes anterior 
                int previousFirstIndex = daysInPreviousMonth - monthFirstDayOfWeek + 1; //Define qual sera o primeiro dia do mes anterior a ser adicionado

                for (int i = previousFirstIndex; i <= daysInPreviousMonth; i++) 
                {
                    DayItemLabel dayItemLabel = DayItemList[DayItemListIndex];
                    dayItemLabel.Day = i; dayItemLabel.Year = prevYear; dayItemLabel.Month = prevMonth;
                    dayItemLabel.IsCurrentMonth = false; dayItemLabel.ForeColor = SecondaryForeColor;
                    DayItemListIndex ++;
                }
            }

            for (int i = 1; i <= daysInMonth; i++) //Loop para criar os dias do mes atual
            {
                DayItemLabel dayItemLabel = DayItemList[DayItemListIndex];
                dayItemLabel.Day = i; dayItemLabel.Year = year; dayItemLabel.Month = month;
                dayItemLabel.IsCurrentMonth = true; dayItemLabel.ForeColor = ForeColor;
                DayItemListIndex ++;
            }

            if (DayItemListIndex < DayItemList.Count) //Se os dias do mes atual e do mes anterior nao preencheram a quantidade de determinada de labels
            {
                int nextMonth = (month == 12) ? 1 : month + 1; //Se o mes for 12: proximo mes será 1, se nao: sera mes+1
                int nextYear = (month == 12) ? year + 1 : year; //Se o mes for 12: proximo ano será ano+1, se nao: sera ==
                int daysInNextMonth = DateTime.DaysInMonth(nextYear, nextMonth);
                int nextMaxIndex = DayItemList.Count - DayItemListIndex; //Define ate qual dia do proximo mes deve ser adicionado

                for (int i = 1; i <= nextMaxIndex; i++) 
                {
                    DayItemLabel dayItemLabel = DayItemList[DayItemListIndex];
                    dayItemLabel.Day = i; dayItemLabel.Year = nextYear; dayItemLabel.Month = nextMonth;
                    dayItemLabel.IsCurrentMonth = false; dayItemLabel.ForeColor = SecondaryForeColor;
                    DayItemListIndex ++;
                }
            }            
        }

        protected void OnLabelClick(int rowIndex, int colIndex)
        {
            var item = DayGrid[ rowIndex, colIndex];

            if (parentControl is CustomDatePicker dp)
            {
                dp.selectedDay.Text = item.Day.ToString("D2");
                dp.selectedMonth.Text = item.Month.ToString("D2");
                dp.selectedYear.Text = item.Year.ToString();
            }

            this.Parent?.Controls.Remove(this);
        }

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
            NewUpdateDayList(CurrentYear, CurrentMonth);
            AdjustControlSize();
        }

        /// <summary>
        /// Esse Método deve ser responsavel por chamar os metodos que criam os elementos virtuais, tais como DayItems 
        /// </summary>
        protected void CreateDayItems()
        {
            DayGrid = new DayItemLabel[NumberOfRows, ItemsPerRow];

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < ItemsPerRow; col++)
                {
                    int index = row * ItemsPerRow + col;

                    var label = new DayItemLabel
                    {
                        Font = Font,
                        BackgroundColor = BackgroundColor,
                        BackGroundShape = BackGroundShape.SymmetricCircle
                    };

                    int capturedRow = row;
                    int capturedCol = col;

                    label.MouseEnter += (s, e) =>
                    {
                        label.ForeColor = BackgroundColor;
                        label.BackgroundColor = OnFocusBorderColor;
                        Invalidate();
                    };
                    label.MouseLeave += (s, e) =>
                    {
                        label.ForeColor = DayGrid[capturedRow, capturedCol].IsCurrentMonth ? ForeColor : SecondaryForeColor;
                        label.BackgroundColor = BackgroundColor;
                        Invalidate();
                    };
                    label.Click += (s, e) => OnLabelClick(capturedRow, capturedCol);

                    InnerControls.Add(label);
                    DayGrid[row, col] = label;
                }
                
            }
            NewUpdateDayList(CurrentYear, CurrentMonth);
        }

        public void CreateHeaderLabels()
        {
            string[] weekDays = { "D", "S", "T", "Q", "Q", "S", "S" };
            HeaderLabels = new InnerLabel[weekDays.Length];

            for (int i = 0; i < weekDays.Length; i++)
            {
                var headerLabel = new InnerLabel
                {
                    Text = weekDays[i],
                    Font = Font,
                    BackgroundColor = HeaderBackgroundColor,
                    BackGroundShape = BackGroundShape.SymmetricCircle
                };
                InnerControls.Add(headerLabel);
                HeaderLabels[i] = headerLabel;
            }
        }

        protected override void AdjustControlSize()
        {
            

            if (DayGrid == null || DayGrid.Length == 0 || ItemsPerRow <= 0)
                return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int itemUniformSize = NhegazSizeMethods.TextProportionalSize("00", this.Font, 1.3f).Height;

            int totalItems = NumberOfRows * ItemsPerRow;
            int numRows = (int)Math.Ceiling((double)totalItems / ItemsPerRow);

            Width = xPadding + (ItemsPerRow * (itemUniformSize + xPadding));
            Height = yPadding + ((numRows + 2) * (itemUniformSize + yPadding));

            BackwardIcon.Location = new Point(xPadding, yPadding);
            ForwardIcon.Location = new Point(Width - (ForwardIcon.Width + xPadding), yPadding);
            MonthLabel.Location = new Point((Width - MonthLabel.Width) / 2, yPadding);

            AdjustInnerLocations();

        }
        protected override void AdjustInnerSizes()
        {
        }
        protected override void AdjustInnerLocations()
        {
            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;
            int itemUniformSize = NhegazSizeMethods.TextProportionalSize("00", this.Font, 1.3f).Height;

            int headerY = (2 * yPadding) + ForwardIcon.Height;
            int baseGridY = headerY + itemUniformSize + yPadding;

            for (int col = 0; col < ItemsPerRow; col++)
            {
                int x = xPadding + col * (itemUniformSize + xPadding);

                var header = HeaderLabels[col];
                header.Location = new Point(x, headerY);
                header.Width = itemUniformSize;
                header.Height = itemUniformSize;

                for (int row = 0; row < NumberOfRows; row++)
                {
                    int y = baseGridY + row * (itemUniformSize + yPadding);

                    var label = DayGrid[row, col];
                    label.Location = new Point(x, y);
                    label.Width = itemUniformSize;
                    label.Height = itemUniformSize;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;
            base.DrawBackGround(e);
            Rectangle HeaderRectangle = new Rectangle(HorizontalPadding, VerticalPadding, Width - HorizontalPadding, HeaderLabels[0].Height);

            using (GraphicsPath headerBackgroundPath = NhegazDrawingMethods.RectBackgroundPath(HeaderRectangle, 4))//Define o GraphicsPath da area interna do Control
            {
                using (SolidBrush brush = new SolidBrush(HeaderBackgroundColor)) //Preenche a area com o BackgroundColor
                {
                    e.Graphics.FillPath(brush, headerBackgroundPath);
                }
            }
            base.DrawInnerControls(e);
            base.DrawBorder(e);
        }
    }
}
