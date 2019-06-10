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
        Dictionary<string, string[]> GPS = new Dictionary<string, string[]>
        {      {"Aveiro", new String [2] { "40,644270", "-8,645540" } },
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

        public RestaurantPage(string restauID)
        {
            this.restID = Convert.ToInt32(restauID);
            InitializeComponent();
        }

        private SqlConnection getSGBDConnection()
        {   
            //////INSERT USERNAME AND PASSWORD/////
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
            CreatePromotionalTable();


            loadOrderTable();
            loadProfile();
            loadMeals();
            loadStats();
            loadCodes();
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



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantOrderRestaurantPage('" + restID+"')", cn);


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;


            while (reader.Read())
            {

                string requestID = reader["RequestID"].ToString();
                string mealName= reader["MealName"].ToString();
                string totalCost = reader["TotalCost"].ToString();
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

            var dataSource = new List<string>();

            foreach (var city in GPS)
            {
                dataSource.Add(city.Key);
            }
            comboBox2.DataSource = dataSource;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantProfile('" + restID + "')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                

                textBox15.Text = reader["Name"].ToString();
                textBox14.Text = reader["Contact"].ToString();
                textBox13.Text = reader["Street"].ToString();
                comboBox2.Text = reader["City"].ToString();
                textBox5.Text = reader["PostalCode"].ToString();
                textBox10.Text = reader["Type"].ToString();
                
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            cn.Close();
        }

        private void loadStats()
        {
            string expensive = "";
            string expensivename = "";
            string cheapest = "";
            string cheapestname = "";
            string moneymade = "";
            string clientID = "";
            string numberClients = "";
            string mainIng = "";
            string mainSide = "";
            string mainDrink = "";

            disableTextBoxes();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select top 1 * from FoodDelivery_FinalProject.getMeals('None', " + restID + ") order by MealCost desc", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            expensivename = reader["Name"].ToString();
            expensive = reader["MealCost"].ToString();

            textBox29.Text = expensivename;
            textBox33.Text = expensive;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            cmd = new SqlCommand("select top 1 * from FoodDelivery_FinalProject.getMeals('None', " + restID + ") order by MealCost asc", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            cheapestname = reader["Name"].ToString();
            cheapest = reader["MealCost"].ToString();

            textBox28.Text = cheapestname;
            textBox32.Text = cheapest;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.getMoneyMade("+ restID +")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            moneymade = reader["moneyMade"].ToString();

            textBox31.Text = moneymade;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.BestClient(" + restID + ")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            clientID = reader["ClientID"].ToString();

            textBox30.Text = clientID;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.NumClients(" + restID + ")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            numberClients = reader["RequestsNo"].ToString();

            textBox7.Text = numberClients;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.soldMain(" + restID + ")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            mainIng = reader["MainIngredient"].ToString();

            textBox25.Text = mainIng;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.soldSide(" + restID + ")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            mainSide = reader["SideIngredient"].ToString();

            textBox27.Text = mainSide;

            reader.Close(); // <- too easy to forget
            reader.Dispose();

            //
            cmd = new SqlCommand("select * from FoodDelivery_FinalProject.soldDrink(" + restID + ")", cn);
            reader = cmd.ExecuteReader();

            reader.Read();
            mainDrink = reader["Drink"].ToString();

            textBox26.Text = mainDrink;

            reader.Close(); // <- too easy to forget
            reader.Dispose();
        }

        private void disableTextBoxes()
        {
            textBox7.ReadOnly = true;
            textBox25.ReadOnly = true;
            textBox26.ReadOnly = true;
            textBox27.ReadOnly = true;
            textBox28.ReadOnly = true;
            textBox29.ReadOnly =  true;
            textBox30.ReadOnly = true;
            textBox31.ReadOnly = true;
            textBox32.ReadOnly = true;
            textBox33.ReadOnly = true;
        }

        private void loadMeals()
        {

            string op1 = comboBox1.Text;
            string op2 = textBox12.Text;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = null;

            if (op1.Equals("Price Ascending"))
            {
                cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'" + restID + "') where Name LIKE '%" + op2 + "%' order by MealCost ASC", cn);

            }
            else if (op1.Equals("Price Descending"))
            {
                cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'" + restID + "') where Name LIKE '%" + op2 + "%' order by MealCost DESC", cn);
            }
            else if (op1.Equals("Name Ascending"))
            {
                cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'" + restID + "') where Name LIKE '%" + op2 + "%' order by Name ASC", cn);
            }
            else if(op1.Equals("Name Descending"))
            {
                cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'" + restID + "') where Name LIKE '%" + op2 + "%' order by Name DESC", cn);
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getMeals('" + op1 + "' ,'" + restID + "') where Name LIKE '%" + op2 + "%' order by Name ASC", cn);
            }


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

        private void loadCodes()
        {

            string op1 = comboBox1.Text;
            string op2 = textBox12.Text;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = null;

            
            cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getCodeByRestaurant('" + restID + "')", cn);
           


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView3.Items.Clear();

            while (reader.Read())
            {
                string code = reader["Code"].ToString();
                string Sdate = reader["StartDate"].ToString();
                string Edate = reader["EndDate"].ToString();
                string discount = reader["Discount"].ToString();
                var row = new string[] { code,Sdate,Edate,discount };
                var lvi = new ListViewItem(row);
                listView3.View = View.Details;
                listView3.Items.Add(lvi);

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

        private void CreatePromotionalTable() {
            listView3.Columns.Add("Code", 130);
            listView3.Columns.Add("Start Date", 130);
            listView3.Columns.Add("End Date", 130);
            listView3.Columns.Add("Discount", 130);
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
            loadStats();
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
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
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
            string city = comboBox2.Text;
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

            MessageBox.Show(cmd.Parameters["@responseMessage"].Value.ToString());

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

        private void button7_Click(object sender, EventArgs e)
        {
            Form v2 = new Form2();
            v2.Show();
            this.Close();
            
            
        }

        private void TabPage4_Click(object sender, EventArgs e)
        {
            loadStats();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.DeleteRestaurant", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RestaurantID", SqlDbType.Int).Value = restID;


            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;





            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            MessageBox.Show(cmd.Parameters["@responseMessage"].Value.ToString());

            Form v1 = new Form2();
            v1.Show();
            this.Close();
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            loadMeals();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand("FoodDelivery_FinalProject.addCode", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value =textBox37.Text;
            cmd.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = textBox36.Text;
            cmd.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = textBox35.Text;
            cmd.Parameters.Add("@Discount", SqlDbType.Int).Value = Convert.ToInt32(textBox34.Text);
            cmd.Parameters.Add("@RestaurantID", SqlDbType.VarChar).Value = restID;




            cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;



            

            if (!verifySGBDConnection())
                return;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["@responseMessage"].Value.ToString() != "")
            {
                MessageBox.Show(cmd.Parameters["@responseMessage"].Value.ToString());
            }
            loadCodes();
           
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
