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
    public partial class Language_Operations : Form
    {
        private Admin _admin;
        public Language_Operations(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        LanguageManager manager = new LanguageManager(new LanguageDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtDil.Clear();
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Language WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
           
        }

        private void Language_Operations_Load(object sender, EventArgs e)
        {
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
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Lütfen silinecek dili seçin...");
                    return;
                }

                DialogResult dialogResult = wehMessageBox.Show("Silmek istediğinize emin misiniz? Bu işlem bu dile ait olan bütün kitapları da silecektir!",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Language language = new Language(txtId.Text, txtDil.Text);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + language.Id + " | " + language.LanguageName + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    manager.Delete(language);
                    listDataToTable();
                    clearAllFields();
                }
                
            }
            catch (Exception ex)
            {
                wehMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtDil.Text == "" || txtDil.Text.Length < 3)
            {
                wehMessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Language languageToAdd = new Language(createGUID, txtDil.Text);

            try
            {
                manager.Add(languageToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + languageToAdd.Id + " | " + languageToAdd.LanguageName + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                wehMessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDil.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
           
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            Language languageToUpdate = new Language(txtId.Text, txtDil.Text);

            try
            {
                if (txtDil.Text == "" && txtDil.Text.Length < 3)
                {
                    wehMessageBox.Show("Geçerli bir değer giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    manager.Update(languageToUpdate);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + languageToUpdate.Id + " | " + languageToUpdate.LanguageName + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    listDataToTable();
                    clearAllFields();
                }

            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }
    }
}
