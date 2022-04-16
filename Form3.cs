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
    public partial class Form3 : Form
    {
        int midk = 0;
        int ind = 0;
        Boolean pp = false;
        DateTimePicker dtp = new DateTimePicker();
        Rectangle _Rectangle;
        int mids = 0;
        int midt = 0;


        public Form3()
        {
            InitializeComponent();
            journalDataGridView.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.Value = DateTime.Now;
            dtp.CustomFormat = "dd-MM-yyyy";
            //dtp.TextChanged += new EventHandler(dtp_TextChange);


        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение операции",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) journalBindingSource.RemoveCurrent();

        }
        //private void journalDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int column = journalDataGridView.CurrentCell.ColumnIndex;
        //    string headertext = journalDataGridView.Columns[column].HeaderText;
        //    for (int i = 0; i < journalDataGridView.RowCount - 1; i++)
        //    {
        //        if (headertext.Equals("Дата поверки"))
        //        {
        //            _Rectangle = journalDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
        //            dtp.Size = new Size(_Rectangle.Width, _Rectangle.Height);
        //            dtp.Location = new Point(_Rectangle.X, _Rectangle.Y);
        //            dtp.Visible = true;
        //        }
                
                
                
        //        if (headertext.Equals("Дата след. поверки"))
        //        {
        //            _Rectangle = journalDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
        //            dtp.Size = new Size(_Rectangle.Width, _Rectangle.Height);
        //            dtp.Location = new Point(_Rectangle.X, _Rectangle.Y);
        //            dtp.Visible = true;
        //        }
                
                
                

        //    }
        //}
        //private void dtp_TextChange(object sende, EventArgs e)
        //{
        //    for (int i = 0; i < journalDataGridView.RowCount - 1; i++)
        //    {
        //        journalDataGridView.CurrentCell.Value = dtp.Text.ToString();
        //        dtp.Visible = false;
        //    }
        //}


        private void journalBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.journalBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox2.Text = "";
            button1.Enabled = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.status". При необходимости она может быть перемещена или удалена.
            this.statusTableAdapter.Fill(this.dbDataSet.status);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dbDataSet.users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.producer". При необходимости она может быть перемещена или удалена.
            this.producerTableAdapter.Fill(this.dbDataSet.producer);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.types". При необходимости она может быть перемещена или удалена.
            this.typesTableAdapter.Fill(this.dbDataSet.types);

            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.journal". При необходимости она может быть перемещена или удалена.

            this.journalTableAdapter.FillByhp(this.dbDataSet.journal,midk,mids,midt); 
            comboBox1.Text = "Выберите тип средств измерений";
            comboBox2.Text = "Выберите пользователя";
            comboBox3.Text = "Выберите статус";
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (; ind < journalDataGridView.RowCount; ind++)
                if (journalDataGridView[0, ind].FormattedValue.ToString().Contains(textBox2.Text.Trim()))
                {
                    pp = true;
                    journalDataGridView.CurrentCell = journalDataGridView[0, ind];
                    if (ind < journalDataGridView.RowCount - 1) ind++;
                    else ind = 0;
                    return;
                }
                else
                if (ind >= journalDataGridView.RowCount - 1)
                    if (pp) ind = -1;
                    else
                    {
                        MessageBox.Show("Совпадений не найдено...", "Поиск " + textBox2.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ind = journalDataGridView.RowCount - 2;
                        return;
                    }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox2.Text != "");
            ind = 0;
            pp = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            save(null, null);
            midk = Int32.Parse(comboBox1.SelectedValue.ToString());
            this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt);  
            typesBindingSource.Filter = "id_type=" + midk.ToString();
        }
        private void save(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            DataSet changedRecords = dbDataSet.GetChanges();
            if (changedRecords != null)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) journalBindingNavigatorSaveItem_Click(sender, e);
                else this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt);
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1_TextUpdate(null, null);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            midk = 0;
            mids = 0;
            midt = 0;

            comboBox1.Dispose();
            comboBox2.Dispose();
            comboBox3.Dispose();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            save(null, null);
        }

        private void journalDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            foreach (DataGridViewRow r in journalDataGridView.Rows)
            {
                if (!string.IsNullOrEmpty(r.Cells[5].Value.ToString()))
                {
                    string cellc = r.Cells[5].Value.ToString();
                    string status = r.Cells[8].Value.ToString();
                    DateTime.TryParse(cellc, out DateTime cellValue5);

                    if (cellValue5 >= thisDay)
                    {
                        r.DefaultCellStyle.BackColor = Color.LightCyan;
                    }
                    else if (status == "1")
                    {
                        r.DefaultCellStyle.BackColor = Color.Red;
                        
                     }
                    else r.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            journalDataGridView.Refresh();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            save(null, null);
            mids = Int32.Parse(comboBox2.SelectedValue.ToString());
            this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt); 
            usersBindingSource.Filter = "id_user=" + mids.ToString();
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                comboBox2.Text = "Выберите пользователя";
                save(null, null);
                mids = 0;
                usersBindingSource.Filter = "";
                this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt); 
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            save(null, null);
            midt = Int32.Parse(comboBox3.SelectedValue.ToString());
            this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt); 
            statusBindingSource.Filter = "id_status=" + midt.ToString();
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                comboBox3.Text = "Выберите статус";
                save(null, null);
                midt = 0;
                statusBindingSource.Filter = "";
                this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt); 
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2_TextUpdate(null, null);         //здесь удалены лишние строки
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            comboBox3_TextUpdate(null, null);   //здесь удалены лишние строки
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "Выберите тип средств измерений";
                save(null, null);
                midk = 0;
                typesBindingSource.Filter = "";
                this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt); 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            save(null, null);
            comboBox1.Text = "Выберите тип средств измерений";
            comboBox2.Text = "Выберите пользователя";
            comboBox3.Text = "Выберите статус";
            typesBindingSource.Filter = "";
            statusBindingSource.Filter = "";
            usersBindingSource.Filter = "";
            midk = 0;
            mids = 0;
            midt = 0;
            this.journalTableAdapter.FillByhp(this.dbDataSet.journal, midk, mids, midt);
        }

        private void journalBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
