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

            loadMeals();
            loadStats();
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
    }
}
