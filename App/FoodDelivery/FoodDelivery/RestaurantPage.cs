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
    public partial class RestaurantPage : Form
    {
        private int restID;
        private SqlConnection cn;
        public RestaurantPage(string restauID)
        {
            this.restID = Convert.ToInt32(restauID);
            InitializeComponent();
            MessageBox.Show(restID.ToString());
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

        private void RestaurantPage_Load(object sender, EventArgs e)
        {
            createTable();
            createOrderTable();
            loadOrderTable();
            loadProfile();
            loadMeals();
            loadStats();
        }

        private void createOrderTable() {
            listView1.Columns.Add("ID", 150);
            listView1.Columns.Add("Meal Name", 150);
            listView1.Columns.Add("Total Cost", 150);
            listView1.Columns.Add("RequestStatus", 90);
        }

        private void loadOrderTable() {
            if(!verifySGBDConnection())
                return;



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantOrder('"+restID+"')", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;


            while (reader.Read())
            {

                string requestID = reader["RequestID"].ToString();
                string mealName= reader["mealName"].ToString();
                string totalCost = reader["MealCost"].ToString();
                byte [] status_byte =(byte []) reader["RequestStatus"];

                string RequestStatus = "";
                if ((int)status_byte[0] == 0)
                {
                    RequestStatus = "In transit";
                }
                else
                {
                    RequestStatus = "Delivered";
                }

                var row = new string[] { requestID, mealName, totalCost, RequestStatus };
                var lvi = new ListViewItem(row);
                listView1.View = View.Details;
                listView1.Items.Add(lvi);

            }

            






            cn.Close();


        }

        private void loadProfile()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantProfile('" + restID + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                

                textBox15.Text = reader["Name"].ToString();
                textBox14.Text = reader["Contact"].ToString();
                textBox13.Text = reader["Street"].ToString();
                textBox11.Text = reader["City"].ToString();
                textBox5.Text = reader["PostalCode"].ToString();
                textBox10.Text = reader["Type"].ToString();
                
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose();






            cn.Close();
        }

        private void loadStats()
        {
            
        }

        private void loadMeals()
        {

            string op1 = comboBox1.Text;
            string op2 = textBox12.Text;

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'"+ restID + "') where Name LIKE '%" + op2 + "%'", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string name = reader["Name"].ToString();
                string cost = reader["MealCost"].ToString();
                string mainI = reader["MainIngredient"].ToString();
                string sideI = reader["SideIngredient"].ToString();
                string drink = reader["Drink"].ToString();
                var row = new string[] { name, cost, mainI, sideI, drink };
                var lvi = new ListViewItem(row);
                listView2.View = View.Details;
                listView2.Items.Add(lvi);

            }

            cn.Close();
        }

        private void createTable()
        {
            listView2.Columns.Add("Name", 120);
            listView2.Columns.Add("Price", 100);
            listView2.Columns.Add("Main Ingredient", 130);
            listView2.Columns.Add("Side Ingredient", 130);
            listView2.Columns.Add("Drink", 120);
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            loadMeals();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMeals();
        }

        private void createMeal()
        {
            SqlCommand cmd = null;
            string main = textBox1.Text;
            string side = textBox2.Text;
            string drink = textBox3.Text;
            string cost = textBox4.Text.Replace(',','.');
            if (side.Equals(""))
            {
                string name = main + "_" + drink;
                cmd = new SqlCommand("exec FoodDelivery_FinalProject.AddMeal '"+name+"',"+restID+", "+ cost +" ,'"+main+"', NULL , '"+ drink +"'", cn);

            }
            else
            {
                string name = main + "_" + side + "_" + drink;
                MessageBox.Show(name);
                cmd = new SqlCommand("exec FoodDelivery_FinalProject.AddMeal '" + name + "'," + restID + ", " + cost + " ,'" + main + "', "+side+" , '" + drink + "'", cn);
            }

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            loadMeals();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            createMeal();
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Hide();
            button3.Show();
            button1.Show();
            
            enableTextBoxs(false);
        }

        private void enableTextBoxs(Boolean check)
        {
            textBox15.ReadOnly = check;
            textBox14.ReadOnly = check;
            textBox13.ReadOnly = check;
            textBox11.ReadOnly = check;
            textBox10.ReadOnly = check;
            textBox5.ReadOnly = check;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Show();
            button1.Hide();
            button3.Hide();
            
            enableTextBoxs(true);
            loadProfile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            


            string restaurantName = textBox15.Text;
            string contact = textBox14.Text;
            string street = textBox13.Text;
            string city = textBox11.Text;
            string postalCode = textBox5.Text;
            string type = textBox10.Text;


            
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.UpdateRestaurant", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RestaurantID", SqlDbType.Int).Value = restID;
            cmd.Parameters.Add("@RestaurantName", SqlDbType.NVarChar).Value = restaurantName;
            cmd.Parameters.Add("@Contact", SqlDbType.NChar, 9).Value = contact;
            cmd.Parameters.Add("@Street", SqlDbType.NVarChar).Value = street;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
            cmd.Parameters.Add("@PostalCode ", SqlDbType.NVarChar).Value = postalCode;
            cmd.Parameters.Add("@Type", SqlDbType.NChar, 16).Value = type;
            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;





            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);

            button4.Show();
            button1.Hide();
            button3.Hide();
            
            enableTextBoxs(true);
            loadProfile();  
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void loadOrderDetails(string RequestID,string cost, string mealName) {
            string op1 = comboBox1.Text;
            string op2 = textBox12.Text;
            textBox9.Text = mealName;
            textBox8.Text = cost + " €";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getClientProfilebyOrder('"+RequestID+"') ", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            //listView2.Items.Clear();
            string name = "";
            string street ="";
            string city="";
            string contact="";
            string photo="";


            while (reader.Read())
            {
                 name = reader["Name"].ToString();
                 street = reader["Street"].ToString();
                 city = reader["City"].ToString();
                 contact = reader["Contact"].ToString();
                 photo = reader["photo"].ToString();
                

            }

            if (photo != "")
            {
                byte[] image = Convert.FromBase64String(photo);
                pictureBox1.Image = byteArrayToImage(image);
            }
            textBox24.Text = name;
            textBox23.Text = contact;
            textBox22.Text = city;
            textBox21.Text = street;


            reader.Close(); // <- too easy to forget
            reader.Dispose();

            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getDriverbyOrder('"+RequestID+"') ", cn);

            reader = cmd.ExecuteReader();

            name = "";
            string vehicle = "";
            string licensePlate = "";
            string DriverID = "";
            contact = "";
            photo = "";

            while (reader.Read())
            { 
                name = reader["Name"].ToString();
                vehicle = reader["Vehicle"].ToString();
                licensePlate = reader["LicensePlate"].ToString();
                contact = reader["Contact"].ToString();
                photo = reader["Photo"].ToString();
                DriverID= reader["DriverID"].ToString();


            }

            if (photo != "")
            {
                byte[] image = Convert.FromBase64String(photo);
                pictureBox2.Image = byteArrayToImage(image);
            }

            textBox17.Text = name;
            textBox16.Text = vehicle;
            textBox6.Text = licensePlate;
            textBox18.Text = contact;

            reader.Close(); // <- too easy to forget
            reader.Dispose();
            MessageBox.Show(DriverID);

            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getTripbyDriver('" + DriverID + "') ", cn);

            reader = cmd.ExecuteReader();

            
            string estimatedTime = "";
            string distance = "";
            

            while (reader.Read())
            {
                estimatedTime = reader["EstimatedTime"].ToString();
                distance= reader["Distance"].ToString();
                


            }

            
            textBox19.Text = estimatedTime;
            textBox20.Text = distance;
            


            cn.Close();

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string RequestID = listView1.SelectedItems[0].SubItems[0].Text;
            string mealName = listView1.SelectedItems[0].SubItems[2].Text;
            string cost = listView1.SelectedItems[0].SubItems[1].Text;


            loadOrderDetails(RequestID,mealName,cost);
            MessageBox.Show(RequestID);
            panel1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
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
    }
}
