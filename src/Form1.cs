using System;
using System.Windows.Forms;

namespace ST9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void resBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int days = Convert.ToInt32(daysTextBox.Text);
                int category = Convert.ToInt32(categoryTextBox.Text);
                int capacity = Convert.ToInt32(capacityTextBox.Text);
                bool safe = safeTextBox.Text.Trim().Equals("да", StringComparison.OrdinalIgnoreCase);
                bool breakfast = breakfastTextBox.Text.Trim().Equals("да", StringComparison.OrdinalIgnoreCase);

                resultTextBox.Text = ((category * 250 + capacity * 90 + (safe ? 40 : 0) + (breakfast ? 100 : 0)) * days).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Input Error");
            }
        }
    }
}

