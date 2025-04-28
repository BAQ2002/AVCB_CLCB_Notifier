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

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public abstract class DropDownDateBase : CustomControl
    {
        protected Color itemFocusColor;
        protected CustomControl parentControl;
        protected int hoveredIndex = -1;
        protected StringCollection itemList = new StringCollection();

        public DropDownDateBase(CustomControl _control)
        {
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
            this.BackColor = Color.Transparent;
            this.TabStop = true;
            this.Font = _control.Font;
        }

        protected void AdjustControlSize(int maxItemsPerLine)
        {
            string referenceText = "0";
            int textHeight = TextRenderer.MeasureText(referenceText, this.Font).Height;
            int itemHeight = textHeight + VerticalPadding;

            this.Controls.Clear();
            if (itemList == null || itemList.Count == 0 || maxItemsPerLine <= 0)
                return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;
            int totalItems = itemList.Count;

            int numRows = (int)Math.Ceiling((double)totalItems / maxItemsPerLine);
            int itemWidth = (this.Width - (2 * xPadding)) / maxItemsPerLine;
            this.Height = numRows * itemHeight + yPadding;

            for (int i = 0; i < totalItems; i++)
            {
                int row = i / maxItemsPerLine;
                int col = i % maxItemsPerLine;
                int x = xPadding + col * itemWidth;
                int y = yPadding + row * itemHeight;

                InnerLabel lbl = CreateDateLabel(i, itemList[i], x, y, itemWidth, textHeight);
                this.Controls.Add(lbl);
            }
        }

        protected InnerLabel CreateDateLabel(int index, string text, int x, int y, int width, int height)
        {
            InnerLabel lbl = new InnerLabel()
            {
                Name = $"Item{index}",
                Text = text,
                Font = Font,
                Location = new Point(x, y),
                Width = width,
                Height = height,
                ForeColor = parentControl.ForeColor,                
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
                lbl.ForeColor = parentControl.ForeColor;
                Invalidate();
            };

            lbl.Click += (s, e) => OnLabelClick(lbl.Text);
            return lbl;
        }

        protected abstract void OnLabelClick(string selectedText);
    }

    public class DropDownDay : DropDownDateBase
    {
        public DropDownDay(CustomControl control) : base(control)
        {
            for (int i = 1; i <= 31; i++)
                itemList.Add(i.ToString("D2"));

            AdjustControlSize(7);
        }

        protected override void OnLabelClick(string selectedText)
        {
            if (parentControl is RoundedDatePicker dp)
                dp.selectedDay.Text = selectedText;

            this.Parent?.Controls.Remove(this);
        }
    }

    public class DropDownMonth : DropDownDateBase
    {
        public DropDownMonth(CustomControl control) : base(control)
        {
            for (int i = 1; i <= 12; i++)
                itemList.Add(i.ToString("D2"));

            AdjustControlSize(1);
        }

        protected override void OnLabelClick(string selectedText)
        {
            if (parentControl is RoundedDatePicker dp)
                dp.selectedMonth.Text = selectedText;

            this.Parent?.Controls.Remove(this);
        }
    }
    
    public class DropDownYear : DropDownDateBase
    {
        private InnerLabel decadeLabel = new InnerLabel(); 
        private InnerLabel backwardIcon = new InnerLabel(); //Label&&Button para passar para a década anteriror
        private InnerLabel forwardIcon = new InnerLabel(); //Label&&Button para passar para a década posterior
        private int currentDecade;
        
        public DropDownYear(CustomControl control) : base(control)
        {
            itemList = GenerateFullYearList();
            currentDecade = (DateTime.Now.Year / 10) * 10;
            

            this.Controls.Add(backwardIcon);
            backwardIcon.Text = "<";
            backwardIcon.ForeColor = this.ForeColor;
            backwardIcon.Click += (s, e) => ChangeDecade(-10);

            this.Controls.Add(forwardIcon);
            forwardIcon.Text = ">";
            forwardIcon.ForeColor = this.ForeColor;
            forwardIcon.Click += (s, e) => ChangeDecade(10);           

            this.Controls.Add(decadeLabel);

            AdjustControlSize(4);
        }

        private StringCollection GenerateFullYearList()
        {
            var list = new StringCollection(); //Cria uma Nova StringCollection
            for (int i = 0; i <= 2100; i++) //Cria um int para cada ano de 1900 ate 2100
                list.Add(i.ToString()); //Adiciona int.ToString na StringCollection
            return list;
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

        private void ChangeDecade(int offset)
        {
            currentDecade += offset;
        }
        protected void AdjustControlSize(int maxItemsPerLine)
        {           
            //this.Controls.Clear();
            if (itemList == null || itemList.Count == 0 || maxItemsPerLine <= 0)
                return;

            

            int itemWidth = textExactSize("0000", this.Font).Width;
            int itemHeight = textExactSize("0000", this.Font).Height;
            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int numRows = (int)Math.Ceiling((double)10 / maxItemsPerLine); //Calcula a quantidade de linhas"row" necessarias de acordo com o maxItemsPerLine 
            this.Height = (numRows + 1) * (itemHeight + yPadding) + yPadding;

            backwardIcon.Location = new Point(HorizontalPadding, VerticalPadding);
            forwardIcon.Location = new Point(Width - (forwardIcon.Width + HorizontalPadding), VerticalPadding);
            
            for (int i = 0; i <=9; i++)
            {
                int row = i / maxItemsPerLine; //Define a linha"row" em que o item"Label" deve ser inserido
                int col = i % maxItemsPerLine; //Define a coluna"column" em que o item"Label" deve ser inserido
                int x = xPadding + ((Width / maxItemsPerLine) * col); //Define a coordenada X em que o item"Label" deve ser inserido
                int y = (backwardIcon.Height + 2 * yPadding) + (row * (itemHeight+ yPadding)); //Define a coordenada y em que o item"Label" deve ser inserido
                int yearIndex = currentDecade + i; //Define o ano/índice(ano==índice) da lista que será referenciado
                                                   //
                InnerLabel lbl = CreateDateLabel(i, itemList[yearIndex], x, y, itemWidth, itemHeight);
                this.Controls.Add(lbl);
            }
        }
        protected override void OnLabelClick(string selectedText)
        {
            if (parentControl is RoundedDatePicker dp)
                dp.selectedYear.Text = selectedText;

            this.Parent?.Controls.Remove(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
        }
    }


}
