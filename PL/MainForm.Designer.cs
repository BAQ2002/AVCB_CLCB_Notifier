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
            comboBox1 = new CustomComboBox();
            label1 = new Label();
            addButton = new Button();
            deleteButton = new Button();
            TableSelectorComboBox = new ComboBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            innerTextBox1 = new InnerTextBox();
            innerTextBox2 = new InnerTextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            checkBox1 = new CheckBox();
            dateTimePicker1 = new DateTimePicker();
            textBox2 = new TextBox();
            dataGridView1 = new DataGridView();
            customDatePicker1 = new CustomDatePicker();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(696, 120);
            searchButton.Margin = new Padding(3, 4, 3, 4);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(142, 29);
            searchButton.TabIndex = 2;
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // editButton
            // 
            editButton.Location = new Point(520, 84);
            editButton.Margin = new Padding(3, 4, 3, 4);
            editButton.Name = "editButton";
            editButton.Size = new Size(155, 29);
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
            comboBox1.BorderWidth = 2;
            comboBox1.ForeColor = SystemColors.ActiveCaption;
            comboBox1.HorizontalPadding = 2;
            comboBox1.ItemList = (System.Collections.Specialized.StringCollection)resources.GetObject("comboBox1.ItemList");
            comboBox1.Location = new Point(441, 121);
            comboBox1.MinimumSize = new Size(5, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.OnFocusBool = false;
            comboBox1.SecondaryForeColor = SystemColors.GrayText;
            comboBox1.Size = new Size(234, 31);
            comboBox1.TabIndex = 4;
            comboBox1.VerticalPadding = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.GrayText;
            label1.Location = new Point(345, 124);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 5;
            label1.Text = "Ordenar por";
            label1.Click += label1_Click;
            // 
            // addButton
            // 
            addButton.BackColor = SystemColors.Control;
            addButton.Location = new Point(353, 84);
            addButton.Name = "addButton";
            addButton.Size = new Size(155, 29);
            addButton.TabIndex = 6;
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(682, 84);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(155, 29);
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
            TableSelectorComboBox.Location = new Point(82, 23);
            TableSelectorComboBox.Margin = new Padding(3, 4, 3, 4);
            TableSelectorComboBox.Name = "TableSelectorComboBox";
            TableSelectorComboBox.Size = new Size(138, 28);
            TableSelectorComboBox.TabIndex = 0;
            TableSelectorComboBox.SelectedIndexChanged += TableSelectorComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 27);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 8;
            label2.Text = "Tabela de ";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(TableSelectorComboBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(14, 88);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(232, 67);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // innerTextBox1
            // 
            innerTextBox1.BackColor = Color.White;
            innerTextBox1.Font = new Font("Segoe UI", 10F);
            innerTextBox1.ForeColor = Color.Black;
            innerTextBox1.Location = new Point(96, 33);
            innerTextBox1.Margin = new Padding(3, 4, 3, 4);
            innerTextBox1.Name = "innerTextBox1";
            innerTextBox1.Size = new Size(111, 31);
            innerTextBox1.TabIndex = 13;
            innerTextBox1.Text = "innerTextBox1";
            // 
            // innerTextBox2
            // 
            innerTextBox2.BackColor = Color.White;
            innerTextBox2.Font = new Font("Segoe UI", 10F);
            innerTextBox2.ForeColor = Color.Black;
            innerTextBox2.Location = new Point(261, 59);
            innerTextBox2.Margin = new Padding(3, 4, 3, 4);
            innerTextBox2.Name = "innerTextBox2";
            innerTextBox2.Size = new Size(86, 31);
            innerTextBox2.TabIndex = 14;
            innerTextBox2.Text = "innerTextBox2";
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.MenuHighlight;
            textBox1.Location = new Point(498, 303);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(114, 27);
            textBox1.TabIndex = 15;
            textBox1.Text = "ssss";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(645, 384);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 17;
            label3.Text = "label3";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(441, 303);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 24);
            checkBox1.TabIndex = 20;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarForeColor = SystemColors.ActiveCaption;
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ControlLight;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ButtonHighlight;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(184, 419);
            dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(109, 27);
            dateTimePicker1.TabIndex = 21;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(141, 337);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(114, 27);
            textBox2.TabIndex = 23;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 173);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(824, 307);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // customDatePicker1
            // 
            customDatePicker1.BackColor = Color.Transparent;
            customDatePicker1.BackgroundColor = SystemColors.Window;
            customDatePicker1.BorderColor = SystemColors.WindowFrame;
            customDatePicker1.BorderColorFocus = SystemColors.Highlight;
            customDatePicker1.BorderFocusExtraWidth = 1;
            customDatePicker1.BorderRadius = 5;
            customDatePicker1.BorderWidth = 1;
            customDatePicker1.HorizontalPadding = 5;
            customDatePicker1.Location = new Point(72, 193);
            customDatePicker1.MinimumSize = new Size(125, 30);
            customDatePicker1.Name = "customDatePicker1";
            customDatePicker1.OnFocusBool = false;
            customDatePicker1.SecondaryForeColor = SystemColors.GrayText;
            customDatePicker1.Size = new Size(125, 30);
            customDatePicker1.TabIndex = 24;
            customDatePicker1.VerticalPadding = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(848, 495);
            Controls.Add(customDatePicker1);
            Controls.Add(textBox2);
            Controls.Add(dateTimePicker1);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(innerTextBox2);
            Controls.Add(innerTextBox1);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            Controls.Add(deleteButton);
            Controls.Add(addButton);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(editButton);
            Controls.Add(searchButton);
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
        private CustomComboBox comboBox1;
        private Label label1;
        private Button addButton;
        private Button deleteButton;
        private ComboBox TableSelectorComboBox;
        private Label label2;
        private GroupBox groupBox1;
        private InnerTextBox innerTextBox1;
        private InnerTextBox innerTextBox2;
        private TextBox textBox1;
        private Label label3;
        private CheckBox checkBox1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox2;
        private DataGridView dataGridView1;
        private CustomDatePicker customDatePicker1;
    }
}
