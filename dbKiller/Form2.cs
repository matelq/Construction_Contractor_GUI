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
    public partial class Form2 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSource;    
        public Form2(Database database, Form1 form1)
        {
            this.database = database;
            this.form1 = form1;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            string query = "EXEC CheckDoneProcedure @from, @to";

            try
            {
                //checkdone table
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCommand, "@from", SqlDbType.Date, 0, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCommand, "@to", SqlDbType.Date, 0, dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                adapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = dataTable;
                for(int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }
    }
}
