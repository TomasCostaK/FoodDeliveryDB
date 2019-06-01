using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        Dictionary<string, string[]> GPS = new Dictionary<string, string[]>
        { {"Aveiro", new String [2] { "40,644270", "-8,645540" } },
               {"Beja",new String [2]  {"38,015060", "-7,863230" } },
               {"Braga",new String [2] {"41,550320","-8,420050"}},
               {"Bragança",new String [2] {"41,805820", "-6,757190"}},
               {"Castelo Branco",new String [2] {"39,822190","-7,490870"}},
               {"Coimbra",new String [2] {"40,205640","-8,419550"}},
               {"Évora",new String [2] {"38,566670","-7,900000"}},
               {"Faro",new String [2] {"37,019370","-7,932230"}},
               {"Guarda",new String [2] {"40,537330","-7,265750"}},
               {"Leiria",new String [2] {"39,743620","-8,807050"}},
               {"Lisboa",new String [2] {"38,716670","-9,133330"}},

               {"Portalegre",new String [2] {"39,293790","-7,431220"}},
               {"Porto",new String [2] {"41,149610","-8,610990"}},
               {"Santarém",new String [2] {"39,233330","-8,683330"}},
               {"Setúbal",new String [2] {"38,524400","-8,888200"}},
               {"Viana do Castelo",new String [2]   {"41,693230","-8,832870"}},
               {"Vila Real",new String [2]          {"41,300620","-7,744130"}},
               { "Viseu",new String [2]                {"40,661010","-7,909710"}},
        };

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
            listView2.Columns.Add("City", 130);
            listView2.Columns.Add("Date", 120);
            listView2.Columns.Add("Hour", 120);

        }

        private void createTable2()
        {
            listView3.Columns.Add("RequestID", 100);
            listView3.Columns.Add("ClientID", 100);
            listView3.Columns.Add("City", 150);
            listView3.Columns.Add("Street", 150);

            listView1.Columns.Add("RequestID", 120);
            listView1.Columns.Add("ClientID", 120);
            listView1.Columns.Add("TravelCost", 120);
            listView1.Columns.Add("EstimatedTime", 150);

        }


        private void loadProfile()
        {
            if (!verifySGBDConnection())
                return;

            textBox10.Hide();

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
            populateComboBox2();

            enableTextBoxs(true);
            loadTrackings();
            loadRequests();
            loadDoneRequests();
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
            string chunk = textBox12.Text;
            SqlCommand cmd = null;
            string city = textBox12.Text;
            listView2.Items.Clear();
            if (GPS.ContainsKey(city))
            {
                string gps_lat = GPS[city][0];//.Replace(',', '.');
                string gps_lon = GPS[city][1];//.Replace(',', '.');

                cmd = new SqlCommand("select * from FoodDelivery_FinalProject.getAllTrackings('" + op1 + "' , '" + driverID + "', '" + chunk + "')", cn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["GPS_Latitude"].ToString().Equals(gps_lat) && reader["GPS_Longitude"].ToString().Equals(gps_lon))
                    {

                        string trdate = (reader["Date"].ToString()).Split(' ')[0];
                        string hour = reader["Hour"].ToString();
                        var row = new string[] { city, trdate, hour };
                        var lvi = new ListViewItem(row);
                        listView2.View = View.Details;
                        listView2.Items.Add(lvi);
                    }
                }

            }

            cn.Close();
        }
        

        private void loadTrackings()
        {
            if (!verifySGBDConnection())
                return;

            string op1 = comboBox1.Text;
            string chunk = textBox12.Text;
            SqlCommand cmd = new SqlCommand("select * from FoodDelivery_FinalProject.getAllTrackings('"+op1+ "' , '" + driverID + "', '" + chunk + "')", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            listView2.Items.Clear();

            while (reader.Read())
            {
                string lat = reader["GPS_Latitude"].ToString();
                string longi = reader["GPS_Longitude"].ToString();
                string displaycity = "None";
                foreach (var city in GPS)
                {
                    Debug.WriteLine(city.Value[0] + " -> " + lat + " : " + city.Value[1]  +" -> " + longi);
                    if (city.Value[0].Equals(lat) && city.Value[1].Equals(longi))
                    {
                        displaycity = city.Key.ToString();
                    }
                }

                string trdate = (reader["Date"].ToString()).Split(' ')[0];
                string hour = reader["Hour"].ToString();
                var row = new string[] { displaycity, trdate, hour };
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

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getOrders('" +driverID + "',0x00)", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView3.Items.Clear();

            while (reader.Read())
            {
                string req = reader["RequestID"].ToString();
                string cli = reader["ClientID"].ToString();
                string city = reader["City"].ToString();
                string street = reader["Street"].ToString();
                var row = new string[] { req, cli, city, street };
                var lvi = new ListViewItem(row);
                listView3.View = View.Details;
                listView3.Items.Add(lvi);

            }

            cn.Close();
        }

        private void loadDoneRequests()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getOrders('"+ driverID + "',0x01)", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView1.Items.Clear();

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

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            String filename = null;
            
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFile.FileName;
                textBox10.Text = filename;
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string picture = null;
            string filename = textBox10.Text;
            string Name = textBox9.Text;
            string contact = textBox8.Text;
            string street = textBox7.Text;
            string postalcode = textBox5.Text;
            string city = textBox6.Text;

            if (!(string.IsNullOrEmpty(filename)))
            {
                using (Image image = Image.FromFile(filename))
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

            cmd = new SqlCommand("FoodDelivery_FinalProject.EditDriver", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pLogin", SqlDbType.NVarChar).Value = driverID;
            cmd.Parameters.Add("@pName", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = picture;
            cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = street;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@PostalCode ", SqlDbType.NVarChar).Value = postalcode;

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);

            loadProfile();

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            createTracking();
        }

        private void populateComboBox2()
        {
            var dataSource = new List<string>();

            foreach (var city in GPS)
            {
                dataSource.Add(city.Key);
            }
            comboBox2.DataSource = dataSource;
        }

        private void createTracking()
        {
            string city = comboBox2.Text;
            string gps_lat = GPS[city][0].Replace(',', '.');
            string gps_lon = GPS[city][1].Replace(',', '.');

            SqlCommand cmd = new SqlCommand("exec FoodDelivery_FinalProject.AddTracking '" + driverID +"', '"+ gps_lat +"', '" + gps_lon +"'",cn);

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            loadTrackings();
        }
    }
}
