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

    public class InnerTextBox : Control // Classe personalizada que simula um TextBox com renderização manual
    {
        private string text = ""; // Texto interno do controle
        private int caretIndex = 0; // Índice do cursor (posição onde o texto será inserido)
        private int selectionStart = 0; // Posição inicial da seleção
        private int selectionLength = 0; // Tamanho da seleção

        private bool isSelecting = false; // Indica se o usuário está selecionando texto com o mouse
        private int selectionAnchor = 0; // Ponto onde a seleção começou (posição do clique)

        private Timer caretTimer; // Timer responsável por piscar o cursor
        private bool showCaret = true; // Flag para indicar se o cursor deve estar visível

        public InnerTextBox() // Construtor da classe
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true); // Habilita estilos para desenhar manualmente e melhorar a performance

            Font = new Font("Segoe UI", 10f); // Fonte padrão do controle
            BackColor = Color.White; // Cor de fundo
            ForeColor = Color.Black; // Cor do texto

            caretTimer = new Timer(); // Instancia o timer para o cursor
            caretTimer.Interval = 500; // Intervalo de 500ms para piscar
            caretTimer.Tick += (s, e) => // Alterna a visibilidade do cursor a cada tick
            {
                showCaret = !showCaret;
                Invalidate(); // Redesenha o controle
            };
            caretTimer.Start(); // Inicia o timer

            this.DoubleBuffered = true; // Evita flickering ao redesenhar
        }

        public override string Text // Sobrescreve a propriedade Text padrão
        {
            get => text; // Retorna o texto interno
            set // Define novo texto
            {
                text = value ?? ""; // Garante que o texto não seja nulo
                caretIndex = Math.Min(text.Length, caretIndex); // Ajusta o cursor se necessário
                Invalidate(); // Redesenha o controle
            }
        }

        protected override void OnPaint(PaintEventArgs e) // Desenha o controle
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.Clear(BackColor); // Limpa o fundo

            var textFormat = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding; // Formatação do texto
            Rectangle textRect = new Rectangle(2, 0, Width - 4, Height); // Retângulo onde o texto será desenhado

            if (selectionLength > 0) // Se houver seleção de texto
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength); // Índice inicial da seleção
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength); // Índice final da seleção

                string preText = text.Substring(0, selStart); // Texto antes da seleção
                string selText = text.Substring(selStart, selEnd - selStart); // Texto selecionado

                int xStart = TextRenderer.MeasureText(preText, Font, textRect.Size, textFormat).Width; // Posição inicial da seleção
                int xSel = TextRenderer.MeasureText(selText, Font, textRect.Size, textFormat).Width; // Largura da seleção

                Rectangle selectionRect = new Rectangle(2 + xStart, 2, xSel, Height - 4); // Retângulo da área selecionada
                g.FillRectangle(SystemBrushes.Highlight, selectionRect); // Preenche com a cor de seleção do sistema
            }

            TextRenderer.DrawText(g, text, Font, textRect, ForeColor, textFormat); // Desenha o texto normal

            if (selectionLength > 0) // Se houver seleção, redesenha o texto selecionado com cor invertida
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength);
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength);

                string preText = text.Substring(0, selStart);
                string selText = text.Substring(selStart, selEnd - selStart);

                int xStart = TextRenderer.MeasureText(preText, Font, textRect.Size, textFormat).Width;

                TextRenderer.DrawText(g, selText, Font, new Point(2 + xStart, 12), SystemColors.HighlightText, textFormat);
            }

            if (Focused && showCaret && selectionLength == 0) // Se o controle estiver focado, mostra o cursor
            {
                string caretText = text.Substring(0, caretIndex); // Texto até o cursor
                int caretX = TextRenderer.MeasureText(caretText, Font, textRect.Size, textFormat).Width; // Posição X do cursor

                using (Pen caretPen = new Pen(ForeColor)) // Desenha uma linha vertical como cursor
                {
                    g.DrawLine(caretPen, 2 + caretX, 3, 2 + caretX, Height - 4);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) // Trata pressionamento de teclas especiais
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Left && caretIndex > 0) caretIndex--; // Move cursor para a esquerda
            else if (e.KeyCode == Keys.Right && caretIndex < text.Length) caretIndex++; // Move cursor para a direita
            else if (e.KeyCode == Keys.Back) // Tecla Backspace
            {
                if (selectionLength > 0) DeleteSelection(); // Apaga seleção
                else if (caretIndex > 0)
                {
                    text = text.Remove(caretIndex - 1, 1); // Remove caractere à esquerda
                    caretIndex--;
                }
            }
            else if (e.KeyCode == Keys.Delete) // Tecla Delete
            {
                if (selectionLength > 0) DeleteSelection(); // Apaga seleção
                else if (caretIndex < text.Length) text = text.Remove(caretIndex, 1); // Remove caractere à direita
            }

            selectionLength = 0; // Limpa a seleção
            Invalidate(); // Redesenha
        }

        protected override void OnKeyPress(KeyPressEventArgs e) // Trata inserção de caracteres
        {
            base.OnKeyPress(e);

            if (!char.IsControl(e.KeyChar)) // Se for caractere imprimível
            {
                DeleteSelection(); // Apaga seleção antes de inserir
                text = text.Insert(caretIndex, e.KeyChar.ToString()); // Insere caractere
                caretIndex++; // Move cursor
                selectionLength = 0; // Limpa seleção
                Invalidate(); // Redesenha
            }
        }

        private void DeleteSelection() // Apaga o trecho selecionado
        {
            if (selectionLength > 0)
            {
                int selStart = Math.Min(selectionStart, selectionStart + selectionLength);
                int selEnd = Math.Max(selectionStart, selectionStart + selectionLength);

                text = text.Remove(selStart, selEnd - selStart); // Remove texto
                caretIndex = selStart; // Move cursor para o início da seleção
                selectionLength = 0;
            }
        }

        protected override void OnGotFocus(EventArgs e) // Quando o controle ganha foco
        {
            base.OnGotFocus(e);
            showCaret = true;
            caretTimer.Start(); // Começa a piscar o cursor
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e) // Quando o controle perde foco
        {
            base.OnLostFocus(e);
            caretTimer.Stop(); // Para de piscar o cursor
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData) // Permite usar teclas como setas
        {
            if (keyData == Keys.Left || keyData == Keys.Right)
                return true;
            return base.IsInputKey(keyData);
        }

        // ========== Lógica de Seleção com Mouse ==========

        protected override void OnMouseDown(MouseEventArgs e) // Início da seleção com clique
        {
            base.OnMouseDown(e);
            Focus(); // Garante que o controle ganhe foco

            int index = GetCaretIndexFromX(e.X - 2); // Converte X do mouse para posição de caractere
            caretIndex = index;

            selectionAnchor = index; // Define ponto inicial da seleção
            selectionStart = index;
            selectionLength = 0;
            isSelecting = true;

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e) // Arrastar para selecionar texto
        {
            base.OnMouseMove(e);

            if (isSelecting) // Se estiver em processo de seleção
            {
                int index = GetCaretIndexFromX(e.X - 2); // Atualiza posição do cursor
                caretIndex = index;

                if (index < selectionAnchor) // Ajusta início/fim da seleção conforme direção do arraste
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

        protected override void OnMouseUp(MouseEventArgs e) // Finaliza a seleção
        {
            base.OnMouseUp(e);
            isSelecting = false;
        }

        private int GetCaretIndexFromX(int x) // Converte a posição X do mouse para índice do texto
        {
            for (int i = 0; i <= text.Length; i++)
            {
                string substr = text.Substring(0, i); // Texto parcial até a posição i
                int width = TextRenderer.MeasureText(substr, Font).Width; // Mede largura
                if (x < width) // Se X estiver dentro da largura, retorna índice
                    return i;
            }

            return text.Length; // Se passou de todo o texto, retorna o final
        }
    }


}
