using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task2
{
    public partial class LoadForm : Form
    {
        public string ExperimentName { get; private set; }

        public LoadForm(List<string> experimentNames)
        {
            InitializeComponent();
            lstExperimentsBox.DataSource = experimentNames;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (lstExperimentsBox.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите имя эксперимента!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ExperimentName = lstExperimentsBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lstExperimentsBox_DoubleClick(object sender, EventArgs e)
        {
            if (lstExperimentsBox.SelectedItem != null)
                btnLoad.PerformClick();
        }
    }
}
