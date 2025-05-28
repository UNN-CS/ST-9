using System;
using System.Windows.Forms;

namespace WinFormsAppium_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            try
            {
                // ��������� ������ �� ��������� �����
                int days = int.Parse(txtDays.Text); // ���������� ���� ����������
                int category = int.Parse(txtCategory.Text); // ��������� ������ (1, 2, 3)
                int capacity = int.Parse(txtCapacity.Text); // ����������� ������ (1, 2, 3)
                bool hasSafe = txtSafe.Text.ToLower() == "��"; // ���� (��/���)
                bool hasBreakfast = txtBreakfast.Text.ToLower() == "��"; // ������� (��/���)

                // �������� ������������ ������
                if (days <= 0)
                {
                    MessageBox.Show("���������� ���� ������ ���� ������������� ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (category < 1 || category > 3)
                {
                    MessageBox.Show("��������� ������ ������ ���� �� 1 �� 3!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (capacity < 1 || capacity > 3)
                {
                    MessageBox.Show("����������� ������ ������ ���� �� 1 �� 3!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSafe.Text.ToLower() != "��" && txtSafe.Text.ToLower() != "���")
                {
                    MessageBox.Show("��� ����� ������� '��' ��� '���'!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtBreakfast.Text.ToLower() != "��" && txtBreakfast.Text.ToLower() != "���")
                {
                    MessageBox.Show("��� �������� ������� '��' ��� '���'!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ������� ��������� �� ���� � ����������� �� ���������
                double basePrice = category switch
                {
                    1 => 1000, // ������
                    2 => 2000, // ��������
                    3 => 3000, // ����
                    _ => 0
                };

                // ������� �� ����������� (���. ����� +500)
                double capacityCost = (capacity - 1) * 500;

                // ������� �� �����
                double safeCost = hasSafe ? 200 : 0; // ���� +200
                double breakfastCost = hasBreakfast ? 300 : 0; // ������� +300

                // �������� �����
                double total = days * (basePrice + capacityCost + safeCost + breakfastCost);
                txtSum.Text = total.ToString("F2"); // ����� ����� � 2 ������� ����� �������
            }
            catch (FormatException)
            {
                MessageBox.Show("����������, ������� ���������� �������� �������� � '��'/'���' ��� �����!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}