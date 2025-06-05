using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static AVBC_CLCB_Notifier.PL.CustomControls.CustomControl;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public enum ButtonPreSet
    {
        None,
        DropDown,
        Add,
        Edit,
        Delete
    }
    public enum IconSizeMode
    {
        Absolute,
        RelativeToFont
    }
    public class InnerButton : InnerControl
    {
        public float IconSizePercent { get; set; } = 0.4f;
        public int IconSize { get; set; } = 10;

        private IconSizeMode iconSizeMode = IconSizeMode.RelativeToFont;
        public IconSizeMode IconSizeMode 
        {
            get => iconSizeMode;
            set
            {
                iconSizeMode = value;
                AdjustIconSize();
            }
        }
        public ButtonPreSet ButtonPreSet {  get; set; } = ButtonPreSet.None;

        public InnerButton(ButtonPreSet? preSet = null, BackGroundShape? backGroundShape = null, IconSizeMode? iconSizeMode = null)
        {
            if (preSet.HasValue) 
                ButtonPreSet = preSet.Value;           

            if (backGroundShape.HasValue)
                BackGroundShape = backGroundShape.Value;

            if (iconSizeMode.HasValue)
                IconSizeMode = iconSizeMode.Value;
        }

        protected virtual void AdjustIconSize()
        {
            if (IconSizeMode == IconSizeMode.RelativeToFont)
            {
                int fontHeight = NhegazSizeMethods.FontUnitSize(Font).Height;
                IconSize = (int)(fontHeight * IconSizePercent);
            }
        }

        public override void OnPaint(CustomControl parent, PaintEventArgs e)
        {
            base.OnPaint(parent, e);
            using (GraphicsPath IconPath = NhegazDrawingMethods.DropDownIconPath(this, IconSize))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor)) //Preenche a area com o BackgroundColor
                {
                    e.Graphics.FillPath(brush, IconPath);
                    
                }
                e.Graphics.DrawPath(new Pen(ForeColor, 1f), IconPath);
            }
        }
    }
}
