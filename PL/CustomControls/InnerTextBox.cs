using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class InnerTextBox : Control
    {
        private string text = "";
        private int caretIndex = 0;
        private int selectionStart = 0;
        private int selectionLength = 0;

        private bool isSelecting = false;     // Indica se o usuário está selecionando texto com o mouse
        private int selectionAnchor = 0;      // Ponto inicial da seleção (posição onde clicou)

        private Timer caretTimer;
        private bool showCaret = true;

        public InnerTextBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);

            Font = new Font("Segoe UI", 10f);
            BackColor = Color.White;
            ForeColor = Color.Black;

            // Timer para piscar o cursor
            caretTimer = new Timer();
            caretTimer.Interval = 500;
            caretTimer.Tick += (s, e) =>
            {
                showCaret = !showCaret;
                Invalidate();
            };
            caretTimer.Start();

            this.DoubleBuffered = true;
        }

        public override string Text
        {
            get => text;
            set
            {
                text = value ?? "";
                caretIndex = Math.Min(text.Length, caretIndex);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.Clear(BackColor);

            var textFormat = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding;
            Rectangle textRect = new Rectangle(2, 0, Width - 4, Height);

            // Desenha seleção (se houver)
            if (selectionLength > 0)
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength);
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength);

                string preText = text.Substring(0, selStart);
                string selText = text.Substring(selStart, selEnd - selStart);

                int xStart = TextRenderer.MeasureText(preText, Font, textRect.Size, textFormat).Width;
                int xSel = TextRenderer.MeasureText(selText, Font, textRect.Size, textFormat).Width;

                Rectangle selectionRect = new Rectangle(2 + xStart, 2, xSel, Height - 4);
                g.FillRectangle(SystemBrushes.Highlight, selectionRect);
            }

            // Desenha texto
            TextRenderer.DrawText(g, text, Font, textRect, ForeColor, textFormat);

            // Desenha texto selecionado com cor invertida
            if (selectionLength > 0)
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength);
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength);

                string preText = text.Substring(0, selStart);
                string selText = text.Substring(selStart, selEnd - selStart);

                int xStart = TextRenderer.MeasureText(preText, Font, textRect.Size, textFormat).Width;

                Rectangle selRect = new Rectangle(2 + xStart, 0, Width - xStart - 2, Height);
                TextRenderer.DrawText(g, selText, Font, new Point(2 + xStart, (Height - Font.Height) / 2), SystemColors.HighlightText, textFormat);
            }

            // Desenha cursor (caret)
            if (Focused && showCaret && selectionLength == 0)
            {
                string caretText = text.Substring(0, caretIndex);
                int caretX = TextRenderer.MeasureText(caretText, Font, textRect.Size, textFormat).Width;

                using (Pen caretPen = new Pen(ForeColor))
                {
                    g.DrawLine(caretPen, 2 + caretX, 3, 2 + caretX, Height - 4);
                }
            }

            // Borda do controle
            ControlPaint.DrawBorder(g, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Left)
            {
                if (caretIndex > 0)
                    caretIndex--;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (caretIndex < text.Length)
                    caretIndex++;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (selectionLength > 0)
                {
                    DeleteSelection();
                }
                else if (caretIndex > 0)
                {
                    text = text.Remove(caretIndex - 1, 1);
                    caretIndex--;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (selectionLength > 0)
                {
                    DeleteSelection();
                }
                else if (caretIndex < text.Length)
                {
                    text = text.Remove(caretIndex, 1);
                }
            }

            selectionLength = 0;
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (!char.IsControl(e.KeyChar))
            {
                DeleteSelection(); // Apaga seleção antes de digitar
                text = text.Insert(caretIndex, e.KeyChar.ToString());
                caretIndex++;
                selectionLength = 0;
                Invalidate();
            }
        }

        private void DeleteSelection()
        {
            if (selectionLength > 0)
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength);
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength);

                text = text.Remove(selStart, selEnd - selStart);
                caretIndex = selStart;
                selectionLength = 0;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            showCaret = true;
            caretTimer.Start();
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            caretTimer.Stop();
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            // Permite usar setas
            if (keyData == Keys.Left || keyData == Keys.Right)
                return true;
            return base.IsInputKey(keyData);
        }

        // ========== Seleção com o mouse ==========

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            int index = GetCaretIndexFromX(e.X - 2);
            caretIndex = index;

            // Começa a seleção
            selectionAnchor = index;
            selectionStart = index;
            selectionLength = 0;
            isSelecting = true;

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isSelecting)
            {
                int index = GetCaretIndexFromX(e.X - 2);
                caretIndex = index;

                if (index < selectionAnchor)
                {
                    selectionStart = index;
                    selectionLength = selectionAnchor - index;
                }
                else
                {
                    selectionStart = selectionAnchor;
                    selectionLength = index - selectionAnchor;
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isSelecting = false;
        }

        // Converte a posição X do mouse em índice de caractere
        private int GetCaretIndexFromX(int x)
        {
            for (int i = 0; i <= text.Length; i++)
            {
                string substr = text.Substring(0, i);
                int width = TextRenderer.MeasureText(substr, Font).Width;
                if (x < width)
                    return i;
            }

            return text.Length;
        }
    }

}
