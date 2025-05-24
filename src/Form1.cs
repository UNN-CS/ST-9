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
            try
            {
                int days = int.Parse(txtDays.Text);
                int category = int.Parse(txtCategory.Text);
                int capacity = int.Parse(txtCapacity.Text);
                bool safe = txtSafe.Text.Trim().ToLower() == "да";
                bool breakfast = txtBreakfast.Text.Trim().ToLower() == "да";

                int basePrice = 0;
                switch (category)
                {
                    case 1:
                        basePrice = 5000;
                        break;
                    case 2:
                        basePrice = 3000;
                        break;
                    case 3:
                        basePrice = 1500;
                        break;
                    default:
                        basePrice = 0;
                        break;
                }

                int total = days * basePrice;
                total += capacity * 500;

                if (safe) total += 1000;
                if (breakfast) total += days * 700;

                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message);
            }
        }
    }
}
