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

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getProfile('"+username+"')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader["LoginName"].ToString();
                String temp = reader["Photo"].ToString();
                MessageBox.Show(temp);
                if (temp != "") {
                    byte[] image = Convert.FromBase64String(temp);
                    pictureBox1.Image = byteArrayToImage(image);
                }
               
                textBox3.Text = reader["Name"].ToString();
                textBox4.Text = reader["Contact"].ToString();
                textBox5.Text = reader["Street"].ToString();
                textBox7.Text = reader["City"].ToString();
                textBox6.Text = reader["PostalCode"].ToString();
                textBox8.Text = reader["CardNumber"].ToString();
                textBox9.Text = reader["CardExpirationDate"].ToString();
            }






            cn.Close();
        }

        private void loadRestaurants()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurant()", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            listView1.Columns.Add("Name", 150);
            //listView1.Dock = DockStyle.Fill;

            listView1.Items.Clear();

            while (reader.Read())
            {
                string text= reader["Name"].ToString();
                var row = new string[] { text };
                var lvi = new ListViewItem(row);
                listView1.View=View.Details;
                listView1.Items.Add(lvi);
                
            }






            cn.Close();
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
            loadRestaurants();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void enableTextBoxs(Boolean check)
        {
            textBox1.ReadOnly = check;
            textBox2.ReadOnly = check;
            textBox3.ReadOnly = check;
            textBox4.ReadOnly = check;
            textBox5.ReadOnly = check;
            textBox6.ReadOnly = check;
            textBox7.ReadOnly = check;
            textBox8.ReadOnly = check;
            textBox9.ReadOnly = check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Show();
            button3.Show();
            button7.Show();
            textBox10.Show();
            enableTextBoxs(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Show();
            button2.Hide();
            button3.Hide();
            button7.Hide();
            textBox10.Hide();
            enableTextBoxs(true);
            loadProfile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string LoginName = textBox1.Text;
            string picturePath = textBox10.Text;
            

            string Name =textBox3.Text;
            string contact = textBox4.Text;
            string street = textBox5.Text;
            string city =textBox7.Text;
            string postalCode = textBox6.Text;
            string cardNumber = textBox8.Text;
            string cardExpiration=textBox9.Text;


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

            cmd = new SqlCommand("FoodDelivery_FinalProject.UpdateUser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pLogin", SqlDbType.NVarChar, 50).Value = LoginName;
            cmd.Parameters.Add("@pName", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            if (picture == "")
            {
                cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = "nothing";
            }
            else {
                cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = picture;

            }
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

            button1.Show();
            button2.Hide();
            button3.Hide();
            button7.Hide();
            textBox10.Hide();
            enableTextBoxs(true);
            loadProfile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String filename = openFile.FileName;
                textBox10.Text = filename;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
