using System.Runtime.CompilerServices;

namespace st9
{
    public partial class Form1 : Form
    {
        private const int EconomyRate = 800;
        private const int StandardRate = 1500;
        private const int LuxuryRate = 3000;
        private const int TwoBedSurcharge = 450;
        private const int ThreeBedSurcharge = 700;
        private const int SafeCost = 200;
        private const int BreakfastCost = 350;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var (nights, category, beds) = ParseNumericInputs();
                var hasSafe = ParseYesNoInput(tbSafe.Text);
                var hasBreakfast = ParseYesNoInput(tbBreakfast.Text);

                var basePrice = CalculateBasePrice(category);
                var bedPrice = CalculateBedPrice(beds);
                var extrasPrice = CalculateExtrasPrice(hasSafe, hasBreakfast);

                DisplayTotalPrice((basePrice + bedPrice + extrasPrice) * nights);
            }
            catch (FormatException ex)
            {
                ShowError("Invalid input format. Please enter valid numeric values.");
            }
            catch (Exception ex)
            {
                ShowError($"An unexpected error occurred: {ex.Message}");
            }
        }

        private (int nights, int category, int beds) ParseNumericInputs()
        {
            return (
                Convert.ToInt32(tbDays.Text),
                Convert.ToInt32(tbCategory.Text),
                Convert.ToInt32(tbCapacity.Text)
            );
        }

        private bool ParseYesNoInput(string input)
        {
            var cleanedInput = input.Trim();
            return cleanedInput.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                   cleanedInput.Equals("да", StringComparison.OrdinalIgnoreCase);
        }

        private int CalculateBasePrice(int roomCategory)
        {
            return roomCategory switch
            {
                1 => EconomyRate,
                2 => StandardRate,
                3 => LuxuryRate,
                _ => throw new ArgumentException("Invalid room category")
            };
        }

        private int CalculateBedPrice(int numberOfBeds)
        {
            return numberOfBeds switch
            {
                2 => TwoBedSurcharge,
                3 => ThreeBedSurcharge,
                _ => 0
            };
        }

        private int CalculateExtrasPrice(bool safeRequired, bool breakfastIncluded)
        {
            var extrasTotal = 0;

            if (safeRequired)
                extrasTotal += SafeCost;

            if (breakfastIncluded)
                extrasTotal += BreakfastCost;

            return extrasTotal;
        }

        private void DisplayTotalPrice(int totalPrice)
        {
            tbTotal.Text = totalPrice.ToString();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Input Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
