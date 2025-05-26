using System;
using System.Globalization;
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
                int days = int.Parse(txtDays.Text);
                int guests = int.Parse(txtGuests.Text);
                decimal basePrice = 0;

                switch (cmbCategory.SelectedItem.ToString())
                {
                    case "Люкс":
                        basePrice = 5000;
                        break;
                    case "Стандарт":
                        basePrice = 3000;
                        break;
                    case "Эконом":
                        basePrice = 1000;
                        break;
                }

                decimal total = basePrice * days * guests;

                if (chkBreakfast.Checked)
                    total += 500 * days * guests;
                if (chkCleaning.Checked)
                    total += 300 * days;

                lblTotal.Text = total.ToString("C0", CultureInfo.GetCultureInfo("en-US")) + " ₽";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода данных!");
            }
        }
    }
}