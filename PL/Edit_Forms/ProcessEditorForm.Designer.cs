using AVBC_CLCB_Notifier.PL.CustomControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace AVBC_CLCB_Notifier.PL.Edit_Forms
{
    partial class ProcessEditorForm
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
            RoundedTextBox processNumberInput;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessEditorForm));
            processGroupBox = new GroupBox();
            processNumberLabel = new Label();
            processTypeLabel = new Label();
            processTypeComboBox = new RoundedComboBox();
            establishmentGroupBox = new GroupBox();
            removeEstablishmentButton = new Button();
            addEstablishmentButton = new Button();
            editEstablishmentButton = new Button();
            establishmentFantasyNameInput = new RoundedTextBox();
            clientGroupBox = new GroupBox();
            removeClientButton = new Button();
            clientNameInput = new RoundedTextBox();
            editClientButton = new Button();
            addClientButton = new Button();
            licensingGroupBox = new GroupBox();
            statusLabel = new Label();
            expirationLabel = new Label();
            issuanceLabel = new Label();
            expirationDateInput = new DateTimePicker();
            statusOutput = new RoundedTextBox();
            issuanceDateInput = new DateTimePicker();
            extraInfoGroupBox = new GroupBox();
            extraInfoInput = new RoundedTextBox();
            saveButton = new Button();
            processNumberInput = new RoundedTextBox();
            processGroupBox.SuspendLayout();
            establishmentGroupBox.SuspendLayout();
            clientGroupBox.SuspendLayout();
            licensingGroupBox.SuspendLayout();
            extraInfoGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // processNumberInput
            // 
            processNumberInput.BackColor = SystemColors.Control;
            processNumberInput.BackgroundColor = Color.White;
            processNumberInput.BorderColor = Color.Crimson;
            processNumberInput.BorderColorFocus = Color.Blue;
            processNumberInput.BorderFocusExtraWidth = 1;
            processNumberInput.BorderRadius = 5;
            processNumberInput.BorderWidth = 1;
            processNumberInput.Horizontalpadding = 1;
            processNumberInput.Location = new Point(163, 37);
            processNumberInput.MinimumSize = new Size(50, 23);
            processNumberInput.Name = "processNumberInput";
            processNumberInput.OnFocusBool = false;
            processNumberInput.Padding = new Padding(5);
            processNumberInput.Size = new Size(149, 23);
            processNumberInput.TabIndex = 1;
            processNumberInput.Verticalpadding = 1;
            // 
            // processGroupBox
            // 
            processGroupBox.BackColor = SystemColors.Control;
            processGroupBox.Controls.Add(processNumberLabel);
            processGroupBox.Controls.Add(processTypeLabel);
            processGroupBox.Controls.Add(processTypeComboBox);
            processGroupBox.Controls.Add(processNumberInput);
            processGroupBox.Location = new Point(20, 9);
            processGroupBox.Name = "processGroupBox";
            processGroupBox.Size = new Size(375, 70);
            processGroupBox.TabIndex = 16;
            processGroupBox.TabStop = false;
            processGroupBox.Text = "Processo";
            // 
            // processNumberLabel
            // 
            processNumberLabel.AutoSize = true;
            processNumberLabel.BackColor = SystemColors.Control;
            processNumberLabel.Location = new Point(163, 19);
            processNumberLabel.Name = "processNumberLabel";
            processNumberLabel.Size = new Size(51, 15);
            processNumberLabel.TabIndex = 5;
            processNumberLabel.Text = "Número";
            // 
            // processTypeLabel
            // 
            processTypeLabel.AutoSize = true;
            processTypeLabel.Location = new Point(8, 19);
            processTypeLabel.Name = "processTypeLabel";
            processTypeLabel.Size = new Size(31, 15);
            processTypeLabel.TabIndex = 4;
            processTypeLabel.Text = "Tipo";
            // 
            // processTypeComboBox
            // 
            processTypeComboBox.BackColor = Color.Transparent;
            processTypeComboBox.BackgroundColor = Color.MistyRose;
            processTypeComboBox.BorderColor = Color.Blue;
            processTypeComboBox.BorderColorFocus = Color.Green;
            processTypeComboBox.BorderFocusExtraWidth = 1;
            processTypeComboBox.BorderRadius = 6;
            processTypeComboBox.BorderWidth = 1;
            processTypeComboBox.ForeColor = SystemColors.MenuHighlight;
            processTypeComboBox.Horizontalpadding = 2;
            processTypeComboBox.ItemList = (List<string>)resources.GetObject("processTypeComboBox.ItemList");
            processTypeComboBox.Location = new Point(8, 37);
            processTypeComboBox.MinimumSize = new Size(100, 1);
            processTypeComboBox.Name = "processTypeComboBox";
            processTypeComboBox.OnFocusBool = false;
            processTypeComboBox.Size = new Size(128, 23);
            processTypeComboBox.TabIndex = 3;
            processTypeComboBox.Verticalpadding = 4;
            // 
            // establishmentGroupBox
            // 
            establishmentGroupBox.Controls.Add(removeEstablishmentButton);
            establishmentGroupBox.Controls.Add(addEstablishmentButton);
            establishmentGroupBox.Controls.Add(editEstablishmentButton);
            establishmentGroupBox.Controls.Add(establishmentFantasyNameInput);
            establishmentGroupBox.Location = new Point(20, 85);
            establishmentGroupBox.Name = "establishmentGroupBox";
            establishmentGroupBox.Size = new Size(375, 55);
            establishmentGroupBox.TabIndex = 11;
            establishmentGroupBox.TabStop = false;
            establishmentGroupBox.Text = "Estabelecimento";
            establishmentGroupBox.Enter += establishmentGroupBox_Enter;
            // 
            // removeEstablishmentButton
            // 
            removeEstablishmentButton.ForeColor = SystemColors.ControlText;
            removeEstablishmentButton.Location = new Point(343, 20);
            removeEstablishmentButton.Name = "removeEstablishmentButton";
            removeEstablishmentButton.Size = new Size(27, 23);
            removeEstablishmentButton.TabIndex = 18;
            removeEstablishmentButton.Text = "button7";
            removeEstablishmentButton.UseVisualStyleBackColor = true;
            // 
            // addEstablishmentButton
            // 
            addEstablishmentButton.BackColor = SystemColors.Control;
            addEstablishmentButton.Location = new Point(284, 20);
            addEstablishmentButton.Name = "addEstablishmentButton";
            addEstablishmentButton.Size = new Size(27, 23);
            addEstablishmentButton.TabIndex = 8;
            addEstablishmentButton.UseVisualStyleBackColor = false;
            // 
            // editEstablishmentButton
            // 
            editEstablishmentButton.Image = (Image)resources.GetObject("editEstablishmentButton.Image");
            editEstablishmentButton.Location = new Point(312, 20);
            editEstablishmentButton.Name = "editEstablishmentButton";
            editEstablishmentButton.Size = new Size(27, 23);
            editEstablishmentButton.TabIndex = 7;
            editEstablishmentButton.UseVisualStyleBackColor = true;
            // 
            // establishmentFantasyNameInput
            // 
            establishmentFantasyNameInput.BackgroundColor = Color.White;
            establishmentFantasyNameInput.BorderColor = Color.Crimson;
            establishmentFantasyNameInput.BorderColorFocus = Color.Blue;
            establishmentFantasyNameInput.BorderFocusExtraWidth = 1;
            establishmentFantasyNameInput.BorderRadius = 5;
            establishmentFantasyNameInput.BorderWidth = 1;
            establishmentFantasyNameInput.Horizontalpadding = 1;
            establishmentFantasyNameInput.Location = new Point(8, 21);
            establishmentFantasyNameInput.MinimumSize = new Size(50, 23);
            establishmentFantasyNameInput.Name = "establishmentFantasyNameInput";
            establishmentFantasyNameInput.OnFocusBool = false;
            establishmentFantasyNameInput.Padding = new Padding(5);
            establishmentFantasyNameInput.Size = new Size(276, 23);
            establishmentFantasyNameInput.TabIndex = 6;
            establishmentFantasyNameInput.Verticalpadding = 1;
            // 
            // clientGroupBox
            // 
            clientGroupBox.Controls.Add(removeClientButton);
            clientGroupBox.Controls.Add(clientNameInput);
            clientGroupBox.Controls.Add(editClientButton);
            clientGroupBox.Controls.Add(addClientButton);
            clientGroupBox.Location = new Point(20, 146);
            clientGroupBox.Name = "clientGroupBox";
            clientGroupBox.Size = new Size(375, 55);
            clientGroupBox.TabIndex = 10;
            clientGroupBox.TabStop = false;
            clientGroupBox.Text = "Cliente";
            // 
            // removeClientButton
            // 
            removeClientButton.Location = new Point(348, 21);
            removeClientButton.Name = "removeClientButton";
            removeClientButton.Size = new Size(24, 24);
            removeClientButton.TabIndex = 10;
            removeClientButton.Text = "button6";
            removeClientButton.UseVisualStyleBackColor = true;
            // 
            // clientNameInput
            // 
            clientNameInput.BackgroundColor = Color.White;
            clientNameInput.BorderColor = Color.Crimson;
            clientNameInput.BorderColorFocus = Color.Blue;
            clientNameInput.BorderFocusExtraWidth = 1;
            clientNameInput.BorderRadius = 5;
            clientNameInput.BorderWidth = 1;
            clientNameInput.Horizontalpadding = 1;
            clientNameInput.Location = new Point(8, 22);
            clientNameInput.MinimumSize = new Size(50, 23);
            clientNameInput.Name = "clientNameInput";
            clientNameInput.OnFocusBool = false;
            clientNameInput.Padding = new Padding(5);
            clientNameInput.Size = new Size(276, 23);
            clientNameInput.TabIndex = 0;
            clientNameInput.Verticalpadding = 1;
            // 
            // editClientButton
            // 
            editClientButton.Location = new Point(318, 21);
            editClientButton.Name = "editClientButton";
            editClientButton.Size = new Size(24, 24);
            editClientButton.TabIndex = 9;
            editClientButton.Text = "button3";
            editClientButton.UseVisualStyleBackColor = true;
            // 
            // addClientButton
            // 
            addClientButton.Location = new Point(289, 21);
            addClientButton.Name = "addClientButton";
            addClientButton.Size = new Size(22, 23);
            addClientButton.TabIndex = 8;
            addClientButton.Text = "button2";
            addClientButton.UseVisualStyleBackColor = true;
            // 
            // licensingGroupBox
            // 
            licensingGroupBox.Controls.Add(statusLabel);
            licensingGroupBox.Controls.Add(expirationLabel);
            licensingGroupBox.Controls.Add(issuanceLabel);
            licensingGroupBox.Controls.Add(expirationDateInput);
            licensingGroupBox.Controls.Add(statusOutput);
            licensingGroupBox.Controls.Add(issuanceDateInput);
            licensingGroupBox.Location = new Point(20, 207);
            licensingGroupBox.Name = "licensingGroupBox";
            licensingGroupBox.Size = new Size(375, 70);
            licensingGroupBox.TabIndex = 13;
            licensingGroupBox.TabStop = false;
            licensingGroupBox.Text = "Licenciamento";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(244, 19);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 15;
            statusLabel.Text = "Status";
            // 
            // expirationLabel
            // 
            expirationLabel.AutoSize = true;
            expirationLabel.Location = new Point(118, 19);
            expirationLabel.Name = "expirationLabel";
            expirationLabel.Size = new Size(51, 15);
            expirationLabel.TabIndex = 14;
            expirationLabel.Text = "Validade";
            // 
            // issuanceLabel
            // 
            issuanceLabel.AutoSize = true;
            issuanceLabel.Location = new Point(6, 19);
            issuanceLabel.Name = "issuanceLabel";
            issuanceLabel.Size = new Size(50, 15);
            issuanceLabel.TabIndex = 13;
            issuanceLabel.Text = "Emissão";
            // 
            // expirationDateInput
            // 
            expirationDateInput.Format = DateTimePickerFormat.Short;
            expirationDateInput.Location = new Point(118, 37);
            expirationDateInput.Name = "expirationDateInput";
            expirationDateInput.Size = new Size(100, 23);
            expirationDateInput.TabIndex = 4;
            // 
            // statusOutput
            // 
            statusOutput.BackgroundColor = Color.White;
            statusOutput.BorderColor = Color.Crimson;
            statusOutput.BorderColorFocus = Color.Blue;
            statusOutput.BorderFocusExtraWidth = 1;
            statusOutput.BorderRadius = 5;
            statusOutput.BorderWidth = 1;
            statusOutput.Horizontalpadding = 1;
            statusOutput.Location = new Point(244, 37);
            statusOutput.MinimumSize = new Size(50, 23);
            statusOutput.Name = "statusOutput";
            statusOutput.OnFocusBool = false;
            statusOutput.Padding = new Padding(5);
            statusOutput.Size = new Size(98, 23);
            statusOutput.TabIndex = 12;
            statusOutput.Verticalpadding = 1;
            // 
            // issuanceDateInput
            // 
            issuanceDateInput.Format = DateTimePickerFormat.Short;
            issuanceDateInput.Location = new Point(6, 37);
            issuanceDateInput.Name = "issuanceDateInput";
            issuanceDateInput.Size = new Size(100, 23);
            issuanceDateInput.TabIndex = 5;
            issuanceDateInput.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // extraInfoGroupBox
            // 
            extraInfoGroupBox.Controls.Add(extraInfoInput);
            extraInfoGroupBox.Location = new Point(20, 283);
            extraInfoGroupBox.Name = "extraInfoGroupBox";
            extraInfoGroupBox.Size = new Size(375, 55);
            extraInfoGroupBox.TabIndex = 17;
            extraInfoGroupBox.TabStop = false;
            extraInfoGroupBox.Text = "Informação Adicional";
            // 
            // extraInfoInput
            // 
            extraInfoInput.BackgroundColor = Color.White;
            extraInfoInput.BorderColor = Color.Crimson;
            extraInfoInput.BorderColorFocus = Color.Blue;
            extraInfoInput.BorderFocusExtraWidth = 1;
            extraInfoInput.BorderRadius = 5;
            extraInfoInput.BorderWidth = 1;
            extraInfoInput.Horizontalpadding = 1;
            extraInfoInput.Location = new Point(6, 19);
            extraInfoInput.MinimumSize = new Size(50, 23);
            extraInfoInput.Name = "extraInfoInput";
            extraInfoInput.OnFocusBool = false;
            extraInfoInput.Padding = new Padding(5);
            extraInfoInput.Size = new Size(350, 23);
            extraInfoInput.TabIndex = 7;
            extraInfoInput.Verticalpadding = 1;
            extraInfoInput.TextChanged += textBox4_TextChanged;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(168, 379);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(70, 23);
            saveButton.TabIndex = 2;
            saveButton.Text = "button1";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // ProcessEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(421, 450);
            Controls.Add(extraInfoGroupBox);
            Controls.Add(processGroupBox);
            Controls.Add(licensingGroupBox);
            Controls.Add(establishmentGroupBox);
            Controls.Add(clientGroupBox);
            Controls.Add(saveButton);
            Name = "ProcessEditorForm";
            Text = " ";
            Load += ProcessEditorForm_Load;
            processGroupBox.ResumeLayout(false);
            processGroupBox.PerformLayout();
            establishmentGroupBox.ResumeLayout(false);
            clientGroupBox.ResumeLayout(false);
            licensingGroupBox.ResumeLayout(false);
            licensingGroupBox.PerformLayout();
            extraInfoGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RoundedTextBox clientNameInput;
        private RoundedTextBox processNumberInput;
        private Button saveButton;
        private DateTimePicker expirationDateInput;
        private DateTimePicker issuanceDateInput;
        private RoundedTextBox establishmentFantasyNameInput;
        private RoundedTextBox extraInfoInput;
        private Button addClientButton;
        private Button editClientButton;
        private GroupBox clientGroupBox;
        private GroupBox establishmentGroupBox;
        private Button addEstablishmentButton;
        private Button editEstablishmentButton;
        private RoundedTextBox statusOutput;
        private GroupBox licensingGroupBox;
        private Label statusLabel;
        private Label expirationLabel;
        private Label issuanceLabel;
        private GroupBox processGroupBox;
        private Button removeClientButton;
        private Label processNumberLabel;
        private Label processTypeLabel;
        private GroupBox extraInfoGroupBox;
        private Button removeEstablishmentButton;
        private RoundedComboBox processTypeComboBox;
    }
}