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
            customDatePicker1 = new CustomDatePicker();
            dataGridView1 = new DataGridView();
            customDataGridView1 = new CustomDataGridView();
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
            comboBox1.BackgroundColor = SystemColors.Control;
            comboBox1.BorderColor = Color.Red;
            comboBox1.BorderColorFocus = Color.Blue;
            comboBox1.BorderFocusExtraWidth = 1;
            comboBox1.BorderRadius = 6;
            comboBox1.BorderWidth = 1;
            comboBox1.ForeColor = SystemColors.ControlDark;
            comboBox1.HeaderBackgroundColor = SystemColors.ButtonHighlight;
            comboBox1.HorizontalPadding = 5;
            comboBox1.ItemList = (System.Collections.Specialized.StringCollection)resources.GetObject("comboBox1.ItemList");
            comboBox1.Location = new Point(386, 91);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.MinimumSize = new Size(4, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.OnFocusBool = false;
            comboBox1.PaddingMode = CustomControl.PaddingModeEnum.Absolute;
            comboBox1.PaddingRelativePercent = 1F;
            comboBox1.SecondaryBackgroundColor = SystemColors.ControlLightLight;
            comboBox1.SecondaryForeColor = SystemColors.GrayText;
            comboBox1.SelectIndexText = "Teste123456";
            comboBox1.Size = new Size(205, 23);
            comboBox1.TabIndex = 4;
            comboBox1.VerticalPadding = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.GrayText;
            label1.Location = new Point(302, 93);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 5;
            label1.Text = "Ordenar por";
            label1.Click += label1_Click;
            // 
            // addButton
            // 
            addButton.BackColor = SystemColors.Control;
            addButton.Location = new Point(309, 63);
            addButton.Margin = new Padding(3, 2, 3, 2);
            addButton.Name = "addButton";
            addButton.Size = new Size(136, 22);
            addButton.TabIndex = 6;
            addButton.UseVisualStyleBackColor = false;
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
            // innerTextBox2
            // 
            innerTextBox2.BackColor = Color.White;
            innerTextBox2.Font = new Font("Segoe UI", 10F);
            innerTextBox2.ForeColor = Color.Black;
            innerTextBox2.Location = new Point(228, 44);
            innerTextBox2.Name = "innerTextBox2";
            innerTextBox2.Size = new Size(75, 23);
            innerTextBox2.TabIndex = 14;
            innerTextBox2.Text = "innerTextBox2";
            // 
            // customDatePicker1
            // 
            customDatePicker1.BackColor = Color.Transparent;
            customDatePicker1.BackgroundColor = SystemColors.Window;
            customDatePicker1.BorderColor = SystemColors.WindowFrame;
            customDatePicker1.BorderColorFocus = SystemColors.Highlight;
            customDatePicker1.BorderFocusExtraWidth = 1;
            customDatePicker1.BorderRadius = 6;
            customDatePicker1.BorderWidth = 3;
            customDatePicker1.ForeColor = SystemColors.ActiveCaption;
            customDatePicker1.HeaderBackgroundColor = SystemColors.GrayText;
            customDatePicker1.HorizontalPadding = 4;
            customDatePicker1.Location = new Point(85, 134);
            customDatePicker1.Margin = new Padding(3, 2, 3, 2);
            customDatePicker1.MinimumSize = new Size(96, 23);
            customDatePicker1.Name = "customDatePicker1";
            customDatePicker1.OnFocusBool = false;
            customDatePicker1.PaddingMode = CustomControl.PaddingModeEnum.Absolute;
            customDatePicker1.PaddingRelativePercent = 0.9F;
            customDatePicker1.SecondaryBackgroundColor = SystemColors.ControlLightLight;
            customDatePicker1.SecondaryForeColor = SystemColors.GrayText;
            customDatePicker1.Size = new Size(125, 30);
            customDatePicker1.TabIndex = 24;
            customDatePicker1.VerticalPadding = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(9, 281);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(822, 79);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // customDataGridView1
            // 
            customDataGridView1.BackgroundColor = SystemColors.Control;
            customDataGridView1.BorderColor = SystemColors.WindowFrame;
            customDataGridView1.BorderColorFocus = SystemColors.Highlight;
            customDataGridView1.BorderFocusExtraWidth = 1;
            customDataGridView1.BorderRadius = 9;
            customDataGridView1.BorderWidth = 1;
            customDataGridView1.ColumnWidthMode = CustomDataGridView.ColumnWidthModeEnum.HeaderWidth;
            customDataGridView1.DifferentColorsBetweenRows = true;
            customDataGridView1.FixedCharCount = 10;
            customDataGridView1.HeaderBackgroundColor = SystemColors.ActiveCaption;
            customDataGridView1.HorizontalPadding = 1;
            customDataGridView1.LinesBetweenColumns = true;
            customDataGridView1.LinesBetweenRows = true;
            customDataGridView1.LinesWidth = 1;
            customDataGridView1.Location = new Point(228, 119);
            customDataGridView1.Name = "customDataGridView1";
            customDataGridView1.OnFocusBool = false;
            customDataGridView1.PaddingMode = CustomControl.PaddingModeEnum.RelativeToFont;
            customDataGridView1.PaddingRelativePercent = 1F;
            customDataGridView1.SecondaryBackgroundColor = SystemColors.ControlLightLight;
            customDataGridView1.SecondaryForeColor = SystemColors.GrayText;
            customDataGridView1.Size = new Size(217, 150);
            customDataGridView1.TabIndex = 25;
            customDataGridView1.VerticalPadding = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(843, 371);
            Controls.Add(customDataGridView1);
            Controls.Add(customDatePicker1);
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
        private CustomComboBox comboBox1;
        private Label label1;
        private Button addButton;
        private Button deleteButton;
        private ComboBox TableSelectorComboBox;
        private Label label2;
        private GroupBox groupBox1;
        private InnerTextBox innerTextBox1;
        private InnerTextBox innerTextBox2;
        private CustomDatePicker customDatePicker1;
        private DataGridView dataGridView1;
        private CustomDataGridView customDataGridView1;
      
    }
}
