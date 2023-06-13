using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dbKiller
{
    public partial class Form3 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSource;
        public Form3(Database database, Form1 form1)
        {
            InitializeComponent();
            this.database = database;
            this.form1 = form1;
            LoadData();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {

            string query = "EXEC ListForForeman";
            database.OpenConnection();
            try
            {
                //checkdone table
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(query, database.GetConnection());
                adapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = dataTable;
                comboBox1.DataSource = dataTable;
                comboBox1.DisplayMember = "Идентификатор накладной";
                comboBox1.ValueMember = "Идентификатор накладной";
                
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    /*if (dataTable.Rows.Count > 0)
                    {
                        comboBox1.Items.Add(dataTable.Rows[i]["Идентификатор накладной"].ToString());
                    }*/
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.SelectedCells[0] == null)
                    return;

                string searchValue = "";
                DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    searchValue = selectedRow["Идентификатор накладной"].ToString();
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    DataGridViewRow rowTarget = dataGridView1.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["Идентификатор накладной"].Value.ToString().Equals(searchValue));

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Selected = false;
                    }

                    if (rowTarget != null)
                    {
                        rowTarget.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDates();
            LoadData();
        }

        private void UpdateDates()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить даты в накладной №" +
                        (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                if(checkBox1.Checked)
                {
                    string queryFrom = "EXEC UpdateDateFromRealForeman @start, @id";
                    SqlCommand sqlCommandFrom = new SqlCommand(queryFrom, database.GetConnection());
                    Utility.AddSqlParameter(sqlCommandFrom, "@start", SqlDbType.Date, 0, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    Utility.AddSqlParameter(sqlCommandFrom, "@id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString());
                    sqlCommandFrom.ExecuteNonQuery();
                }

                if (checkBox2.Checked)
                {
                    string queryTo = "EXEC UpdateDateToRealForeman @end, @id";
                    SqlCommand sqlCommandTo = new SqlCommand(queryTo, database.GetConnection());
                    Utility.AddSqlParameter(sqlCommandTo, "@end", SqlDbType.Date, 0, dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    Utility.AddSqlParameter(sqlCommandTo, "@id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString());
                    sqlCommandTo.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RemoveDates();
            LoadData();
        }

        private void RemoveDates()
        {
            string confirmation = string.Format("Вы уверены что хотите удалить даты в накладной №" +
                                    (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;


            database.OpenConnection();
            try
            {
                string queryFrom = "EXEC UpdateDateFromRealForeman @start, @id";
                SqlCommand sqlCommandFrom = new SqlCommand(queryFrom, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandFrom, "@start", SqlDbType.Date, 0, DBNull.Value);
                Utility.AddSqlParameter(sqlCommandFrom, "@id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString());
                sqlCommandFrom.ExecuteNonQuery();

                string queryTo = "EXEC UpdateDateToRealForeman @end, @id";
                SqlCommand sqlCommandTo = new SqlCommand(queryTo, database.GetConnection());
                Utility.AddSqlParameter(sqlCommandTo, "@end", SqlDbType.Date, 0, DBNull.Value);
                Utility.AddSqlParameter(sqlCommandTo, "@id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["Идентификатор накладной"].ToString());
                sqlCommandTo.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }
    }
}
