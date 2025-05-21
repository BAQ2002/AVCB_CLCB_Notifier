using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class CustomControl : UserControl
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
        private Color backgroundColor = SystemColors.Control; //Cor do fundo
        private Color secondaryBackgroundColor = SystemColors.ControlLightLight; //Cor do fundo secundaria

        private Color borderColor = SystemColors.WindowFrame;
        private Color dropdownBorderColor = Color.Green;
        private Color borderColorFocus = SystemColors.Highlight; //Cor da borda
        
        private PaddingModeEnum paddingMode = PaddingModeEnum.Absolute;
        public InnerControls InnerControls { get; } = new();
        public enum PaddingModeEnum
        {
            Absolute,    // HorizontalPadding e VerticalPadding são definidos diretamente
            RelativeToFont   // Calculados dinamicamente com base no tamanho da fonte do Control
        }

        public PaddingModeEnum PaddingMode
        {
            get => paddingMode;
            set
            {
                paddingMode = value;
                Invalidate();     // Redesenha o controle
            }
        }
        
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; Invalidate(); }
        }
        public bool OnFocusBool
        {
            get { return onFocusBool; }
            set { onFocusBool = value; Invalidate(); }
        }
        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set { horizontalPadding = value; Invalidate(); }
        }
        public int VerticalPadding
        {
            get { return verticalPadding; }
            set { verticalPadding = value; Invalidate(); }
        }
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; Invalidate(); }
        }
        public int BorderFocusExtraWidth
        {
            get { return borderFocusExtraWidth; }
            set { borderFocusExtraWidth = value; Invalidate(); }
        }

        public Color SecondaryBackgroundColor
        {
            get { return secondaryBackgroundColor; }
            set { secondaryBackgroundColor = value; Invalidate(); }
        }

        public Color SecondaryForeColor
        {
            get { return secondaryForeColor; }
            set { secondaryForeColor = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }
        public Color BorderColorFocus
        {
            get { return borderColorFocus; }
            set { borderColorFocus = value; Invalidate(); }
        }
        public Color BackgroundColor
        {
            get { return backgroundColor; }
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

        protected virtual void AdjustControlSize()
        {
            AdjustPadding(); // comportamento comum a todos
                             // Se quiser: Invalidate(); // opcionalmente desenhar o controle aqui
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            InnerControls.HandleClick(this, e.Location); // detecta clique virtual
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
            InnerControls.OnPaintAll(this, e);
        }

    }
}
