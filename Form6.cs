using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P0
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void statusBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.statusBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.status". При необходимости она может быть перемещена или удалена.
            this.statusTableAdapter.Fill(this.dbDataSet.status);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение операции",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) statusBindingSource.RemoveCurrent();
        }
        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            DataSet changedRecords = dbDataSet.GetChanges();
            if (changedRecords != null)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) statusBindingNavigatorSaveItem_Click(sender, e);
                else this.statusTableAdapter.Fill(this.dbDataSet.status);
            }

        }
    }
}
