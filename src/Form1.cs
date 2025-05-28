using System;
using System.Windows.Forms;

namespace HotelCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(tbDays.Text);
                int category = int.Parse(tbCategory.Text);
                int capacity = int.Parse(tbCapacity.Text);
                bool hasSafe = tbSafe.Text.Trim().ToLower() == "да";
                bool hasBreakfast = tbBreakfast.Text.Trim().ToLower() == "да";

                int basePrice = category switch
                {
                    1 => 3000,
                    2 => 2000,
                    3 => 1000,
                    _ => 0
                };

                int capacityFee = capacity * 500;
                int safeFee = hasSafe ? 200 : 0;
                int breakfastFee = hasBreakfast ? 300 : 0;

                int total = days * (basePrice + capacityFee + safeFee + breakfastFee);
                tbResult.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message);
            }
        }
    }
}
