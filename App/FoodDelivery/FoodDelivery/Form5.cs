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
    

    public partial class Form5 : Form
    {
        private SqlConnection cn;
        public Form5()
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

        private void button12_Click(object sender, EventArgs e)
        {
            string Name = textBox25.Text;
            string password = textBox1.Text;
            string contact = textBox30.Text;
            string street = textBox28.Text;
            string city = textBox27.Text;
            string postalCode = textBox26.Text;
            string Type = textBox14.Text;




            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.AddRestaurant", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*SqlParameter outPutVal = new SqlParameter("@IdentityOutput", SqlDbType.Int);
            outPutVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutVal);*/
            cmd.Parameters.Add("@pName", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = password;

            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = street;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@PostalCode ", SqlDbType.NVarChar).Value = postalCode;
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;


            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;





            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            string restaurantID="";
            /*if (outPutVal.Value != DBNull.Value) restaurantID = outPutVal.Value.ToString();

            MessageBox.Show("Your restaurant ID " + restaurantID);*/

            /*panel2.Visible = false;
            panel3.Visible = false;
            panel1.Visible = false;
            panel4.Visible = false;*/
        }
    }
}
