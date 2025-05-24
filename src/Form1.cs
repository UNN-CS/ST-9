using System;
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

        /// <summary>
        /// Handles the click event for the "Calculate" button.
        /// Parses user input, performs the calculation, and displays the result.
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values from textboxes
                var numberOfNights = Convert.ToInt32(tbDays.Text);
                var roomClass = Convert.ToInt32(tbCategory.Text);      // 1: economy, 2: standard, 3: luxury
                var beds = Convert.ToInt32(tbCapacity.Text);           // 1, 2, or 3 beds

                var hasSafe = string.Equals(tbSafe.Text.Trim(), "yes", StringComparison.OrdinalIgnoreCase)
                           || string.Equals(tbSafe.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);

                var includesBreakfast = string.Equals(tbBreakfast.Text.Trim(), "yes", StringComparison.OrdinalIgnoreCase)
                                     || string.Equals(tbBreakfast.Text.Trim(), "да", StringComparison.OrdinalIgnoreCase);

                // Determine base price based on room class
                int roomRate = 0;
                switch (roomClass)
                {
                    case 1:
                        roomRate = 950;
                        break;
                    case 2:
                        roomRate = 1850;
                        break;
                    case 3:
                        roomRate = 2800;
                        break;
                }

                // Additional charge for extra beds
                int bedSurcharge = beds switch
                {
                    2 => 400,
                    3 => 850,
                    _ => 0
                };

                // Add-on services cost
                var addOns = 0;
                if (hasSafe)
                    addOns += 250;
                if (includesBreakfast)
                    addOns += 270;

                // Final calculation
                var finalCost = (roomRate + bedSurcharge + addOns) * numberOfNights;

                // Output the result
                tbTotal.Text = finalCost.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please enter valid numeric values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
