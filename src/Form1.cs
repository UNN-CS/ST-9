using System;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            outputTotal.Text = "";

            bool isValidDays = int.TryParse(inputStayDays.Text, out int numDays);
            bool isValidClass = int.TryParse(inputRoomClass.Text, out int roomClass);
            bool isValidGuests = int.TryParse(inputCapacity.Text, out int guestCount);

            if (!isValidDays || !isValidClass || !isValidGuests || numDays < 0 || guestCount < 0)
            {
                DisplayInputError();
                return;
            }

            string safeOption = inputSafe.Text.Trim().ToLowerInvariant();
            string breakfastOption = inputBreakfast.Text.Trim().ToLowerInvariant();

            bool includeSafe = safeOption == "да";
            bool includeBreakfast = breakfastOption == "да";

            int baseRate;
            switch (roomClass)
            {
                case 1:
                    baseRate = 100000;
                    break;
                case 2:
                    baseRate = 50000;
                    break;
                case 3:
                    baseRate = 25000;
                    break;
                default:
                    baseRate = -1;
                    break;
            }


            if (baseRate < 0)
            {
                DisplayInputError();
                return;
            }

            int result = baseRate * numDays * guestCount;

            if (includeSafe)
                result += 700;

            if (includeBreakfast)
                result += 350 * guestCount * numDays;

            outputTotal.Text = result.ToString();
        }

        private void DisplayInputError()
        {
            outputTotal.Text = "";
            MessageBox.Show("Пожалуйста, введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
