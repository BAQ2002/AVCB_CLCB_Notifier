using AVBC_CLCB_Notifier.PL.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVBC_CLCB_Notifier.PL
{
    public static class NhegazDrawingMethods
    {
        public static void DrawControl(CustomControl control, PaintEventArgs e)
        {
            int borderRadius = control.BorderRadius;
            Color BackgroundColor = control.BackgroundColor;
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = 18;//OnFocusBool ? (borderRadius * 2) + borderFocusExtraWidth : borderRadius * 2;
                int radius = diameter / 2;              //int centerX = (borderRadius % 2 == 0) ? borderRadius / 2 : (borderRadius + 1) / 2;

                path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90); //Arco superior Esquerdo
                path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90); //Arco superior Direito
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); //Arco Infeiror Direito
                path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90); //Arco Infeiror Esquerdo
                path.CloseFigure();
                using (Region region = new Region(path))
                {
                    e.Graphics.Clip = region;

                    using (SolidBrush brush = new SolidBrush(BackgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    e.Graphics.ResetClip();
                }

                int borderWidth = (control.BorderWidth * 2) - 1;
                int borderFocusWidth = ((control.BorderWidth + control.BorderFocusExtraWidth) * 2) - 1;

                Pen pen = new(control.OnFocusBool ? control.BorderColorFocus : control.BorderColor,
                              control.OnFocusBool ? borderFocusWidth : borderWidth);

                Color arcsColor = Color.FromArgb(128, pen.Color.R, pen.Color.G, pen.Color.B);
                Pen arcsPen = new(arcsColor, 1);


                int ExtraLenght = (pen.Width > 1) ? 1 : 0;
                e.Graphics.DrawLine(pen, rect.Left + radius, rect.Top, rect.Right - radius + ExtraLenght, rect.Top); //Linha Superior
                e.Graphics.DrawLine(pen, rect.Left, rect.Top + radius, rect.Left, rect.Bottom - radius + ExtraLenght); //Linha Esquerda
                e.Graphics.DrawLine(pen, rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius + ExtraLenght); //Linha Direita
                e.Graphics.DrawLine(pen, rect.Left + radius, rect.Bottom, rect.Right - radius + ExtraLenght, rect.Bottom); //Linha Inferior
                //SmoothBorderArcs(diameter, rect, pen, e);
            }
        }

        private static List<PointF> GenerateCornerArcPoints(float radius, int segments = 12)
        {
            List<PointF> points = new();

            for (int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments; //Valor do progresso atual até o final do arco(de 0 a 1)
                float angle = (float)(Math.PI / 2 * t); //Valor do angulo atual até 90°
                float x = radius * (1 - (float)Math.Cos(angle)); //Gera o X do ponto atual
                float y = radius * (1 - (float)Math.Sin(angle)); //Gera o Y do ponto atual
                points.Add(new PointF(x, y)); //Adiciona o ponto a lista de pontos
            }
            return points;
        }

        public static void DrawPreciseRoundedBorder(CustomControl control, PaintEventArgs e)
        {
            int borderRadius = control.BorderRadius;
            if (borderRadius <= 0) return;

            float radius = borderRadius; //Transforma o valor de BorderRadius em float
            float width = control.Width - 1; //Ajuste necessario do Width para ficar dentro do tamanho do control
            float height = control.Height - 1; //Ajuste necessario do Height para ficar dentro do tamanho do control
            int borderWidth = (control.BorderWidth * 2) - 1; //Ajuste necessario para o enquadramento da caneta
            int borderFocusWidth = ((control.BorderWidth + control.BorderFocusExtraWidth) * 2) - 1; //Ajuste necessario para o enquadramento da caneta

            int penWidth = control.OnFocusBool ? borderFocusWidth : borderWidth; //Largura da caneta
            Color baseColor = control.OnFocusBool ? control.BorderColorFocus : control.BorderColor; //Cor da caneta       
            Pen pen = new(baseColor, penWidth);

            var arcPoints = GenerateCornerArcPoints(radius, 12);
            var innerArcPoints = GenerateCornerArcPoints(radius*0.9f, 12);

            GraphicsPath path = new();

            foreach (var p in arcPoints) //Arco Superior Esquerdo
                path.AddLine(new PointF(p.X, p.Y), new PointF(p.X, p.Y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto


            foreach (var p in arcPoints) //Arco Superior Direito
            {
                float x = width - p.Y;
                float y = p.X;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }

            foreach (var p in arcPoints) // Arco inferior Direito
            {
                float x = width - p.X;
                float y = height - p.Y;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }

            foreach (var p in arcPoints) // Arco inferior Esquerdo
            {
                float x = p.Y;
                float y = height - p.X;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }
            foreach (var p in arcPoints) //Arco Superior Esquerdo
                path.AddLine(new PointF(p.X, p.Y), new PointF(p.X, p.Y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto

            foreach (var p in arcPoints) //Arco Superior Direito
            {
                float x = width - p.Y;
                float y = p.X;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }

            foreach (var p in arcPoints) // Arco inferior Direito
            {
                float x = width - p.X;
                float y = height - p.Y;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }

            foreach (var p in arcPoints) // Arco inferior Esquerdo
            {
                float x = p.Y;
                float y = height - p.X;
                path.AddLine(new PointF(x, y), new PointF(x, y)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
            }
            if (control.BorderWidth > 1)
            {
                foreach (var p in innerArcPoints) //Arco Superior Esquerdo
                    path.AddLine(new PointF(p.X + 1, p.Y + 1), new PointF(p.X + 1, p.Y + 1)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto

                foreach (var p in innerArcPoints) //Arco Superior Direito
                {
                    float x = width - p.Y;
                    float y = p.X;
                    path.AddLine(new PointF(x - 1, y + 1), new PointF(x - 1, y + 1)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto

                }

                foreach (var p in innerArcPoints) // Arco inferior Direito
                {
                    float x = width - p.X;
                    float y = height - p.Y;
                    path.AddLine(new PointF(x - 1, y - 1), new PointF(x - 1, y - 1)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
                }

                foreach (var p in innerArcPoints) // Arco inferior Esquerdo
                {
                    float x = p.Y;
                    float y = height - p.X;
                    path.AddLine(new PointF(x + 1, y - 1), new PointF(x + 1, y - 1)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.DrawPath(pen, path);


        }
    }
}
