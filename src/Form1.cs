namespace HotelCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboCategoryBox.SelectedIndex = 0;
            comboCapacity.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int days = (int)numericDays.Value;
            int category = comboCategoryBox.SelectedIndex + 1;
            int capacity = comboCapacity.SelectedIndex + 1;
            bool hasSafe = checkSafe.Checked;
            bool hasBreakfast = checkBreakfast.Checked;

            double basePrice = 0;

            switch (category)
            {
                case 1: // Люкс
                    basePrice = 5000 + (capacity - 1) * 2000;
                    break;
                case 2: // Стандарт
                    basePrice = 3000 + (capacity - 1) * 1500;
                    break;
                case 3: // Эконом
                    basePrice = 2000 + (capacity - 1) * 1000;
                    break;
            }

            double total = basePrice * days;

            if (hasSafe)
                total += 500 * days;

            if (hasBreakfast)
                total += 800 * capacity * days;

            txtSum.Text = total.ToString("N0") + " R";
        }

        private void numericDays_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
