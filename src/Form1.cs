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
            // Проверка корректности ввода
            if (!int.TryParse(txtDays.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Введите корректное количество дней (положительное число).");
                return;
            }

            if (!int.TryParse(txtCategory.Text, out int category) || category < 1 || category > 3)
            {
                MessageBox.Show("Категория номера должна быть 1, 2 или 3.");
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 3)
            {
                MessageBox.Show("Вместимость номера должна быть 1, 2 или 3.");
                return;
            }

            string safeInput = txtSafe.Text.Trim().ToLower();
            string breakfastInput = txtBreakfast.Text.Trim().ToLower();

            if (safeInput != "да" && safeInput != "нет")
            {
                MessageBox.Show("Введите 'да' или 'нет' для опции 'Сейф'.");
                return;
            }

            if (breakfastInput != "да" && breakfastInput != "нет")
            {
                MessageBox.Show("Введите 'да' или 'нет' для опции 'Завтрак'.");
                return;
            }


            double[] prices = { 5000, 3000, 1500 }; // Люкс, Стандарт, Эконом
            double[] multipliers = { 1.0, 1.5, 2.0 };

            double safeCost = (safeInput == "да") ? 500 : 0;
            double breakfastCost = (breakfastInput == "да") ? 1000 : 0;

            // Расчет суммы
            double total = days * prices[category - 1] * multipliers[capacity - 1]
                           + safeCost
                           + breakfastCost;

            lblTotal.Text = $"{total}";
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {

        }
    }
}
