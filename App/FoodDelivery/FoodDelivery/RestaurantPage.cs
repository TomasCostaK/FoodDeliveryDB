﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public RestaurantPage(string restauID)
        {
            this.restID = Convert.ToInt32(restauID);
            InitializeComponent();
        }

        private void RestaurantPage_Load(object sender, EventArgs e)
        {

        }
    }
}
