using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBC_CLCB_Notifier.PL.Templates
{
    public class CustomButton : Button
    {

        public CustomButton()
        {
            //Code in this video
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //Code in this video
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            //int borderSize = Math.Min(BorderSize, Math.Min(rect.Width, rect.Height) / 2);

            Rectangle arc = new Rectangle(rect.Left, rect.Top, diameter, diameter);
            path.AddArc(arc, 180, 90);

            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            //Code in this video
        }
    }
}
