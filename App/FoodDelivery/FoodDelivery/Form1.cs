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
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        

        public Form1()
        {
            InitializeComponent();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String logname = textBox1.Text;
            String logPass = textBox2.Text;

            if (logname == string.Empty)
            {
                MessageBox.Show("Insert Username");
            }
            else if (logPass == string.Empty)
            {
                MessageBox.Show("Insert Password");
            }
            else {
                SqlCommand cmd = new SqlCommand("FoodDelivery_FinalProject.uspLogin", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@pLoginName", SqlDbType.NVarChar, 50).Value = logname;
                cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = logPass;
                cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);
            }


            
            






        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel4.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                String filename = openFile.FileName;
                textBox8.Text = filename;
            }

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string picturePath=textBox8.Text ;
            string LoginName = textBox3.Text;
            string Password = textBox5.Text;
            string Name = textBox6.Text;
            string contact = textBox7.Text;
            string street = textBox9.Text;
            string city = textBox10.Text;
            string postalCode = textBox11.Text;
            string cardNumber = textBox12.Text;
            string cardExpiration = textBox13.Text;
            
            
            string picture=null;

            //Sign up
            if (!(string.IsNullOrEmpty(picturePath))) {
                using (Image image = Image.FromFile(picturePath)) {
                    using (MemoryStream m = new MemoryStream()) {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        picture = Convert.ToBase64String(imageBytes);

                    }

                }
            }
            SqlCommand cmd=null;
            
            cmd = new SqlCommand("FoodDelivery_FinalProject.AddUser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pLogin", SqlDbType.NVarChar, 50).Value = LoginName;
            cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = Password;
            cmd.Parameters.Add("@pName", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = picture;
            cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = street;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@PostalCode ", SqlDbType.NVarChar).Value = postalCode;
            cmd.Parameters.Add("@CardNumber", SqlDbType.NChar, 16).Value = cardNumber;
            cmd.Parameters.Add("@CardExpirationDate", SqlDbType.NChar, 5).Value = cardExpiration;

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
            

            
            

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);

            panel2.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel4.Visible = false;
            panel1.Visible = false;
           



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string LicensePlate = textBox24.Text;
            string Model = textBox15.Text;


            string picturePath = textBox19.Text;
            string LoginName = textBox23.Text;
            string Password = textBox22.Text;
            string Name = textBox21.Text;
            string contact = textBox20.Text;
            string street = textBox18.Text;
            string city = textBox17.Text;
            string postalCode = textBox16.Text;
            


            string picture = null;

            //Sign up
            if (!(string.IsNullOrEmpty(picturePath)))
            {
                using (Image image = Image.FromFile(picturePath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        picture = Convert.ToBase64String(imageBytes);

                    }

                }
            }
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.AddDriver", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LicensePlate", SqlDbType.NChar, 8).Value = LicensePlate;
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar,20).Value =Model;
            cmd.Parameters.Add("@pLogin", SqlDbType.NVarChar, 50).Value = LoginName;
            cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = Password;
            cmd.Parameters.Add("@pName", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = picture;
            cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = street;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@PostalCode ", SqlDbType.NVarChar).Value = postalCode;
            

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;





            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);

            panel2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            

        }

        private void button10_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String filename = openFile.FileName;
                textBox19.Text = filename;
            }

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;

            string RestaurantID = textBox4.Text;

            cmd = new SqlCommand("SELECT FoodDelivery_FinalProject.restaurantLogin("+RestaurantID+")", cn);
            

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            
            int ret=(int)cmd.ExecuteScalar();

            MessageBox.Show("ola " + ret);

            panel2.Visible = false;
        }
    }
}
