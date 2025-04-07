using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVBC_CLCB_Notifier.PL
{
    public partial class ClientEditorForm : Form
    {
        private bool updating = false;

        public ClientEditorForm()
        {
            InitializeComponent();
            cpfInput.MaxLength = 14; // Define o tamanho máximo do CPF formatado
            cpfInput.TextChanged += cpfInput_TextChanged;
            cpfInput.KeyPress += cpfInput_KeyPress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientRepos.Add(nameInput.Text, cpfInput.Text, emailInput.Text, numInput.Text, DateTime.Now);
        }

        private void cpfInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e a tecla Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cpfInput_TextChanged(object sender, EventArgs e)
        {
            if (updating) return;

            updating = true;

            // Remove qualquer caractere não numérico
            string digits = new string(cpfInput.Text.Where(char.IsDigit).ToArray());

            // Aplica a máscara
            if (digits.Length > 3)
                digits = digits.Insert(3, ".");
            if (digits.Length > 7)
                digits = digits.Insert(7, ".");
            if (digits.Length > 11)
                digits = digits.Insert(11, "-");

            cpfInput.Text = digits;

            // Move o cursor para o final do texto
            cpfInput.SelectionStart = cpfInput.Text.Length;

            updating = false;
        }
    }
}
