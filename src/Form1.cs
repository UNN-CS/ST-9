using System;
using System.Windows.Forms;

namespace HotelCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnCompute.Click += BtnCompute_Click;
        }

        private void BtnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(inputDays.Text);
                int roomType = int.Parse(inputRoomType.Text);
                int guestCount = int.Parse(inputGuestCount.Text);
                bool safeIncluded = inputSafeOption.Text.Trim().ToLower() == "да";
                bool breakfastIncluded = inputBreakfastOption.Text.Trim().ToLower() == "да";

                int roomBasePrice = roomType switch
                {
                    1 => 3000,
                    2 => 5000,
                    3 => 8000,
                    _ => throw new ArgumentException("Неверная категория номера")
                };

                int guestExtra = (guestCount - 1) * 1000;

                int safePrice = safeIncluded ? 200 : 0;
                int breakfastPrice = breakfastIncluded ? 500 : 0;

                int dailyTotal = roomBasePrice + guestExtra + safePrice + breakfastPrice;
                int total = days * dailyTotal;

                outputTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
