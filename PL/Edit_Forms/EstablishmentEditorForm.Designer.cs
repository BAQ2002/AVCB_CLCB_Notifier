namespace AVBC_CLCB_Notifier.PL.Edit_Forms
{
    partial class EstablishmentEditorForm
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
            socialReasonInput = new TextBox();
            fantasyNameInput = new TextBox();
            cnpjInput = new TextBox();
            addressInput = new TextBox();
            SuspendLayout();
            // 
            // socialReasonInput
            // 
            socialReasonInput.Location = new Point(60, 30);
            socialReasonInput.Name = "socialReasonInput";
            socialReasonInput.Size = new Size(258, 23);
            socialReasonInput.TabIndex = 0;
            // 
            // fantasyNameInput
            // 
            fantasyNameInput.Location = new Point(60, 58);
            fantasyNameInput.Name = "fantasyNameInput";
            fantasyNameInput.Size = new Size(100, 23);
            fantasyNameInput.TabIndex = 1;
            // 
            // cnpjInput
            // 
            cnpjInput.Location = new Point(60, 97);
            cnpjInput.Name = "cnpjInput";
            cnpjInput.Size = new Size(100, 23);
            cnpjInput.TabIndex = 2;
            // 
            // addressInput
            // 
            addressInput.Location = new Point(60, 126);
            addressInput.Name = "addressInput";
            addressInput.Size = new Size(100, 23);
            addressInput.TabIndex = 3;
            // 
            // EstablishmentEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 450);
            Controls.Add(addressInput);
            Controls.Add(cnpjInput);
            Controls.Add(fantasyNameInput);
            Controls.Add(socialReasonInput);
            Name = "EstablishmentEditorForm";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox socialReasonInput;
        private TextBox fantasyNameInput;
        private TextBox cnpjInput;
        private TextBox addressInput;
    }
}