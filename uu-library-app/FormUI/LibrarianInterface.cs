﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace uu_library_app.FormUI
{
    public partial class LibrarianInterface : Form
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
        public LibrarianInterface()
        {
            InitializeComponent();
            customizeDesign();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNewNav.Height = btnDashboard.Height;
            pnlNewNav.Top = btnDashboard.Top;
            pnlNewNav.Left = btnDashboard.Left;
        }
        #region Methods
        private void customizeDesign()
        {
            panelBookSubMenu.Visible = false;
            panelMembersSubMenu.Visible = false;
            pnlDepositSubMenu.Visible = false;
            
        }
        private void hideSubMenu()
        {
            if (panelBookSubMenu.Visible == true)
                panelBookSubMenu.Visible = false;
            if (panelMembersSubMenu.Visible == true)
                panelMembersSubMenu.Visible = false;
            if (pnlDepositSubMenu.Visible == true)
                pnlDepositSubMenu.Visible = false;


        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
#endregion
        private void LibrarianInterface_Load(object sender, EventArgs e)
        {

        }
        #region Buttons Click Events
        private void btnDashboard_Click(object sender, EventArgs e)
        {
           
            pnlNewNav.Height = btnDashboard.Height;
            pnlNewNav.Top = btnDashboard.Top;
            pnlNewNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnBooks.Height;
            pnlNewNav.Top = btnBooks.Top;
            pnlNewNav.Left = btnBooks.Left;
            btnBooks.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panelBookSubMenu);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnMembers.Height;
            pnlNewNav.Top = btnMembers.Top;
            pnlNewNav.Left = btnMembers.Left;
            btnMembers.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panelMembersSubMenu);
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnDeposit.Height;
            pnlNewNav.Top = btnDeposit.Top;
            pnlNewNav.Left = btnDeposit.Left;
            btnDeposit.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(pnlDepositSubMenu);
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnSettings.Height;
            pnlNewNav.Top = btnSettings.Top;
            pnlNewNav.Left = btnSettings.Left;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnLogOut.Height;
            pnlNewNav.Top = btnLogOut.Top;
            pnlNewNav.Left = btnLogOut.Left;
            btnLogOut.BackColor = Color.FromArgb(46, 51, 73);
            Application.Exit();
        }
#endregion
        #region Buttons Leave
        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnBooks_Leave(object sender, EventArgs e)
        {
            btnBooks.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnMembers_Leave(object sender, EventArgs e)
        {
            btnMembers.BackColor = Color.FromArgb(24, 30, 54);
        }

        
        private void btnDeposit_Leave(object sender, EventArgs e)
        {
            btnDeposit.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnLogOut_Leave(object sender, EventArgs e)
        {
            btnLogOut.BackColor = Color.FromArgb(24, 30, 54);
        }
#endregion
        #region CRUD Books Buttons
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            
            hideSubMenu();
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }


        #endregion
        #region CRUD Members Buttons
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            openChildForm(new Add_Student());
            hideSubMenu();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnLending_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }





        #endregion

       
    }
}
