using Microsoft.VisualBasic;

namespace HotelCalculator {
    public partial class Form1 : Form {
        readonly Dictionary<string, decimal> cats = new() {
            { "эконом", 100 },
            { "стандарт", 200 },
            { "люкс", 300 }
        };

        readonly Dictionary<string, bool> yeah = new() {
            { "да", true },
            { "нет", false }
        };

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (cats.TryGetValue(textBoxCat.Text, out decimal sum));
            else { 
                textBoxSum.Text = "ОШИБКА КАТЕГОРИИ"; 
                return; 
            }

            if (Decimal.TryParse(textBoxDays.Text, out decimal days) && days > 0)
                sum *= days;
            else {
                textBoxSum.Text = "ОШИБКА ДНЕЙ";
                return;                                                                                                                 
            }
            if (Decimal.TryParse(textBoxCapacity.Text, out decimal capacity) && capacity > 0 && capacity < 4)
                sum *= capacity;
            else {
                textBoxSum.Text = "ОШИБКА ВМЕСТИМОСТИ";
                return;
            }

            if (yeah.TryGetValue(textBoxSafe.Text, out bool safe))
                sum += safe ? 100 : 0;
            else {
                textBoxSum.Text = "ОШИБКА СЕЙФА";
                return;
            }

            if (yeah.TryGetValue(textBoxBreakfast.Text, out bool breakfast))
                sum += breakfast ? 150 : 0;
            else {
                textBoxSum.Text = "ОШИБКА ЗАВТРАКА";
                return;
            }

            textBoxSum.Text = sum.ToString("0.00");
        }
    }
}
