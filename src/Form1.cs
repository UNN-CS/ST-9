using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST_9_LABS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(daysTextBox.Text);
                int category = int.Parse(categoryTextBox.Text);
                int capacity = int.Parse(capacityTextBox.Text);
                bool safe = safeTextBox.Text.Trim().ToLower() == "да";
                bool breakfast = breakfastTextBox.Text.Trim().ToLower() == "да";

                // Пример расчета стоимости:
                int basePrice = category * 1000 + capacity * 500;
                int safePrice = safe ? 200 : 0;
                int breakfastPrice = breakfast ? 300 : 0;

                int total = (basePrice + safePrice + breakfastPrice) * days;
                resultTextBox.Text = total.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода. Пожалуйста, проверьте данные.");
            }
        }

    }
}