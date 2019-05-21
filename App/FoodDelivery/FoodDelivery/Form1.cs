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
            //panel2.Visible = false;
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

            /*string picturePath=//buscar

            //Sign up
            if (!(string.IsNullOrEmpty(picturePath))) {
                using (Image image = Image.FromFile(picturePath)) {
                    using (MemoryStream m = new MemoryStream()) {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        picturePath = Convert.ToBase64String(imageBytes);

                    }

                }
            }*/
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
