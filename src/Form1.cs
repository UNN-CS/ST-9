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
        /// <summary>
        /// Initializes the main form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                var nums_of_nights = Convert.ToInt32(Days.Text);
                var category_room = Convert.ToInt32(Category.Text);
                var nums_beds = Convert.ToInt32(Capacity.Text);
                var isSafe = string.Equals(Safe.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);
                var isBreakfast = string.Equals(Breakfast.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);
                int roomRang = 0;
                int bedType = 0;
                if (category_room == 1)
                {
                    roomRang = 1000;
                }
                else if (category_room == 2)
                {
                    roomRang = 2000;
                }
                else if (category_room == 3)
                {
                    roomRang = 3000;
                }
                if (nums_beds == 2)
                {
                    bedType = 500;
                }
                else if (nums_beds == 3)
                {
                    bedType = 1000;
                }
                else
                {
                    bedType = 0;
                }
                var addOns = 0;
                if (isSafe)
                    addOns += 520;
                if (isBreakfast)
                    addOns += 420;
                var finalCost = (roomRang + bedType + addOns) * nums_of_nights;
                Total.Text = finalCost.ToString();
            }

            catch (FormatException)
            {
                MessageBox.Show("Input Error");
            }
        }
    }
}