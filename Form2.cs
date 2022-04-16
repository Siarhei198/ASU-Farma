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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void typesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.typesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.types". При необходимости она может быть перемещена или удалена.
            this.typesTableAdapter.Fill(this.dbDataSet.types);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.types". При необходимости она может быть перемещена или удалена.
            this.typesTableAdapter.Fill(this.dbDataSet.types);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение операции",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) typesBindingSource.RemoveCurrent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            DataSet changedRecords = dbDataSet.GetChanges();
            if (changedRecords != null)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) typesBindingNavigatorSaveItem_Click(sender, e);
                else this.typesTableAdapter.Fill(this.dbDataSet.types);
            }

        }

    }
}
