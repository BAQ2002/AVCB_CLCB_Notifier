using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using AVBC_CLCB_Notifier.PL;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class RoundedComboBox : CustomControl
    {

        private List<String> itemList = new List<String>(); //Opções da combo Box     
        public Label selectIndex = new Label(); //Opção atualmente selecionada
        public Label dropDownIcon = new Label(); //Icone de visual
        private DropDownInstance dropDownInstance = null;

        public List<string> ItemList
        {
            get { return itemList; } 
            set { itemList = value; Invalidate(); }
        }
        
        public override Font Font
        {
            get { return selectIndex.Font; }
            set { selectIndex.Font = value; dropDownIcon.Font = value; AdjustControlSize(); }
        }

        public RoundedComboBox()
        {          
            this.DoubleBuffered = true;
            this.Size = new Size(121, 23);            
            this.BackColor = Color.Transparent;
            this.Horizontalpadding += BorderWidth;
            this.MinimumSize =new Size(5, 5);
            this.Controls.Add(selectIndex);
            selectIndex.Text = "Teste123456";
            selectIndex.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            selectIndex.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            selectIndex.GotFocus += (s, e) => { this.OnGotFocus(e); };
            selectIndex.LostFocus += (s, e) => { this.OnLostFocus(e); };

            this.Controls.Add(dropDownIcon);
            dropDownIcon.Text = "▼";
            dropDownIcon.DoubleClick += (s, e) => { this.Focus(); this.OnClick(e); };
            dropDownIcon.Click += (s, e) => { this.Focus(); this.OnClick(e); };
            dropDownIcon.GotFocus += (s, e) => { this.OnGotFocus(e); };
            dropDownIcon.LostFocus += (s, e) => { this.OnLostFocus(e); };

            AdjustControlSize();
        }

        //Método para ajuste automatizado do tamanho dos componentes internos e do Padding vertical
        private void AdjustControlSize()
        {                        
            string referenceText = string.IsNullOrEmpty(selectIndex.Text) ? "A" : selectIndex.Text;
            int textHeight = TextRenderer.MeasureText(referenceText, selectIndex.Font).Height;
            Verticalpadding = (this.Height - textHeight) / 2;

            selectIndex.Location = new Point(Horizontalpadding, Verticalpadding);
            selectIndex.Width = this.Width - (dropDownIcon.Width + Horizontalpadding); // Deixa espaço para o ícone
            selectIndex.Height = selectIndex.Font.Height;

            dropDownIcon.Width = dropDownIcon.Font.Height;
            dropDownIcon.Height = dropDownIcon.Font.Height;
            dropDownIcon.Location = new Point(this.Width - (dropDownIcon.Width + Horizontalpadding), this.Verticalpadding);
            Invalidate();
        }

        //Sobrescrever o Click para ter o comportamento adequado
        protected override void OnClick(EventArgs e)
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
                dropDownInstance = new DropDownInstance(this);
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
    public class DropDownInstance : CustomControl
    {      
        private Color itemFocusColor;
        private RoundedComboBox parentComboBox;
        private int hoveredIndex = -1;
        private List<string> itemList = new List<string>();

        public DropDownInstance(RoundedComboBox _roundedComboBox)
        {
            this.parentComboBox = _roundedComboBox;
            this.BorderRadius = _roundedComboBox.BorderRadius;
            this.BorderWidth = _roundedComboBox.BorderWidth;
            this.BorderColor = _roundedComboBox.BorderColor;
            this.itemFocusColor = _roundedComboBox.BorderColorFocus;
            this.BackgroundColor = _roundedComboBox.BackgroundColor;
            this.Width = _roundedComboBox.Width;
            this.Horizontalpadding = _roundedComboBox.Horizontalpadding;
            this.Verticalpadding = _roundedComboBox.Verticalpadding;
            this.MinimumSize = new Size(5, 5);
            this.DoubleBuffered = true;
            this.itemList = _roundedComboBox.ItemList;
            this.BackColor = Color.Transparent;
            this.TabStop = true;
            this.Font = _roundedComboBox.Font;
            AdjustControlSize(_roundedComboBox);          
        }

        //Método para ajuste automatizado do tamanho do elemento
        private void AdjustControlSize(RoundedComboBox _sender)
        {
            string referenceText = "A";
            int textHeight = TextRenderer.MeasureText(referenceText, this.Font).Height;
            this.Height = (textHeight + Verticalpadding) * _sender.ItemList.Count;
            
        
            this.Controls.Clear(); // Limpa labels antigos se necessário
            int y = Verticalpadding/2; // Posição inicial vertical
            if (_sender.ItemList == null || _sender.ItemList.Count == 0) return;

            for (int i = 0; i < _sender.ItemList.Count; i++) //Cria uma Label para cada item do ItemList
            {
                Label lbl = new Label
                {
                    Name = $"Item{i}", // Define um nome único para cada Label
                    Text = _sender.ItemList[i],
                    Location = new System.Drawing.Point( _sender.Horizontalpadding, y), 
                    Height = textHeight,                 
                    Width = _sender.Width - _sender.Horizontalpadding - (2 * _sender.BorderWidth),
                    ForeColor = _sender.ForeColor
                };

                int index = i; 
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

                this.Controls.Add(lbl);
                y += textHeight + Verticalpadding; // Ajuste da posição vertical para o próximo label
            }
        }

        //Método para: ao clicar em uma das Opções, transfere a opção selecionada para o ComboBox
        private void OnLabelClick(string _labelText)
        {
            if (parentComboBox != null)
            {
                parentComboBox.selectIndex.Text = _labelText; // Atualiza a opção selecionada
                parentComboBox.OnFocusBool = false;
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
                    int rectBottomY = (this.Height / parentComboBox.ItemList.Count)-1;
                    
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
                else if (hoveredIndex == parentComboBox.ItemList.Count - 1 ) //Ultimo item da lista
                {
                    int rectTopY = rect.Bottom - (this.Height / parentComboBox.ItemList.Count)+1;//; + verticalpadding;
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
                            e.Graphics.DrawLine(pen, rect.X+1, rectTopY, rect.Right-1, rectTopY);
                            //e.Graphics.DrawPath(pen, itemRectPath);
                        }
                    }
                }
                else if (hoveredIndex != -1 && hoveredIndex != parentComboBox.ItemList.Count - 1 && hoveredIndex != 0) //items do meio da lista
                {
                    int rectTopY = hoveredIndex * (this.Height / parentComboBox.ItemList.Count);
                    int rectBottomY = (this.Height / parentComboBox.ItemList.Count)-1;
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
