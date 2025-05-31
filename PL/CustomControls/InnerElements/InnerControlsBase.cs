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
        private CustomControl? Parent;

        public void Add(InnerControl innerControl)
        {
            elements.Add(innerControl);
        }

        public void Remove(InnerControl innerControl)
        {
            elements.Remove(innerControl);
        }

        public void Clear()
        {
            elements.Clear();
        }

        public InnerControls(CustomControl parent) { Parent = parent; }
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
                if (element.Visible && element.HitBox(clickLocation))
                {
                    element.RaiseClick(parent);
                    break;
                }
            }
        }
        public void HandleDoubleClick(CustomControl parent, Point clickLocation)
        {
            foreach (var element in elements)
            {
                if (element.Visible && element.HitBox(clickLocation))
                {
                    element.RaiseDoubleClick(parent);
                    break;
                }
            }
        }
        public void HandleMouseMove(CustomControl parent, Point mouseLocation)
        {
            foreach (var element in elements)
            {
                if (element.Visible)
                {
                    bool contains = element.HitBox(mouseLocation);

                    if (contains && !element.IsHovering)
                    {
                        element.OnMouseEnter();
                        parent.Invalidate();  // força repaint do controle pai para refletir a mudança
                    }
                    else if (!contains && element.IsHovering)
                    {
                        element.OnMouseLeave();
                        parent.Invalidate();
                    }
                }
            }
        }     
        public void HandleGotFocus(CustomControl parent, Point focusLocation)
        {
            foreach (var element in elements)
            {
                if (element.Visible && element.HitBox(focusLocation))
                {
                    
                    element.RaiseGotFocus(parent);
                    break;
                }
            }
        }

        public void HandleLostFocus(CustomControl parent, Point focusLocation)
        {
            foreach (var element in elements)
            {
                if (element.Visible && element.HitBox(focusLocation))
                {
                    element.RaiseLostFocus(parent);
                    break;
                }
            }
        }
        public IEnumerable<InnerControl> GetAll() => elements;
    }


    public abstract class InnerControl
    {
        public bool Visible { get; set; } = true;
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public Color ForeColor { get; set; } = SystemColors.ControlText;
        //public Color HoverForeColor { get; set; } = SystemColors.ControlText;
        public Color BackgroundColor { get; set; } = SystemColors.Control;
        //public Color hoverBackgroundColor { get; set; } = SystemColors.Control;
        public Size Size { get; set; } = new(0, 0);
        public Point Location { get; set; } = new(0, 0);
        public Rectangle Bounds => new(Location, Size);
        public bool HitBox(Point p) => Bounds.Contains(p);
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


        private bool isHovering = false;

        public bool IsHovering => isHovering;

        public event EventHandler? Click;  // evento estilo padrão .NET
        public event EventHandler? DoubleClick;

        public event EventHandler? GotFocus;
        public event EventHandler? LostFocus;

        public event EventHandler? MouseEnter;
        public event EventHandler? MouseLeave;


        public void RaiseClick(object sender)
        {
            Click?.Invoke(sender, EventArgs.Empty);
        }

        public void RaiseDoubleClick(object sender)
        {
            DoubleClick?.Invoke(sender, EventArgs.Empty);
        }
        public void RaiseGotFocus(object sender)
        {
            GotFocus?.Invoke(sender, EventArgs.Empty);
        }

        public void RaiseLostFocus(object sender)
        {
            LostFocus?.Invoke(sender, EventArgs.Empty);
        }
        public virtual void OnMouseEnter()
        {
            if (!isHovering)
            {
                isHovering = true;
                MouseEnter?.Invoke(this, EventArgs.Empty);
            }
        }
        public virtual void OnMouseLeave()
        {
            if (isHovering)
            {
                isHovering = false;
                MouseLeave?.Invoke(this, EventArgs.Empty);
            }
        }

        public abstract void OnPaint(CustomControl parent, PaintEventArgs e);
    }

}
