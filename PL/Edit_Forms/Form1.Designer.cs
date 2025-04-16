namespace AVBC_CLCB_Notifier.PL.Edit_Forms
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            roundedDatePicker1 = new Templates.RoundedDatePicker();
            textBox1 = new TextBox();
            SuspendLayout();
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
            roundedDatePicker1.HorizontalPadding = 10;
            roundedDatePicker1.ItemList = (System.Collections.Specialized.StringCollection)resources.GetObject("roundedDatePicker1.ItemList");
            roundedDatePicker1.Location = new Point(71, 208);
            roundedDatePicker1.MinimumSize = new Size(5, 5);
            roundedDatePicker1.Name = "roundedDatePicker1";
            roundedDatePicker1.OnFocusBool = false;
            roundedDatePicker1.Size = new Size(165, 30);
            roundedDatePicker1.TabIndex = 0;
            roundedDatePicker1.VerticalPadding = 5;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(356, 256);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "test";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(roundedDatePicker1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Templates.RoundedDatePicker roundedDatePicker1;
        private TextBox textBox1;
    }
}