namespace Task2
{
    partial class LoadForm
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
            label1 = new Label();
            lstExperimentsBox = new ListBox();
            btnLoad = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(14, 9);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(288, 25);
            label1.TabIndex = 16;
            label1.Text = "Выберите имя эксперимента:";
            // 
            // lstExperimentsBox
            // 
            lstExperimentsBox.Font = new Font("Segoe UI", 14.25F);
            lstExperimentsBox.FormattingEnabled = true;
            lstExperimentsBox.ItemHeight = 25;
            lstExperimentsBox.Location = new Point(14, 39);
            lstExperimentsBox.Margin = new Padding(5);
            lstExperimentsBox.Name = "lstExperimentsBox";
            lstExperimentsBox.Size = new Size(454, 279);
            lstExperimentsBox.TabIndex = 19;
            lstExperimentsBox.DoubleClick += lstExperimentsBox_DoubleClick;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.PeachPuff;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Location = new Point(476, 39);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(40, 40);
            btnLoad.TabIndex = 20;
            btnLoad.Text = "📥";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // LoadForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PaleGreen;
            ClientSize = new Size(528, 332);
            Controls.Add(btnLoad);
            Controls.Add(lstExperimentsBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 14.25F);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoadForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Загрузка эксперимента!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ListBox lstExperimentsBox;
        private Button btnLoad;
    }
}