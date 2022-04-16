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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void producerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.producerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.producer". При необходимости она может быть перемещена или удалена.
            this.producerTableAdapter.Fill(this.dbDataSet.producer);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение операции",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question,
           MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) producerBindingSource.RemoveCurrent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            DataSet changedRecords = dbDataSet.GetChanges();
            if (changedRecords != null)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) producerBindingNavigatorSaveItem_Click(sender, e);
                else this.producerTableAdapter.Fill(this.dbDataSet.producer);
            }

        }
    }
}
