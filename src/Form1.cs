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
                int numNights = int.Parse(tbDays.Text);
                int roomType = int.Parse(tbCategory.Text);
                int guestCount = int.Parse(tbCapacity.Text);
                bool hasSafe = tbSafe.Text.Trim().ToLower() == "да";
                bool hasBreakfast = tbBreakfast.Text.Trim().ToLower() == "да";

                int pricePerNight = 0;
                switch (roomType)
                {
                    case 1:
                        pricePerNight = 1000;
                        break;
                    case 2:
                        pricePerNight = 2000;
                        break;
                    case 3:
                        pricePerNight = 3000;
                        break;
                }

                int guestSurcharge = 0;
                if (guestCount == 2)
                    guestSurcharge = 500;
                else if (guestCount == 3)
                    guestSurcharge = 1000;

                int addOns = 0;
                if (hasSafe)
                    addOns += 200;
                if (hasBreakfast)
                    addOns += 300;

                int finalSum = (pricePerNight + guestSurcharge + addOns) * numNights;

                tbTotal.Text = finalSum.ToString();
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введённых данных.");
            }
        }
    }
}
