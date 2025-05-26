namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly decimal[] roomClassRates = { 2500m, 5000m, 10000m };
        private readonly decimal safeRate = 500m;
        private readonly decimal breakfastRate = 800m;

        public Form1()
        {
            InitializeComponent();
            SolveBtn.Click += SolveBtn_Click;
        }

        private void SolveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(DaysTB.Text);
                string roomClass = ClassTB.Text.ToLower();
                int capacity = int.Parse(CapacityTB.Text);
                bool hasSafe = SafeCB.Checked;
                bool hasBreakfast = BreakfastCB.Checked;

                if (days <= 0 || capacity <= 0)
                {
                    MessageBox.Show("Количество дней и вместимость должны быть положительными числами", "Ошибка");
                    return;
                }

                decimal roomRate = 0;
                switch (roomClass)
                {
                    case "1":
                        roomRate = roomClassRates[0];
                        break;
                    case "2":
                        roomRate = roomClassRates[1];
                        break;
                    case "3":
                        roomRate = roomClassRates[2];
                        break;
                    default:
                        MessageBox.Show("Неизвестный класс номера. Допустимые значения: эконом, стандарт, люкс", "Ошибка");
                        return;
                }

                decimal total = days * roomRate * capacity;

                if (hasSafe)
                {
                    total += safeRate;
                }

                if (hasBreakfast)
                {
                    total += breakfastRate * days * capacity;
                }

                SummaryTB.Text = total.ToString("C");
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка");
            }
        }
    }
}
