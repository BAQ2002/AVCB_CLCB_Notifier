using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class DropDownYear : DropDownDateBase
    {
        private CustomLabel decadeLabel = new CustomLabel();
        private CustomLabel backwardIcon = new CustomLabel(); //Label&&Button para passar para a década anteriror
        private CustomLabel forwardIcon = new CustomLabel(); //Label&&Button para passar para a década posterior
        private List<CustomLabel> yearLabels = new List<CustomLabel>();

        private int currentDecade;
        private int decadeLastYear;
        private int MaxItemsPerLine;

        public DropDownYear(CustomControl control) : base(control)
        {
            itemList = GenerateFullYearList();
            currentDecade = (DateTime.Now.Year / 10) * 10;
            decadeLastYear = currentDecade + 9;
            MaxItemsPerLine = 4;

            this.Controls.Add(backwardIcon);
            backwardIcon.Text = "◀";
            backwardIcon.ForeColor = this.ForeColor;
            backwardIcon.BackgroundColor = BackgroundColor;
            backwardIcon.Click += (s, e) => { ChangeDecade(-10); Invalidate(); };
            backwardIcon.DoubleClick += (s, e) => { ChangeDecade(-20); Invalidate(); };

            this.Controls.Add(forwardIcon);
            forwardIcon.Text = "▶";
            forwardIcon.ForeColor = this.ForeColor;
            forwardIcon.BackgroundColor = BackgroundColor;
            forwardIcon.Click += (s, e) => { ChangeDecade(10); Invalidate(); };
            forwardIcon.DoubleClick += (s, e) => { ChangeDecade(20); Invalidate(); };

            this.Controls.Add(decadeLabel);
            decadeLabel.Text = $"{currentDecade} - {decadeLastYear}";
            decadeLabel.ForeColor = this.ForeColor;
            decadeLabel.BackgroundColor = BackgroundColor;


            AdjustControlSize();
        }

        private StringCollection GenerateFullYearList()
        {
            var list = new StringCollection(); //Cria uma Nova StringCollection
            for (int i = 0; i <= 2099; i++) //Cria um int para cada ano de 0000 ate 2099
                list.Add(i.ToString()); //Adiciona int.ToString na StringCollection
            return list;
        }


        private void ChangeDecade(int offset)
        {
            if (currentDecade + offset >= itemList.Count)
            {
                return;
            }
            currentDecade += offset;
            decadeLastYear = currentDecade + 9;

            decadeLabel.Text = $"{currentDecade} - {decadeLastYear}";
            AdjustControlSize(); // Atualiza os anos no lugar!
        }
        protected override void AdjustControlSize()
        {
            int maxItemsPerLine = MaxItemsPerLine;
            if (itemList == null || itemList.Count == 0 || maxItemsPerLine <= 0)
                return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int itemWidth = textExactSize("0000", this.Font).Width;
            int itemHeight = textExactSize("0000", this.Font).Height;

            int numRows = (int)Math.Ceiling((double)10 / maxItemsPerLine); //Calcula a quantidade de linhas"row" necessarias de acordo com o maxItemsPerLine 
            Width = xPadding + (maxItemsPerLine * (itemWidth + xPadding));
            Height = yPadding + ((numRows + 1) * (itemHeight + yPadding));

            backwardIcon.Location = new Point(xPadding, yPadding);
            forwardIcon.Location = new Point(Width - (forwardIcon.Width + xPadding), yPadding);
            decadeLabel.Location = new Point((Width - decadeLabel.Width) / 2, yPadding);

            if (yearLabels.Count == 0)
            {
                for (int i = 0; i <= 9; i++)
                {
                    int row = i / maxItemsPerLine; //Define a linha"row" em que o item"Label" deve ser inserido
                    int col = i % maxItemsPerLine; //Define a coluna"column" em que o item"Label" deve ser inserido
                    int x = xPadding + (col * (itemWidth + xPadding));
                    int y = (backwardIcon.Height + 2 * yPadding) + (row * (itemHeight + yPadding)); //Define a coordenada y em que o item"Label" deve ser inserido
                    int yearIndex = currentDecade + i; //Define o ano/índice(ano==índice) da lista que será referenciado

                    CustomLabel lbl = CreateDateLabel(i, itemList[yearIndex], x, y, itemWidth, itemHeight);
                    yearLabels.Add(lbl);
                    this.Controls.Add(lbl);
                }
            }
            else // Atualiza apenas os textos
            {
                for (int i = 0; i <= 9; i++)
                {
                    int yearIndex = currentDecade + i;
                    if (yearIndex >= 0 && yearIndex < itemList.Count)
                    {
                        yearLabels[i].Text = itemList[yearIndex];
                    }
                }
            }
        }
        protected override void OnLabelClick(int index)
        {
            if (parentControl is CustomDatePicker dp)
                dp.selectedYear.Text = yearLabels[index].Text;

            this.Parent?.Controls.Remove(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
            Pen pen = new Pen(this.BorderColor, 1);
            Point leftPoint = new Point(BorderWidth, backwardIcon.Bottom);
            Point rightPoint = new Point(Width - BorderWidth, forwardIcon.Bottom);
            e.Graphics.DrawLine(pen, leftPoint, rightPoint);
        }
    }
}
