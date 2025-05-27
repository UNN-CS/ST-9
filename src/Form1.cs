using System;
using System.Globalization;
using System.Windows.Forms;

namespace HotelCalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int ParseInput(TextBox box, string field, int min = 1, int max = 100)
            {
                if (!int.TryParse(box.Text.Trim(), out int value) || value < min || value > max)
                    throw new ApplicationException($"Проверьте поле \"{field}\": значение некорректно.");
                return value;
            }
            bool IsYes(TextBox box) => box.Text.Trim().ToLowerInvariant() == "да";
            try
            {
                int days = ParseInput(tbDays, "Дней", 1, 365);
                int category = ParseInput(tbCategory, "Категория номера", 1, 3);
                int capacity = ParseInput(tbCapacity, "Вместимость", 1, 4);
                bool hasSafe = IsYes(tbSafe);
                bool hasBreakfast = IsYes(tbBreakfast);
                int basePrice = 800 + 700 * category;
                int capacityFee = 300 * (capacity - 1);
                int extras = (hasSafe ? 150 : 0) + (hasBreakfast ? 250 : 0);
                int daily = basePrice + capacityFee + extras;
                int total = daily * days;
                tbResult.Text = total
                    .ToString("C2", CultureInfo.GetCultureInfo("ru-RU"));
            }
            catch (ApplicationException ex)
            {
                tbResult.Text = "";
                MessageBox.Show(ex.Message, "Ошибка данных",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                tbResult.Text = "";
                MessageBox.Show("Что-то пошло не так!\n\n" + ex.Message,
                                "Неизвестная ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
