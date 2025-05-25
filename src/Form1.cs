using System.Runtime.CompilerServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // �������� ������ �� �����
                int days = int.Parse(txtDays.Text);
                int roomCategory = int.Parse(txtCategory.Text);
                int capacity = int.Parse(txtCapacity.Text);

                // ������� ���� �� ��������� �������
                double[] categoryPrices = { 0, 1500, 3000, 5000 }; // 0 - �� ������������, ������� 1-3

                // �������� ������������ ����� ���������
                if (roomCategory < 1 || roomCategory > 3)
                {
                    throw new ArgumentException("��������� ������ ������ ���� 1, 2 ��� 3");
                }

                // �������� ������������ ����� �����������
                if (capacity < 1 || capacity > 3)
                {
                    throw new ArgumentException("����������� ������ ������ ���� 1, 2 ��� 3");
                }

                // ������� ���������
                double basePrice = categoryPrices[roomCategory] * days;

                // �������������� ������
                double optionsPrice = 0;
                if (cbSafe.Checked) optionsPrice += 200 * days;
                if (cbBreakfast.Checked) optionsPrice += 500 * capacity * days;

                // �������� ���������
                double total = basePrice + optionsPrice;

                // ������� ���������
                lblTotal.Text = $"�����: {total:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}", "������ �������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
    }
}
