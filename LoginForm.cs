using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Manager
{
    public partial class LoginForm : Form
    {
        private Form1 inventoryManagerForm;
        public LoginForm(Form1 form = null)
        {
            InitializeComponent();
            inventoryManagerForm = form;
            passwordTxtBox.UseSystemPasswordChar = true; // Hide the password characters

            // Letting the "Enter: key be used to login as well
            this.AcceptButton = loginBtn;

            // Attching an evem handler for FormClosed event
            this.FormClosed += LoginForm_FormClosed;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void SetInventoryManagerForm(Form1 form)
        {
            inventoryManagerForm = form;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameTxtBox.Text;
            string password = passwordTxtBox.Text;

            // Authentication logic performed here (check against a database)
            bool isAuthenticated = PerformAuthentication(username, password);

            if (isAuthenticated)
            {
                if (inventoryManagerForm == null || inventoryManagerForm.IsDisposed)
                {
                    inventoryManagerForm = new Form1();
                }
                inventoryManagerForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool PerformAuthentication(string username, string password)
        {
            // Authentication logic will go hewre, but I am just going to use "admin" for username and "password" for password
            return (username == "admin" && password == "password");
        }

        public void ClearCredentials()
        {
            usernameTxtBox.Text = string.Empty;
            passwordTxtBox.Text = string.Empty;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
