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

namespace dbKiller
{
    
    public partial class Form1 : Form
    {
        Database database = new Database();

        public Form1()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFormValid())
                return;

            AuthenticateAndGrantAccess();
        }

        private void AuthenticateAndGrantAccess()
        {
            // Connect to db as an "auth"
            //database.CloseConnection();
            database.InitializeConnectionString("auth", "auth");
            try
            {
                database.OpenConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Get role auth data 
            string login = "", password = "";
            string query = "EXEC GetAuthData @login, @password";            

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCommand, "@login", SqlDbType.VarChar, 50, textBox1.Text);
                Utility.AddSqlParameter(sqlCommand, "@password", SqlDbType.VarChar, 50, textBox2.Text);
                adapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    login = dataTable.Rows[0]["externalLogin"].ToString();
                    password = dataTable.Rows[0]["externalPassword"].ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Connect to db with desired role
            database.InitializeConnectionString(login, password);
            try
            {
                database.OpenConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            database.CloseConnection();

            switch (login)
            {
                case "CEO":
                    Form4 form4 = new Form4(database, this);
                    this.Hide();
                    break;
                case "manager":
                    Form2 form2 = new Form2(database, this);
                    this.Hide();
                    break;
                case "foreman":
                    Form3 form3 = new Form3(database, this);
                    this.Hide();
                    break;
                case "fired":
                    MessageBox.Show("Вы уволены.");
                    break;
                default:
                    MessageBox.Show("Пользователь не существует.");
                    break;
            }

        }

        private bool isFormValid()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Логин не введен!");
                return false;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Пароль не введен!");
                return false;
            }

            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
        }
    }
}
