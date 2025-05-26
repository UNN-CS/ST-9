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
            // �������� ������������ �����
            if (!int.TryParse(txtDays.Text, out int days) || days <= 0)
            {
                MessageBox.Show("������� ���������� ���������� ���� (������������� �����).");
                return;
            }

            if (!int.TryParse(txtCategory.Text, out int category) || category < 1 || category > 3)
            {
                MessageBox.Show("��������� ������ ������ ���� 1, 2 ��� 3.");
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 3)
            {
                MessageBox.Show("����������� ������ ������ ���� 1, 2 ��� 3.");
                return;
            }

            string safeInput = txtSafe.Text.Trim().ToLower();
            string breakfastInput = txtBreakfast.Text.Trim().ToLower();

            if (safeInput != "��" && safeInput != "���")
            {
                MessageBox.Show("������� '��' ��� '���' ��� ����� '����'.");
                return;
            }

            if (breakfastInput != "��" && breakfastInput != "���")
            {
                MessageBox.Show("������� '��' ��� '���' ��� ����� '�������'.");
                return;
            }


            double[] prices = { 5000, 3000, 1500 }; // ����, ��������, ������
            double[] multipliers = { 1.0, 1.5, 2.0 };

            double safeCost = (safeInput == "��") ? 500 : 0;
            double breakfastCost = (breakfastInput == "��") ? 1000 : 0;

            // ������ �����
            double total = days * prices[category - 1] * multipliers[capacity - 1]
                           + safeCost
                           + breakfastCost;

            lblTotal.Text = $"{total}";
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {

        }
    }
}
