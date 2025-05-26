using System;
using System.Windows.Forms;

namespace ST_9
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
                // Получение данных из полей ввода
                int days = int.Parse(textBoxDays.Text);

                // Преобразование категории
                string category;
                switch (textBoxCategory.Text)
                {
                    case "1":
                        category = "люкс";
                        break;
                    case "2":
                        category = "стандарт";
                        break;
                    case "3":
                        category = "эконом";
                        break;
                    default:
                        throw new ArgumentException("Неверный номер категории (1-3)");
                }

                int guests = int.Parse(textBoxGuests.Text);
                bool safe = checkBoxSafe.Checked;
                bool breakfast = checkBoxBreakfast.Checked;

                // Расчет базовой стоимости
                decimal basePrice;
                switch (category)
                {
                    case "люкс":
                        basePrice = 3000m;
                        break;
                    case "стандарт":
                        basePrice = 2000m;
                        break;
                    case "эконом":
                        basePrice = 1000m;
                        break;
                    default:
                        throw new ArgumentException("Неизвестная категория");
                }

                // Расчет общей стоимости
                decimal pricePerDay = basePrice * (1 + 0.2m * (guests - 1));
                decimal total = days * pricePerDay;

                if (safe) total += days * 700;
                if (breakfast) total += days * 500;

                // Вывод результата
                textBoxTotal.Text = total.ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                              "Ошибка расчета",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
    }
}
