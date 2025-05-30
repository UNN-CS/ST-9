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
                int days = int.Parse(tbDays.Text);
                int category = int.Parse(tbCategory.Text);
                int capacity = int.Parse(tbCapacity.Text);
                string safe = tbSafe.Text.Trim().ToLower();
                string breakfast = tbBreakfast.Text.Trim().ToLower();

                int basePrice = category switch
                {
                    1 => 8000,
                    2 => 4000,
                    3 => 2000,
                    _ => throw new Exception("Некорректная категория номера")
                };

                int capacitySurcharge = capacity switch
                {
                    1 => 0,
                    2 => 500,
                    3 => 1000,
                    _ => throw new Exception("Некорректная вместимость номера")
                };

                int options = 0;
                if (safe == "да") options += 400;
                if (breakfast == "да") options += 200;

                int total = (basePrice + capacitySurcharge + options) * days;

                tbResult.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при расчёте: " + ex.Message);
            }
        }
    }
}