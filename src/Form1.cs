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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Calc_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values from textboxes
                var numberOfNights = Convert.ToInt32(Days.Text);
                var roomClass = Convert.ToInt32(Category.Text);
                var beds = Convert.ToInt32(Capacity.Text);
                var hasSafe = string.Equals(Safe.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);
                var includesBreakfast = string.Equals(Breakfast.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);
                // Determine base price based on room class
                int roomRate = 0;
                switch (roomClass)
                {
                    case 1:
                        roomRate = 900;
                        break;
                    case 2:
                        roomRate = 1800;
                        break;
                    case 3:
                        roomRate = 3600;
                        break;
                }
                // Additional charge for extra beds
                int bedSurcharge;
                switch (beds)
                {
                    case 2:
                        bedSurcharge = 1000;
                        break;
                    case 3:
                        bedSurcharge = 2000;
                        break;
                    default:
                        bedSurcharge = 0;
                        break;
                }
                // Add-on services cost
                var addOns = 0;
                if (hasSafe)
                    addOns += 300;
                if (includesBreakfast)
                    addOns += 350;
                // Final calculation
                var finalCost = (roomRate + bedSurcharge + addOns) * numberOfNights;
                // Output the result
                Total.Text = finalCost.ToString();
            }

            catch (FormatException)
            {
                MessageBox.Show("Invalid input format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
