using System;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            tbResult.Text = "";

            bool parsedDays = int.TryParse(tbDays.Text, out int days);
            bool parsedCategory = int.TryParse(tbCategory.Text, out int category);
            bool parsedPlaces = int.TryParse(tbPlaces.Text, out int people);

            if (!parsedDays || !parsedCategory || !parsedPlaces || days < 0 || people < 0)
            {
                ShowWarning();
                return;
            }

            string safeText = tbSafe.Text.Trim().ToLowerInvariant();
            string breakfastText = tbBreakfast.Text.Trim().ToLowerInvariant();

            bool withSafe = safeText == "да";
            bool withBreakfast = breakfastText == "да";

            int rate;
            switch (category)
            {
                case 1:
                    rate = 4200;
                    break;
                case 2:
                    rate = 2800;
                    break;
                case 3:
                    rate = 1800;
                    break;
                default:
                    rate = -1;
                    break;
            }


            if (rate < 0)
            {
                ShowWarning();
                return;
            }

            int total = rate * days * people;

            if (withSafe)
                total += 700;

            if (withBreakfast)
                total += 350 * people * days;

            tbResult.Text = total.ToString();
        }

        private void ShowWarning()
        {
            tbResult.Text = "";
            MessageBox.Show("Пожалуйста, введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
