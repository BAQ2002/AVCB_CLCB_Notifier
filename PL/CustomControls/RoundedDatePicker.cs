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


namespace AVBC_CLCB_Notifier.PL.Templates
{
    public class RoundedDatePicker : CustomControl
    {

        private List<StringCollection> itemListsIndexes = new List<StringCollection>();
        private StringCollection itemList = new StringCollection(); //Opções da combo Box     
        public TextBox selectedDay = new TextBox(); //Opção atualmente selecionada Dia
        public TextBox selectedMonth = new TextBox(); //Opção atualmente selecionada mes
        public TextBox selectedYear = new TextBox(); //Opção atualmente selecionada ano
        public Label dayDropDownIcon = new Label(); //Icone de visual
        public Label monthDropDownIcon = new Label();//Icone de visual
        public Label yearDropDownIcon = new Label();//Icone de visual
        private DropDownInstanceDate dropDownInstance = null;

        public StringCollection ItemList
        {
            get { return itemList; }
            set { itemList = value; Invalidate(); }
        }

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

        public RoundedDatePicker()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(121, 23);
            this.BackColor = Color.Transparent;
            this.HorizontalPadding += BorderWidth;
            this.MinimumSize = new Size(5, 5);       
            
            this.Controls.Add(selectedDay);
            selectedDay.Name = this.Name + "selectedDay";
            selectedDay.Text = " 01";
            selectedDay.BorderStyle = BorderStyle.None; 
            selectedDay.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedDay.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedDay.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedDay.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedDay.BackColor = Color.Transparent;

            this.Controls.Add(dayDropDownIcon);
            dayDropDownIcon.Text = "";
            dayDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, "day"); };
            dayDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, "day"); };
            dayDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            dayDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //dayDropDownIcon.BackColor = Color.Transparent;

            this.Controls.Add(selectedMonth);
            selectedMonth.Text = " 01";
            selectedMonth.BorderStyle = BorderStyle.None;
            selectedMonth.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedMonth.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedMonth.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedMonth.BackColor = Color.Transparent;

            this.Controls.Add(monthDropDownIcon);
            monthDropDownIcon.Text = "";
            monthDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, "month"); };
            monthDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, "month"); };
            monthDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            monthDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //monthDropDownIcon.BackColor = Color.Transparent;

            this.Controls.Add(selectedYear);
            selectedYear.Text = " 1990";
            selectedYear.BorderStyle = BorderStyle.None;
            selectedYear.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectedYear.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectedYear.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //selectedYear.BackColor = Color.Transparent;

            this.Controls.Add(yearDropDownIcon);
            yearDropDownIcon.Text = "";
            yearDropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e, "year"); };
            yearDropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e, "year"); };
            yearDropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            yearDropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };
            //yearDropDownIcon.BackColor = Color.Transparent;
            
            AdjustControlSize();
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
            string referenceSlashText = "/"; int SlashTextWidth = textExactSize(referenceSlashText, selectedDay.Font).Width;
            string referenceDropDownText = "▼"; int dropDownIconTextWidth = textExactSize(referenceDropDownText, selectedDay.Font).Width;
            string referenceDayAndMonthText = "000"; int dayAndMonthTextWidth = textExactSize(referenceDayAndMonthText, selectedDay.Font).Width;
            string referenceYearText = "00000"; int yearTextWidth = textExactSize(referenceYearText, selectedDay.Font).Width;

            int textHeight = TextRenderer.MeasureText(referenceDayAndMonthText, selectedDay.Font).Height;

            VerticalPadding = (this.Height - textHeight) / 2;
            
            selectedDay.Width = dayAndMonthTextWidth; // Deixa espaço para o ícone
            selectedDay.Height = selectedDay.Font.Height;
            selectedDay.Location = new Point(HorizontalPadding, VerticalPadding);

            dayDropDownIcon.Width = dropDownIconTextWidth;
            dayDropDownIcon.Height = dayDropDownIcon.Font.Height;
            dayDropDownIcon.Location = new Point(selectedDay.Location.X + selectedDay.Width, VerticalPadding);
            
            selectedMonth.Width = dayAndMonthTextWidth; // Deixa espaço para o ícone
            selectedMonth.Height = selectedMonth.Font.Height;
            selectedMonth.Location = new Point(dayDropDownIcon.Location.X + dayDropDownIcon.Width + SlashTextWidth, VerticalPadding);

            monthDropDownIcon.Width = dropDownIconTextWidth;
            monthDropDownIcon.Height = dayDropDownIcon.Font.Height;
            monthDropDownIcon.Location = new Point(selectedMonth.Location.X + selectedMonth.Width, VerticalPadding);

            selectedYear.Width = yearTextWidth; // Deixa espaço para o ícone
            selectedYear.Height = selectedYear.Font.Height;
            selectedYear.Location = new Point(monthDropDownIcon.Location.X + monthDropDownIcon.Width + SlashTextWidth, VerticalPadding);

            yearDropDownIcon.Width = dropDownIconTextWidth;
            yearDropDownIcon.Height = yearDropDownIcon.Font.Height;
            yearDropDownIcon.Location = new Point(selectedYear.Location.X + selectedYear.Width, VerticalPadding);

            int minimumWidth = yearDropDownIcon.Location.X + yearDropDownIcon.Width + HorizontalPadding;
            MinimumSize = new Size(minimumWidth, 5);
            Invalidate();
            
        }

        //Sobrescrever o Click para ter o comportamento adequado
        protected void OnClick(EventArgs e, string dateElementReference)
        {
            base.OnClick(e);
            if (dropDownInstance != null) // Se o dropdown já estiver aberto, fecha ele
            {
                Form parentForm = this.FindForm();
                parentForm.Controls.Remove(dropDownInstance);
                dropDownInstance = null;
                OnFocusBool = false;
            }
            else
            {
                AdjustControlSize();
                dropDownInstance = new DropDownInstanceDate(this, dateElementReference);
                Form parentForm = this.FindForm();
                if (parentForm == null)
                {
                    return;
                }

                Point screenLocation = this.Parent.PointToScreen(this.Location);
                Point formLocation = parentForm.PointToClient(screenLocation);

                dropDownInstance.Location = new Point(formLocation.X, formLocation.Y + this.Height + 6);
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

            TextRenderer.DrawText(g, "▼", Font, new Point(dayDropDownIcon.Left, dayDropDownIcon.Location.Y), this.ForeColor, textFormat);
            TextRenderer.DrawText(g, "▼", Font, new Point(monthDropDownIcon.Left, monthDropDownIcon.Location.Y), this.ForeColor, textFormat);
            TextRenderer.DrawText(g, "▼", Font, new Point(selectedYear.Right, yearDropDownIcon.Location.Y), this.ForeColor, textFormat);
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

    //Classe da Lista de Items/Opções
    public class DropDownInstanceDate : CustomControl
    {
        private Color itemFocusColor;
        private CustomControl parentControl;
        private string dateElement;       
        private int hoveredIndex = -1;
        private StringCollection itemList = new StringCollection();

        public DropDownInstanceDate(CustomControl _control, string _dateElement)
        {
            this.dateElement = _dateElement;
            this.parentControl = _control;
            this.BorderRadius = _control.BorderRadius;
            this.BorderWidth = _control.BorderWidth;
            this.BorderColor = _control.BorderColor;
            this.itemFocusColor = _control.BorderColorFocus;
            this.BackgroundColor = _control.BackgroundColor;
            this.Width = _control.Width;
            this.HorizontalPadding = _control.HorizontalPadding;
            this.VerticalPadding = _control.VerticalPadding;
            this.MinimumSize = new Size(5, 5);
            this.DoubleBuffered = true;
            this.itemList = GenerateItemList(_dateElement);
            this.BackColor = Color.Transparent;
            this.TabStop = true;
            this.Font = _control.Font;
            int row = 0;
            switch (dateElement.ToLower())
            {
                case "day":
                    row = 7;
                    break;
                case "month":
                    row = 1;
                    break;
                case "year":
                    row = 4;
                    break;
            }
            AdjustControlSize(_control, row);
        }

        //Método para ajuste automatizado do tamanho do elemento
        private void AdjustControlSize(CustomControl _sender, int maxItemsPerLine)
        {
            string referenceText = "0";
            int textHeight = TextRenderer.MeasureText(referenceText, this.Font).Height;
            int itemHeight = textHeight + VerticalPadding;

            this.Controls.Clear();
            if (itemList == null || itemList.Count == 0 || maxItemsPerLine <= 0)
                return;

            int xPadding = _sender.HorizontalPadding;
            int yPadding = VerticalPadding;
            int totalItems = itemList.Count;

            // Calcula número de linhas necessárias
            int numRows = (int)Math.Ceiling((double)totalItems / maxItemsPerLine);

            // Largura de cada item baseado no número máximo por linha
            int itemWidth = (this.Width - (2 * xPadding)) / maxItemsPerLine;

            // Altura total do controle
            this.Height = numRows * itemHeight + yPadding;

            for (int i = 0; i < totalItems; i++)
            {
                int row = i / maxItemsPerLine;
                int col = i % maxItemsPerLine;

                int x = xPadding + col * itemWidth;
                int y = yPadding + row * itemHeight;

                Label lbl = CreateDateLabel(i, itemList[i], x, y, itemWidth, textHeight, _sender);
                this.Controls.Add(lbl);
            }
        }

        // Método auxiliar para criar a Label
        private Label CreateDateLabel(int index, string text, int x, int y, int width, int height, CustomControl _sender)
        {
            Label lbl = new Label
            {
                Name = $"Item{index}",
                Text = text,
                Location = new Point(x, y),
                Width = width,
                Height = height,
                ForeColor = _sender.ForeColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lbl.MouseEnter += (s, e) =>
            {
                hoveredIndex = index;
                lbl.ForeColor = this.BackColor;
                Invalidate();
            };

            lbl.MouseLeave += (s, e) =>
            {
                if (hoveredIndex == index) hoveredIndex = -1;
                lbl.ForeColor = _sender.ForeColor;
                Invalidate();
            };

            lbl.Click += (s, e) => OnLabelClick(lbl.Text);

            return lbl;
        }


        private StringCollection GenerateItemList(string _dateElement)
        {
            var list = new StringCollection();

            if (_dateElement == "day")
            {
                for (int i = 1; i <= 31; i++)
                    list.Add(i.ToString("D2"));
            }
            else if (_dateElement == "month")
            {
                for (int i = 1; i <= 12; i++)
                    list.Add(i.ToString("D2"));
            }
            else if (_dateElement == "year")
            {
                for (int i = 1900; i <= DateTime.Now.Year; i++)
                    list.Add(i.ToString());
            }

            return list;
        }


        //Método para: ao clicar em uma das Opções, transfere a opção selecionada para o ComboBox
        private void OnLabelClick(string _labelText)
        {
            if (parentControl is RoundedDatePicker datePicker)
                switch (dateElement.ToLower())
                {
                    case "day":
                        datePicker.selectedDay.Text = _labelText;
                        break;
                    case "month":
                        datePicker.selectedMonth.Text = _labelText;
                        break;
                    case "year":
                        datePicker.selectedYear.Text = _labelText;
                        break;
                }

            this.Parent?.Controls.Remove(this); // Fecha o dropdown após a seleção
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                int radius = BorderRadius * 2;
                // Desenha o caminho arredondado para o dropdown inteiro
                NhegazDrawingMethods.DrawControl(this, e);

                string referenceText = "A";
                int textHeight = TextRenderer.MeasureText(referenceText, this.Font).Height;

                if (hoveredIndex == 0) //Primeiro item da lista
                {
                    int rectBottomY = (this.Height / itemList.Count) - 1;

                    using (GraphicsPath itemRectPath = new GraphicsPath())
                    {
                        itemRectPath.AddArc(rect.X, rect.Y, radius, radius, 180, 90);// Arco inferior esquerdo
                        itemRectPath.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);// Arco inferior direito
                        itemRectPath.AddLine(rect.Right, rectBottomY, rect.X, rectBottomY);
                        itemRectPath.CloseFigure();

                        using (SolidBrush hoverBrush = new SolidBrush(itemFocusColor))
                        {
                            e.Graphics.FillPath(hoverBrush, itemRectPath);
                        }
                        using (Pen pen = new Pen(Color.Chocolate, 1))
                        {
                            e.Graphics.DrawPath(pen, itemRectPath);
                        }
                    }
                }
                else if (hoveredIndex == itemList.Count - 1) //Ultimo item da lista
                {
                    int rectTopY = rect.Bottom - (this.Height / itemList.Count) + 1;//; + verticalpadding;
                    using (GraphicsPath itemRectPath = new GraphicsPath())
                    {
                        itemRectPath.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);// Arco inferior direito
                        itemRectPath.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);// Arco inferior esquerdo
                        itemRectPath.AddLine(rect.X, rectTopY, rect.Right, rectTopY);
                        itemRectPath.CloseFigure();

                        using (SolidBrush hoverBrush = new SolidBrush(itemFocusColor))
                        {
                            e.Graphics.FillPath(hoverBrush, itemRectPath);
                        }
                        using (Pen pen = new Pen(itemFocusColor, 1))
                        {
                            e.Graphics.DrawLine(pen, rect.X + 1, rectTopY, rect.Right - 1, rectTopY);
                            //e.Graphics.DrawPath(pen, itemRectPath);
                        }
                    }
                }
                else if (hoveredIndex != -1 && hoveredIndex != itemList.Count - 1 && hoveredIndex != 0) //items do meio da lista
                {
                    int rectTopY = hoveredIndex * (this.Height / itemList.Count);
                    int rectBottomY = (this.Height / itemList.Count) - 1;
                    using (GraphicsPath itemRectPath = new GraphicsPath())
                    {
                        Rectangle itemRect = new Rectangle(rect.Left, rectTopY, rect.Width, rectBottomY);
                        itemRectPath.AddRectangle(itemRect);

                        using (SolidBrush hoverBrush = new SolidBrush(itemFocusColor))
                        {
                            e.Graphics.FillPath(hoverBrush, itemRectPath);
                        }
                        using (Pen pen = new Pen(Color.Chocolate, 1))
                        {
                            e.Graphics.DrawPath(pen, itemRectPath);
                        }
                    }
                }
            }
        }
    }
}
