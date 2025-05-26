using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(tbDays.Text);
                int category = int.Parse(tbCategory.Text);
                int places = int.Parse(tbPlaces.Text);
                string safe = tbSafe.Text.ToLower();
                string breakfast = tbBreakfast.Text.ToLower();

                int basePrice = 0;
                if (category == 1)
                    basePrice = 5000;
                else if (category == 2)
                    basePrice = 3000;
                else if (category == 3)
                    basePrice = 1500;


                int total = days * places * basePrice;

                if (safe == "да") total += 500;
                if (breakfast == "да") total += days * places * 300;

                tbResult.Text = total.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных!");
            }
        }

    }
}
