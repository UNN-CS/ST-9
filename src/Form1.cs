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
                int nights = int.Parse(tbDays.Text);

                int roomType = int.Parse(tbCategory.Text);
 
                int quests = int.Parse(tbCapacity.Text);
                
                bool vault = tbSafe.Text.Trim().ToLower() == "да";
                
                bool breakfast = tbBreakfast.Text.Trim().ToLower() == "да";

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

                int guestSurcharge = quests * 500;

                int extras = 0;
                if (vault)
                    extras += 200;
                if (breakfast)
                    extras += 300;

                int finalSum = (pricePerNight + guestSurcharge + extras) * nights;

                tbTotal.Text = finalSum.ToString();
            }
            catch
            {
                MessageBox.Show("Something is incorrect. Please, try again");
            }
        }
    }
}
