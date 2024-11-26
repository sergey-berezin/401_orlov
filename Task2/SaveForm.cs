namespace Task2
{
    public partial class SaveForm : Form
    {
        public string ExperimentName { get; private set; }

        public SaveForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(
                    "Имя не может быть пустым!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ExperimentName = textBox1.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
