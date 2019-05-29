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
    public partial class Client : Form
    {
        private SqlConnection cn;
        private String username;
        

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
        

        private void loadProfile() {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getProfile('"+username+"')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader["LoginName"].ToString();
                String temp = reader["Photo"].ToString();
                MessageBox.Show(temp);
                if (temp != "") {
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
            string op=comboBox1.Text;
            string op3 = comboBox3.Text;
            string op2 = comboBox2.Text;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantByType('" + op + "','"+op3+"') ORDER BY "+op2, cn); ;


            SqlDataReader reader = cmd.ExecuteReader();
            
            //listView1.Dock = DockStyle.Fill;

            listView1.Items.Clear();

            while (reader.Read())
            {
                string Name= reader["Name"].ToString();
                string contact = reader["Contact"].ToString();
                string city = reader["City"].ToString();
                string street = reader["Street"].ToString();
                string type = reader["Type"].ToString();
                var row = new string[] { Name,contact,city,street,type };
                var lvi = new ListViewItem(row);
                listView1.View=View.Details;
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
            

            SqlCommand cmd = new SqlCommand("SELECT * FROM  FoodDelivery_FinalProject.getRestaurantOrderComplex('"+username+"')", cn); ;


            SqlDataReader reader = cmd.ExecuteReader();

            //listView1.Dock = DockStyle.Fill;

            listView2.Items.Clear();

            while (reader.Read())
            {
                string OrderID = reader["RequestID"].ToString();
                
                string PaymentType = reader["PaymentType"].ToString();
                string TotalCost = reader["TotalCost"].ToString();
                string EstimatedTime = reader["EstimatedTime"].ToString();
                bool Status =Convert.ToBoolean(reader.GetOrdinal("RequestStatus"));
                
                string RequestStatus = "";
                if ( Status)
                {
                    RequestStatus = "In transit";
                }
                else {
                    RequestStatus = "Delivered";
                }
               
                var row = new string[] {OrderID,PaymentType,TotalCost,EstimatedTime,RequestStatus};
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
            populateComboBox();
            populateComboBox1();
            loadOrders();



            loadProfile();
           
        }
        



        private void createTable() {
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

        }

        private void populateComboBox() {
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



            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantCity()", cn);


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
            textBox2.ReadOnly = check;
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

        private void button3_Click(object sender, EventArgs e)
        {
            string LoginName = textBox1.Text;
            string picturePath = textBox10.Text;
            

            string Name =textBox3.Text;
            string contact = textBox4.Text;
            string street = textBox5.Text;
            string city =textBox7.Text;
            string postalCode = textBox6.Text;
            string cardNumber = textBox8.Text;
            string cardExpiration=textBox9.Text;


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
            else {
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

            MessageBox.Show("ola " + cmd.Parameters["@responseMessage"].Value);

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

            if (!verifySGBDConnection())
                return;

            string name = textBox11.Text;

            SqlCommand cmd = new SqlCommand("SELECT * FROM   FoodDelivery_FinalProject.getRestaurantName('"+name+"')", cn);


            SqlDataReader reader = cmd.ExecuteReader();
            
            //listView1.Dock = DockStyle.Fill;

            listView1.Items.Clear();

            while (reader.Read())
            {
                string Name = reader["Name"].ToString();
                string contact = reader["Contact"].ToString();
                string city = reader["City"].ToString();
                string street = reader["Street"].ToString();
                string type = reader["Type"].ToString();
                var row = new string[] { Name, contact, city, street, type };
                var lvi = new ListViewItem(row);
                listView1.View = View.Details;
                listView1.Items.Add(lvi);

            }






            cn.Close();
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
            panel1.Visible = true;
            
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

            }
            
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

        
    }

    class ListGrid
    {
        public string Name{ get; set; }
        public string MealCost { get; set; }
        public string MainIngredient { get; set; }
        public string SideIngredient { get; set; }
        public string Drink { get; set; }


    }
}
