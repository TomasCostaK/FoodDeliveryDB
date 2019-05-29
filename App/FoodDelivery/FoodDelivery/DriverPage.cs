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
    public partial class DriverPage : Form
    {

        private SqlConnection cn;
        private String driverID;

        public DriverPage(String driverID)
        {
            InitializeComponent();
            this.driverID = driverID;

        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;Initial Catalog = p5g10; uid =p5g10 ;password =PasssNovaBD!2018;MultipleActiveResultSets=True ");

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

        private void createTable()
        {
            listView2.Columns.Add("Latitude", 120);
            listView2.Columns.Add("Longitude", 120);
            listView2.Columns.Add("Date", 120);
            listView2.Columns.Add("Hour", 120);

        }

        private void createTable2()
        {
            listView1.Columns.Add("RequestID", 120);
            listView1.Columns.Add("ClientID", 120);
            listView1.Columns.Add("TravelCost", 120);
            listView1.Columns.Add("EstimatedTime", 150);

        }

        private void populateComboBox1()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantCity()", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            var dataSource = new List<string>();

            while (reader.Read())
            {

                string type = reader["City"].ToString();
                dataSource.Add(type);

            }

            comboBox1.DataSource = dataSource;

            cn.Close();
        }

        private void loadProfile()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getDriver('" + driverID + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                String temp = reader["Photo"].ToString();
                if (temp != "")
                {
                    byte[] image = Convert.FromBase64String(temp);
                    pictureBox1.Image = byteArrayToImage(image);
                }

                textBox9.Text = reader["Name"].ToString();
                textBox8.Text = reader["Contact"].ToString();
                textBox7.Text = reader["Street"].ToString();
                textBox6.Text = reader["City"].ToString();
                textBox5.Text = reader["PostalCode"].ToString();
                textBox11.Text = reader["LicensePlate"].ToString();
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose();

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

  

        private void enableTextBoxs(Boolean check)
        {
            textBox5.ReadOnly = check;
            textBox6.ReadOnly = check;
            textBox7.ReadOnly = check;
            textBox8.ReadOnly = check;
            textBox9.ReadOnly = check;
            textBox11.ReadOnly = check;
        }

        private void TabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DriverPage_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            createTable();
            createTable2();
            //populateComboBox1();

            enableTextBoxs(true);
            loadTrackings();
            loadRequests();
            loadProfile();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {

            if (!verifySGBDConnection())
                return;

            string op1 = comboBox1.Text;
            string userlookingfor = button6.Text;

            SqlCommand cmd = new SqlCommand("exec FoodDelivery_FinalProject.getAllTrackings '" + op1 + "'", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string lat = reader["GPS_Latitude"].ToString();
                string longi = reader["GPS_Latitude"].ToString();
                string trdate = reader["Date"].ToString();
                string hour = reader["Hour"].ToString();
                var row = new string[] { lat, longi, trdate, hour};
                var lvi = new ListViewItem(row);
                listView2.View = View.Details;
                listView2.Items.Add(lvi);

            }

            cn.Close();
        }
        
        private void loadTrackings()
        {
            if (!verifySGBDConnection())
                return;

            string op1 = comboBox1.Text;
            MessageBox.Show(op1);
            SqlCommand cmd = new SqlCommand("exec FoodDelivery_FinalProject.getAllTrackings '"+op1 + "'", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string lat = reader["GPS_Latitude"].ToString();
                string longi = reader["GPS_Latitude"].ToString();
                string trdate = reader["Date"].ToString();
                string hour = reader["Hour"].ToString();
                var row = new string[] { lat, longi, trdate, hour };
                var lvi = new ListViewItem(row);
                listView2.View = View.Details;
                listView2.Items.Add(lvi);

            }

            cn.Close();
        }

        private void loadRequests()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getOrders('Mariana_Vasconcelos100000080')", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string req = reader["RequestID"].ToString();
                string cli = reader["ClientID"].ToString();
                string cost = reader["TravelCost"].ToString();
                string time = reader["EstimatedTime"].ToString();
                var row = new string[] { req, cli, cost, time };
                var lvi = new ListViewItem(row);
                listView1.View = View.Details;
                listView1.Items.Add(lvi);

            }

            cn.Close();
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTrackings();
        }



    }
}
