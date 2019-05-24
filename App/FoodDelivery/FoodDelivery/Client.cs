using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodDelivery
{
    public partial class Client : Form
    {
        private SqlConnection cn;
        private String username;

        public Client(String username)
        {
            InitializeComponent();
            this.username = username;
            

        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;Initial Catalog = p5g10; uid =p5g10 ;password =PasssNovaBD!2018 ");


        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                //MessageBox.Show("Successful connection to database " + cn.Database + " on the " + cn.DataSource + " server", "Connection Test", MessageBoxButtons.OK);
            }

            return cn.State == ConnectionState.Open;
        }
        

        private void loadProfile() {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getProfile('Amélia_Pereira78')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader["LoginName"].ToString();
                textBox1.Text = reader["LoginName"].ToString();
                //String temp = reader["Photo"].ToString();
                byte [] image=Convert.FromBase64String("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");
                pictureBox1.Image = byteArrayToImage(image);
                textBox3.Text = reader["Name"].ToString();
                textBox4.Text = reader["Contact"].ToString();
                textBox5.Text = reader["Street"].ToString();
                textBox7.Text = reader["City"].ToString();
                textBox6.Text = reader["PostalCode"].ToString();
                textBox8.Text = reader["CardNumber"].ToString();
                textBox9.Text = reader["CardExpirationDate"].ToString();
            }
            






        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        private void Client_Load_1(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            loadProfile();
        }
    }
}
