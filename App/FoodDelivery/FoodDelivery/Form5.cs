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
        public Form5()
        {
            InitializeComponent();
        }

        private SqlConnection getSGBDConnection()
        {   

            //////INSERT USERNAME AND PASSWORD////
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

        private void populatebox1()
        {
            var dataSource = new List<string>();

            foreach (var city in GPS)
            {
                dataSource.Add(city.Key);
            }
            comboBox1.DataSource = dataSource;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string Name = textBox25.Text;
            string password = textBox1.Text;
            string contact = textBox30.Text;
            string street = textBox28.Text;
            string city =  comboBox1.Text;
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
            MessageBox.Show("Success");

            /*if (outPutVal.Value != DBNull.Value) restaurantID = outPutVal.Value.ToString();*/

            //MessageBox.Show("Your restaurant ID " + cmd.Parameters["@responseMessage"].Value.ToString());

            /*panel2.Visible = false;
            panel3.Visible = false;
            panel1.Visible = false;
            panel4.Visible = false;*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form v1 = new Form2();
            v1.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            populatebox1();
        }
    }
}
