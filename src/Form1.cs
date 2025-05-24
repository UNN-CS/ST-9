namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Устанавливаем значение по умолчанию для категории
            cbCategory.SelectedIndex = 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем количество дней
                if (!int.TryParse(tbDays.Text, out int days) || days <= 0)
                {
                    MessageBox.Show("Введите корректное количество дней (больше 0)");
                    return;
                }

                // Получаем количество мест
                if (!int.TryParse(tbPeople.Text, out int people) || people <= 0)
                {
                    MessageBox.Show("Введите корректное количество мест (больше 0)");
                    return;
                }

                // Базовая стоимость за сутки в зависимости от категории номера
                decimal basePricePerDay = 0;
                switch (cbCategory.SelectedItem?.ToString())
                {
                    case "Эконом":
                        basePricePerDay = 1500;
                        break;
                    case "Стандарт":
                        basePricePerDay = 3000;
                        break;
                    case "Люкс":
                        basePricePerDay = 5000;
                        break;
                    default:
                        MessageBox.Show("Выберите категорию номера");
                        return;
                }

                // Рассчитываем базовую стоимость
                decimal totalCost = basePricePerDay * days * people;

                // Добавляем стоимость дополнительных услуг
                if (chkBreakfast.Checked)
                {
                    totalCost += 500 * days * people; // 500 руб за завтрак на человека в день
                }

                if (chkWifi.Checked)
                {
                    totalCost += 200 * days; // 200 руб за Wi-Fi в день
                }

                if (chkParking.Checked)
                {
                    totalCost += 300 * days; // 300 руб за парковку в день
                }

                // Отображаем результат
                tbResult.Text = totalCost.ToString("F2") + " руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка расчета: " + ex.Message);
            }
        }
    }
} 