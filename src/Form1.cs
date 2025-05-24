using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
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
                if (!int.TryParse(tbDays.Text, out int days) ||
                    !int.TryParse(tbCategory.Text, out int roomType) ||
                    !int.TryParse(tbPlaces.Text, out int guests))
                {
                    throw new Exception();
                }

                string safeInput = tbSafe.Text.Trim().ToLower();
                string breakfastInput = tbBreakfast.Text.Trim().ToLower();

                bool hasSafe = safeInput == "да";
                bool hasBreakfast = breakfastInput == "да";

                int ratePerGuest = 0;

                switch (roomType)
                {
                    case 1:
                        ratePerGuest = 5000;
                        break;
                    case 2:
                        ratePerGuest = 3000;
                        break;
                    case 3:
                        ratePerGuest = 1500;
                        break;
                }

                int total = days * guests * ratePerGuest;

                if (hasSafe)
                    total += 500;

                if (hasBreakfast)
                    total += guests * days * 300;

                tbResult.Text = total.ToString();
            }
            catch
            {
                tbResult.Text = "";
                MessageBox.Show("Некорректные данные. Пожалуйста, проверьте ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
