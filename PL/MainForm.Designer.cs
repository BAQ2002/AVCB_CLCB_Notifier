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
            searchButton = new Button();
            editButton = new Button();
            comboBox1 = new RoundedComboBox();
            label1 = new Label();
            addButton = new Button();
            deleteButton = new Button();
            TableSelectorComboBox = new ComboBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            roundedDatePicker1 = new AVBC_CLCB_Notifier.PL.Templates.RoundedDatePicker();
            dateTimePicker1 = new DateTimePicker();
            innerTextBox1 = new InnerTextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
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
            comboBox1.BorderRadius = 7;
            comboBox1.BorderWidth = 1;
            comboBox1.ForeColor = SystemColors.ControlText;
            comboBox1.HorizontalPadding = 2;
            comboBox1.ItemList = (System.Collections.Specialized.StringCollection)resources.GetObject("comboBox1.ItemList");
            comboBox1.Location = new Point(386, 91);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.MinimumSize = new Size(4, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.OnFocusBool = false;
            comboBox1.Size = new Size(205, 23);
            comboBox1.TabIndex = 4;
            comboBox1.VerticalPadding = 4;
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
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 133);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(721, 230);
            dataGridView1.TabIndex = 10;
            // 
            // roundedDatePicker1
            // 
            roundedDatePicker1.BackColor = Color.Transparent;
            roundedDatePicker1.BackgroundColor = Color.White;
            roundedDatePicker1.BorderColor = Color.Red;
            roundedDatePicker1.BorderColorFocus = Color.Blue;
            roundedDatePicker1.BorderFocusExtraWidth = 1;
            roundedDatePicker1.BorderRadius = 5;
            roundedDatePicker1.BorderWidth = 1;
            roundedDatePicker1.ForeColor = SystemColors.ActiveCaption;
            roundedDatePicker1.HorizontalPadding = 2;
            roundedDatePicker1.ItemList = (System.Collections.Specialized.StringCollection)resources.GetObject("roundedDatePicker1.ItemList");
            roundedDatePicker1.Location = new Point(221, 231);
            roundedDatePicker1.MinimumSize = new Size(117, 5);
            roundedDatePicker1.Name = "roundedDatePicker1";
            roundedDatePicker1.OnFocusBool = false;
            roundedDatePicker1.Size = new Size(117, 23);
            roundedDatePicker1.TabIndex = 11;
            roundedDatePicker1.VerticalPadding = 4;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(92, 300);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(79, 23);
            dateTimePicker1.TabIndex = 12;
            // 
            // innerTextBox1
            // 
            innerTextBox1.BackColor = Color.White;
            innerTextBox1.Font = new Font("Segoe UI", 10F);
            innerTextBox1.ForeColor = Color.Black;
            innerTextBox1.Location = new Point(84, 25);
            innerTextBox1.Name = "innerTextBox1";
            innerTextBox1.Size = new Size(97, 23);
            innerTextBox1.TabIndex = 13;
            innerTextBox1.Text = "innerTextBox1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(742, 371);
            Controls.Add(innerTextBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(roundedDatePicker1);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            Controls.Add(deleteButton);
            Controls.Add(addButton);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(editButton);
            Controls.Add(searchButton);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button searchButton;
        private Button editButton;
        private RoundedComboBox comboBox1;
        private Label label1;
        private Button addButton;
        private Button deleteButton;
        private ComboBox TableSelectorComboBox;
        private Label label2;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private AVBC_CLCB_Notifier.PL.Templates.RoundedDatePicker roundedDatePicker1;
        private DateTimePicker dateTimePicker1;
        private InnerTextBox innerTextBox1;
    }
}
