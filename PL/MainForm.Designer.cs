using AVBC_CLCB_Notifier.PL.CustomControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace PL
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView1 = new DataGridView();
            searchButton = new Button();
            editButton = new Button();
            comboBox1 = new RoundedComboBox();
            label1 = new Label();
            addButton = new Button();
            deleteButton = new Button();
            TableSelectorComboBox = new ComboBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            roundedComboBox1 = new RoundedComboBox();
            roundedComboBox2 = new RoundedComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 121);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(718, 239);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(609, 90);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(124, 22);
            searchButton.TabIndex = 2;
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // editButton
            // 
            editButton.Location = new Point(455, 63);
            editButton.Name = "editButton";
            editButton.Size = new Size(136, 22);
            editButton.TabIndex = 3;
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.Transparent;
            comboBox1.BackgroundColor = Color.White;
            comboBox1.BorderColor = Color.Red;
            comboBox1.BorderColorFocus = Color.Blue;
            comboBox1.BorderFocusExtraWidth = 1;
            comboBox1.BorderRadius = 8;
            comboBox1.BorderWidth = 1;
            comboBox1.ForeColor = SystemColors.ControlText;
            comboBox1.Horizontalpadding = 1;
            comboBox1.ItemList = (List<string>)resources.GetObject("comboBox1.ItemList");
            comboBox1.Location = new Point(386, 91);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.MinimumSize = new Size(4, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.OnFocusBool = false;
            comboBox1.Size = new Size(205, 24);
            comboBox1.TabIndex = 4;
            comboBox1.Verticalpadding = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(302, 93);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 5;
            label1.Text = "Ordenar por";
            label1.Click += label1_Click;
            // 
            // addButton
            // 
            addButton.Location = new Point(309, 63);
            addButton.Margin = new Padding(3, 2, 3, 2);
            addButton.Name = "addButton";
            addButton.Size = new Size(136, 22);
            addButton.TabIndex = 6;
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(597, 63);
            deleteButton.Margin = new Padding(3, 2, 3, 2);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(136, 22);
            deleteButton.TabIndex = 7;
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // TableSelectorComboBox
            // 
            TableSelectorComboBox.BackColor = Color.WhiteSmoke;
            TableSelectorComboBox.Font = new Font("Segoe UI", 9F);
            TableSelectorComboBox.ForeColor = SystemColors.Desktop;
            TableSelectorComboBox.FormattingEnabled = true;
            TableSelectorComboBox.Items.AddRange(new object[] { "Processos ", "Clientes", "Edificações" });
            TableSelectorComboBox.Location = new Point(72, 17);
            TableSelectorComboBox.Name = "TableSelectorComboBox";
            TableSelectorComboBox.Size = new Size(121, 23);
            TableSelectorComboBox.TabIndex = 0;
            TableSelectorComboBox.SelectedIndexChanged += TableSelectorComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 20);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 8;
            label2.Text = "Tabela de ";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(TableSelectorComboBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 66);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(203, 50);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // roundedComboBox1
            // 
            roundedComboBox1.BackColor = Color.Transparent;
            roundedComboBox1.BackgroundColor = Color.White;
            roundedComboBox1.BorderColor = Color.Red;
            roundedComboBox1.BorderColorFocus = Color.Blue;
            roundedComboBox1.BorderFocusExtraWidth = 1;
            roundedComboBox1.BorderRadius = 12;
            roundedComboBox1.BorderWidth = 2;
            roundedComboBox1.Horizontalpadding = 2;
            roundedComboBox1.ItemList = (List<string>)resources.GetObject("roundedComboBox1.ItemList");
            roundedComboBox1.Location = new Point(270, 171);
            roundedComboBox1.MinimumSize = new Size(5, 5);
            roundedComboBox1.Name = "roundedComboBox1";
            roundedComboBox1.OnFocusBool = false;
            roundedComboBox1.Size = new Size(214, 66);
            roundedComboBox1.TabIndex = 9;
            roundedComboBox1.Verticalpadding = 25;
            // 
            // roundedComboBox2
            // 
            roundedComboBox2.BackColor = Color.Transparent;
            roundedComboBox2.BackgroundColor = Color.White;
            roundedComboBox2.BorderColor = Color.Red;
            roundedComboBox2.BorderColorFocus = Color.Blue;
            roundedComboBox2.BorderFocusExtraWidth = 1;
            roundedComboBox2.BorderRadius = 22;
            roundedComboBox2.BorderWidth = 3;
            roundedComboBox2.Horizontalpadding = 2;
            roundedComboBox2.ItemList = (List<string>)resources.GetObject("roundedComboBox2.ItemList");
            roundedComboBox2.Location = new Point(129, 186);
            roundedComboBox2.MinimumSize = new Size(5, 5);
            roundedComboBox2.Name = "roundedComboBox2";
            roundedComboBox2.OnFocusBool = false;
            roundedComboBox2.Size = new Size(110, 134);
            roundedComboBox2.TabIndex = 10;
            roundedComboBox2.Verticalpadding = 59;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(742, 371);
            Controls.Add(roundedComboBox2);
            Controls.Add(roundedComboBox1);
            Controls.Add(groupBox1);
            Controls.Add(deleteButton);
            Controls.Add(addButton);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(editButton);
            Controls.Add(searchButton);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button searchButton;
        private Button editButton;
        private RoundedComboBox comboBox1;
        private Label label1;
        private Button addButton;
        private Button deleteButton;
        private ComboBox TableSelectorComboBox;
        private Label label2;
        private GroupBox groupBox1;
        private RoundedComboBox roundedComboBox1;
        private RoundedComboBox roundedComboBox2;
    }
}
