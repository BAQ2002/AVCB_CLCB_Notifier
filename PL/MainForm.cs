using AVBC_CLCB_Notifier.PL;
using AVBC_CLCB_Notifier.PL.Edit_Forms;
using BLL;
using MODEL;

namespace PL
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            TableSelectorComboBox.SelectedIndex = 0;
            
        }
        public class Carro
        {
            public string Cor { get; set; }
            public string Modelo { get; set; }
            public string Ano { get; set; }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Carro carro = new Carro();
            carro.Cor = "Red"; 
            carro.Modelo = "Uno";
            carro.Ano = "98";
            Carro carro1 = new Carro();
            carro1.Cor = "Red";
            carro1.Modelo = "Uno";
            carro1.Ano = "98";
            Carro carro2 = new Carro();
            carro2.Cor = "Red";
            carro2.Modelo = "Uno";
            carro2.Ano = "98";
            List<Carro> CarrosList = new List<Carro> { carro, carro1 , carro2 };

            List<string> optionsList = new List<string> { "Processo", "Cliente", "Edificação" };
            List<string> buttonsDefaultTextsList = new List<string> { "Pesquisar ", "Adicionar ", "Editar ", "Excluir " };
            searchButton.Text = buttonsDefaultTextsList[0] + optionsList[TableSelectorComboBox.SelectedIndex];
            addButton.Text = buttonsDefaultTextsList[1] + optionsList[TableSelectorComboBox.SelectedIndex];
            editButton.Text = buttonsDefaultTextsList[2] + optionsList[TableSelectorComboBox.SelectedIndex];
            deleteButton.Text = buttonsDefaultTextsList[3] + optionsList[TableSelectorComboBox.SelectedIndex];
            if (TableSelectorComboBox.SelectedIndex == 0)
            {
                List<DocProcess> Process = ProcessRepos.GetAllProcesses();
                dataGridView1.DataSource = Process;
            }
            else if (TableSelectorComboBox.SelectedIndex == 1)
            {
                List<Client> clients = ClientRepos.GetAllClients();
                dataGridView1.DataSource = clients;
            }
            else
            {
                List<Establishment> establishments = EstablishmentRepos.GetAllEstablishments();
                dataGridView1.DataSource = establishments;
            }
            customDataGridView1.SetDataSource(CarrosList);

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (TableSelectorComboBox.SelectedIndex == 0)
            {
                ProcessEditorForm form2 = new ProcessEditorForm(); // Cria uma instância do formulário
                form2.Show(); // Abre o formulário de forma independente
            }
            else if (TableSelectorComboBox.SelectedIndex == 1)
            {
                ClientEditorForm form2 = new ClientEditorForm(); // Cria uma instância do formulário
                form2.Show(); // Abre o formulário de forma independente
            }
            else
            {
                EstablishmentEditorForm form2 = new EstablishmentEditorForm(); // Cria uma instância do formulário
                form2.Show(); // Abre o formulário de forma independente
            }

        }
        private void editButton_Click(object sender, EventArgs e)
        {

        }
        private void deleteButton_Click(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TableSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm_Load(sender, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void roundedComboBox2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void customDatePicker1_Load(object sender, EventArgs e)
        {

        }
    }
}
