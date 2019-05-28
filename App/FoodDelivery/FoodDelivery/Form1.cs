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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form v2 = new Form2();
            v2.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form v3 = new Form3();
            v3.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form v4 = new DriverSignUp();
            v4.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
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
            else
            {
                SqlCommand cmd = new SqlCommand("FoodDelivery_FinalProject.uspLogin", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@pLoginName", SqlDbType.NVarChar, 50).Value = logname;
                cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = logPass;
                cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                String type =(String) cmd.Parameters["@responseMessage"].Value;

                if (type == "Client Login") {
                    Form client = new Client(logname);
                    client.Show();
                    this.Hide();
                }
            }
        }
    }
}
