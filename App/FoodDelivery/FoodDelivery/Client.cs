using System;
using System.Collections;
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
    public partial class Client : Form
    {
        private SqlConnection cn;
        private String username;
        private String paymentID;
        private String RequestID;
        private String driverID;
        private double minDistance;
        private double travelCost;
        private double totalCost;
        private int pageNumberMeals = 1;
        private int count = 0;
        Dictionary<string, string[]> GPS = new Dictionary<string, string[]>
        {      {"Aveiro", new String [2] {"40.64427", "-8.64554" } },
               {"Beja",new String [2]  {"38.015060"," -7.863230" } },
               {"Braga",new String [2] {"41.550320","-8.420050"}},
               {"Bragança",new String [2] {"41.805820", "-6.757190"}},
               {"Castelo Branco",new String [2] {"39.822190","-7.490870"}},
               {"Coimbra",new String [2] {"40.205640","-8.419550"}},
               {"Évora",new String [2] {"38.566670"," -7.900000"}},
               {"Faro",new String [2] {"37.019370","-7.932230"}},
               {"Guarda",new String [2] {"40.537330","-7.265750"}},
               {"Leiria",new String [2] {"39.743620"," -8.807050"}},
               {"Lisboa",new String [2] {"38.716670"," -9.133330"}},
               {"Portalegre",new String [2] {"39.293790","-7.431220"}},
               {"Porto",new String [2] {"41.149610","-8.610990"}},
               {"Santarém",new String [2] {"39.233330","-8.683330"}},
               {"Setúbal",new String [2] {"38.524400","-8.888200"}},
               {"Viana do Castelo",new String [2] {"41.693230","-8.832870"}},
               {"Vila Real",new String [2] {"41.300620"," -7.744130"}},
            { "Viseu",new String [2] {"40.661010"," -7.909710"}},
        };



        public Client(String username)
        {
            InitializeComponent();
            this.username = username;


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


        private void loadProfile()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getProfile('" + username + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader["LoginName"].ToString();
                String temp = reader["Photo"].ToString();
                if (temp != "")
                {
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

            reader.Close(); // <- too easy to forget
            reader.Dispose();






            cn.Close();
        }

        private void loadRestaurants()
        {
            if (!verifySGBDConnection())
                return;
            string op = comboBox1.Text;
            string op3 = comboBox3.Text;
            string op2 = comboBox2.Text;
            string search = textBox11.Text;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantByType('" + op + "','" + op3 + "','"+search+"') ORDER BY " + op2, cn); ;


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView1.Items.Clear();

            while (reader.Read())
            {
                string RestaurantID = reader["RestaurantID"].ToString();
                string Name = reader["Name"].ToString();
                string contact = reader["Contact"].ToString();
                string city = reader["City"].ToString();
                string street = reader["Street"].ToString();
                string type = reader["Type"].ToString();
                var row = new string[] { RestaurantID, Name, contact, city, street, type };
                var lvi = new ListViewItem(row);
                listView1.View = View.Details;
                listView1.Items.Add(lvi);

            }
            reader.Close(); // <- too easy to forget
            reader.Dispose();






            cn.Close();
        }

        private void loadOrders()
        {
            if (!verifySGBDConnection())
                return;

            string op = comboBox5.Text;
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT RequestID,PaymentType,TotalCost,EstimatedTime, RequestStatus FROM  FoodDelivery_FinalProject.getRestaurantOrderComplex('" + username + "','"+op+"')", cn); ;


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string OrderID = reader["RequestID"].ToString();

                string PaymentType = reader["PaymentType"].ToString();
                string TotalCost = reader["TotalCost"].ToString();
                string EstimatedTime = reader["EstimatedTime"].ToString();
                byte [] status_byte =(byte []) reader["RequestStatus"];
                
                //bool Status = true;
                
                string RequestStatus = "";
                if ((int)status_byte[0]==0)
                {
                    RequestStatus = "In transit";
                }
                else
                {
                    RequestStatus = "Delivered";
                }

                var row = new string[] { OrderID, PaymentType, TotalCost, EstimatedTime, RequestStatus };
                var lvi = new ListViewItem(row);
                listView2.View = View.Details;
                listView2.Items.Add(lvi);

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

        private void Client_Load_1(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            createTable();
            createMealTable();
            createMealTabTable();
            createAvailableMealTable();
            createOrderTable();
            createRestaurantTable();
            populateComboBox();
            populateComboBox1();
            populateTabMeal();
            loadOrders();



            loadProfile();

        }




        private void createTable()
        {
            listView1.Columns.Add("RestaurantID", 80);
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Contact", 150);
            listView1.Columns.Add("City", 150);
            listView1.Columns.Add("Street", 150);
            listView1.Columns.Add("Type", 90);
            listView2.Columns.Add("ID", 150);
            listView2.Columns.Add("PaymentType", 150);
            listView2.Columns.Add("Total Cost", 150);
            listView2.Columns.Add("Estimated Time", 150);
            listView2.Columns.Add("RequestStatus", 90);

        }

        private void createMealTable()
        {

            listView3.Columns.Add("Name", 150);
            listView3.Columns.Add("Main Ingredient", 150);
            listView3.Columns.Add("Side Ingredient", 150);
            listView3.Columns.Add("Drink", 150);
            listView3.Columns.Add("Meal cost", 90);
            listView3.Columns.Add("Restaurant ID", 90);

        }

        private void createRestaurantTable()
        {

            listView7.Columns.Add("RestaurantID", 80);
            listView7.Columns.Add("Name", 150);
            listView7.Columns.Add("Contact", 150);
            listView7.Columns.Add("City", 150);
            listView7.Columns.Add("Street", 150);
            listView7.Columns.Add("Type", 90);

        }                     

        private void createMealTabTable()
        {
            listView6.Columns.Add("Meal Name", 150);
            listView6.Columns.Add("Main Ingredient", 150);
            listView6.Columns.Add("Side Ingredient", 150);
            listView6.Columns.Add("Drink", 150);
            listView6.Columns.Add("Price", 150);

        }

        private void createAvailableMealTable()
        {

            listView4.Columns.Add("Meal Name", 150);
            listView4.Columns.Add("Main Ingredient", 150);
            listView4.Columns.Add("Side Ingredient", 150);
            listView4.Columns.Add("Drink", 150);
            listView4.Columns.Add("Price", 150);

        }

        private void createOrderTable()
        {

            listView5.Columns.Add("Meal Name", 150);
            listView5.Columns.Add("Main Ingredient", 150);
            listView5.Columns.Add("Side Ingredient", 150);
            listView5.Columns.Add("Drink", 150);
            listView5.Columns.Add("Price", 150);
            listView5.Columns.Add("Restaurant Name", 150);
            listView5.Columns.Add("Restaurant ID", 150);



        }

        private void populateComboBox()
        {
            if (!verifySGBDConnection())
                return;



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantType()", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            var dataSource = new List<string>();
            dataSource.Add("Todos");

            while (reader.Read())
            {

                string type = reader["Type"].ToString();
                dataSource.Add(type);

            }

            comboBox1.DataSource = dataSource;






            cn.Close();
        }

        private void populateComboBox1()
        {
            if (!verifySGBDConnection())
                return;



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantCity", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            var dataSource = new List<string>();
            dataSource.Add("Todos");

            while (reader.Read())
            {

                string type = reader["City"].ToString();
                dataSource.Add(type);

            }

            comboBox3.DataSource = dataSource;






            cn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void enableTextBoxs(Boolean check)
        {
            textBox1.ReadOnly = check;
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

        private void populateTabMeal()
        {
            
            int pageSize = 20;
            count = 0;
            label37.Text = pageNumberMeals.ToString();

            string sort = comboBox4.Text;
            


            
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.MealTab", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pageNum", SqlDbType.Int).Value = pageNumberMeals;
            cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
            cmd.Parameters.Add("@sortColumnName", SqlDbType.NVarChar).Value =sort;
            cmd.Parameters.Add("@search", SqlDbType.NVarChar).Value = textBox24.Text;






            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read()) {
                count++;
                string Name = reader["Name"].ToString();
                
                double mealCost=Convert.ToDouble(reader["MealCost"]);
                string MainIngredient = reader["MainIngredient"].ToString();
                string SideIngredient = reader["SideIngredient"].ToString();
                string Drink = reader["Drink"].ToString();
                var row = new string[] { Name, MainIngredient, SideIngredient, Drink,mealCost.ToString() };
                var lvi = new ListViewItem(row);
                listView6.View = View.Details;
                listView6.Items.Add(lvi);


            }

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string LoginName = textBox1.Text;
            string picturePath = textBox10.Text;


            string Name = textBox3.Text;
            string contact = textBox4.Text;
            string street = textBox5.Text;
            string city = textBox7.Text;
            string postalCode = textBox6.Text;
            string cardNumber = textBox8.Text;
            string cardExpiration = textBox9.Text;


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
            else
            {
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

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            loadRestaurants();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRestaurants();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRestaurants();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRestaurants();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //panel1.Visible = true;

            /*
            createMealTable();
            ListView.SelectedIndexCollection indices = listView3.SelectedIndices;
            if (listView3.SelectedItems.Count > 0) {
                if (listView3.SelectedItems[0].Tag != null)
                {
                    MessageBox.Show(listView3.SelectedItems[0].Tag.ToString());
                }
                else
                {
                    MessageBox.Show("");
                }

            }*/

            //ListGrid media = (ListGrid)listView3.SelectedItems[0];
            //listView3.SelectedItems[0].SubItems[0].Text);

            //filTable();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
        private void loadMealDetails(String RequestID)
        {

            if (!verifySGBDConnection())
                return;



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantMeal('" + RequestID + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView3.Items.Clear();
            string MealCost = "";
            double TotalMealCost = 0;
            while (reader.Read())
            {
                string Name = reader["Name"].ToString();
                string tempMealCost = reader["MealCost"].ToString();
                TotalMealCost += Convert.ToDouble(tempMealCost);
                string MainIngredient = reader["MainIngredient"].ToString();
                string SideIngredient = reader["SideIngredient"].ToString();
                string Drink = reader["Drink"].ToString();
                string RestaurantID = reader["RestaurantID"].ToString();
                var row = new string[] { Name, MainIngredient, SideIngredient, Drink,tempMealCost,RestaurantID };
                var lvi = new ListViewItem(row);
                listView3.View = View.Details;
                listView3.Items.Add(lvi);

            }
            textBox12.Text = TotalMealCost + " €";

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRequest('" + RequestID + "')", cn);
            reader = cmd.ExecuteReader();
            string driverID = "";
            string realTotalCost = "";
            while (reader.Read())
            {
                realTotalCost = reader["TotalCost"].ToString();
                string paymentID = reader["PaymentID"].ToString();
                string travelCost = reader["TravelCost"].ToString();
                string estimatedTime = reader["EstimatedTime"].ToString();
                string distance = reader["Distance"].ToString();
                driverID = reader["DriverID"].ToString();


                textBox13.Text = travelCost + " €";
                textBox14.Text = (Convert.ToDouble(travelCost)+Convert.ToDouble(TotalMealCost)).ToString() + " €";
                textBox19.Text = estimatedTime;
                textBox20.Text = distance + " km";

            }

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            if (Convert.ToDouble(textBox14.Text.Replace("€", "")) != Convert.ToDouble(realTotalCost)){
                textBox30.Text = realTotalCost.ToString() + " €";
                textBox30.Visible = true;
                label53.Visible = true;
                label16.Visible = true;
                int discount =Convert.ToInt32(100-((Convert.ToDouble(realTotalCost)) * 100) / Convert.ToDouble(textBox14.Text.Replace("€", "")));
                label53.Text = "After discount:" + "(" + discount.ToString() + "%)";


            }

            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getDriverDetails('" + driverID + "')", cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string Name = reader["Name"].ToString();
                string Contact = reader["Contact"].ToString();
                string licensePlate = reader["LicensePlate"].ToString();
                string model = reader["Model"].ToString();
                string temp = reader["Photo"].ToString();
                if (temp != "")
                {
                    byte[] image = Convert.FromBase64String(temp);
                    pictureBox2.Image = byteArrayToImage(image);
                }

                label27.Text = Contact;
                textBox17.Text = Name;
                textBox16.Text = model;
                textBox15.Text = licensePlate;


            }
            reader.Close(); // <- too easy to forget
            reader.Dispose();

            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getDriverTracking('" + driverID + "')", cn);
            reader = cmd.ExecuteReader();
            string latitude = "";
            string longitude = "";
            string city = "";

            while (reader.Read())
            {
                latitude = reader["GPS_Latitude"].ToString();
                longitude = reader["GPS_Longitude"].ToString();
                city = reader["City"].ToString();






            }
            //MessageBox.Show(GPS["Lisboa"][0].Length+" "+latitude.Length);
            //MessageBox.Show(Convert.ToDouble(latitude) + " "+longitude+"--" + Convert.ToDecimal(GPS["Lisboa"][0].Trim()));




            textBox18.Text = city;
                





            cn.Close();


        }

        private void loadAvailableMeals(String RestaurantID, String RestaurantName)
        {

            if (!verifySGBDConnection())
                return;


            int count = 0;
            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getAvailableMeals('" + Convert.ToInt32(RestaurantID) + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;
            label35.Text = RestaurantName;
            label36.Text = RestaurantID;

            listView4.Items.Clear();

            while (reader.Read())
            {
                count++;
                string mealName = reader["MealName"].ToString();

                string MainIngredient = reader["MainIngredient"].ToString();
                string SideIngredient = reader["SideIngredient"].ToString();
                string Drink = reader["Drink"].ToString();
                string MealCost = reader["MealCost"].ToString() + " ";
                var row = new string[] { mealName, MainIngredient, SideIngredient, Drink, MealCost };
                var lvi = new ListViewItem(row);
                listView4.View = View.Details;
                listView4.Items.Add(lvi);

            }

            if (count == 0)
                label47.Visible = true;
            reader.Close(); // <- too easy to forget
            reader.Dispose();
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            string RequestID = listView2.SelectedItems[0].SubItems[0].Text;
            loadMealDetails(RequestID);
            panel1.Visible = true;
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string RestaurantID = listView1.SelectedItems[0].SubItems[0].Text;
            string RestaurantName = listView1.SelectedItems[0].SubItems[1].Text;
            loadAvailableMeals(RestaurantID,RestaurantName);
            panel2.Visible = true;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            label53.Visible = false;
            label16.Visible = false;
            textBox30.Visible = false;
            foreach (Control c in panel1.Controls)
            {
                TextBox t = (c as TextBox);
                NumericUpDown n = (c as NumericUpDown);

                if (t != null)
                    t.Text = "";

                if (n != null)
                    n.Value = 0;
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //loadAvailableMeals(RestaurantID);
            //panel2.Visible = true;


            if (listView4.SelectedItems.Count >= 1)
            {


                foreach (ListViewItem item in listView4.SelectedItems)
                {


                    var row = new string[] { item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text,label35.Text,label36.Text };
                    var lvi = new ListViewItem(row);
                    listView5.View = View.Details;
                    listView5.Items.Add(lvi);
                }
                MessageBox.Show("Added to cart");
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listView5.SelectedItems.Count >= 1)
            {


                for (int i = listView5.Items.Count - 1; i >= 0; i--)
                {


                    if (listView5.Items[i].Selected)
                    {
                        listView5.Items[i].Remove();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            if (listView5.Items.Count != 0)
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.CheckAvailableDrivers", cn);


                SqlDataReader reader = cmd.ExecuteReader();

                //listView1.Dock = DockStyle.Fill;

                listView4.Items.Clear();

                while (reader.Read())
                {
                    string numberOfDrivers = reader["NavailableDrivers"].ToString();

                    if (numberOfDrivers == "0")
                    {
                        MessageBox.Show("No available Drivers at the moment, try later :)");
                    }
                    else
                    {
                        getDriver();
                    }



                }


                reader.Close(); // <- too easy to forget
                reader.Dispose();


            }
            else
            {
                MessageBox.Show("Add items to the cart");
            }




        }

        public double HaversineDistance(double pos1_latitude, double pos1_longitude, double pos2_latitude, double pos2_longitude)
        {
            double R = 6371;
            var lat = (pos2_latitude - pos1_latitude).ToRadians();
            var lng = (pos2_longitude - pos1_longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(pos1_latitude.ToRadians()) * Math.Cos(pos2_latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        private void getDriver()
        {

            if (!verifySGBDConnection())
                return;

            ArrayList list = new ArrayList();

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.PickDriver( )", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string DriverID = reader["DriverID"].ToString();
                string Date = reader["Date"].ToString();
                string Hour = reader["Hour"].ToString();
                string latitude = reader["GPS_Latitude"].ToString();
                string longitude = reader["GPS_Longitude"].ToString();
                string City = reader["City"].ToString();
                list.Add(new Driver(DriverID, Date, Hour, latitude, longitude,City));

                //verificar se existe algum driver na cidade do cliente











            }



            minDistance = 500;
            driverID = "";

            foreach (Driver b in list)
            {
                double driverLatitude = Convert.ToDouble(b.Latitude);
                double driverLongitude = Convert.ToDouble(b.Longitude);
                string[] coord;


                GPS.TryGetValue(textBox7.Text, out coord);

                if (Equals(b.City, textBox7.Text))
                {
                    MessageBox.Show("Found Driver");
                    driverID = b.Id;
                    minDistance = 0;
                    break;
                }
                else
                {
                    double latitude_client = Convert.ToDouble(coord[0].Replace(".", ","));
                    double longitude_client = Convert.ToDouble(coord[1].Replace(".", ","));
                    double final_distance = HaversineDistance(driverLatitude, driverLongitude, latitude_client, longitude_client);
                    if (final_distance < minDistance)
                    {
                        minDistance = final_distance;
                        driverID = b.Id;
                    }


                }

            }

            cmd = new SqlCommand("FoodDelivery_FinalProject.changeStatusDriver", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@DriverID", SqlDbType.NVarChar).Value = driverID;
            cmd.Parameters.Add("@check", SqlDbType.Int).Value = 1;



            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();


            createpayment();

        }

        private void createpayment()
        {
            panel3.Visible = true;
            double totalMealCost = 0;
            double costByKm = 0.36;

            for (int i = listView5.Items.Count - 1; i >= 0; i--)
            {

                totalMealCost += Convert.ToDouble(listView5.Items[i].SubItems[4].Text);

            }
            textBox23.Text = totalMealCost + " €";
            textBox22.Text = String.Format("{0:0.00}", costByKm * minDistance)  + " €";
            travelCost = costByKm * minDistance;
            totalCost = travelCost + totalMealCost;
            textBox21.Text = String.Format("{0:0.00}", totalMealCost + costByKm * minDistance)  +" €";

            
            





        }
        

        class ListGrid
        {
            public string Name { get; set; }
            public string MealCost { get; set; }
            public string MainIngredient { get; set; }
            public string SideIngredient { get; set; }
            public string Drink { get; set; }


        }

        public class Driver
        {
            public string Id { get; set; }
            public string Date { get; set; }
            public string Hour { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string City { get; set; }


            public Driver(string id, string date, string hour, string latitude, string longitude, string City)
            {
                this.Id = id;
                this.Date = date;
                this.Hour = hour;
                this.Latitude = latitude;
                this.Longitude = Longitude;
                this.City = City;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            var checkedButton = panel3.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            if (checkedButton == null)
            {
                MessageBox.Show("Select type of payment");

            }
            else {

                SqlCommand cmd = null;
                
                

                cmd = new SqlCommand("FoodDelivery_FinalProject.addPayment", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outPutVal = new SqlParameter("@PaymentID", SqlDbType.Int);
                outPutVal.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPutVal);

                cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = checkedButton.Text;
                cmd.Parameters.Add("@ValueGiven", SqlDbType.Decimal).Value = Convert.ToDecimal(totalCost);
                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();


                if (outPutVal.Value != DBNull.Value) paymentID = outPutVal.Value.ToString();
                //MessageBox.Show(paymentID);*/

                createRequest();
            }
        }

        private void createRequest() {
            SqlCommand cmd = null;


           cmd = new SqlCommand("FoodDelivery_FinalProject.addRequest", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter outPutVal = new SqlParameter("@RequestID", SqlDbType.Int);
            outPutVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutVal);

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@PaymentID", SqlDbType.NVarChar).Value = paymentID;
            cmd.Parameters.Add("@TotalCost ", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox21.Text.Replace("€",""));
            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            if (outPutVal.Value != DBNull.Value) RequestID = outPutVal.Value.ToString();
            
            
            

            for (int i = listView5.Items.Count - 1; i >= 0; i--)
            {
                // MessageBox.Show("ola");

                cmd = new SqlCommand("FoodDelivery_FinalProject.addBelong", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                //MessageBox.Show(RequestID_2);
                cmd.Parameters.Add("@RestaurantID", SqlDbType.NVarChar);
                cmd.Parameters.Add("@RequestID", SqlDbType.NVarChar).Value = RequestID;

                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;

                string MealName=listView5.Items[i].SubItems[0].Text;
                
                string RestaurantID = listView5.Items[i].SubItems[6].Text;
                MessageBox.Show(MealName);
                cmd.Parameters["@Name"].Value = MealName;

                cmd.Parameters["@RestaurantID"].Value = RestaurantID;


                cmd.ExecuteNonQuery();
                //MessageBox.Show(cmd.Parameters["@responseMessage"].Value.ToString());
                cmd.Parameters.Clear();

            }

            createTrip();

        }

        private void createTrip()
        {
            SqlCommand cmd = null;


            cmd = new SqlCommand("FoodDelivery_FinalProject.addTripforRequest", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@DriverID", SqlDbType.NVarChar).Value = driverID;
            cmd.Parameters.Add("@EstimatedTime", SqlDbType.Int).Value = Convert.ToInt32((minDistance/80)*3600);//velocidade media de 80 km/h
            cmd.Parameters.Add("@Distance", SqlDbType.Decimal).Value = minDistance;
            cmd.Parameters.Add("@RequestID", SqlDbType.NVarChar).Value = RequestID;
            cmd.Parameters.Add("@TravelCost", SqlDbType.Decimal).Value = travelCost;
            //MessageBox.Show(driverID + "--" + minDistance + "--" + RequestID + "--" + travelCost+"--"+Convert.ToInt32((minDistance / 80) * 3600));




            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if (minDistance == 0)
            {
                cmd = new SqlCommand("FoodDelivery_FinalProject.changeStatusDriver", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DriverID", SqlDbType.NVarChar).Value = driverID;
                cmd.Parameters.Add("@check", SqlDbType.Int).Value = 0;



                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("FoodDelivery_FinalProject.changeStatusRequest", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@RequestID", SqlDbType.NVarChar).Value = RequestID;
                cmd.Parameters.Add("@check", SqlDbType.Int).Value = 1;



                if (!verifySGBDConnection())
                    return;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Request submitted");
            loadOrders();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (count == 20) {
                pageNumberMeals++;
                listView6.Items.Clear();
                populateTabMeal();
            }
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (pageNumberMeals != 1) {
                pageNumberMeals--;
                listView6.Items.Clear();
                populateTabMeal();
            }
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView6.Items.Clear();
            pageNumberMeals = 1;

            populateTabMeal();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            listView6.Items.Clear();
            pageNumberMeals = 1;
            populateTabMeal();
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void listView7_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void loadRestaurantsView(String mealName)
        {

            if(!verifySGBDConnection())
                return;
            SqlCommand cmd = null;
           
            
            cmd = new SqlCommand("SELECT * FROM FoodDelivery_FinalProject.getRestaurantByMeal('"+mealName+"')", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            label42.Text = mealName;
            string mainIngredient = "";
            string sideIngredient = "";
            string drink = "";
            string mealCost = "";

            while (reader.Read())
            {
                string restaurantName = reader["Name"].ToString();
                string restaurantID = reader["RestaurantID"].ToString();
                string contact = reader["Contact"].ToString();
                string street = reader["Street"].ToString();
                string city = reader["City"].ToString();
                string type = reader["Type"].ToString();
                mainIngredient = reader["mainIngredient"].ToString();
                sideIngredient = reader["sideIngredient"].ToString();
                drink = reader["drink"].ToString();
                mealCost = reader["mealCost"].ToString();




                var row = new string[] {restaurantID,restaurantName, contact, city, street, type };
                var lvi = new ListViewItem(row);
                listView7.View = View.Details;
                listView7.Items.Add(lvi);

            }
           

            textBox25.Text = mainIngredient;
            textBox26.Text = sideIngredient;
            textBox27.Text = drink;
            textBox28.Text = mealCost + " $";

        }

        private void listView6_MouseClick(object sender, MouseEventArgs e)
        {
            //if (listView7.SelectedItems.Count >= 1) {
            string MealName = listView6.SelectedItems[0].SubItems[0].Text;
            panel4.Visible = true;
            loadRestaurantsView(MealName);
            
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {

            
            if (listView7.SelectedItems.Count >= 1)
            {


                foreach (ListViewItem item in listView7.SelectedItems)
                {


                    var row = new string[] { label42.Text, textBox25.Text, textBox26.Text, textBox27.Text, textBox28.Text.Replace("$",""), listView7.SelectedItems[0].SubItems[1].Text, listView7.SelectedItems[0].SubItems[0].Text };
                    var lvi = new ListViewItem(row);
                    listView5.View = View.Details;
                    listView5.Items.Add(lvi);
                }

                MessageBox.Show("Added to cart");
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form v1 = new Form1();
            v1.Show();
            this.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.DeleteUser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pLogin", SqlDbType.NVarChar, 50).Value = username;
            

            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;





            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show(cmd.Parameters["@responseMessage"].Value.ToString());

            Form v1 = new Form1();
            v1.Show();
            this.Close();

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadOrders();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            if (!verifySGBDConnection())
                return;
            
            


            for (int i = listView3.Items.Count - 1; i >= 0; i--)
            {




                string restID = listView3.Items[i].SubItems[5].Text;
                string mealName= listView3.Items[i].SubItems[0].Text;
                string mainIngredient= listView3.Items[i].SubItems[1].Text;
                string sideIngredient=listView3.Items[i].SubItems[2].Text;
                string drink=listView3.Items[i].SubItems[3].Text;
                string mealCost= listView3.Items[i].SubItems[4].Text;
                cmd = new SqlCommand("Select * from FoodDelivery_FinalProject.getRestaurantProfile ('"+Convert.ToInt32(restID)+"')", cn);
                cmd.Connection = cn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string Name = reader["Name"].ToString();

                    var row = new string[] { mealName, mainIngredient, sideIngredient, drink, mealCost.Replace("€",""), Name,restID};
                    var lvi = new ListViewItem(row);
                    listView5.View = View.Details;
                    listView5.Items.Add(lvi);











                }
            }

            MessageBox.Show("Added to cart");

        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlCommand cmd = null;
                if (!verifySGBDConnection())
                    return;
                cmd = new SqlCommand("Select * from FoodDelivery_FinalProject.verifyCode ('" + textBox2.Text + "')", cn);
                cmd.Connection = cn;
                SqlDataReader reader = cmd.ExecuteReader();
                string restaurantID = "";
                string startDate = "";
                string endDate = "";
                string discount = "";
                bool check = false;
                while (reader.Read())
                {
                    restaurantID = reader["RestaurantID"].ToString();
                    startDate = reader["StartDate"].ToString();
                    endDate = reader["EndDate"].ToString();
                    discount = reader["Discount"].ToString();


                    

                }
                
                for (int i = listView5.Items.Count - 1; i >= 0; i--)
                {
                    // MessageBox.Show("ola");
                    string MealName = listView5.Items[i].SubItems[0].Text;

                    string RestaurantID = listView5.Items[i].SubItems[6].Text;
                    if (restaurantID != "")
                    {
                        if (RestaurantID == restaurantID)
                        {
                            check = true;
                        }
                    }
                    else if (discount != "") {
                        check = true;
                    }
                    
                    
                }
                if (!check)
                {
                    MessageBox.Show("Your code is not valid! Try another one");
                }
                else {
                    if (startDate != "" & endDate != "")
                    {
                        DateTime start = DateTime.Parse(startDate);
                        DateTime end = DateTime.Parse(endDate);
                        if (DateTime.Today> start & DateTime.Today<end )
                        {
                            double originalPrice = Convert.ToDouble(textBox21.Text.Replace("€",""));
                            double discountDouble = Convert.ToDouble(discount);
                            double finalPrice = originalPrice * (1-(discountDouble / 100));
                            textBox21.Text = finalPrice.ToString()+" €";
                            button19.Enabled = false;
                            textBox29.Text = discount+" %";
                            textBox29.Visible = true;
                            label51.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Your code is expired! Try another one");
                        }


                    }
                    else if (startDate != "")
                    {
                        DateTime start = DateTime.Parse(startDate);

                        if (DateTime.Today >start)
                        {
                            double originalPrice = Convert.ToDouble(textBox21.Text.Replace("€", ""));
                            double discountDouble = Convert.ToDouble(discount);
                            double finalPrice = originalPrice * (1 - (discountDouble / 100));
                            textBox21.Text = finalPrice.ToString() + " €";
                            button19.Enabled = false;
                            textBox29.Text = discount + " %";
                            textBox29.Visible = true;
                            label51.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Your code is expired! Try another one");
                        }
                    }
                    else if (endDate != "") {
                        DateTime end = DateTime.Parse(endDate);


                        if (DateTime.Today < end)
                        {
                            double originalPrice = Convert.ToDouble(textBox21.Text.Replace("€", ""));
                            double discountDouble = Convert.ToDouble(discount);
                            double finalPrice = originalPrice * (1 - (discountDouble / 100));
                            textBox21.Text = finalPrice.ToString() + " €";
                            button19.Enabled = false;
                            textBox29.Text = discount + " %";
                            textBox29.Visible = true;
                            label51.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Your code is expired! Try another one");
                        }
                    }
                    else
                    {
                        double originalPrice = Convert.ToDouble(textBox21.Text.Replace("€", ""));
                        double discountDouble = Convert.ToDouble(discount);
                        double finalPrice = originalPrice * (1 - (discountDouble / 100));
                        textBox21.Text = finalPrice.ToString() + " €";
                        button19.Enabled = false;
                        textBox29.Text = discount + " %";
                        textBox29.Visible = true;
                        label51.Visible = true;
                    }
                }

            }
            else {
                MessageBox.Show("Insert promotional code");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            textBox23.Clear();
            textBox22.Clear();
            textBox21.Clear();
            textBox29.Clear();
            textBox29.Visible = false;
            label51.Visible = false;
            textBox2.Clear();
            button19.Enabled = true;

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public static class Foo
    {

        public static double ToRadians(this double angleIn10thofaDegree)
        {
            // Angle in 10th of a degree
            return (angleIn10thofaDegree * Math.PI) / 1800;
        }

    }
}
