namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // Базовая стоимость за день для каждой категории номера
        private readonly int[] categoryBasePrices = { 1000, 2000, 3000 };

        // Коэффициенты для вместимости номера
        private readonly double[] capacityMultipliers = { 1.0, 1.2, 1.5 };

        // Дополнительные услуги
        private readonly int safePrice = 500;
        private readonly int breakfastPrice = 800;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(txtDays.Text);
                int category = int.Parse(txtCategory.Text); 
                int capacity = int.Parse(txtCapacity.Text);
                bool hasSafe = txtSafe.Text.Trim().ToLower() == "да";
                bool hasBreakfast = txtBreakfast.Text.Trim().ToLower() == "да";

                if (days <= 0)
                {
                    MessageBox.Show("Количество дней должно быть положительным числом");
                    return;
                }

                if (category < 1 || category > 3)
                {
                    MessageBox.Show("Категория номера должна быть 1, 2 или 3");
                    return;
                }

                if (capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("Вместимость номера должна быть 1, 2 или 3");
                    return;
                }

                category--;
                capacity--;

                double basePrice = categoryBasePrices[category] * capacityMultipliers[capacity];
                int total = (int)(days * basePrice);

                if (hasSafe) total += safePrice * days;
                if (hasBreakfast) total += breakfastPrice * days;

                txtSum.Text = total.ToString("N0") + " руб.";
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите числа во все поля");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
