using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Game game = new Game();
                string userName, password, command;
                int id = int.Parse(txtId.Text);

                userName = txtName.Text;
                password = txtPassword.Text;

                SqlConnection mySqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;database=dugma;Integrated Security=SSPI;");
                SqlCommand mysqlcommand = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command = mysqlcommand.CommandText = $"SELECT * FROM user_Info WHERE id = {id} AND user_name = '{userName}' AND password = '{password}'";
                id = -1;
                SqlDataReader r = mysqlcommand.ExecuteReader();

                while (r.Read())
                {
                    id = int.Parse(r["id"].ToString());
                }
                if (id == -1)
                    MessageBox.Show("user name , passwored or id is invalid ...");
                else
                    game.ShowDialog();



                txtName.Text = "";
                txtPassword.Text = "";
                txtId.Text = "";
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnRegester_Click(object sender, EventArgs e)
        {
            try
            {
                string Password, fname;
                int id;
                id = int.Parse(txtRegesterId.Text);
                fname = txtRegesterName.Text;
                Password = txtRegesterPassword.Text;
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;database=dugma;Integrated Security=SSPI;");
                SqlCommand mysqlcommand = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                mysqlcommand.CommandText = $"insert into user_Info values({id}, '{fname}', '{Password}')";

                int n = mysqlcommand.ExecuteNonQuery();
                MessageBox.Show(" you regesterd " + n);
                txtRegesterName.Text = "";
                txtRegesterPassword.Text = "";
                txtRegesterId.Text = "";
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to exit ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
