using System;
using System.Windows.Forms;

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
            try
            {
                // Получаем данные из полей
                int days = int.Parse(tbDays.Text);
                int guests = int.Parse(tbGuests.Text);
                string roomType = cbRoomType.Text.Trim();

                // Базовые цены за номер в сутки
                decimal basePrice = 0;
                switch (roomType)
                {
                    case "Эконом":
                        basePrice = 1500;
                        break;
                    case "Стандарт":
                        basePrice = 2500;
                        break;
                    case "Люкс":
                        basePrice = 4000;
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип номера. Используйте: Эконом, Стандарт, Люкс", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                // Учитываем количество мест (дополнительная плата за каждое место свыше 1)
                decimal guestMultiplier = 1 + (guests - 1) * 0.5m; // +50% за каждое дополнительное место

                // Базовая стоимость
                decimal totalCost = basePrice * days * guestMultiplier;

                // Добавляем дополнительные услуги
                decimal additionalServices = 0;

                if (chkBreakfast.Checked)
                    additionalServices += 500 * days * guests; // завтрак за каждый день для каждого гостя

                if (chkWiFi.Checked)
                    additionalServices += 200 * days; // Wi-Fi за каждый день

                if (chkParking.Checked)
                    additionalServices += 300 * days; // парковка за каждый день

                totalCost += additionalServices;

                // Выводим результат
                tbResult.Text = totalCost.ToString("0") + " руб.";
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}