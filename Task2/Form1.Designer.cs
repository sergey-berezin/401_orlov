namespace Task2
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
            groupBox1 = new GroupBox();
            btnLoadCities = new Button();
            btnGenerateCities = new Button();
            txtCityCount = new TextBox();
            label1 = new Label();
            groupBox3 = new GroupBox();
            txtMaxStagnationCnt = new TextBox();
            label8 = new Label();
            txtPrecision = new TextBox();
            label5 = new Label();
            txtMaxGenerations = new TextBox();
            label4 = new Label();
            txtMutationRate = new TextBox();
            label3 = new Label();
            txtPopulationSize = new TextBox();
            label2 = new Label();
            btnStop = new Button();
            btnStart = new Button();
            pictureBoxRoute = new PictureBox();
            lblGeneration = new Label();
            lblBestFitness = new Label();
            cartesianChartFitness = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            lblCitiesCnt = new Label();
            label7 = new Label();
            lblSumLength = new Label();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRoute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLoadCities);
            groupBox1.Controls.Add(btnGenerateCities);
            groupBox1.Controls.Add(txtCityCount);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(218, 191);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Параметры задачи";
            // 
            // btnLoadCities
            // 
            btnLoadCities.BackColor = Color.Khaki;
            btnLoadCities.FlatStyle = FlatStyle.Flat;
            btnLoadCities.Location = new Point(6, 143);
            btnLoadCities.Name = "btnLoadCities";
            btnLoadCities.Size = new Size(206, 41);
            btnLoadCities.TabIndex = 2;
            btnLoadCities.Text = "Загрузить";
            btnLoadCities.UseVisualStyleBackColor = false;
            btnLoadCities.Click += btnLoadCities_Click;
            // 
            // btnGenerateCities
            // 
            btnGenerateCities.BackColor = Color.PeachPuff;
            btnGenerateCities.FlatStyle = FlatStyle.Flat;
            btnGenerateCities.Location = new Point(6, 96);
            btnGenerateCities.Name = "btnGenerateCities";
            btnGenerateCities.Size = new Size(206, 41);
            btnGenerateCities.TabIndex = 1;
            btnGenerateCities.Text = "Сгенерировать";
            btnGenerateCities.UseVisualStyleBackColor = false;
            btnGenerateCities.Click += btnGenerateCities_Click;
            // 
            // txtCityCount
            // 
            txtCityCount.Location = new Point(6, 57);
            txtCityCount.MaxLength = 10;
            txtCityCount.Name = "txtCityCount";
            txtCityCount.Size = new Size(206, 33);
            txtCityCount.TabIndex = 1;
            txtCityCount.Text = "100";
            txtCityCount.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(6, 29);
            label1.Name = "label1";
            label1.Size = new Size(149, 25);
            label1.TabIndex = 0;
            label1.Text = "Кол-во городов";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtMaxStagnationCnt);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(txtPrecision);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(txtMaxGenerations);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(txtMutationRate);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(txtPopulationSize);
            groupBox3.Controls.Add(label2);
            groupBox3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(12, 209);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(218, 381);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Параметры алгоритма";
            // 
            // txtMaxStagnationCnt
            // 
            txtMaxStagnationCnt.Location = new Point(6, 341);
            txtMaxStagnationCnt.MaxLength = 10;
            txtMaxStagnationCnt.Name = "txtMaxStagnationCnt";
            txtMaxStagnationCnt.Size = new Size(206, 33);
            txtMaxStagnationCnt.TabIndex = 9;
            txtMaxStagnationCnt.Text = "100";
            txtMaxStagnationCnt.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(6, 313);
            label8.Name = "label8";
            label8.Size = new Size(170, 25);
            label8.TabIndex = 8;
            label8.Text = "Предел стагнации";
            // 
            // txtPrecision
            // 
            txtPrecision.Location = new Point(6, 272);
            txtPrecision.MaxLength = 10;
            txtPrecision.Name = "txtPrecision";
            txtPrecision.Size = new Size(206, 33);
            txtPrecision.TabIndex = 7;
            txtPrecision.Text = "0,0001";
            txtPrecision.TextAlign = HorizontalAlignment.Center;
            txtPrecision.KeyPress += txtMutationRate_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 244);
            label5.Name = "label5";
            label5.Size = new Size(93, 25);
            label5.TabIndex = 6;
            label5.Text = "Точность";
            // 
            // txtMaxGenerations
            // 
            txtMaxGenerations.Location = new Point(6, 208);
            txtMaxGenerations.MaxLength = 10;
            txtMaxGenerations.Name = "txtMaxGenerations";
            txtMaxGenerations.Size = new Size(206, 33);
            txtMaxGenerations.TabIndex = 5;
            txtMaxGenerations.Text = "0";
            txtMaxGenerations.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 180);
            label4.Name = "label4";
            label4.Size = new Size(161, 25);
            label4.TabIndex = 4;
            label4.Text = "Макс. поколений";
            // 
            // txtMutationRate
            // 
            txtMutationRate.Location = new Point(6, 144);
            txtMutationRate.MaxLength = 10;
            txtMutationRate.Name = "txtMutationRate";
            txtMutationRate.Size = new Size(206, 33);
            txtMutationRate.TabIndex = 3;
            txtMutationRate.Text = "0,02";
            txtMutationRate.TextAlign = HorizontalAlignment.Center;
            txtMutationRate.KeyPress += txtMutationRate_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(6, 116);
            label3.Name = "label3";
            label3.Size = new Size(200, 25);
            label3.TabIndex = 2;
            label3.Text = "Вероятность мутации";
            // 
            // txtPopulationSize
            // 
            txtPopulationSize.Location = new Point(6, 80);
            txtPopulationSize.MaxLength = 10;
            txtPopulationSize.Name = "txtPopulationSize";
            txtPopulationSize.Size = new Size(206, 33);
            txtPopulationSize.TabIndex = 1;
            txtPopulationSize.Text = "20";
            txtPopulationSize.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(6, 52);
            label2.Name = "label2";
            label2.Size = new Size(175, 25);
            label2.TabIndex = 0;
            label2.Text = "Размер популяции";
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.LightCoral;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnStop.Location = new Point(12, 643);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(218, 41);
            btnStop.TabIndex = 2;
            btnStop.Text = "Остановить алгоритм";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.PaleGreen;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.Location = new Point(12, 596);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(218, 41);
            btnStart.TabIndex = 1;
            btnStart.Text = "Запустить алгоритм";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // pictureBoxRoute
            // 
            pictureBoxRoute.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxRoute.Location = new Point(235, 253);
            pictureBoxRoute.Name = "pictureBoxRoute";
            pictureBoxRoute.Size = new Size(784, 454);
            pictureBoxRoute.TabIndex = 4;
            pictureBoxRoute.TabStop = false;
            // 
            // lblGeneration
            // 
            lblGeneration.AutoSize = true;
            lblGeneration.BackColor = Color.Transparent;
            lblGeneration.Location = new Point(1024, 94);
            lblGeneration.Name = "lblGeneration";
            lblGeneration.Size = new Size(106, 25);
            lblGeneration.TabIndex = 5;
            lblGeneration.Text = "Generation";
            // 
            // lblBestFitness
            // 
            lblBestFitness.AutoSize = true;
            lblBestFitness.BackColor = Color.Transparent;
            lblBestFitness.Location = new Point(1024, 119);
            lblBestFitness.Name = "lblBestFitness";
            lblBestFitness.Size = new Size(109, 25);
            lblBestFitness.TabIndex = 6;
            lblBestFitness.Text = "Best Fitness";
            // 
            // cartesianChartFitness
            // 
            cartesianChartFitness.BorderStyle = BorderStyle.FixedSingle;
            cartesianChartFitness.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cartesianChartFitness.Location = new Point(235, 12);
            cartesianChartFitness.Margin = new Padding(2, 3, 2, 3);
            cartesianChartFitness.Name = "cartesianChartFitness";
            cartesianChartFitness.Size = new Size(784, 235);
            cartesianChartFitness.TabIndex = 7;
            // 
            // lblCitiesCnt
            // 
            lblCitiesCnt.AutoSize = true;
            lblCitiesCnt.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCitiesCnt.Location = new Point(1024, 69);
            lblCitiesCnt.Name = "lblCitiesCnt";
            lblCitiesCnt.Size = new Size(176, 25);
            lblCitiesCnt.TabIndex = 8;
            lblCitiesCnt.Text = "Кол-во городов: 0";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(1024, 12);
            label7.Name = "label7";
            label7.Size = new Size(365, 41);
            label7.TabIndex = 9;
            label7.Text = "Информация";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSumLength
            // 
            lblSumLength.AutoSize = true;
            lblSumLength.BackColor = Color.Transparent;
            lblSumLength.Location = new Point(1024, 144);
            lblSumLength.Name = "lblSumLength";
            lblSumLength.Size = new Size(112, 25);
            lblSumLength.TabIndex = 10;
            lblSumLength.Text = "Расстояние";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1025, 172);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(364, 99);
            dataGridView1.TabIndex = 11;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1401, 714);
            Controls.Add(dataGridView1);
            Controls.Add(lblSumLength);
            Controls.Add(label7);
            Controls.Add(lblBestFitness);
            Controls.Add(lblGeneration);
            Controls.Add(lblCitiesCnt);
            Controls.Add(cartesianChartFitness);
            Controls.Add(btnStop);
            Controls.Add(groupBox3);
            Controls.Add(btnStart);
            Controls.Add(groupBox1);
            Controls.Add(pictureBoxRoute);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TSP MultiThread";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRoute).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtCityCount;
        private Label label1;
        private Button btnGenerateCities;
        private Button btnLoadCities;
        private GroupBox groupBox3;
        private TextBox txtPopulationSize;
        private Label label2;
        private Button btnStop;
        private Button btnStart;
        private TextBox txtMaxGenerations;
        private Label label4;
        private TextBox txtMutationRate;
        private Label label3;
        private PictureBox pictureBoxRoute;
        private Label lblGeneration;
        private Label lblBestFitness;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChartFitness;
        private TextBox txtPrecision;
        private Label label5;
        private Label lblCitiesCnt;
        private Label label7;
        private Label lblSumLength;
        private DataGridView dataGridView1;
        private TextBox txtMaxStagnationCnt;
        private Label label8;
    }
}