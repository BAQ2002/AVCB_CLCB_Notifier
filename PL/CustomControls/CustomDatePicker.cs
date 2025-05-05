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
                                                                    //
        public TextBox selectedDay = new TextBox(); //Opção atualmente selecionada Dia
        public TextBox selectedMonth = new TextBox(); //Opção atualmente selecionada mes
        public TextBox selectedYear = new TextBox(); //Opção atualmente selecionada ano

        public InnerLabel dayDropDownIcon = new InnerLabel(); //Icone de visual
        public InnerLabel monthDropDownIcon = new InnerLabel();//Icone de visual
        public InnerLabel yearDropDownIcon = new InnerLabel();//Icone de visual

        private CustomControl dropDownInstance = null;
      

        public override Font Font
        {
            get { return selectedDay.Font; }
            set
            {
                selectedDay.Font = value; selectedMonth.Font = value; selectedYear.Font = value;
                dayDropDownIcon.Font = value; monthDropDownIcon.Font = value; yearDropDownIcon.Font = value;
                AdjustControlSize();
            }
        }

        public CustomDatePicker()
        {
            this.DoubleBuffered = true;            
            this.BackColor = Color.Transparent;
            this.HorizontalPadding += BorderWidth;
            
            this.Controls.Add(selectedDay);
            selectedDay.Name = this.Name + "selectedDay";
            selectedDay.Text = DateTime.Now.Day.ToString("D2");
            selectedDay.BorderStyle = BorderStyle.None;
            selectedDay.ForeColor = this.ForeColor;
            selectedDay.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedDay.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedDay.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedDay.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedDay.BackColor = Color.Red;

            this.Controls.Add(dayDropDownIcon);
            dayDropDownIcon.Text = "▼";           
            dayDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownDay(this)); };
            dayDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownDay(this)); };
            dayDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            dayDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            dayDropDownIcon.BackgroundColor = BackgroundColor;

            this.Controls.Add(selectedMonth);
            selectedMonth.Text = DateTime.Now.Month.ToString("D2");
            selectedMonth.BorderStyle = BorderStyle.None;
            selectedMonth.ForeColor = this.ForeColor;
            selectedMonth.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedMonth.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedMonth.BackColor = Color.Red;//FromArgb(BackgroundColor.R, BackgroundColor.G, BackgroundColor.B);
           
            this.Controls.Add(monthDropDownIcon);
            monthDropDownIcon.Text = "▼";
            monthDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownMonth(this)); };
            monthDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownMonth(this)); };
            monthDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            monthDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            monthDropDownIcon.BackgroundColor = BackgroundColor;

            this.Controls.Add(selectedYear);
            selectedYear.Text = DateTime.Now.Year.ToString();
            selectedYear.BorderStyle = BorderStyle.None;
            selectedYear.Font = this.Font;
            selectedYear.ForeColor = this.ForeColor;
            selectedYear.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedYear.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedYear.BackColor = Color.Red;

            this.Controls.Add(yearDropDownIcon);
            yearDropDownIcon.Text = "▼";
            yearDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, new DropDownYear(this)); };
            yearDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, new DropDownYear(this)); };
            yearDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            yearDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            yearDropDownIcon.BackgroundColor = BackgroundColor;

            AdjustControlSize(); this.Size = this.MinimumSize;
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

        //Método para ajuste automatizado do tamanho dos componentes internos e do Padding vertical
        private void AdjustControlSize()
        {          
            int SlashTextWidth = textExactSize("/", Font).Width; //Define a Largura"Width"    
            int dropDownIconTextWidth = textExactSize("▼", Font).Width; //Define a Largura"Width"  
            int dayAndMonthTextWidth = textExactSize("00", Font).Width; //Define a Largura"Width"  
            int yearTextWidth = textExactSize("0000", Font).Width; //Define a Largura"Width"  

            Size textUnitSize = textExactSize("0", Font);

            HorizontalPadding = (int)Math.Round(textUnitSize.Width * (2f / 3f));
            VerticalPadding = (int)Math.Round(textUnitSize.Height / 4f);

            selectedDay.Width = dayAndMonthTextWidth;
            selectedDay.Height = Font.Height;
            selectedDay.Location = new Point(HorizontalPadding, VerticalPadding);

            dayDropDownIcon.Width = dropDownIconTextWidth;
            dayDropDownIcon.Height = Font.Height;
            dayDropDownIcon.Location = new Point(selectedDay.Location.X + selectedDay.Width, VerticalPadding);
            
            selectedMonth.Width = dayAndMonthTextWidth;
            selectedMonth.Height = Font.Height;
            selectedMonth.Location = new Point(dayDropDownIcon.Location.X + dayDropDownIcon.Width + SlashTextWidth, VerticalPadding);

            monthDropDownIcon.Width = dropDownIconTextWidth;
            monthDropDownIcon.Height = Font.Height;
            monthDropDownIcon.Location = new Point(selectedMonth.Location.X + selectedMonth.Width, VerticalPadding);

            selectedYear.Width = yearTextWidth; 
            selectedYear.Height = Font.Height;
            selectedYear.Location = new Point(monthDropDownIcon.Location.X + monthDropDownIcon.Width + SlashTextWidth, VerticalPadding);

            yearDropDownIcon.Width = dropDownIconTextWidth;
            yearDropDownIcon.Height = Font.Height;
            yearDropDownIcon.Location = new Point(selectedYear.Location.X + selectedYear.Width, VerticalPadding);
            int minimumWidth = yearDropDownIcon.Location.X + yearDropDownIcon.Width + HorizontalPadding;

            //int minimumWidth = (int)Math.Round(textUnitSize.Width * (58f / 3f));
            int minimumHeight = (2 * VerticalPadding) + textUnitSize.Height;
            
            MinimumSize = new Size(minimumWidth, minimumHeight);
            Invalidate();
            
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

        //Sobrescrever o gerenciamento visual para que tenha as bordas arredondadas e cores personalizadas
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
            var g = e.Graphics;
            var textFormat = TextFormatFlags.Left| TextFormatFlags.NoPadding;

            TextRenderer.DrawText(g, "/", Font, new Point(dayDropDownIcon.Right, dayDropDownIcon.Location.Y), this.ForeColor, textFormat);
            TextRenderer.DrawText(g, "/", Font, new Point(monthDropDownIcon.Right, monthDropDownIcon.Location.Y), this.ForeColor, textFormat);
        }

        //Override necessario para que quando seja clicado fora do elemento o DropDown Seja removido
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
