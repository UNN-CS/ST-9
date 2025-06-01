using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeUIComponents();
        }

        private void OnCalculateButtonClicked(object sender, EventArgs e)
        {
            ResetResultField();

            var daysValid = int.TryParse(daysField.Text, out var daysCount);
            var categoryValid = int.TryParse(categoryField.Text, out var categoryId);
            var guestsValid = int.TryParse(guestsField.Text, out var guestsNumber);

            if (!ValidateInputs(daysValid, categoryValid, guestsValid, daysCount, guestsNumber))
            {
                ShowInputError();
                return;
            }

            ProcessRoomBooking(daysCount, categoryId, guestsNumber);
        }

        private bool ValidateInputs(bool daysValid, bool categoryValid, bool guestsValid, int days, int guests)
        {
            return daysValid && categoryValid && guestsValid && days >= 0 && guests >= 0;
        }

        private void ProcessRoomBooking(int days, int category, int guests)
        {
            var safeIncluded = safeField.Text.Trim().Equals("да", StringComparison.OrdinalIgnoreCase);
            var breakfastIncluded = breakfastField.Text.Trim().Equals("да", StringComparison.OrdinalIgnoreCase);

            var roomRate = GetCategoryRate(category);
            if (roomRate < 0)
            {
                ShowInputError();
                return;
            }

            var total = ComputeTotalCost(roomRate, days, guests, safeIncluded, breakfastIncluded);
            resultField.Text = total.ToString();
        }

        private int GetCategoryRate(int category)
        {
            return category switch
            {
                1 => 4200,
                2 => 2800,
                3 => 1800,
                _ => -1
            };
        }

        private int ComputeTotalCost(int rate, int days, int guests, bool safe, bool breakfast)
        {
            var baseCost = rate * days * guests;
            if (safe) baseCost += 700;
            if (breakfast) baseCost += 350 * guests * days;
            return baseCost;
        }

        private void ResetResultField()
        {
            resultField.Text = string.Empty;
        }

        private void ShowInputError()
        {
            MessageBox.Show("Введены неверные данные. Проверьте правильность ввода.",
                          "Ошибка ввода",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
        }
    }
}