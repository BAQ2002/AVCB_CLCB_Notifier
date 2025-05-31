using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class CustomComboBox : CustomControl
    {

        private StringCollection itemList  = new(); //Opções da combo Box     
        private InnerLabel selectIndex = new(); //Opção atualmente selecionada
        private InnerLabel dropDownIcon = new(); //Icone de visual
        private DropDownInstance dropDownInstance = null;
        public string SelectIndexText
        {
            get { return selectIndex.Text; }
            set { selectIndex.Text = value; Invalidate(); }
        }
        public StringCollection ItemList { 
            get { return itemList; }
            set { itemList = value; Invalidate(); }
        }
        public override Color BackgroundColor
        {
            get => base.BackgroundColor;
            set
            { 
                base.BackgroundColor = value;
                selectIndex.BackgroundColor = value; dropDownIcon.BackgroundColor = value; Invalidate();
            }
        }
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                selectIndex.ForeColor = value; dropDownIcon.ForeColor = value; Invalidate();
            }
        }
        public override Font Font
        {
            get { return selectIndex.Font; }
            set { selectIndex.Font = value; dropDownIcon.Font = value; AdjustControlSize(); }
        }

        public CustomComboBox()
        {          
            DoubleBuffered = true;
            Size = new Size(121, 23);            
            BackColor = Color.Transparent;
            MinimumSize =new Size(5, 5);
            
            this.InnerControls.Add(selectIndex);
            selectIndex.Text = "Teste123456";
            selectIndex.DoubleClick += (s, e) => { this.Focus(); base.OnClick(e); };
            selectIndex.Click += (s, e) => { this.Focus(); base.OnClick(e); };          

            this.InnerControls.Add(dropDownIcon);
            dropDownIcon.Text = "▼";
            dropDownIcon.DoubleClick += (s, e) => { this.Focus(); base.OnClick(e); };
            dropDownIcon.Click += (s, e) => { this.Focus(); base.OnClick(e); };

            AdjustControlSize();
        }

        //Método para ajuste automatizado do tamanho dos componentes internos e do Padding vertical
        protected override void AdjustControlSize()
        {
            base.AdjustControlSize();

            int selectIndexX = BorderWidth + HorizontalPadding;
            int selectIndexY = (Height - selectIndex.Height) / 2;
            selectIndex.Location = new Point(selectIndexX, selectIndexY);

            int dropDownIconX = Width - (dropDownIcon.Width + HorizontalPadding + BorderWidth);
            int dropDownIconY = (Height - dropDownIcon.Height) / 2;
            dropDownIcon.Location = new Point(dropDownIconX, dropDownIconY);

            Invalidate();
        }
        private void ToggleDropDown()
        {
            if (dropDownInstance != null) // Se o dropdown já estiver aberto, fecha ele
            {
                Form parentForm = FindForm();
                parentForm.Controls.Remove(dropDownInstance);
                dropDownInstance = null; OnFocusBool = false;
            }
            else
            {
                dropDownInstance = new DropDownInstance(this);
                Form parentForm = this.FindForm();
                if (parentForm == null)
                {
                    return;
                }

                Point screenLocation = this.Parent.PointToScreen(this.Location);
                Point formLocation = parentForm.PointToClient(screenLocation);

                dropDownInstance.Location = new Point(formLocation.X, formLocation.Y + Height + BorderWidth);
                dropDownInstance.BringToFront();
                parentForm.Controls.Add(dropDownInstance);
                parentForm.Controls.SetChildIndex(dropDownInstance, 0);
                OnFocusBool = true;
                Invalidate();
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ToggleDropDown();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            OnClick(e);
        }
        //protected override void OnLostFocus(EventArgs e)
        //{
         //   base.OnLostFocus(e);
          //  if (dropDownInstance != null)
          //  {
         //       Form parentForm = this.FindForm();
         //       parentForm.Controls.Remove(dropDownInstance);
         //       dropDownInstance = null;
         //       OnFocusBool = false;
         //       return;
        //    }
        //}
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (dropDownInstance != null)
            {
                var mousePos = Cursor.Position;
                var form = this.FindForm();

                if (form != null)
                {
                    Control controlAtMouse = form.GetChildAtPoint(form.PointToClient(mousePos));

                    if (controlAtMouse != null && controlAtMouse == dropDownInstance)
                    {
                        // Clique ocorreu no dropdown — NÃO REMOVER
                        return;
                    }
                }

                form?.Controls.Remove(dropDownInstance);
                dropDownInstance = null;
                OnFocusBool = false;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustControlSize();
            Invalidate();
        }
    }

    public class DropDownInstance : CustomControl
    {
        private CustomComboBox ParentComboBox;
        private List<InnerLabel> innerLabels = new List<InnerLabel>();
        public DropDownInstance(CustomComboBox customComboBox)
        {
            ParentComboBox = customComboBox;
            BorderRadius = customComboBox.BorderRadius;
            BorderWidth = customComboBox.BorderWidth;
            BorderColor = customComboBox.BorderColor;
            BackgroundColor = customComboBox.BackgroundColor;
            Width = customComboBox.Width;
            HorizontalPadding = customComboBox.HorizontalPadding;
            VerticalPadding = customComboBox.VerticalPadding;
            MinimumSize = new Size(5, 5);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            ForeColor = customComboBox.ForeColor;
            TabStop = true;
            Font = customComboBox.Font;
            CreateOptionsLabels();
            AdjustControlSize();
        }

        private void CreateOptionsLabels()
        {
            if (ParentComboBox.ItemList == null || ParentComboBox.ItemList.Count == 0) return;

            for (int i = 0; i < ParentComboBox.ItemList.Count; i++) //Cria uma Label para cada item do ItemList
            {
                string labelText = ParentComboBox.ItemList[i];

                InnerLabel InnerLabel = new InnerLabel
                {
                    Text = labelText,
                    Font = Font,
                    ForeColor = ForeColor,
                    BackgroundColor = BackgroundColor                 
                };
                InnerLabel.MouseEnter += (s, e) =>
                {
                    InnerLabel.ForeColor = BackgroundColor;
                    InnerLabel.BackgroundColor = BorderColorFocus;
                    Invalidate();
                };
                InnerLabel.MouseLeave += (s, e) =>
                {
                    InnerLabel.ForeColor = ForeColor;
                    InnerLabel.BackgroundColor = BackgroundColor;
                    Invalidate();
                };

                InnerLabel.Click += (s, e) => OnLabelClick(InnerLabel.Text);

                this.InnerControls.Add(InnerLabel);
                innerLabels.Add(InnerLabel);
            }

        }
        //Método para ajuste automatizado do tamanho do elemento
        protected override void AdjustControlSize()
        {
           
            base.AdjustControlSize();
            
            if (ParentComboBox.ItemList == null || ParentComboBox.ItemList.Count == 0) return;
            
            int count = innerLabels.Count;
            int currentY = 0;
            for (int i = 0; i < count; i++) 
            {
                var lbl = innerLabels[i];
                lbl.Location = new Point(BorderWidth, currentY);
                lbl.Height = lbl.Height + VerticalPadding;
                lbl.Width = Width;
                lbl.TextHorizontalPadding = true;
                lbl.HorizontalPaddingMode = InnerLabel.TextHorizontalPaddingEnum.Absolute;
                lbl.HorizontalPaddingAbsolute = HorizontalPadding;

                currentY += lbl.Height; 
                
            }
            Height = count * innerLabels[0].Height;
        }

        //Método para: ao clicar em uma das Opções, transfere a opção selecionada para o ComboBox
        private void OnLabelClick(string labelText)
        {
            if (ParentComboBox != null)
            {
                ParentComboBox.SelectIndexText = labelText; // Atualiza a opção selecionada
                ParentComboBox.OnFocusBool = false;
            }
            this.Parent?.Controls.Remove(this); // Fecha o dropdown após a seleção
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
    }
}
