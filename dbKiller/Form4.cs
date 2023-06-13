using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbKiller
{
    public partial class Form4 : Form
    {
        Database database;
        Form1 form1;
        List<string[]> dataRoles;
        public Form4(Database database, Form1 form1)
        {
            InitializeComponent();
            this.database = database;
            this.form1 = form1;

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }
    }
}
