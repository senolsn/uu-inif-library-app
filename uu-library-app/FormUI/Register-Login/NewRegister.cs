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
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Register_Login
{
    public partial class NewRegister : Form
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
        public NewRegister()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void NewRegister_Load(object sender, EventArgs e)
        {
            txtSifre.ForeColor = Color.White;
            txtEmail.ForeColor = Color.White;
            txtAd.ForeColor = Color.White;
            txtSoyad.ForeColor = Color.White;
            txtSifreTekrar.ForeColor = Color.White;
        }

        AdminManager adminManager = new AdminManager(new AdminDal());
        Admin admin;
        string code;
        private void button2_Click(object sender, EventArgs e)
        {
            code = EmailVerificator.GenerateCode();
            Console.WriteLine(code);
            string createGUID = System.Guid.NewGuid().ToString();
            admin = new Admin(createGUID, txtAd.Text, txtSoyad.Text, txtEmail.Text, StringEncoder.Encrypt(txtSifre.Text));
            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                wehMessageBox.Show("Parolalar uyuşmuyor!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtSifre.Text == "")
            {
                wehMessageBox.Show("Tüm alanları doldurun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmailVerificator.isValidPersonnelMail(txtEmail.Text))
            {
                Console.WriteLine(code);
                //adminManager.sendEmailVerificationCode(txtEmail.Text, code);
                panelOnay.Visible = true;
                button2.Enabled = false;
                txtAd.Enabled = false;
                txtSoyad.Enabled = false;
                txtEmail.Enabled = false;
                txtSifre.Enabled = false;
                txtSifreTekrar.Enabled = false;
            }
            else{
                wehMessageBox.Show("E-posta; 'uludag.edu.tr' uzantısına sahip geçerli bir e-posta adresi olmalıdır!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void btnOnayKodu_Click(object sender, EventArgs e)
        {
            if (txtOnayKodu.Text == code)
            {
                adminManager.Add(admin, txtOnayKodu.Text);
                wehMessageBox.Show("Başarıyla kaydoldunuz! Giriş sayfasına yönlendiriliyorsunuz...",
                 "Başarılı!",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                System.Threading.Thread.Sleep(4000);
                this.Hide();
                NewLogin login = new NewLogin();
                login.Show();

            }
            else
            {
                wehMessageBox.Show("Onay kodu hatalı! Tekrar giriniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewLogin newLogin = new NewLogin();
            newLogin.Show();
        }
    }
}
