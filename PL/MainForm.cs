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
            public string Marca {  get; set; }
            public string Modelo { get; set; }
            public string Cor { get; set; }     
            public string Ano { get; set; }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

            List<Carro> CarrosList = new List<Carro>
            {
                new Carro { Marca = "Volkswagen", Modelo = "Gol", Cor = "Branco", Ano = "2018" },
                new Carro { Marca = "Chevrolet", Modelo = "Onix", Cor = "Preto", Ano = "2020" },
                new Carro { Marca = "Fiat", Modelo = "Uno", Cor = "Prata", Ano = "2017" },
                new Carro { Marca = "Hyundai", Modelo = "HB20", Cor = "Azul", Ano = "2021" },
                new Carro { Marca = "Toyota", Modelo = "Corolla", Cor = "Cinza", Ano = "2019" },
                new Carro { Marca = "Ford", Modelo = "Ka", Cor = "Vermelho", Ano = "2018" },
                new Carro { Marca = "Renault", Modelo = "Sandero", Cor = "Branco", Ano = "2016" },
                new Carro { Marca = "Honda", Modelo = "Civic", Cor = "Preto", Ano = "2022" },
                new Carro { Marca = "Jeep", Modelo = "Renegade", Cor = "Verde", Ano = "2021" },
                new Carro { Marca = "Nissan", Modelo = "Kicks", Cor = "Prata", Ano = "2020" },
                new Carro { Marca = "Volkswagen", Modelo = "T-Cross", Cor = "Azul", Ano = "2023" },
                new Carro { Marca = "Chevrolet", Modelo = "Tracker", Cor = "Branco", Ano = "2022" },
                new Carro { Marca = "Fiat", Modelo = "Argo", Cor = "Cinza", Ano = "2021" },
                new Carro { Marca = "Toyota", Modelo = "Hilux", Cor = "Preto", Ano = "2020" },
                new Carro { Marca = "Ford", Modelo = "EcoSport", Cor = "Prata", Ano = "2019" },
                new Carro { Marca = "Hyundai", Modelo = "Creta", Cor = "Vermelho", Ano = "2022" },
                new Carro { Marca = "Renault", Modelo = "Logan", Cor = "Branco", Ano = "2020" },
                new Carro { Marca = "Honda", Modelo = "Fit", Cor = "Cinza", Ano = "2018" },
                new Carro { Marca = "Jeep", Modelo = "Compass", Cor = "Preto", Ano = "2023" },
                new Carro { Marca = "Nissan", Modelo = "Versa", Cor = "Azul", Ano = "2021" },
                new Carro { Marca = "Volkswagen", Modelo = "Voyage", Cor = "Prata", Ano = "2017" },
                new Carro { Marca = "Chevrolet", Modelo = "Spin", Cor = "Branco", Ano = "2019" },
                new Carro { Marca = "Fiat", Modelo = "Mobi", Cor = "Vermelho", Ano = "2022" },
                new Carro { Marca = "Toyota", Modelo = "Etios", Cor = "Cinza", Ano = "2016" },
                new Carro { Marca = "Ford", Modelo = "Fusion", Cor = "Preto", Ano = "2018" },
                new Carro { Marca = "Hyundai", Modelo = "i30", Cor = "Azul", Ano = "2017" },
                new Carro { Marca = "Renault", Modelo = "Duster", Cor = "Verde", Ano = "2020" },
                new Carro { Marca = "Honda", Modelo = "WR-V", Cor = "Branco", Ano = "2021" },
                new Carro { Marca = "Jeep", Modelo = "Commander", Cor = "Prata", Ano = "2023" },
                new Carro { Marca = "Nissan", Modelo = "March", Cor = "Vermelho", Ano = "2018" },
                new Carro { Marca = "Volkswagen", Modelo = "Fox", Cor = "Cinza", Ano = "2019" },
                new Carro { Marca = "Chevrolet", Modelo = "Cobalt", Cor = "Branco", Ano = "2020" },
                new Carro { Marca = "Fiat", Modelo = "Cronos", Cor = "Preto", Ano = "2021" },
                new Carro { Marca = "Toyota", Modelo = "Yaris", Cor = "Azul", Ano = "2022" },
                new Carro { Marca = "Ford", Modelo = "Ranger", Cor = "Cinza", Ano = "2023" },
                new Carro { Marca = "Hyundai", Modelo = "Tucson", Cor = "Prata", Ano = "2017" },
                new Carro { Marca = "Renault", Modelo = "Captur", Cor = "Preto", Ano = "2020" },
                new Carro { Marca = "Honda", Modelo = "City", Cor = "Branco", Ano = "2019" },
                new Carro { Marca = "Jeep", Modelo = "Cherokee", Cor = "Verde", Ano = "2018" },
                new Carro { Marca = "Nissan", Modelo = "Frontier", Cor = "Prata", Ano = "2021" },
                new Carro { Marca = "Volkswagen", Modelo = "Saveiro", Cor = "Vermelho", Ano = "2020" },
                new Carro { Marca = "Chevrolet", Modelo = "Montana", Cor = "Cinza", Ano = "2016" },
                new Carro { Marca = "Fiat", Modelo = "Strada", Cor = "Preto", Ano = "2023" },
                new Carro { Marca = "Toyota", Modelo = "SW4", Cor = "Branco", Ano = "2022" },
                new Carro { Marca = "Ford", Modelo = "Edge", Cor = "Azul", Ano = "2019" },
                new Carro { Marca = "Hyundai", Modelo = "Santa Fe", Cor = "Cinza", Ano = "2021" },
                new Carro { Marca = "Renault", Modelo = "Kwid", Cor = "Laranja", Ano = "2023" },
                new Carro { Marca = "Honda", Modelo = "HR-V", Cor = "Prata", Ano = "2020" }
            };

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
