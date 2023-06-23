using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace dbKiller
{
    public partial class Form2 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSourceCD,
                      bindingSourceInvoice,
                      bindingSourceContractIDs, 
                      bindingSourceWorkType,
                      bindingSourceWorkTypeFull;
        public Form2(Database database, Form1 form1)
        {
            this.database = database;
            this.form1 = form1;
            InitializeComponent();
            LoadData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {

            database.OpenConnection();       

            try
            {
                //invoice table and invoiceID combobox
                string querySelectInvoice = "EXEC selectInvoice";
                SqlDataAdapter adapterSelectInvoice = new SqlDataAdapter();
                SqlCommand sqlCommandSelectInvoice = new SqlCommand(querySelectInvoice, database.GetConnection());
                adapterSelectInvoice.SelectCommand = sqlCommandSelectInvoice;
                DataTable dataTableSelectInvoice = new DataTable();
                adapterSelectInvoice.Fill(dataTableSelectInvoice);
                bindingSourceInvoice = new BindingSource();
                bindingSourceInvoice.DataSource = dataTableSelectInvoice;
                dataGridView2.DataSource = dataTableSelectInvoice;
                comboBox3.DataSource = dataTableSelectInvoice;
                comboBox3.DisplayMember = "ID накладной";
                comboBox3.ValueMember = "ID накладной";
                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }

                // Contract IDs combobox
                string querySelectContractIDs = "EXEC selectContractIDs";
                SqlDataAdapter adapterSelectContractIDs = new SqlDataAdapter();
                SqlCommand sqlCommandSelectContractIDs = new SqlCommand(querySelectContractIDs, database.GetConnection());
                adapterSelectContractIDs.SelectCommand = sqlCommandSelectContractIDs;
                DataTable dataTableSelectContractIDs = new DataTable();
                adapterSelectContractIDs.Fill(dataTableSelectContractIDs);
                bindingSourceContractIDs = new BindingSource();
                bindingSourceContractIDs.DataSource = dataTableSelectContractIDs;
                comboBox2.DataSource = dataTableSelectContractIDs;
                comboBox2.DisplayMember = "Contract_ID";
                comboBox2.ValueMember = "Contract_ID";

                // WorkType combobox
                string querySelectWorkType = "EXEC selectWorkType";
                SqlDataAdapter adapterWorkType = new SqlDataAdapter();
                SqlCommand sqlCommandWorkType = new SqlCommand(querySelectWorkType, database.GetConnection());
                adapterWorkType.SelectCommand = sqlCommandWorkType;
                DataTable dataTableWorkType = new DataTable();
                adapterWorkType.Fill(dataTableWorkType);
                bindingSourceWorkType = new BindingSource();
                bindingSourceWorkType.DataSource = dataTableWorkType;
                comboBox1.DataSource = dataTableWorkType;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Name";

                // WorkType Table and WorkType IDs combobox
                string queryselectWorkTypeFull = "EXEC selectWorkTypeFull";
                SqlDataAdapter adapterFullWorkType = new SqlDataAdapter();
                SqlCommand sqlCommandFullWorkType = new SqlCommand(queryselectWorkTypeFull, database.GetConnection());
                adapterFullWorkType.SelectCommand = sqlCommandFullWorkType;
                DataTable dataTableFullWorkType = new DataTable();
                adapterFullWorkType .Fill(dataTableFullWorkType);
                bindingSourceWorkTypeFull = new BindingSource();
                bindingSourceWorkTypeFull.DataSource = dataTableFullWorkType;
                dataGridView3.DataSource = dataTableFullWorkType;
                comboBox4.DataSource = dataTableFullWorkType;
                comboBox4.DisplayMember = "ID типа работ";
                comboBox4.ValueMember = "ID типа работ";
                for (int i = 0; i < dataGridView3.ColumnCount; i++)
                {
                    dataGridView3.Columns[i].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void LoadCheckDone()
        {
            database.OpenConnection();
            //checkdone table
            string queryCD = "EXEC CheckDoneProcedure @from, @to";
            try
            {
                SqlDataAdapter adapterCD = new SqlDataAdapter();
                SqlCommand sqlCommandCD = new SqlCommand(queryCD, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandCD, "@from", SqlDbType.Date, 0, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCommandCD, "@to", SqlDbType.Date, 0, dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                adapterCD.SelectCommand = sqlCommandCD;
                DataTable dataTableCD = new DataTable();
                adapterCD.Fill(dataTableCD);
                bindingSourceCD = new BindingSource();
                bindingSourceCD.DataSource = dataTableCD;
                dataGridView1.DataSource = dataTableCD;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadCheckDone();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                return;
            comboBox3.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
                return;
            comboBox3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InvoiceTableHandler();
            LoadData();
        }

        private void InvoiceTableHandler()
        {
            if (radioButton1.Checked)
            {
                InvoiceInsert();
                return;
            }
            if (radioButton2.Checked)
            {
                InvoiceUpdate();
                return;
            }
            return;
        }

        private void InvoiceUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить накладную №" +
            (comboBox3.SelectedItem as DataRowView)["ID накладной"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Объем может быть только числом!");
                return;
            }
            database.OpenConnection();
            try
            {
                string queryInvoiceUpdate = "EXEC updateInvoice @invoice_id, @contract_id, @worktype_id, @amount, @datefrom, @dateto";
                SqlCommand sqlCommandInvoiceUpdate = new SqlCommand(queryInvoiceUpdate, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@invoice_id", SqlDbType.Int, 0, (comboBox3.SelectedItem as DataRowView)["ID накладной"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@contract_id", SqlDbType.Int, 0, (comboBox2.SelectedItem as DataRowView)["Contract_ID"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@worktype_id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Work_ID"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@amount", SqlDbType.Int, 0, textBox1.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@datefrom", SqlDbType.Date, 0, dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@dateto", SqlDbType.Date, 0, dateTimePicker4.Value.ToString("yyyy-MM-dd"));

                sqlCommandInvoiceUpdate.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void InvoiceInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить накладную?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Объем может быть только числом!");
                return;
            }
            database.OpenConnection();
            try
            {
                string queryInvoiceInsert = "EXEC AddInvoice @contract_id, @worktype_id, @amount, @datefrom, @dateto";
                SqlCommand sqlCommandInvoiceInsert = new SqlCommand(queryInvoiceInsert, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandInvoiceInsert, "@contract_id", SqlDbType.Int, 0, (comboBox2.SelectedItem as DataRowView)["Contract_ID"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceInsert, "@worktype_id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Work_ID"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceInsert, "@amount", SqlDbType.Int, 0, textBox1.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceInsert, "@datefrom", SqlDbType.Date, 0, dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCommandInvoiceInsert, "@dateto", SqlDbType.Date, 0, dateTimePicker4.Value.ToString("yyyy-MM-dd"));

                sqlCommandInvoiceInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkTypeHandler();
            LoadData();
        }

        private void WorkTypeHandler()
        {
            if (radioButton4.Checked)
            {
                WorkTypeInsert();
                return;
            }
            if (radioButton3.Checked)
            {
                WorkTypeUpdate();
                return;
            }
            return;
        }

        private void WorkTypeUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить тип работ №" +
                                                (comboBox4.SelectedItem as DataRowView)["ID типа работ"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (!Regex.IsMatch(textBox3.Text.Trim(), @"^[0-9]+(\,[0-9]+)?$"))
            {
                MessageBox.Show("Цена может быть только числом!");
                return;
            }
            database.OpenConnection();
            try
            {
                string queryInvoiceUpdate = "EXEC updateWorkType @id, @unit, @price, @name, @shortUnit";
                decimal pricePerUnit;
                decimal.TryParse(textBox3.Text.Trim().ToString(), out pricePerUnit);
                SqlCommand sqlCommandInvoiceUpdate = new SqlCommand(queryInvoiceUpdate, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@id", SqlDbType.Int, 0, (comboBox4.SelectedItem as DataRowView)["ID типа работ"].ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@unit", SqlDbType.VarChar, 100, textBox2.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@price", SqlDbType.Money, 0, pricePerUnit);
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@name", SqlDbType.VarChar, 100, textBox4.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandInvoiceUpdate, "@shortUnit", SqlDbType.VarChar, 5, textBox5.Text.Trim().ToString());

                sqlCommandInvoiceUpdate.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void WorkTypeInsert()
        {
            //[AddWorkType]
            string confirmation = string.Format("Вы уверены что хотите добавить тип работ?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (!Regex.IsMatch(textBox3.Text.Trim(), @"^[0-9]+(\,[0-9]+)?$"))
            {
                MessageBox.Show("Цена может быть только числом!");
                return;
            }
            database.OpenConnection();
            try
            {
                string queryWorkTypeInsert = "EXEC AddWorkType @unit, @price, @name, @shortUnit";
                decimal pricePerUnit;
                decimal.TryParse(textBox3.Text.Trim().ToString(), out pricePerUnit);
                SqlCommand sqlCommandWorkTypeInsert = new SqlCommand(queryWorkTypeInsert, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandWorkTypeInsert, "@unit", SqlDbType.VarChar, 100, textBox2.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandWorkTypeInsert, "@price", SqlDbType.Money, 0, pricePerUnit);
                Utility.AddSqlParameter(sqlCommandWorkTypeInsert, "@name", SqlDbType.VarChar, 100, textBox4.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCommandWorkTypeInsert, "@shortUnit", SqlDbType.VarChar, 5, textBox5.Text.Trim().ToString());

                sqlCommandWorkTypeInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton4.Checked)
                return;
            comboBox4.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton3.Checked)
                return;
            comboBox4.Enabled = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
