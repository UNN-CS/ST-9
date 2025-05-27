namespace HotelCalculator
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
                int days = int.Parse(textBox_days.Text);
                int category = int.Parse(textBox_category.Text);
                int capacity = int.Parse(textBox_capacity.Text);
                bool safe = textBox_safe.Text == "да";
                bool breakfast = textBox_breakfast.Text == "да";

                if ((days <= 0) || (category < 1) || (category > 3) || (capacity < 1) || (capacity > 3))
                {
                    MessageBox.Show("Данные введены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int base_cost = 0;
                switch (category)
                {
                    case 1:
                        base_cost = 100; break;
                    case 2:
                        base_cost = 200; break;
                    case 3:
                        base_cost = 300; break;
                }

                int sum = base_cost * capacity * days;
                if (safe)
                    sum += 10 * days;
                if (breakfast)
                    sum += 20 * capacity * days;

                textBox_sum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}