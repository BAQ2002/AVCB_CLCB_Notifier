using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public abstract class CustomControl : UserControl
    {
        private bool onFocusBool = false;
        private int borderRadius = 5;
        private int borderWidth = 1;
        private int borderFocusExtraWidth = 1;
        private int horizontalPadding = 1;
        private int verticalPadding = 1;
        private float paddingRelativePercent = 0.6f; // 60% por padrão

        private Color secondaryForeColor = SystemColors.GrayText; //Cor de textos secundarios
        private Color headerBackgroundColor = SystemColors.GrayText; //Cor do fundo de cabecalhos
        private Color backgroundColor = SystemColors.Window; //Cor do fundo
        private Color secondaryBackgroundColor = SystemColors.ControlLightLight; //Cor do fundo secundaria

        private Color borderColor = SystemColors.WindowFrame;
        private Color dropdownBorderColor = Color.Green;
        private Color onFocusBorderColor = SystemColors.Highlight; //Cor da borda
        
        private Color hoverColor = SystemColors.Highlight;

        private PaddingModeEnum paddingMode = PaddingModeEnum.Absolute;
        public InnerControls InnerControls { get; }

        public CustomControl()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            InnerControls = new InnerControls(this);
        }
        public enum PaddingModeEnum
        {
            Absolute,    // HorizontalPadding e VerticalPadding são definidos diretamente
            RelativeToFont   // Calculados dinamicamente com base no tamanho da fonte do Control
        }

        public PaddingModeEnum PaddingMode
        {
            get => paddingMode;
            set { paddingMode = value; Invalidate(); }
        } 
        public bool OnFocusBool
        {
            get => onFocusBool;
            set { onFocusBool = value; Invalidate(); }
        }
        public int HorizontalPadding
        {
            get => horizontalPadding; 
            set { horizontalPadding = value; Invalidate(); }
        }
        public int VerticalPadding
        {
            get => verticalPadding; 
            set { verticalPadding = value; Invalidate(); }
        }
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }
        public int BorderWidth
        {
            get => borderWidth; 
            set { borderWidth = value; Invalidate(); }
        }
        public int BorderFocusExtraWidth
        {
            get => borderFocusExtraWidth;
            set { borderFocusExtraWidth = value; Invalidate(); }
        }
        public Color SecondaryBackgroundColor
        {
            get => secondaryBackgroundColor; 
            set { secondaryBackgroundColor = value; Invalidate(); }
        }
        public virtual Color HeaderBackgroundColor
        {
            get => headerBackgroundColor; 
            set { headerBackgroundColor = value; Invalidate(); }
        }
        public Color SecondaryForeColor
        {
            get => secondaryForeColor; 
            set { secondaryForeColor = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get => borderColor; 
            set { borderColor = value; Invalidate(); }
        }
        public Color OnFocusBorderColor
        {
            get => onFocusBorderColor; 
            set { onFocusBorderColor = value; Invalidate(); }
        }
        public Color HoverColor
        {
            get => hoverColor;
            set { hoverColor = value; Invalidate(); }
        }
        public virtual Color BackgroundColor
        {
            get => backgroundColor; 
            set { backgroundColor = value; Invalidate(); }
        }
        public float PaddingRelativePercent
        {
            get => paddingRelativePercent;
            set
            {
                // Garante que esteja entre 0 e 1
                paddingRelativePercent = Math.Max(0f, Math.Min(1f, value));
                if (PaddingMode == PaddingModeEnum.RelativeToFont)
                {
                    Invalidate();
                }
            }
        }
        //protected abstract void SetHooverColors();
 
        /// <summary>
        /// Metodo responsavel pelo ajuste dos valores de Padding.
        /// </summary>
        /// 
        protected virtual void AdjustPadding()
        {
            if (PaddingMode == PaddingModeEnum.RelativeToFont)
            {
                Size unit = TextRenderer.MeasureText("0", this.Font);
                HorizontalPadding = (int)Math.Round(unit.Width * paddingRelativePercent);
                VerticalPadding = (int)Math.Round(unit.Height * paddingRelativePercent);
            }
            // Se for Absolute, não altera — valores já foram definidos diretamente
        }

        protected virtual void AdjustHoverColors()
        {

        }

        /// <summary>
        /// Método responsavel pelo ajuste do tamanho dos InnerControls.
        /// </summary>
        protected virtual void AdjustInnerSizes()
        { }    
        protected virtual void AdjustInnerSizes(int col, int itemSize)
        { }
        protected virtual void AdjustInnerSizes(int row, int col, int itemSize)
        { }

        /// <summary>
        /// Metodo responsavel pelo ajuste das posicoes dos InnerControls.
        /// </summary>
        /// 
        protected virtual void AdjustInnerLocations()
        { }
        protected virtual void AdjustInnerLocations(int index, int x, int y)
        { }
        protected virtual void AdjustInnerLocations(int row, int col, int x, int y)
        {}     

        /// <summary>
        /// Método responsavel por definir o MinimumSize a partir dos InnerControls.
        /// </summary>
        protected virtual void SetMinimumSize()
        {

        }

        /// <summary>
        /// Método que invoca todos ajustes de posicoes e tamanhos.
        /// </summary>
        protected virtual void AdjustControlSize()
        {
            AdjustPadding();
            AdjustInnerLocations();
            AdjustInnerSizes();
            SetMinimumSize();
            Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            InnerControls.HandleClick(this, e.Location); // detecta clique virtual
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            InnerControls.HandleDoubleClick(this, e.Location);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InnerControls.HandleMouseMove(this, e.Location);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (Focused)
                InnerControls.HandleGotFocus(this, PointToClient(Cursor.Position));
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            InnerControls.HandleLostFocus(this, PointToClient(Cursor.Position));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.None;
            DrawBackGround(e);
            DrawInnerControls(e);
            DrawBorder(e);
        }
        protected virtual void DrawBackGround(PaintEventArgs e)
        {
            float borderRadius = BorderWidth > 1 ? BorderRadius * 0.9f : BorderRadius;

            using (GraphicsPath backgroundPath = NhegazDrawingMethods.ControlBackgroundPath(new Rectangle(Location, Size), borderRadius, BorderWidth)) //Define o GraphicsPath da area interna do Control
            {
                using (SolidBrush brush = new SolidBrush(BackgroundColor)) //Preenche a area com o BackgroundColor
                {
                    e.Graphics.FillPath(brush, backgroundPath);
                }
                e.Graphics.SetClip(backgroundPath); //Define que o limite do Paint é o GraphicsPath da area interna do Control
                
            }
        }
        protected virtual void DrawInnerControls(PaintEventArgs e)
        {
            InnerControls.OnPaintAll(this, e); 
            e.Graphics.ResetClip();
        }
        protected virtual void DrawBorder(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color paintBorderColor = OnFocusBool ? OnFocusBorderColor : BorderColor;
            int borderWidth = OnFocusBool ? BorderWidth + BorderFocusExtraWidth : BorderWidth;

            using (GraphicsPath borderPath = NhegazDrawingMethods.ControlBorderPath(new Rectangle(Location, Size), BorderRadius, borderWidth))
            {              
                if (borderWidth > 1)
                {                   
                    using (SolidBrush borderBrush = new SolidBrush(paintBorderColor))
                    {
                        e.Graphics.FillPath(borderBrush, borderPath);
                    }
                }
                e.Graphics.DrawPath(new Pen(paintBorderColor, 1f), borderPath);
            }

        }
       
    }
}
