using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbKiller
{
    public partial class Form4 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSelectClients,
                      bindingSourceObjectives,
                      bindingSourceResponsible,
                      bindingSourceContract,
                      bindingSourceEmployee,
                      bindingSourceForeman,
                      bindingSourceSelectClientNameAndIDs,
                      bindingSourceSelectRoleNamesAndIds,
                      bindingSourceSelectObjectivesNameAndIDs;

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public Form4(Database database, Form1 form1)
        {
            this.database = database;
            this.form1 = form1;
            InitializeComponent();
            this.Show();
            LoadData();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            database.OpenConnection();

            try
            {
                // Employee table and combobox
                string querySelectEmployee = "EXEC ListEmployees";
                SqlDataAdapter adapterEmployee = new SqlDataAdapter();
                SqlCommand sqlCommandEmployee = new SqlCommand(querySelectEmployee, database.GetConnection());
                adapterEmployee.SelectCommand = sqlCommandEmployee;
                DataTable dataTableEmployee = new DataTable();
                adapterEmployee.Fill(dataTableEmployee);
                bindingSourceEmployee = new BindingSource();
                bindingSourceEmployee.DataSource = dataTableEmployee;
                dataGridView5.DataSource = dataTableEmployee;
                dataGridView5.Columns["bdRole_id"].Visible = false;
                comboBox5.DataSource = dataTableEmployee;
                comboBox5.DisplayMember = "ID Сотрудника";
                comboBox5.ValueMember = "ID Сотрудника";
                //dataGridView4.Columns["Foreman_ID"].Visible = false;
                for (int i = 0; i < dataGridView5.ColumnCount; i++)
                {
                    dataGridView5.Columns[i].ReadOnly = true;
                }
                // Employee "Role" combobox
                string querySelectRoleNamesAndIds = "EXEC selectRoleNamesAndIds";
                SqlDataAdapter adapterSelectRoleNamesAndIds = new SqlDataAdapter();
                SqlCommand sqlCommandSelectRoleNamesAndIds = new SqlCommand(querySelectRoleNamesAndIds, database.GetConnection());
                adapterSelectRoleNamesAndIds.SelectCommand = sqlCommandSelectRoleNamesAndIds;
                DataTable dataTableSelectRoleNamesAndIds = new DataTable();
                adapterSelectRoleNamesAndIds.Fill(dataTableSelectRoleNamesAndIds);
                bindingSourceSelectRoleNamesAndIds = new BindingSource();
                bindingSourceSelectRoleNamesAndIds.DataSource = dataTableSelectRoleNamesAndIds;
                comboBox7.DataSource = dataTableSelectRoleNamesAndIds;
                comboBox7.DisplayMember = "RoleName";
                comboBox7.ValueMember = "RoleName";


                // Foreman table and combobox
                string querySelectForeman = "EXEC ListForeman";
                SqlDataAdapter adapterForeman = new SqlDataAdapter();
                SqlCommand sqlCommandForeman = new SqlCommand(querySelectForeman, database.GetConnection());
                adapterForeman.SelectCommand = sqlCommandForeman;
                DataTable dataTableForeman = new DataTable();
                adapterForeman.Fill(dataTableForeman);
                bindingSourceForeman = new BindingSource();
                bindingSourceForeman.DataSource = dataTableForeman;
                dataGridView6.DataSource = dataTableForeman;
                comboBox6.DataSource = dataTableForeman;
                comboBox6.DisplayMember = "ID Прораба";
                comboBox6.ValueMember = "ID Прораба";
                for (int i = 0; i < dataGridView6.ColumnCount; i++)
                {
                    dataGridView6.Columns[i].ReadOnly = true;
                }

                //Clients table and combobox
                string querySelectClients = "EXEC ListClientsForCEO";
                SqlDataAdapter adapterSelectClients = new SqlDataAdapter();
                SqlCommand sqlCommandSelectClients = new SqlCommand(querySelectClients, database.GetConnection());
                adapterSelectClients.SelectCommand = sqlCommandSelectClients;
                DataTable dataTableSelectClients = new DataTable();
                adapterSelectClients.Fill(dataTableSelectClients);
                bindingSelectClients = new BindingSource();
                bindingSelectClients.DataSource = dataTableSelectClients;
                dataGridView1.DataSource = dataTableSelectClients;
                comboBox3.DataSource = dataTableSelectClients;
                comboBox3.DisplayMember = "ID Компании";
                comboBox3.ValueMember = "ID Компании";
                
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                // Responsible table and comboboxes
                string querySelectResponsible = "EXEC selectResponsible";
                SqlDataAdapter adapterselectResponsible = new SqlDataAdapter();
                SqlCommand sqlCommandResponsible = new SqlCommand(querySelectResponsible, database.GetConnection());
                adapterselectResponsible.SelectCommand = sqlCommandResponsible;
                DataTable dataTableResponsible = new DataTable();
                adapterselectResponsible.Fill(dataTableResponsible);
                bindingSourceResponsible = new BindingSource();
                bindingSourceResponsible.DataSource = dataTableResponsible;
                dataGridView2.DataSource = dataTableResponsible;
                dataGridView2.Columns["Client_ID"].Visible = false;
                comboBox2.DataSource = dataTableResponsible;
                comboBox2.DisplayMember = "ID Ответственного";
                comboBox2.ValueMember = "ID Ответственного";
                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                // Responsible and contract "Client" combobox
                string querySelectClientNameAndIDs = "EXEC selectClientNameAndIDs";
                SqlDataAdapter adapterSelectClientNameAndIDs = new SqlDataAdapter();
                SqlCommand sqlCommandSelectClientNameAndIDs = new SqlCommand(querySelectClientNameAndIDs, database.GetConnection());
                adapterSelectClientNameAndIDs.SelectCommand = sqlCommandSelectClientNameAndIDs;
                DataTable dataTableSelectClientNameAndIDs = new DataTable();
                adapterSelectClientNameAndIDs.Fill(dataTableSelectClientNameAndIDs);
                bindingSourceSelectClientNameAndIDs = new BindingSource();
                bindingSourceSelectClientNameAndIDs.DataSource = dataTableSelectClientNameAndIDs;
                comboBox4.DataSource = dataTableSelectClientNameAndIDs;
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "Name";
                comboBox9.DataSource = dataTableSelectClientNameAndIDs;
                comboBox9.DisplayMember = "Name";
                comboBox9.ValueMember = "Name";

                // Objective table and combobox
                string querySelectObjectives = "EXEC ListObjectsForCEO";
                SqlDataAdapter adapterSelectObjectives = new SqlDataAdapter();
                SqlCommand sqlCommandSelectObjectives = new SqlCommand(querySelectObjectives, database.GetConnection());
                adapterSelectObjectives.SelectCommand = sqlCommandSelectObjectives;
                DataTable dataTableSelectObjectives = new DataTable();
                adapterSelectObjectives.Fill(dataTableSelectObjectives);
                bindingSourceObjectives = new BindingSource();
                bindingSourceObjectives.DataSource = dataTableSelectObjectives;
                dataGridView3.DataSource = dataTableSelectObjectives;
                comboBox1.DataSource = dataTableSelectObjectives;
                comboBox1.DisplayMember = "ID Объекта";
                comboBox1.ValueMember = "ID Объекта";

                for (int i = 0; i < dataGridView3.ColumnCount; i++)
                {
                    dataGridView3.Columns[i].ReadOnly = true;
                }

                // Contracts table and combobox
                string querySelectContract = "EXEC selectContract";
                SqlDataAdapter adapterContract = new SqlDataAdapter();
                SqlCommand sqlCommandContract = new SqlCommand(querySelectContract, database.GetConnection());
                adapterContract.SelectCommand = sqlCommandContract;
                DataTable dataTableContract = new DataTable();
                adapterContract.Fill(dataTableContract);
                bindingSourceContract = new BindingSource();
                bindingSourceContract.DataSource = dataTableContract;
                dataGridView4.DataSource = dataTableContract;
                dataGridView4.Columns["Client_ID"].Visible = false;
                dataGridView4.Columns["Objective_ID"].Visible = false;
                dataGridView4.Columns["Foreman_ID"].Visible = false;
                comboBox8.DataSource = dataTableContract;
                comboBox8.DisplayMember = "ID Контракта";
                comboBox8.ValueMember = "ID Контракта";
                for (int i = 0; i < dataGridView4.ColumnCount; i++)
                {
                    dataGridView4.Columns[i].ReadOnly = true;
                }

                // Contract "Objective" combobox
                string querySelectObjectivesNameAndIDs = "EXEC selectObjectivesNameAndIDs";
                SqlDataAdapter adapterSelectObjectivesNameAndIDs = new SqlDataAdapter();
                SqlCommand sqlCommandSelectObjectivesNameAndIDs = new SqlCommand(querySelectObjectivesNameAndIDs, database.GetConnection());
                adapterSelectObjectivesNameAndIDs.SelectCommand = sqlCommandSelectObjectivesNameAndIDs;
                DataTable dataTableSelectObjectivesNameAndIDs = new DataTable();
                adapterSelectObjectivesNameAndIDs.Fill(dataTableSelectObjectivesNameAndIDs);
                bindingSourceSelectObjectivesNameAndIDs = new BindingSource();
                bindingSourceSelectObjectivesNameAndIDs.DataSource = dataTableSelectObjectivesNameAndIDs;
                comboBox10.DataSource = dataTableSelectObjectivesNameAndIDs;
                comboBox10.DisplayMember = "Name";
                comboBox10.ValueMember = "Name";

                comboBox11.DataSource = dataTableForeman;
                comboBox11.DisplayMember = "ФИО";
                comboBox11.ValueMember = "ФИО";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            database.CloseConnection();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResponsibleHandler();
            LoadData();
        }

        private void ResponsibleHandler()
        {
            if (radioButton6.Checked)
            {
                ResponsibleInsert();
                return;
            }
            if (radioButton5.Checked)
            {
                ResponsibleUpdate();
                return;
            }
            return;
        }

        private void ResponsibleInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить ответственного ?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC AddResponsible @FIO, @about, @phone, @Client_ID";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox6.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@about", SqlDbType.VarChar, 100, textBox7.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 12, textBox9.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@Client_ID", SqlDbType.Int, 0, (comboBox4.SelectedItem as DataRowView)["Client_ID"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ResponsibleUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить ответственного №" +
                        (comboBox2.SelectedItem as DataRowView)["ID Ответственного"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateResponsible @id, @FIO, @about, @phone, @Client_ID";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox2.SelectedItem as DataRowView)["ID Ответственного"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox6.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@about", SqlDbType.VarChar, 100, textBox7.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 12, textBox9.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@Client_ID", SqlDbType.Int, 0, (comboBox4.SelectedItem as DataRowView)["Client_ID"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ForemanHandler();
            LoadData();
        }

        private void ForemanHandler()
        {
            if (radioButton10.Checked)
            {
                ForemanInsert();
                return;
            }
            if (radioButton9.Checked)
            {
                ForemanUpdate();
                return;
            }
            return;
        }
        private void ForemanInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить прораба?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC addForeman @FIO, @phone, @about";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox12.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 100, textBox13.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@about", SqlDbType.VarChar, 100, textBox14.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ForemanUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить прораба №" +
                         (comboBox6.SelectedItem as DataRowView)["ID Прораба"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateForeman @id, @FIO, @phone, @about";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox6.SelectedItem as DataRowView)["ID Прораба"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox12.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 100, textBox13.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@about", SqlDbType.VarChar, 100, textBox14.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmployeesHandler();
            LoadData();
        }

        private void EmployeesHandler()
        {
            if (radioButton8.Checked)
            {
                EmployeesInsert();
                return;
            }
            if (radioButton7.Checked)
            {
                EmployeesUpdate();
                return;
            }
            return;
        }

        private void EmployeesInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить сотрудника?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC AddEmployee @FIO, @login, @password, @role_id";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox11.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@login", SqlDbType.VarChar, 100, textBox8.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@password", SqlDbType.VarChar, 100, textBox10.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@role_id", SqlDbType.Int, 0, (comboBox7.SelectedItem as DataRowView)["bdRole_id"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void EmployeesUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить сотрудника №" +
                         (comboBox5.SelectedItem as DataRowView)["ID Сотрудника"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateEmployee @id, @FIO, @login, @password, @role_id";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox5.SelectedItem as DataRowView)["ID Сотрудника"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@FIO", SqlDbType.VarChar, 100, textBox11.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@login", SqlDbType.VarChar, 100, textBox8.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@password", SqlDbType.VarChar, 100, textBox10.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@role_id", SqlDbType.Int, 0, (comboBox7.SelectedItem as DataRowView)["bdRole_id"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ObjectiveHandler();
            LoadData();
        }

        private void ObjectiveHandler()
        {
            if (radioButton4.Checked)
            {
                ObjectiveInsert();
                return;
            }
            if (radioButton3.Checked)
            {
                ObjectiveUpdate();
                return;
            }
            return;
        }

        private void ObjectiveInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить объект ?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC addObjective @name, @address";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@name", SqlDbType.VarChar, 100, textBox4.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@address", SqlDbType.VarChar, 100, textBox5.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ObjectiveUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить объект №" +
                         (comboBox1.SelectedItem as DataRowView)["ID Объекта"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateObjective @id, @name, @address";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox1.SelectedItem as DataRowView)["ID Объекта"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@name", SqlDbType.VarChar, 100, textBox4.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@address", SqlDbType.VarChar, 100, textBox5.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClientHandler();
            LoadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ContractHandler();
            LoadData();
        }

        private void ContractHandler()
        {
            if (radioButton12.Checked)
            {
                ContractInsert();
                return;
            }
            if (radioButton11.Checked)
            {
                ContractUpdate();
                return;
            }
            return;
        }

        private void ContractInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить контракт?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC AddContract @client_id, @Date, @objective_id, @foreman_id";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@client_id", SqlDbType.Int, 0, (comboBox9.SelectedItem as DataRowView)["Client_ID"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@Date", SqlDbType.Date, 0, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCmd, "@objective_id", SqlDbType.Int, 0, (comboBox10.SelectedItem as DataRowView)["Objective_ID"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@foreman_id", SqlDbType.Int, 0, (comboBox11.SelectedItem as DataRowView)["ID Прораба"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ContractUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить контракт №" +
                        (comboBox8.SelectedItem as DataRowView)["ID Контракта"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateContract @id, @client_id, @Date, @objective_id, @foreman_id";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox8.SelectedItem as DataRowView)["ID Контракта"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@client_id", SqlDbType.Int, 0, (comboBox9.SelectedItem as DataRowView)["Client_ID"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@Date", SqlDbType.Date, 0, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Utility.AddSqlParameter(sqlCmd, "@objective_id", SqlDbType.Int, 0, (comboBox10.SelectedItem as DataRowView)["Objective_ID"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@foreman_id", SqlDbType.Int, 0, (comboBox11.SelectedItem as DataRowView)["ID Прораба"].ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ClientHandler()
        {
            if (radioButton1.Checked)
            {
                ClientInsert();
                return;
            }
            if (radioButton2.Checked)
            {
                ClientUpdate();
                return;
            }
            return;
        }

        private void ClientUpdate()
        {
            string confirmation = string.Format("Вы уверены что хотите обновить компанию №" +
                                    (comboBox3.SelectedItem as DataRowView)["ID Компании"].ToString() + "?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC updateClient @id, @name, @address, @phone";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@id", SqlDbType.Int, 0, (comboBox3.SelectedItem as DataRowView)["ID Компании"].ToString());
                Utility.AddSqlParameter(sqlCmd, "@name", SqlDbType.VarChar, 100, textBox3.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@address", SqlDbType.VarChar, 100, textBox2.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 12, textBox1.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void ClientInsert()
        {
            string confirmation = string.Format("Вы уверены что хотите добавить компанию ?");
            DialogResult result = MessageBox.Show(confirmation, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            database.OpenConnection();
            try
            {
                string query = "EXEC addClient @name, @address, @phone";
                SqlCommand sqlCmd = new SqlCommand(query, database.GetConnection());
                Utility.AddSqlParameter(sqlCmd, "@name", SqlDbType.VarChar, 100, textBox3.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@address", SqlDbType.VarChar, 100, textBox2.Text.Trim().ToString());
                Utility.AddSqlParameter(sqlCmd, "@phone", SqlDbType.VarChar, 12, textBox1.Text.Trim().ToString());
                sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            database.CloseConnection();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                return;
            comboBox3.Enabled = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton8.Checked)
                return;
            comboBox5.Enabled = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton7.Checked)
                return;
            comboBox5.Enabled = true;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton12.Checked)
                return;
            comboBox8.Enabled = false;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton11.Checked)
                return;
            comboBox8.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton10.Checked)
                return;
            comboBox6.Enabled = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton9.Checked)
                return;
            comboBox6.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
                return;
            comboBox3.Enabled = true;
        }


        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton5.Checked)
                return;
            comboBox2.Enabled = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton6.Checked)
                return;
            comboBox2.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton3.Checked)
                return;
            comboBox1.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton4.Checked)
                return;
            comboBox1.Enabled = false;
        }


        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }
    }
}
