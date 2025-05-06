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
using static System.Net.Mime.MediaTypeNames;
namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class DropDownDay : DropDownDateBase
    {

        class DayItem : InnerLabel
        {
            private int _day;
            public int Day
            {
                get => _day;
                set
                {
                    _day = value;
                    Text = _day.ToString("D2"); // Atualiza a visualização
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

        private InnerLabel MonthLabel = new InnerLabel();
        private InnerLabel BackwardIcon = new InnerLabel(); //Label&&Button para passar para a década anteriror
        private InnerLabel ForwardIcon = new InnerLabel();

        private List<DayItem> DayList = new List<DayItem>();
        private string[] MonthTexts =  {"null", "Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho",
                                         "Julho", "Agosto", "Setembro", "Outubro", "Novembro","Dezembro" };
        public DropDownDay(CustomControl control) : base(control)
        {            
            if (parentControl is CustomDatePicker dp)
            {
                NumberOfRows = 6;
                ItemsPerRow = 7;

                CurrentMonth = int.Parse(dp.selectedMonth.Text);
                CurrentYear = int.Parse(dp.selectedYear.Text);
                
                this.Controls.Add(BackwardIcon);
                BackwardIcon.Text = "◀";
                BackwardIcon.ForeColor = this.ForeColor;
                BackwardIcon.BackgroundColor = BackgroundColor;
                BackwardIcon.Click += (s, e) => { ChangeMonth(-1); Invalidate(); };
                BackwardIcon.DoubleClick += (s, e) => { ChangeMonth(-2); Invalidate(); };

                this.Controls.Add(ForwardIcon);
                ForwardIcon.Text = "▶";
                ForwardIcon.ForeColor = this.ForeColor;
                ForwardIcon.BackgroundColor = BackgroundColor;
                ForwardIcon.Click += (s, e) => { ChangeMonth(1); Invalidate(); };
                ForwardIcon.DoubleClick += (s, e) => { ChangeMonth(2); Invalidate(); };

                this.Controls.Add(MonthLabel);
                MonthLabel.ForeColor = this.ForeColor;
                MonthLabel.BackgroundColor = BackgroundColor;
                MonthLabel.Text = MonthTexts[CurrentMonth];

                SecondaryForeColor = Color.FromArgb((ForeColor.R + 255) / 2, (ForeColor.G + 255) / 2, (ForeColor.B + 255) / 2);

                CreateDayItems();
                           
                AdjustControlSize();
            }
        }
        
        private void UpdateDayList(int year, int month)
        {
            int ListIndex = 0;

            DateTime firstDay = new DateTime(year, month, 1); //DateTime do primeiro dia do mes                 
            int monthFirstDayOfWeek = (int)firstDay.DayOfWeek; //Int do Dia da semana do primeiro dia do mes
            int daysInMonth = DateTime.DaysInMonth(year, month); //Quantidade de dias do mes

            if (monthFirstDayOfWeek > 0) //Se o primeiro dia da semana do mes nao for segunda, adiciona os dias do mes anterior ate segunda
            {
                int prevMonth = (month == 1) ? 12 : month - 1; //Se o mes for 1: mes anterior sera 12, se nao: sera mes-1
                int prevYear = (month == 1) ? year - 1 : year; //Se mes for 1: ano anterior sera ano-1, se nao ano sera ==
                int daysInPreviousMonth = DateTime.DaysInMonth(prevYear, prevMonth); //Quantidade de dias no mes anterior 
                int previousFirstIndex = daysInPreviousMonth - monthFirstDayOfWeek + 1; //Define qual sera o primeiro dia do mes anterior a ser adicionado

                for (int i = previousFirstIndex; i <= daysInPreviousMonth; i++) 
                {
                    DayItem dayItem = DayList[ListIndex];
                    dayItem.Day = i; dayItem.Year = prevYear; dayItem.Month = prevMonth;
                    dayItem.IsCurrentMonth = false; dayItem.ForeColor = SecondaryForeColor;
                    ListIndex ++;
                }
            }

            for (int i = 1; i <= daysInMonth; i++) 
            {
                DayItem dayItem = DayList[ListIndex];
                dayItem.Day = i; dayItem.Year = year; dayItem.Month = month;
                dayItem.IsCurrentMonth = true; dayItem.ForeColor = ForeColor;
                ListIndex ++;
            }

            if (ListIndex < DayList.Count) //Se os dias do mes atual e do mes anterior nao preencheram a quantidade de determinada de labels
            {
                int nextMonth = (month == 12) ? 1 : month + 1; //Se o mes for 12: proximo mes será 1, se nao: sera mes+1
                int nextYear = (month == 12) ? year + 1 : year; //Se o mes for 12: proximo ano será ano+1, se nao: sera ==
                int daysInNextMonth = DateTime.DaysInMonth(nextYear, nextMonth);
                int nextMaxIndex = DayList.Count - ListIndex; //Define ate qual dia do proximo mes deve ser adicionado

                for (int i = 1; i <= nextMaxIndex; i++) 
                {
                    DayItem dayItem = DayList[ListIndex];
                    dayItem.Day = i; dayItem.Year = nextYear; dayItem.Month = nextMonth;
                    dayItem.IsCurrentMonth = false; dayItem.ForeColor = SecondaryForeColor;
                    ListIndex ++;
                }
            }            
        }

        protected override void OnLabelClick(int index)
        {
            var item = DayList[index];

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
            UpdateDayList(CurrentYear, CurrentMonth);
            AdjustControlSize();
        }

        private void CreateDayItems()
        {
            int numberOfLabels = NumberOfRows * ItemsPerRow;

            for (int i = 0; i < numberOfLabels; i++)
            {
                int index = i;

                DayItem dayItem = new DayItem()
                {
                    Name = $"Item{index}",
                    Font = Font,
                    BackgroundColor = BackgroundColor
                };        
                dayItem.MouseEnter += (s, e) =>
                {
                    hoveredIndex = index;
                    dayItem.ForeColor = BackgroundColor;
                    dayItem.BackgroundColor = BorderColorFocus;
                    Invalidate();
                };
                dayItem.MouseLeave += (s, e) =>
                {
                    if (hoveredIndex == index) hoveredIndex = -1;
                    dayItem.ForeColor = DayList[index].IsCurrentMonth ? ForeColor : SecondaryForeColor; 
                    dayItem.BackgroundColor = BackgroundColor;
                    Invalidate();
                };
                dayItem.Click += (s, e) => OnLabelClick(index);

                this.Controls.Add(dayItem);
                DayList.Add(dayItem);
                          
            }
            UpdateDayList(CurrentYear, CurrentMonth);
        }

        protected void AdjustControlSize()
        {
            string[] weekDays = { "D", "S", "T", "Q", "Q", "S", "S" };

            if (DayList == null || DayList.Count == 0 || ItemsPerRow <= 0)
                return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int itemHeight = textExactSize("00", this.Font).Height;
            int itemWidth = textExactSize("00", this.Font).Width;

            int totalItems = DayList.Count;
            int numRows = (int)Math.Ceiling((double)totalItems / ItemsPerRow);

            Width = xPadding + (ItemsPerRow * (itemWidth + xPadding));
            Height = yPadding + ((numRows + 2) * (itemHeight + yPadding));

            BackwardIcon.Location = new Point(xPadding, yPadding);
            ForwardIcon.Location = new Point(Width - (ForwardIcon.Width + xPadding), yPadding);
            MonthLabel.Location = new Point((Width - MonthLabel.Width) / 2, yPadding);

            for (int i = 0; i < weekDays.Length; i++)
            {
                int col = i % ItemsPerRow;
                int x = xPadding + (col * (itemWidth + xPadding));
                int y = (2 * yPadding) + ForwardIcon.Height;
                InnerLabel lbl = CreateDateLabel(i, weekDays[i], x, y, itemWidth, itemHeight);
                this.Controls.Add(lbl);
            }
            
            for (int i = 0; i < totalItems; i++)
            {     
                int row = i / ItemsPerRow;
                int column = i % ItemsPerRow;

                int locationX = xPadding + (column * (itemWidth + xPadding));
                int locationY = yPadding + (2 * itemHeight +  yPadding) + (row * (itemHeight + yPadding));

                DayItem dayItem = DayList[i];
                dayItem.Width = itemWidth;
                dayItem.Height = itemHeight;
                dayItem.Location = new Point(locationX, locationY);
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
        }
    }
}
