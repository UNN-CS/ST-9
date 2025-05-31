namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int nightsCount = int.Parse(textBox1.Text);
                int roomType = int.Parse(textBox2.Text);
                int maxGuests = int.Parse(textBox3.Text);
                string safeRequest = textBox4.Text.ToLower();
                string breakfastRequest = textBox5.Text.ToLower();

                int baseRate = roomType switch
                {
                    1 => 5500,
                    2 => 3500,
                    3 => 1800,
                    _ => throw new Exception("Неверно указан тип номера.")
                };

                double occupancyFactor = maxGuests switch
                {
                    1 => 1.0,
                    2 => 1.35,
                    3 => 1.55,
                    _ => throw new Exception("Неверно указана вместимость.")
                };

                double totalCost = baseRate * nightsCount * occupancyFactor;

                if (safeRequest == "да")
                    totalCost += 250;

                if (breakfastRequest == "да")
                    totalCost += 350 * nightsCount;

                textBox6.Text = totalCost.ToString("F2") + " руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка расчета: " + ex.Message);
            }

        }
    }
}
