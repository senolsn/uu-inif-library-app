﻿using MessageBoxDenemesi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.MailSettings
{
    public partial class FacultyQuick : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse
               );
        private Admin _admin;
        public FacultyQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }

        LoggerManager logger = new LoggerManager(new LoggerDal());
        FacultyManager manager = new FacultyManager(new FacultyDal());

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                wehMessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            Faculty facultyToAdd = new Faculty(createGUID, txtAd.Text);
            try
            {
                manager.Add(facultyToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + facultyToAdd.Id + " | " + facultyToAdd.Name + "] eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                wehMessageBox.Show("Başarıyla eklendi!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception)
            {
                wehMessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                throw;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
