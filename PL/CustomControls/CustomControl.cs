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
        private int horizontalpadding = 1;
        private int verticalpadding = 1;
        private Color borderColor = Color.Red;
        private Color dropdownBorderColor = Color.Green;
        private Color borderColorFocus = Color.Blue; //Cor da borda
        private Color backgroundColor = Color.White;

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
        public int Horizontalpadding
        {
            get { return horizontalpadding; }
            set { horizontalpadding = value; Invalidate(); }
        }
        public int Verticalpadding
        {
            get { return verticalpadding; }
            set { verticalpadding = value; Invalidate(); }
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
    }
}
