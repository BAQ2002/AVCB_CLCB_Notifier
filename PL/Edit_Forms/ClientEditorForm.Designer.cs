namespace AVBC_CLCB_Notifier.PL
{
    partial class ClientEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nameInput = new TextBox();
            cpfInput = new TextBox();
            numInput = new TextBox();
            emailInput = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // nameInput
            // 
            nameInput.Location = new Point(65, 48);
            nameInput.Margin = new Padding(3, 2, 3, 2);
            nameInput.Name = "nameInput";
            nameInput.Size = new Size(350, 23);
            nameInput.TabIndex = 0;
            // 
            // cpfInput
            // 
            cpfInput.Location = new Point(65, 82);
            cpfInput.Margin = new Padding(3, 2, 3, 2);
            cpfInput.Name = "cpfInput";
            cpfInput.Size = new Size(120, 23);
            cpfInput.TabIndex = 1;
            // 
            // numInput
            // 
            numInput.Location = new Point(302, 82);
            numInput.Name = "numInput";
            numInput.Size = new Size(112, 23);
            numInput.TabIndex = 2;
            // 
            // emailInput
            // 
            emailInput.Location = new Point(65, 121);
            emailInput.Name = "emailInput";
            emailInput.Size = new Size(350, 23);
            emailInput.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(150, 149);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 50);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 5;
            label1.Text = "Nome";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 84);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 6;
            label2.Text = "CPF";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(191, 85);
            label3.Name = "label3";
            label3.Size = new Size(105, 15);
            label3.TabIndex = 7;
            label3.Text = "Numero de celular";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 123);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 8;
            label4.Text = "Email";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(65, 149);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(79, 23);
            dateTimePicker1.TabIndex = 9;
            // 
            // ClientEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(445, 190);
            Controls.Add(dateTimePicker1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(emailInput);
            Controls.Add(numInput);
            Controls.Add(cpfInput);
            Controls.Add(nameInput);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ClientEditorForm";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameInput;
        private TextBox cpfInput;
        private TextBox numInput;
        private TextBox emailInput;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DateTimePicker dateTimePicker1;
    }
}