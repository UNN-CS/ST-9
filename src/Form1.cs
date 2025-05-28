using System;
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
                // �������� ������ �� �����
                int days = int.Parse(tbDays.Text);
                int guests = int.Parse(tbGuests.Text);
                string roomType = cbRoomType.Text.Trim();

                // ������� ���� �� ����� � �����
                decimal basePrice = 0;
                switch (roomType)
                {
                    case "������":
                        basePrice = 1500;
                        break;
                    case "��������":
                        basePrice = 2500;
                        break;
                    case "����":
                        basePrice = 4000;
                        break;
                    default:
                        MessageBox.Show("����������� ��� ������. �����������: ������, ��������, ����", "������",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                // ��������� ���������� ���� (�������������� ����� �� ������ ����� ����� 1)
                decimal guestMultiplier = 1 + (guests - 1) * 0.5m; // +50% �� ������ �������������� �����

                // ������� ���������
                decimal totalCost = basePrice * days * guestMultiplier;

                // ��������� �������������� ������
                decimal additionalServices = 0;

                if (chkBreakfast.Checked)
                    additionalServices += 500 * days * guests; // ������� �� ������ ���� ��� ������� �����

                if (chkWiFi.Checked)
                    additionalServices += 200 * days; // Wi-Fi �� ������ ����

                if (chkParking.Checked)
                    additionalServices += 300 * days; // �������� �� ������ ����

                totalCost += additionalServices;

                // ������� ���������
                tbResult.Text = totalCost.ToString("0") + " ���.";
            }
            catch (FormatException)
            {
                MessageBox.Show("����������, ������� ���������� �������� ��������", "������ �����",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}