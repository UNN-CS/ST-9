using System;
using System.Windows.Forms;

namespace HotelCostCalculator
{
    public partial class MainForm : Form
    {
        private readonly decimal[] pricePerNight = { 2500m, 5000m, 10000m };

        private const decimal SafeFee = 500m;
        private const decimal MealFee = 800m;

        public MainForm()
        {
            InitializeComponent();
            CalculateButton.Click += OnCalculateClick;
        }

        private void OnCalculateClick(object sender, EventArgs e)
        {
            try
            {
                int days = ParseInput(DaysTextBox.Text);
                string roomType = RoomTypeBox.Text.ToLower().Trim();
                int guests = ParseInput(GuestsTextBox.Text);

                bool useSafe = UseSafeCheckbox.Checked;
                bool includeMeals = IncludeMealsCheckbox.Checked;

                if (days <= 0 || guests <= 0)
                {
                    ShowError("Некорректные значения.");
                    return;
                }

                decimal basePrice = GetBaseRate(roomType) * days * guests;

                decimal total = basePrice;
                if (useSafe) total += SafeFee;
                if (includeMeals) total += MealFee * days * guests;

                ResultLabel.Text = total.ToString("C");
            }
            catch
            {
                ShowError("Ошибка ввода данных.");
            }
        }

        private decimal GetBaseRate(string roomType)
        {
            return roomType switch
            {
                "эконом" => pricePerNight[0],
                "стандарт" => pricePerNight[1],
                "люкс" => pricePerNight[2],
                _ => throw new ArgumentException("Неизвестный тип номера.")
            };
        }

        private int ParseInput(string input)
        {
            return int.Parse(input);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
