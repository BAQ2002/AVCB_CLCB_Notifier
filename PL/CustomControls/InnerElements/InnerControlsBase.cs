using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class InnerControls
    {
        private List<InnerControl> elements = new();

        public void Add(InnerControl drawable)
        {
            elements.Add(drawable);
        }

        public void Remove(InnerControl drawable)
        {
            elements.Remove(drawable);
        }

        public void Clear()
        {
            elements.Clear();
        }

        public void OnPaintAll(CustomControl parent, PaintEventArgs e)
        {
            foreach (var element in elements)
            {
                if (element.Visible)
                    element.OnPaint(parent, e);
            }
        }
        public void HandleClick(CustomControl parent, Point clickLocation)
        {
            foreach (var element in elements)
            {
                if (element.Visible && element.HitTest(clickLocation))
                {
                    element.RaiseClick(parent);
                    break;
                }
            }
        }

        public IEnumerable<InnerControl> GetAll() => elements;
    }


    public abstract class InnerControl
    {
        public bool Visible { get; set; } = true;
        public Size Size { get; set; } = new(0, 0);
        public Point Location { get; set; } = new(0, 0);
        public Rectangle Bounds => new(Location, Size);
        public int Width
        {
            get => Size.Width;
            set => Size = new Size(value, Size.Height);
        }

        public int Height
        {
            get => Size.Height;
            set => Size = new Size(Size.Width, value);
        }

        public event EventHandler? Click;  // evento estilo padrão .NET

        public abstract void OnPaint(CustomControl parent, PaintEventArgs e);

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void RaiseClick(object sender)
        {
            Click?.Invoke(sender, EventArgs.Empty);
        }
    }

}
