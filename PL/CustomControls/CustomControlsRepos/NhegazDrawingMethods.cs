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

namespace AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos
{
    public static class NhegazDrawingMethods
    {
        public static void DrawControl(CustomControl control, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = 2 * control.BorderRadius;

                path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90); //Arco superior Esquerdo
                path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90); //Arco superior Direito
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); //Arco Infeiror Direito
                path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90); //Arco Infeiror Esquerdo
                path.CloseFigure();
                using (Region region = new Region(path))
                {
                    e.Graphics.Clip = region;

                    using (SolidBrush brush = new SolidBrush(control.BackgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    e.Graphics.ResetClip();
                }
                DrawBorder(control, e);
            }
        }

        static float RoundFloat(float valor)
        {
            float parteDecimal = valor - (int)valor;

            // Verifica se a parte decimal é exatamente 0.5
            if (Math.Abs(parteDecimal - 0.5f) < 0.00001f)
            {
                return valor; // mantém com .5
            }
            else
            {
                return (float)Math.Round(valor); // arredonda normalmente
            }
        }
        private static List<PointF> GenerateArcPoints(bool isInner, float BorderRadius)//Método que gera os pontos do arco
        {

            float radius = isInner ? BorderRadius * 0.9f : BorderRadius;
            int segments = (int)radius / 2;

            List<PointF> points = new(); //lista que armazena os pontos do arco

            for (int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments; //Valor do progresso atual até o final do arco(de 0 a 1)
                float angle = (float)(Math.PI / 2 * t); //Valor do angulo do progresso atual(de 0° até 90°)
                float fx = radius * (1 - (float)Math.Cos(angle)); //Gera o X do ponto atual
                float fy = radius * (1 - (float)Math.Sin(angle)); //Gera o Y do ponto atual
                var x = RoundFloat(fx);
                var y = RoundFloat(fy);
                points.Add(new PointF(x, y)); //Adiciona o ponto a lista de pontos
            }
            if (isInner) //Se for o arco interno os pontos são criados na ordem contraria para que o path seja fechado corretamente
            {
                points.Reverse(); //inverte a ordem dos pontos
            }
            return points;
        }

        //Retorna uma nova cor que é a proporção entre de n/10 da primeira em relação a segunda
        private static Color InterpolateColor(int weightFrom1To10, Color color1, Color color2) 

        {           
            weightFrom1To10 = Math.Max(1, Math.Min(10, weightFrom1To10));

            float ratio1 = weightFrom1To10 / 10f;
            float ratio2 = 1f - ratio1;

            int r = (int)(color1.R * ratio1 + color2.R * ratio2);
            int g = (int)(color1.G * ratio1 + color2.G * ratio2);
            int b = (int)(color1.B * ratio1 + color2.B * ratio2);

            return Color.FromArgb(r, g, b);
        }
        public static void DrawBorder(CustomControl control, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int borderRadius = control.BorderRadius;
            if (borderRadius <= 0) return;

            float BorderRadius = borderRadius; //Transforma o valor de BorderRadius em float
            int width = control.Width - 1; //Ajuste necessario do Width para ficar dentro do tamanho do control
            int height = control.Height - 1; //Ajuste necessario do Height para ficar dentro do tamanho do control
            Color arcsColor = control.OnFocusBool ? InterpolateColor(10, control.BorderColorFocus, control.BackgroundColor)
                                                  : InterpolateColor(10, control.BorderColor, control.BackgroundColor);
            Pen arcsPen = new(arcsColor, 1.0f);

            int segments = borderRadius / 2;
            var arcPoints = GenerateArcPoints(false, borderRadius);

            GraphicsPath topLeftPath = new();
            GraphicsPath topRightPath = new();
            GraphicsPath bottomLeftPath = new();
            GraphicsPath bottomRightPath = new();

            foreach (var p in arcPoints)
            {
                // Arco Superior Esquerdo  
                topLeftPath.AddLine(new PointF(p.X, p.Y), new PointF(p.X, p.Y));

                // Arco Superior Direito
                topRightPath.AddLine(new PointF(width - p.X, p.Y), new PointF(width - p.X, p.Y));

                // Arco Inferior Esquerdo
                bottomLeftPath.AddLine(new PointF(p.X, height - p.Y), new PointF(p.X, height - p.Y));

                // Arco Inferior Direito
                bottomRightPath.AddLine(new PointF(width - p.X, height - p.Y), new PointF(width - p.X, height - p.Y));
            }

            if (control.BorderWidth > 1)
            {
                var innerArcPoints = GenerateArcPoints(true, borderRadius * 0.9f);
                int offSet = control.BorderWidth - 1;

                foreach (var p in innerArcPoints)
                {

                    //Arco Superior Esquerdo
                    topLeftPath.AddLine(new PointF(p.X + offSet, p.Y + offSet), new PointF(p.X + offSet, p.Y + offSet)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto

                    //Arco Superior Direito             
                    float topRightX = width - p.X;
                    float topRightY = p.Y;
                    topRightPath.AddLine(new PointF(topRightX - offSet, topRightY + offSet), new PointF(topRightX - offSet, topRightY + offSet)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto                

                    // Arco inferior Esquerdo              
                    float bottomLeftX = p.X;
                    float bottomLeftY = height - p.Y;
                    bottomLeftPath.AddLine(new PointF(bottomLeftX + offSet, bottomLeftY - offSet), new PointF(bottomLeftX + offSet, bottomLeftY - offSet)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto

                    // Arco inferior Direito
                    float bottomRightX = width - p.X;
                    float bottomRightY = height - p.Y;
                    bottomRightPath.AddLine(new PointF(bottomRightX - offSet, bottomRightY - offSet), new PointF(bottomRightX - offSet, bottomRightY - offSet)); //Para cada ponto na lista de Pontos, cria uma Line de um unico ponto
                }
                //SmoothBorderArcs(int quality, int BorderRadius);

                SolidBrush brush = new SolidBrush(arcsColor);

                topLeftPath.CloseFigure(); e.Graphics.FillPath(brush, topLeftPath); //F
                topRightPath.CloseFigure(); e.Graphics.FillPath(brush, topRightPath);
                bottomLeftPath.CloseFigure(); e.Graphics.FillPath(brush, bottomLeftPath);
                bottomRightPath.CloseFigure(); e.Graphics.FillPath(brush, bottomRightPath);
            }
            e.Graphics.DrawPath(arcsPen, topLeftPath);
            e.Graphics.DrawPath(arcsPen, topRightPath);
            e.Graphics.DrawPath(arcsPen, bottomLeftPath);
            e.Graphics.DrawPath(arcsPen, bottomRightPath);


            int borderWidth = control.BorderWidth * 2 - 1; //Ajuste necessario para o enquadramento da caneta
            int borderFocusWidth = (control.BorderWidth + control.BorderFocusExtraWidth) * 2 - 1; //Ajuste necessario para o enquadramento da caneta

            int penWidth = control.OnFocusBool ? borderFocusWidth : borderWidth; //Largura da caneta
            Color baseColor = control.OnFocusBool ? control.BorderColorFocus : control.BorderColor; //Cor da caneta
            Pen pen = new(baseColor, penWidth);

            int ExtraLenght = pen.Width > 1 ? 1 : 0;
            e.Graphics.DrawLine(pen, borderRadius, 0, width - borderRadius + ExtraLenght, 0); //Linha Superior
            e.Graphics.DrawLine(pen, 0, borderRadius, 0, height - borderRadius + ExtraLenght); //Linha Esquerda
            e.Graphics.DrawLine(pen, width, borderRadius, width, height - borderRadius + ExtraLenght); //Linha Direita
            e.Graphics.DrawLine(pen, 0 + borderRadius, height, width - borderRadius + ExtraLenght, height); //Linha Inferior   
        }

        //public static void SmoothBorderArcs(bool hasInner,int quality, int BorderRadius) 
        //{
        //    int outerAdjust = BorderRadius + 1;
        //    int innerAdjust = BorderRadius - 1;
        //    GraphicsPath topLeftPath = new();
        //    GraphicsPath topRightPath = new();
        //    GraphicsPath bottomLeftPath = new();
        //    GraphicsPath bottomRightPath = new();
        //   for (int i = 0; i < quality; i++)
        //   {
        //       // Arco Superior Esquerdo  
        //       topLeftPath.AddLine(new PointF(p.X, p.Y), new PointF(p.X, p.Y));
        // Arco Superior Direito
        //        topRightPath.AddLine(new PointF(width - p.X, p.Y), new PointF(width - p.X, p.Y));
        // Arco Inferior Esquerdo
        //        bottomLeftPath.AddLine(new PointF(p.X, height - p.Y), new PointF(p.X, height - p.Y));
        // Arco Inferior Direito
        //        bottomRightPath.AddLine(new PointF(width - p.X, height - p.Y), new PointF(width - p.X, height - p.Y));
        //        var outerArcPoints = GenerateArcPoints(false, outerAdjust);
        //        if(hasInner == true)
        //        {
        //            var innerArcPoints = GenerateArcPoints(true, BorderRadius * 0.9f);
        //        }             
        //        outerAdjust++;
        //
        //    }
        //}
    }
}