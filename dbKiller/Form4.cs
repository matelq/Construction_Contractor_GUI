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
                      bindingSourceForeman;
        public Form4(Database database, Form1 form1)
        {
            InitializeComponent();
            this.database = database;
            this.form1 = form1;
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
                //Clients table and ... combobox
                string querySelectClients = "EXEC ListClientsForCEO";
                SqlDataAdapter adapterSelectClients = new SqlDataAdapter();
                SqlCommand sqlCommandSelectClients = new SqlCommand(querySelectClients, database.GetConnection());
                adapterSelectClients.SelectCommand = sqlCommandSelectClients;
                DataTable dataTableSelectClients = new DataTable();
                adapterSelectClients.Fill(dataTableSelectClients);
                bindingSelectClients = new BindingSource();
                bindingSelectClients.DataSource = dataTableSelectClients;
                dataGridView1.DataSource = dataTableSelectClients;
                /*
                comboBox3.DataSource = dataTableSelectInvoice;
                comboBox3.DisplayMember = "ID накладной";
                comboBox3.ValueMember = "ID накладной";
                */
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                // Objective table and ... combobox
                string querySelectObjectives = "EXEC ListObjectsForCEO";
                SqlDataAdapter adapterSelectObjectives = new SqlDataAdapter();
                SqlCommand sqlCommandSelectObjectives = new SqlCommand(querySelectObjectives, database.GetConnection());
                adapterSelectObjectives.SelectCommand = sqlCommandSelectObjectives;
                DataTable dataTableSelectObjectives = new DataTable();
                adapterSelectObjectives.Fill(dataTableSelectObjectives);
                bindingSourceObjectives = new BindingSource();
                bindingSourceObjectives.DataSource = dataTableSelectObjectives;
                dataGridView3.DataSource = dataTableSelectObjectives;
                /*
                comboBox2.DataSource = dataTableSelectContractIDs;
                comboBox2.DisplayMember = "Contract_ID";
                comboBox2.ValueMember = "Contract_ID";
                */
                for (int i = 0; i < dataGridView3.ColumnCount; i++)
                {
                    dataGridView3.Columns[i].ReadOnly = true;
                }

                // Responsible table and ... combobox
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
                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                /*
                comboBox1.DataSource = dataTableWorkType;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Name";
                */

                // Contracts table and ... combobox
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
                for (int i = 0; i < dataGridView4.ColumnCount; i++)
                {
                    dataGridView4.Columns[i].ReadOnly = true;
                }
                /*
                comboBox4.DataSource = dataTableFullWorkType;
                comboBox4.DisplayMember = "ID типа работ";
                comboBox4.ValueMember = "ID типа работ";
                 */

                // Employee table and ... combobox
                string querySelectEmployee = "EXEC ListEmployees";
                SqlDataAdapter adapterEmployee = new SqlDataAdapter();
                SqlCommand sqlCommandEmployee = new SqlCommand(querySelectEmployee, database.GetConnection());
                adapterEmployee.SelectCommand = sqlCommandEmployee;
                DataTable dataTableEmployee = new DataTable();
                adapterEmployee.Fill(dataTableEmployee);
                bindingSourceEmployee = new BindingSource();
                bindingSourceEmployee.DataSource = dataTableEmployee;
                dataGridView5.DataSource = dataTableEmployee;
                dataGridView5.Columns["Employee_ID"].Visible = false;
                dataGridView5.Columns["bdRole_id"].Visible = false;
                //dataGridView4.Columns["Foreman_ID"].Visible = false;
                for (int i = 0; i < dataGridView5.ColumnCount; i++)
                {
                    dataGridView5.Columns[i].ReadOnly = true;
                }

                // Foreman table and ... combobox
                string querySelectForeman = "EXEC ListForeman";
                SqlDataAdapter adapterForeman = new SqlDataAdapter();
                SqlCommand sqlCommandForeman = new SqlCommand(querySelectForeman, database.GetConnection());
                adapterForeman.SelectCommand = sqlCommandForeman;
                DataTable dataTableForeman = new DataTable();
                adapterForeman.Fill(dataTableForeman);
                bindingSourceForeman = new BindingSource();
                bindingSourceForeman.DataSource = dataTableForeman;
                dataGridView6.DataSource = dataTableForeman;
                dataGridView6.Columns["Foreman_ID"].Visible = false;
                //dataGridView4.Columns["Objective_ID"].Visible = false;
                //dataGridView4.Columns["Foreman_ID"].Visible = false;
                for (int i = 0; i < dataGridView6.ColumnCount; i++)
                {
                    dataGridView6.Columns[i].ReadOnly = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            database.CloseConnection();

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }
    }
}
