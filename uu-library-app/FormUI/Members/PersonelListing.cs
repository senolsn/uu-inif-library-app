﻿using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Delete_Student : Form
    {
        Admin _admin;
        public Delete_Student(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        StudentManager manager = new StudentManager(new StudentDal());

        private void Delete_Student_Load(object sender, EventArgs e)
        {
            conn.Open();
            DataListerToTableHelper.listInnerJoinAllPersonnelsNotConcatDataToTable2(dataGridView1, conn);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;


            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            comboSource.Add("firstName", "Personel Adı");
            comboSource.Add("Email", "E-Posta");
            comboSource.Add("departmentName", "Bölüm");
            comboSource.Add("facultyName", "Fakülte");
            cmbAranacakAlan.DataSource = new BindingSource(comboSource, null);
            cmbAranacakAlan.DisplayMember = "Value";
            cmbAranacakAlan.ValueMember = "Key";

            conn.Close();
        }    

     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            
        }

        private void wehTextBox1__TextChanged_1(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("" + value + " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void radiusButton1_Click(object sender, EventArgs e)
        {
            DataListerToTableHelper.listInnerJoinAllPersonnelsDataToTableBetweenDates(dataGridView1, conn, dateTime1.Value, dateTime2.Value);
        }
    }
}
