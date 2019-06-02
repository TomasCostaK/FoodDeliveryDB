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
    public partial class Form2 : Form
    {
        private SqlConnection cn;

        public Form2()
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form v1 = new Form1();
            v1.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form v5 = new Form5();
            v5.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;

            


            String logname = textBox4.Text;
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
                cmd = new SqlCommand("FoodDelivery_FinalProject.restaurantLoginProcedure", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@pLoginName", SqlDbType.NVarChar, 50).Value = logname;
                cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = logPass;
                cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;

                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                String type = (String)cmd.Parameters["@responseMessage"].Value;

                if (type == "Restaurant Login")
                {
                    Form v1 = new RestaurantPage(logname);
                    v1.Show();
                    this.Close();
                }
                
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
