namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values
                int days = int.Parse(textBox1.Text);
                int category = int.Parse(textBox2.Text);
                int capacity = int.Parse(textBox3.Text);
                bool hasSafe = textBox4.Text.ToLower() == "да";
                bool hasBreakfast = textBox5.Text.ToLower() == "да";

                // Define pricing parameters
                double basePricePerDay = 1000;
                double categoryMultiplier = 1.0;
                double capacitySurcharge = 0;
                double safeSurcharge = 0;
                double breakfastSurchargePerPersonPerDay = 150;

                // Calculate category multiplier
                switch (category)
                {
                    case 2:
                        categoryMultiplier = 1.2;
                        break;
                    case 3:
                        categoryMultiplier = 1.5;
                        break;
                }

                // Calculate capacity surcharge
                if (capacity == 2)
                    capacitySurcharge = 200;
                else if (capacity == 3)
                    capacitySurcharge = 350;
                else if (capacity >= 4)
                    capacitySurcharge = 500;

                // Calculate safe surcharge
                if (hasSafe)
                    safeSurcharge = 100;

                // Calculate total cost
                double totalCost = ((basePricePerDay * categoryMultiplier) + capacitySurcharge + safeSurcharge) * days;
                if (hasBreakfast)
                {
                    totalCost += (breakfastSurchargePerPersonPerDay * capacity * days);
                }

                // Display the result
                textBox6.Text = totalCost.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Пожалуйста, введите корректные данные во все поля.", 
                    "Ошибка ввода", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Произошла ошибка: " + ex.Message, 
                    "Ошибка", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}
