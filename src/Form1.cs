using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] basePrices = new int[3, 3] {
                { 1000, 1500, 2000 }, // Категория 1
                { 800, 1200, 1600 },  // Категория 2
                { 500, 800, 1100 }     // Категория 3
            };

            int.TryParse(txtDays.Text, out int days);
            int.TryParse(txtCategory.Text, out int category);
            int.TryParse(txtCapacity.Text, out int capacity);

            if ((days < 1) || (category < 1) || (category > 3) || (capacity < 1) || (capacity > 3)
                || (txtSafe.Text.ToLower() != "да") && (txtSafe.Text.ToLower() != "нет") 
                || (txtBreakfast.Text.ToLower() != "да") && (txtBreakfast.Text.ToLower() != "нет"))
            {
                throw new ArgumentException("Invalid argument");
            }

            int safe = (txtSafe.Text.ToLower() == "да") ? 100 : 0;
            int breakfast = (txtBreakfast.Text.ToLower() == "да") ? 200 : 0;

            int total = (basePrices[category - 1, capacity - 1] + safe + breakfast * capacity) * days;
            txtTotal.Text = total.ToString();
        }
    }
}
