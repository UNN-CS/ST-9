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
                int days = int.Parse(textBox1.Text);
                int category = int.Parse(textBox2.Text);
                int capacity = int.Parse(textBox3.Text);
                string safeOption = textBox4.Text.ToLower();
                string breakfastOption = textBox5.Text.ToLower();

                // Базовая цена по категории
                int basePrice = category switch
                {
                    1 => 5000,
                    2 => 3000,
                    3 => 1500,
                    _ => throw new Exception("Неверная категория номера.")
                };

                // Коэффициент вместимости
                double capacityMultiplier = capacity switch
                {
                    1 => 1.0,
                    2 => 1.3,
                    3 => 1.5,
                    _ => throw new Exception("Неверная вместимость номера.")
                };

                // Подсчет суммы
                double total = basePrice * days * capacityMultiplier;

                // Сейф
                if (safeOption == "да")
                    total += 200;

                // Завтрак
                if (breakfastOption == "да")
                    total += 300 * days;

                // Отображение суммы
                textBox6.Text = total.ToString("F2") + " руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }

        }
    }
}
