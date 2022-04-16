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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dbDataSet.users);

        }
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            DataSet changedRecords = dbDataSet.GetChanges();
            if (changedRecords != null)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) usersBindingNavigatorSaveItem_Click(sender, e);
                else this.usersTableAdapter.Fill(this.dbDataSet.users);
            }

        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение операции",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) usersBindingSource.RemoveCurrent();
        }








    }
}
