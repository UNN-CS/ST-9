using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Sum_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                ShowErrorMessage("Проверьте введенные данные");
                return;
            }

            int nights = GetNightsCount();
            int roomPrice = GetRoomPrice();
            int surcharge = GetGuestSurcharge();
            int extras = GetAdditionalServicesCost();

            int total = (roomPrice + surcharge + extras) * nights;
            DisplayResult(total);
        }

        private bool ValidateInputs()
        {
            return int.TryParse(textBox_Days.Text, out _) &&
                   int.TryParse(textBox_Category.Text, out _) &&
                   int.TryParse(textBox_Capacity.Text, out _) &&
                   !string.IsNullOrWhiteSpace(textBox_Safe.Text) &&
                   !string.IsNullOrWhiteSpace(textBox_Breakfast.Text);
        }

        private int GetNightsCount()
        {
            return Convert.ToInt32(textBox_Days.Text);
        }

        private int GetRoomPrice()
        {
            int category = Convert.ToInt32(textBox_Category.Text);

            return category switch
            {
                1 => 3000,
                2 => 5000,
                3 => 10000,
                _ => 0
            };
        }

        private int GetGuestSurcharge()
        {
            int guests = Convert.ToInt32(textBox_Capacity.Text);

            if (guests == 2) return 1500;
            if (guests == 3) return 3000;
            return 0;
        }

        private int GetAdditionalServicesCost()
        {
            int cost = 0;

            if (IsServiceSelected(textBox_Safe.Text)) cost += 1000;
            if (IsServiceSelected(textBox_Breakfast.Text)) cost += 3000;

            return cost;
        }

        private bool IsServiceSelected(string input)
        {
            return input.Trim().Equals("да", StringComparison.OrdinalIgnoreCase);
        }

        private void DisplayResult(int amount)
        {
            textBox_Sum.Text = amount.ToString();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}