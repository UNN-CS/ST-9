using System;
using System.Runtime.CompilerServices;
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
                int days = int.Parse(tbDays.Text);
                int category = int.Parse(tbCategory.Text);     // 1: эконом, 2: стандарт, 3: люкс
                int capacity = int.Parse(tbCapacity.Text);     // 1: одноместный, 2: двухместный, 3: трехместный
                bool safe = tbSafe.Text.Trim().ToLower() == "да";
                bool breakfast = tbBreakfast.Text.Trim().ToLower() == "да";

                int basePrice = category switch
                {
                    1 => 1000,
                    2 => 2000,
                    3 => 3000,
                    _ => 0
                };

                int capacityPrice = capacity switch
                {
                    1 => 0,
                    2 => 500,
                    3 => 1000,
                    _ => 0
                };

                int extras = 0;
                if (safe) extras += 200;
                if (breakfast) extras += 300;

                int total = (basePrice + capacityPrice + extras) * days;

                tbTotal.Text = total.ToString();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введённых данных.");
            }
        }
    }
}
