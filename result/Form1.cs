using System;
using System.Windows.Forms;

namespace St9
{
    public partial class Form1 : Form
    {
        // ������� ��������� �������
        private readonly int economPrice = 1500;
        private readonly int standartPrice = 2500;
        private readonly int luxPrice = 5000;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double sum = 0;

            if (txtBreakfast.Text.ToLower() == "��")
            {
                sum += 1000 * Convert.ToInt32(txtPlaces.Text) * Convert.ToInt32(txtDays.Text);
            }

            double cof = 100;
            int days = Convert.ToInt32(txtDays.Text);
            int places = Convert.ToInt32(txtPlaces.Text);

            for (int i = 2; i <= days; i++)
            {
                cof += (100 - 5 * (i - 1));
            }

            if (txtCleaning.Text.ToLower() == "��")
            {
                cof += 5 * days;
            }

            cof += 20 * (places - 1) * days;

            string roomType = txtRoomType.Text.ToLower();
            if (roomType == "����")
            {
                sum += (cof / 100) * 40000;
            }
            else if (roomType == "��������")
            {
                sum += (cof / 100) * 20000;
            }
            else if (roomType == "������")
            {
                sum += (cof / 100) * 10000;
            }
            else
            {
                throw new ArgumentException("����������� ��������� ������");
            }

            if (txtParking.Text.ToLower() == "��")
            {
                sum += 1000 * days;
            }

            txtResult.Text = Convert.ToString((int)sum);

        }
    }
}