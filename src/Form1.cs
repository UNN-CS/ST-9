using System.Runtime.CompilerServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Получаем данные из формы
                int days = int.Parse(txtDays.Text);
                int roomCategory = int.Parse(txtCategory.Text);
                int capacity = int.Parse(txtCapacity.Text);

                // Базовые цены за категории номеров
                double[] categoryPrices = { 0, 1500, 3000, 5000 }; // 0 - не используется, индексы 1-3

                // Проверка корректности ввода категории
                if (roomCategory < 1 || roomCategory > 3)
                {
                    throw new ArgumentException("Категория номера должна быть 1, 2 или 3");
                }

                // Проверка корректности ввода вместимости
                if (capacity < 1 || capacity > 3)
                {
                    throw new ArgumentException("Вместимость номера должна быть 1, 2 или 3");
                }

                // Базовая стоимость
                double basePrice = categoryPrices[roomCategory] * days;

                // Дополнительные услуги
                double optionsPrice = 0;
                if (cbSafe.Checked) optionsPrice += 200 * days;
                if (cbBreakfast.Checked) optionsPrice += 500 * capacity * days;

                // Итоговая стоимость
                double total = basePrice + optionsPrice;

                // Выводим результат
                lblTotal.Text = $"Итого: {total:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка расчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
    }
}
