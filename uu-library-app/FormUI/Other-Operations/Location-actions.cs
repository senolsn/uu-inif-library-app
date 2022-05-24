﻿using MySql.Data.MySqlClient;
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

namespace uu_library_app.FormUI.Other_Operations
{
    public partial class Location_actions : Form
    {
        public Location_actions()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LocationManager manager = new LocationManager(new LocationDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Location.id, Location.shelf, Category.name FROM Location INNER JOIN Category ON Location.categoryId=Category.id WHERE Location.deleted=0", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "Konum";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Kategori";
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "")
            {
                MessageBox.Show("Lütfen geçerli bir değer giriniz!");
                return;
            }

            Location locationToAdd = new Location(createGUID, txtAd.Text, cmbKategori.SelectedValue.ToString());
            try
            {
                manager.Add(locationToAdd);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Lütfen silinecek dili seçin...");
                    return;
                }
                manager.Delete(txtId.Text);
                listDataToTable();
                clearAllFields();
                MessageBox.Show("Başarıyla silindi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Location locationToUpdate = new Location(txtId.Text, txtAd.Text, cmbKategori.SelectedValue.ToString());

            try
            {
                if (txtAd.Text == "")
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }
                manager.Update(locationToUpdate);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void Location_actions_Load(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            DataSet dsCategories = new DataSet();
            daCategories.Fill(dsCategories);
            SqlCommandHelper.getCategoriesCommand(conn).ExecuteNonQuery();

            //Konum
            cmbKategori.DataSource = dsCategories.Tables[0];
            cmbKategori.DisplayMember = "name";
            cmbKategori.ValueMember = "id";
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
