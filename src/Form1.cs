using System;
using System.Runtime.CompilerServices;
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

                bool safe = tbSafe.Text.ToLower() == "да";
                bool breakfast = tbBreakfast.Text.ToLower() == "да";

                int categoryPrice = category switch
                {
                    1 => 2000,
                    2 => 4000,
                    3 => 7000,
                    _ => throw new Exception("Неверная категория номера")
                };

                int capacityPrice = capacity switch
                {
                    1 => 0,
                    2 => 1000,
                    3 => 2000,
                    _ => throw new Exception("Неверное количество мест")
                };

                int safePrice = safe ? 500 : 0;
                int breakfastPrice = breakfast ? 1000 : 0;

                int totalPrice = (categoryPrice + capacityPrice + safePrice + breakfastPrice) * days;

                tbTotal.Text = totalPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}