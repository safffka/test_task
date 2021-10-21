using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test_task
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        private BindingSource bindingSource1 = new BindingSource();
        Db test;
        public void SetMyCustomFormat()
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        public Form1()
        {
            
            this.Controls.Add(dataGridView1);
            test = new Db();

            InitializeComponent();
            SetMyCustomFormat();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = null;
            var Change_1 = new Changes_Processing();
            Change_1.UploadChanges(test);
            MessageBox.Show("Successfully uploaded to database");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bindingSource1.DataSource = null;
            string param1 = dateTimePicker1.Text;
            string param2 =assemby_version_input.Text;
            if (param2==string.Empty)
            {
                param2 = null;
                
                param1 = "'" + dateTimePicker1.Text + "'";
            }
            else
            {
                param1 = null;
                param2= "'" + assemby_version_input.Text + "'";
            }
            
            // Automatically generate the DataGridView columns.
            dataGridView1.AutoGenerateColumns = true;

            // Set up the data source.
            bindingSource1.DataSource = test.select_db(param1, param2);
            dataGridView1.DataSource = bindingSource1;

            // Automatically resize the visible rows.
            dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Set the DataGridView control's border.
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Put the cells in edit mode when user enters them.
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
