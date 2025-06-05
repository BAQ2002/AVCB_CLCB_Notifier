using AVBC_CLCB_Notifier.PL.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Xml;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;


namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class CustomDatePicker : CustomControl
    {                                                                    
        public TextBox selectedDay = new TextBox(); //Opção atualmente selecionada Dia
        public TextBox selectedMonth = new TextBox(); //Opção atualmente selecionada mes
        public TextBox selectedYear = new TextBox(); //Opção atualmente selecionada ano

        public InnerButton dayDropDownIcon = new(ButtonPreSet.DropDown, BackGroundShape.SymmetricCircle); 
        public InnerButton monthDropDownIcon = new(ButtonPreSet.DropDown, BackGroundShape.SymmetricCircle);
        public InnerButton yearDropDownIcon = new(ButtonPreSet.DropDown, BackGroundShape.SymmetricCircle);

        private InnerLabel daySlashMonth = new InnerLabel();
        private InnerLabel monthSlashYear = new InnerLabel();
        private CustomControl dropDownInstance = null;
      
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                selectedDay.Font = value; selectedMonth.Font = value; selectedYear.Font = value;
                dayDropDownIcon.Font = value; monthDropDownIcon.Font = value; yearDropDownIcon.Font = value; 
                daySlashMonth.Font = value; monthSlashYear.Font = value;
                AdjustControlSize();
            }
        }
        public override Color BackgroundColor
        {
            get => base.BackgroundColor;
            set
            {
                base.BackgroundColor = value;
                dayDropDownIcon.BackgroundColor = value; monthDropDownIcon.BackgroundColor = value; yearDropDownIcon.BackgroundColor = value;
                daySlashMonth.BackgroundColor = value; monthSlashYear.BackgroundColor = value;
                Invalidate();
            }
        }
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                selectedDay.ForeColor = value; selectedMonth.ForeColor = value; selectedYear.ForeColor = value;
                dayDropDownIcon.ForeColor = value; monthDropDownIcon.ForeColor = value; yearDropDownIcon.ForeColor = value;
                daySlashMonth.ForeColor = value; monthSlashYear.ForeColor = value;
                Invalidate();
            }
        }
        public CustomDatePicker() : base() 
        {                          
            this.Controls.Add(selectedDay);
            selectedDay.Name = this.Name + "selectedDay";
            selectedDay.Text = DateTime.Now.Day.ToString("D2");
            selectedDay.BorderStyle = BorderStyle.None;
            selectedDay.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedDay.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            //selectedDay.GotFocus += (s, e) => { this.OnGotFocus(e); };
            //selectedDay.LostFocus += (s, e) => { this.OnLostFocus(e); };

            this.InnerControls.Add(daySlashMonth);
            daySlashMonth.Text = "/";

            this.InnerControls.Add(dayDropDownIcon);    
            dayDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownDay(this)); };
            dayDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownDay(this)); };

            dayDropDownIcon.MouseEnter += (s, e) =>
            {
                dayDropDownIcon.ForeColor = BackgroundColor;
                dayDropDownIcon.BackgroundColor = HeaderBackgroundColor;
                Invalidate();
            };
            dayDropDownIcon.MouseLeave += (s, e) =>
            {
                dayDropDownIcon.ForeColor = ForeColor;
                dayDropDownIcon.BackgroundColor = BackgroundColor;
                Invalidate();
            };
            this.Controls.Add(selectedMonth);
            selectedMonth.Text = DateTime.Now.Month.ToString("D2");
            selectedMonth.BorderStyle = BorderStyle.None;
            selectedMonth.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedMonth.LostFocus += (s, e) => { this.OnLostFocus(e); };
           
            this.InnerControls.Add(monthDropDownIcon);
            monthDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownMonth(this)); };
            monthDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownMonth(this)); };
            //monthDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            //monthDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };

            this.InnerControls.Add(monthSlashYear);
            monthSlashYear.Text = "/";

            this.Controls.Add(selectedYear);
            selectedYear.Text = DateTime.Now.Year.ToString();
            selectedYear.BorderStyle = BorderStyle.None;
            selectedYear.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedYear.LostFocus += (s, e) => { this.OnLostFocus(e); };
          
            this.InnerControls.Add(yearDropDownIcon);
            yearDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownYear(this)); };
            yearDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownYear(this)); };

            AdjustControlSize(); 
        }
        protected override void SetMinimumSize()
        {
            base.SetMinimumSize();
            int X = yearDropDownIcon.Location.X + yearDropDownIcon.Width + VerticalPadding;
            int Y = (VerticalPadding * 2) + yearDropDownIcon.Height;
            MinimumSize = new Size(X, Y);
        }

        protected override void AdjustInnerSizes()
        {
            base.AdjustInnerSizes();
            selectedDay.Width = NhegazSizeMethods.TextExactSize("00", Font).Width;
            selectedDay.Height = Font.Height;

            dayDropDownIcon.Height = Font.Height;

            selectedMonth.Width = NhegazSizeMethods.TextExactSize("00", Font).Width;
            selectedMonth.Height = Font.Height;

            monthDropDownIcon.Height = Font.Height;

            selectedYear.Width = NhegazSizeMethods.TextExactSize("0000", Font).Width; 
            selectedYear.Height = Font.Height;

            yearDropDownIcon.Height = Font.Height;
        }
        protected override void AdjustInnerLocations()
        {
            base.AdjustInnerLocations();

            selectedDay.Location = new Point(HorizontalPadding, VerticalPadding);
            dayDropDownIcon.Location = new Point(selectedDay.Location.X + selectedDay.Width, VerticalPadding);

            daySlashMonth.Location = new Point(dayDropDownIcon.Location.X + dayDropDownIcon.Width, VerticalPadding);

            selectedMonth.Location = new Point(daySlashMonth.Location.X + daySlashMonth.Width, VerticalPadding);
            monthDropDownIcon.Location = new Point(selectedMonth.Location.X + selectedMonth.Width, VerticalPadding);

            monthSlashYear.Location = new Point(monthDropDownIcon.Location.X + monthDropDownIcon.Width, VerticalPadding);

            selectedYear.Location = new Point(monthSlashYear.Location.X + monthSlashYear.Width, VerticalPadding);
            yearDropDownIcon.Location = new Point(selectedYear.Location.X + selectedYear.Width, VerticalPadding);
        }
        

        //Sobrescrever o Click para ter o comportamento adequado
        protected void OnClick(EventArgs e, CustomControl dropDown)
        {
            base.OnClick(e);
            if (dropDownInstance != null) // Se o dropdown já estiver aberto, fecha ele
            {
                Form parentForm = this.FindForm();
                parentForm.Controls.Remove(dropDownInstance); 
                dropDownInstance = null; //Define o dropDownInstance como Null
                OnFocusBool = false; //Define que o elemento nao esta em foco
            }
            else
            {
                AdjustControlSize();
                dropDownInstance = dropDown;
                Form parentForm = this.FindForm();
                if (parentForm == null)
                {
                    return;
                }

                Point screenLocation = this.Parent.PointToScreen(this.Location);
                Point formLocation = parentForm.PointToClient(screenLocation);

                dropDownInstance.Location = new Point(formLocation.X, formLocation.Y + this.Height+1);
                dropDownInstance.BringToFront();
                parentForm.Controls.Add(dropDownInstance);
                parentForm.Controls.SetChildIndex(dropDownInstance, 0);
                OnFocusBool = true;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);           
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (dropDownInstance != null)
            {
                Form parentForm = this.FindForm();
                parentForm.Controls.Remove(dropDownInstance);
                dropDownInstance = null;
                OnFocusBool = false;
                return;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustControlSize();
            Invalidate();
        }
    }    
}
