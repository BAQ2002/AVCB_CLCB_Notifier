using AVBC_CLCB_Notifier.PL.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AVBC_CLCB_Notifier.PL.CustomControls.InnerLabel;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public static partial class NhegazDrawingMethods
    {
            
        /// <summary>
        /// A partir das propriedades de CustomControl retorna um GraphicsPath que representa a area da sua Border.
        /// </summary>
        public static GraphicsPath ControlBorderPath(Rectangle customControlRect, float borderRadius, int borderWidth)
        {
            int width = customControlRect.Width - 1; //Ajuste necessario do Width para ficar dentro do tamanho do innerControl
            int height = customControlRect.Height - 1; //Ajuste necessario do Height para ficar dentro do tamanho do innerControl

            GraphicsPath borderPath = new();

            var arcTopLeft = GenerateArc(borderRadius); // 0° → 90°
            var arcTopRight = arcTopLeft.Select(p => new PointF(width - p.X, p.Y)).Reverse();
            var arcBottomRight = arcTopLeft.Select(p => new PointF(width - p.X, height - p.Y));
            var arcBottomLeft = arcTopLeft.Select(p => new PointF(p.X, height - p.Y)).Reverse();

            borderPath.StartFigure();

            borderPath.AddLines(arcTopLeft.ToArray());
            borderPath.AddLines(arcTopRight.ToArray());
            borderPath.AddLines(arcBottomRight.ToArray());
            borderPath.AddLines(arcBottomLeft.ToArray());

            borderPath.CloseFigure();

            if (borderWidth > 1)
            {
                int offset = borderWidth - 1;
                var arcInner = GenerateArc(borderRadius * 1f); // já vem invertido

                borderPath.StartFigure();

                var innerTopLeft = arcInner.Select(p => new PointF(p.X + offset, p.Y + offset));
                var innerTopRight = arcInner.Select(p => new PointF(width - p.X - offset, p.Y + offset)).Reverse();
                var innerBottomRight = arcInner.Select(p => new PointF(width - p.X - offset, height - p.Y - offset));
                var innerBottomLeft = arcInner.Select(p => new PointF(p.X + offset, height - p.Y - offset)).Reverse();

                borderPath.AddLines(innerTopLeft.ToArray());
                borderPath.AddLines(innerTopRight.ToArray());
                borderPath.AddLines(innerBottomRight.ToArray());
                borderPath.AddLines(innerBottomLeft.ToArray());

                borderPath.CloseFigure();
            }
            return borderPath;
        }
        /// <summary>
        /// A partir das propriedades de CustomControl retorna um GraphicsPath que representa a area interna do CustomControl.
        /// </summary>
        public static GraphicsPath ControlBackgroundPath(Rectangle customControlRect, float borderRadius, int borderWidth = 1)
        {
            int offset = borderWidth > 1 ? borderWidth - 1 : 0;

            int width = customControlRect.Width - 1; //Ajuste necessario do Width para ficar dentro do tamanho do innerControl
            int height = customControlRect.Height - 1; //Ajuste necessario do Height para ficar dentro do tamanho do innerControl

            var baseArc = GenerateArc(borderRadius); 
            GraphicsPath backgroundPath = new();

            backgroundPath.StartFigure();

            var arcTopLeft = baseArc.Select(p => new PointF(p.X + offset, p.Y + offset));
            var arcTopRight = baseArc.Select(p => new PointF(width - p.X - offset, p.Y + offset)).Reverse();
            var arcBottomRight = baseArc.Select(p => new PointF(width - p.X - offset, height - p.Y - offset));
            var arcBottomLeft = baseArc.Select(p => new PointF(p.X + offset, height - p.Y - offset)).Reverse();

            backgroundPath.AddLines(arcTopLeft.ToArray());
            backgroundPath.AddLines(arcTopRight.ToArray());
            backgroundPath.AddLines(arcBottomRight.ToArray());
            backgroundPath.AddLines(arcBottomLeft.ToArray());

            backgroundPath.CloseFigure();

            return backgroundPath;
        }

        public static GraphicsPath AddIconPath(InnerControl innerControl, int iconSize)
        {
            GraphicsPath path = new GraphicsPath();

            float centerX = innerControl.Location.X + (innerControl.Width / 2f);
            float centerY = innerControl.Location.Y + (innerControl.Height / 2f);

            float halfThickness = iconSize / 6f; // espessura dos traços
            float halfLength = iconSize / 2f;

            // Linha horizontal
            path.StartFigure();
            path.AddRectangle(new RectangleF(
                centerX - halfLength,
                centerY - halfThickness,
                iconSize,
                halfThickness * 2));

            // Linha vertical
            path.StartFigure();
            path.AddRectangle(new RectangleF(
                centerX - halfThickness,
                centerY - halfLength,
                halfThickness * 2,
                iconSize));

            return path;
        }
        public static GraphicsPath DropDownIconPath(InnerControl innerControl, int iconSize)
        {
            GraphicsPath iconPath = new GraphicsPath();

            float centerX = innerControl.Location.X + ((innerControl.Width - 1) / 2f);
            float centerY = innerControl.Location.Y + ((innerControl.Height - 1) / 2f);

            float halfIconSize = iconSize / 2f;
            float height = iconSize * (float)Math.Sqrt(3) / 2f; // altura de triângulo equilátero; pode ajustar se quiser mais achatado

            // Triângulo isósceles apontando para baixo
            PointF topLeft = new PointF(centerX - halfIconSize, centerY - height / 2);
            PointF topRight = new PointF(centerX + halfIconSize, centerY - height / 2);
            PointF bottomCenter = new PointF(centerX, centerY + height / 2);

            iconPath.StartFigure();
            iconPath.AddLine(topLeft, topRight);
            iconPath.AddLine(topRight, bottomCenter);
            iconPath.AddLine(bottomCenter, topLeft);
            iconPath.CloseFigure();

            return iconPath;
        }
        public static GraphicsPath ForwardIconPath(InnerControl innerControl, int iconSize)
        {
            GraphicsPath iconPath = new GraphicsPath();

            float centerX = innerControl.Location.X + ((innerControl.Width - 1) / 2f);
            float centerY = innerControl.Location.Y + ((innerControl.Height - 1) / 2f);

            float halfIconSize = iconSize / 2f;
            float height = iconSize * (float)Math.Sqrt(3) / 2f;

            // Triângulo apontando para a direita
            PointF top = new PointF(centerX - height / 2, centerY - halfIconSize);
            PointF middleRight = new PointF(centerX + height / 2, centerY);
            PointF bottom = new PointF(centerX - height / 2, centerY + halfIconSize);

            iconPath.StartFigure();
            iconPath.AddLine(top, middleRight);
            iconPath.AddLine(middleRight, bottom);
            iconPath.AddLine(bottom, top);
            iconPath.CloseFigure();

            return iconPath;
        }
        public static GraphicsPath BackwardIconPath(InnerControl innerControl, int iconSize)
        {
            GraphicsPath iconPath = new GraphicsPath();

            float centerX = innerControl.Location.X + ((innerControl.Width - 1) / 2f);
            float centerY = innerControl.Location.Y + ((innerControl.Height - 1) / 2f);

            float halfIconSize = iconSize / 2f;
            float height = iconSize * (float)Math.Sqrt(3) / 2f;

            // Triângulo apontando para a esquerda
            PointF top = new PointF(centerX + height / 2, centerY - halfIconSize);
            PointF middleLeft = new PointF(centerX - height / 2, centerY);
            PointF bottom = new PointF(centerX + height / 2, centerY + halfIconSize);

            iconPath.StartFigure();
            iconPath.AddLine(top, middleLeft);
            iconPath.AddLine(middleLeft, bottom);
            iconPath.AddLine(bottom, top);
            iconPath.CloseFigure();

            return iconPath;
        }

        /// <summary>
        /// A partir das propriedades de InnerControl retorna um GraphicsPath que representa a area interna do InnerControl.
        /// </summary>
        public static GraphicsPath InnerControlBackgroundPath(InnerControl innerControl)
        {
            int reference = innerControl.Height;
            float radius = reference / 2;         
            
            int locX = innerControl.Location.X;
            int locY = innerControl.Location.Y;

            int width = innerControl.Width - 1;
            int height = innerControl.Height - 1;
            
            GraphicsPath FullPath = new();
            FullPath.StartFigure();

            switch (innerControl.BackGroundShape)
            {               
                case BackGroundShape.FitRectangle:

                    Rectangle rect = new(innerControl.Location, innerControl.Size);
                    FullPath.AddRectangle(rect);

                    FullPath.CloseFigure();
                    return FullPath;

                case BackGroundShape.SymmetricCircle:
                    radius = reference / 2;

                    break;
                case BackGroundShape.RoundedRectangle:
                    radius = reference / 8;

                    break;                       
            }

            var baseArc = GenerateArc(radius);

            var arcTopLeft = baseArc.Select(p => new PointF(locX + p.X, locY + p.Y));
            var arcTopRight = baseArc.Select(p => new PointF(locX + (width - p.X), locY + p.Y)).Reverse();
            var arcBottomRight = baseArc.Select(p => new PointF(locX + (width - p.X), locY + (height - p.Y)));
            var arcBottomLeft = baseArc.Select(p => new PointF(locX + p.X, locY + (height - p.Y))).Reverse();

            FullPath.AddLines(arcTopLeft.ToArray());
            FullPath.AddLines(arcTopRight.ToArray());
            FullPath.AddLines(arcBottomRight.ToArray());
            FullPath.AddLines(arcBottomLeft.ToArray());

            FullPath.CloseFigure();      
            return FullPath;
        }

        public static GraphicsPath RectBackgroundPath(Rectangle rect, int radius)
        {           
            int locX = rect.Location.X;
            int locY = rect.Location.Y;

            int width = rect.Width-1;
            int height = rect.Height-1;

            radius = Math.Min(radius, Math.Min(width, height) / 2);

            GraphicsPath FullPath = new();
            FullPath.StartFigure();

            var baseArc = GenerateArc(radius);

            var arcTopLeft = baseArc.Select(p => new PointF(locX + p.X, locY + p.Y));
            var arcTopRight = baseArc.Select(p => new PointF(locX + (width - p.X), locY + p.Y)).Reverse();
            var arcBottomRight = baseArc.Select(p => new PointF(locX + (width - p.X), locY + (height - p.Y)));
            var arcBottomLeft = baseArc.Select(p => new PointF(locX + p.X, locY + (height - p.Y))).Reverse();

            FullPath.AddLines(arcTopLeft.ToArray());
            FullPath.AddLines(arcTopRight.ToArray());
            FullPath.AddLines(arcBottomRight.ToArray());
            FullPath.AddLines(arcBottomLeft.ToArray());

            FullPath.CloseFigure();
            return FullPath;
        }
    }
}