using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelClient.ctrl;
using TravelCommon.model;

namespace TravelClient
{
    public partial class LogInWindow : Form
    {


        public LogInWindow()
        {
            InitializeComponent();
        }

        internal LoginController LoginController { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string password = textBox2.Text;
                if (name.Equals(""))
                {
                    MessageBox.Show("Name must be entered");
                    return;
                }
                if (password.Equals(""))
                {
                    MessageBox.Show("Password must be entered");
                    return;
                }
                Account account = LoginController.LogIn(name, password);
                if (account == null)
                {
                    MessageBox.Show("Invalid name or password");
                    return;
                }
                Console.WriteLine("account found" + account);
                clearTextFields();
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                MainController mainController = new MainController(LoginController.TravelServices)
                {
                    Account = account
                };
                mainWindow.MainController = mainController;
                mainWindow.Login = this;
                mainWindow.Text = account.Username;
                mainWindow.Show();
            }

            catch (Exception ex)
            {
                Console.WriteLine("handle login failed");
                MessageBox.Show(ex.Message);
            }

        }


        private void clearTextFields()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void LogInWindow_Load(object sender, EventArgs e)
        {

            this.FormClosed += LoginForm_FormClosed;

        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginController.Logout();
            Application.Exit();
        }

       
    }
}
