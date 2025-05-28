using System;
using System.Windows.Forms;

namespace WinFormsAppium_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Получение данных из текстовых полей
                int days = int.Parse(txtDays.Text); // Количество дней проживания
                int category = int.Parse(txtCategory.Text); // Категория номера (1, 2, 3)
                int capacity = int.Parse(txtCapacity.Text); // Вместимость номера (1, 2, 3)
                bool hasSafe = txtSafe.Text.ToLower() == "да"; // Сейф (да/нет)
                bool hasBreakfast = txtBreakfast.Text.ToLower() == "да"; // Завтрак (да/нет)

                // Проверка корректности данных
                if (days <= 0)
                {
                    MessageBox.Show("Количество дней должно быть положительным числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (category < 1 || category > 3)
                {
                    MessageBox.Show("Категория номера должна быть от 1 до 3!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("Вместимость номера должна быть от 1 до 3!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSafe.Text.ToLower() != "да" && txtSafe.Text.ToLower() != "нет")
                {
                    MessageBox.Show("Для сейфа укажите 'да' или 'нет'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtBreakfast.Text.ToLower() != "да" && txtBreakfast.Text.ToLower() != "нет")
                {
                    MessageBox.Show("Для завтрака укажите 'да' или 'нет'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Базовая стоимость за день в зависимости от категории
                double basePrice = category switch
                {
                    1 => 1000, // Эконом
                    2 => 2000, // Стандарт
                    3 => 3000, // Люкс
                    _ => 0
                };

                // Доплата за вместимость (доп. место +500)
                double capacityCost = (capacity - 1) * 500;

                // Доплаты за опции
                double safeCost = hasSafe ? 200 : 0; // Сейф +200
                double breakfastCost = hasBreakfast ? 300 : 0; // Завтрак +300

                // Итоговая сумма
                double total = days * (basePrice + capacityCost + safeCost + breakfastCost);
                txtSum.Text = total.ToString("F2"); // Вывод суммы с 2 знаками после запятой
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения и 'да'/'нет' для опций!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
