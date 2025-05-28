namespace GuestHotelCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(textBoxDays.Text);
                int category = int.Parse(textBoxCategory.Text); // 1-����, 2-��������, 3-������
                int capacity = int.Parse(textBoxCapacity.Text); // 1, 2, 3 �����
                bool safe = textBoxSafe.Text.ToLower() == "��";
                bool breakfast = textBoxBreakfast.Text.ToLower() == "��";

                decimal basePricePerDay = 0;

                // ����������� ������� ���� �� ���� � ����������� �� ���������
                switch (category)
                {
                    case 1: // ����
                        basePricePerDay = 5000;
                        break;
                    case 2: // ��������
                        basePricePerDay = 3000;
                        break;
                    case 3: // ������
                        basePricePerDay = 1500;
                        break;
                    default:
                        MessageBox.Show("�������� ��������� ������. ����������� 1, 2 ��� 3.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxTotalSum.Text = "";
                        return;
                }

                // ���������� ��������� �� �����������
                // ���������, ��� 1-������� - ������� ����, 2-������� +X%, 3-������� +Y%
                switch (capacity)
                {
                    case 1:
                        // ������� ����
                        break;
                    case 2:
                        basePricePerDay *= 1.2M; // +20% �� 2-�������
                        break;
                    case 3:
                        basePricePerDay *= 1.35M; // +35% �� 3-�������
                        break;
                    default:
                        MessageBox.Show("�������� ����������� ������. ����������� 1, 2 ��� 3.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxTotalSum.Text = "";
                        return;
                }

                decimal totalSum = basePricePerDay * days;

                // �������������� �����
                if (safe)
                {
                    totalSum += days * 200; // 200 ���/���� �� ����
                }
                if (breakfast)
                {
                    totalSum += days * 500; // 500 ���/���� �� �������
                }

                textBoxTotalSum.Text = totalSum.ToString("N2"); // ����������� �����
            }
            catch (FormatException)
            {
                MessageBox.Show("����������, ������� ���������� �������� �������� ��� ����, ��������� � �����������, � '��'/'���' ��� �����.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
            catch (OverflowException)
            {
                MessageBox.Show("��������� �������� ������� ������.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� �������������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotalSum.Text = "";
            }
        }
    }
}
