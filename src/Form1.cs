namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxCategory.KeyPress += textBoxCategory_KeyPress;
            textBoxSize.KeyPress += textBoxSize_KeyPress;
            buttonCalculate.Click += buttonCalculate_Click;
        }

        private void textBoxCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (e.KeyChar < '1' || e.KeyChar > '3'))
            {
                e.Handled = true;
            }
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (e.KeyChar < '1' || e.KeyChar > '3'))
            {
                e.Handled = true;
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int days = int.Parse(textBoxDays.Text);
                int category = int.Parse(textBoxCategory.Text);
                int size = int.Parse(textBoxSize.Text);
                string safe = textBoxSafe.Text.ToLower();
                string breakfast = textBoxBreakfast.Text.ToLower();

                if (days <= 0)
                {
                    MessageBox.Show("���������� ���� ������ ���� ������ 0!");
                    return;
                }
                if (category < 1 || category > 3 || size < 1 || size > 3)
                {
                    MessageBox.Show("��������� � �������� ������ ������ ���� 1, 2 ��� 3!");
                    return;
                }
                if (safe != "��" && safe != "���")
                {
                    MessageBox.Show("���� ������ ���� '��' ��� '���'!");
                    return;
                }
                if (breakfast != "��" && breakfast != "���")
                {
                    MessageBox.Show("������� ������ ���� '��' ��� '���'!");
                    return;
                }

                double basePrice;
                switch (category)
                {
                    case 1: basePrice = 1000; break;
                    case 2: basePrice = 1500; break;
                    case 3: basePrice = 2000; break;
                    default: basePrice = 0; break;
                }

                double sizeMultiplier;
                switch (size)
                {
                    case 1: sizeMultiplier = 1.0; break;
                    case 2: sizeMultiplier = 1.5; break;
                    case 3: sizeMultiplier = 2.0; break;
                    default: sizeMultiplier = 1.0; break;
                }

                double safeCost = (safe == "��") ? 500 * days : 0;

                double breakfastMultiplier = (breakfast == "��") ? 1.1 : 1.0;

                double total = (basePrice * sizeMultiplier + safeCost) * breakfastMultiplier * days;

                textBoxSum.Text = $"{total:F2}";
            }
            catch (FormatException)
            {
                MessageBox.Show("����������, ������� ���������� �������� ������!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}");
            }
        }
    }
}