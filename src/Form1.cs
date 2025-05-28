namespace GuestHotelCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(textBoxDays.Text);
                int category = int.Parse(textBoxCategory.Text); // 1-люкс, 2-стандарт, 3-эконом
                int capacity = int.Parse(textBoxCapacity.Text); // 1, 2, 3 места
                bool safe = textBoxSafe.Text.ToLower() == "да";
                bool breakfast = textBoxBreakfast.Text.ToLower() == "да";

                decimal basePricePerDay = 0;

                // Определение базовой цены за ночь в зависимости от категории
                switch (category)
                {
                    case 1: // Люкс
                        basePricePerDay = 5000;
                        break;
                    case 2: // Стандарт
                        basePricePerDay = 3000;
                        break;
                    case 3: // Эконом
                        basePricePerDay = 1500;
                        break;
                    default:
                        MessageBox.Show("Неверная категория номера. Используйте 1, 2 или 3.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxTotalSum.Text = "";
                        return;
                }

                // Добавление стоимости за вместимость
                // Принимаем, что 1-местный - базовая цена, 2-местный +X%, 3-местный +Y%
                switch (capacity)
                {
                    case 1:
                        // Базовая цена
                        break;
                    case 2:
                        basePricePerDay *= 1.2M; // +20% за 2-местный
                        break;
                    case 3:
                        basePricePerDay *= 1.35M; // +35% за 3-местный
                        break;
                    default:
                        MessageBox.Show("Неверная вместимость номера. Используйте 1, 2 или 3.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxTotalSum.Text = "";
                        return;
                }

                decimal totalSum = basePricePerDay * days;

                // Дополнительные опции
                if (safe)
                {
                    totalSum += days * 200; // 200 руб/день за сейф
                }
                if (breakfast)
                {
                    totalSum += days * 500; // 500 руб/день за завтрак
                }

                textBoxTotalSum.Text = totalSum.ToString("N2"); // Форматируем сумму
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для дней, категории и вместимости, и 'да'/'нет' для опций.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
            catch (OverflowException)
            {
                MessageBox.Show("Введенные значения слишком велики.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
        }
    }
}
