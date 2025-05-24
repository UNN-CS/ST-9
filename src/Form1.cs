namespace ST_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;

            if (category.Text == "1")
            {
                sum = 6000 * Convert.ToInt32(capacity.Text) * Convert.ToInt32(number_people.Text);
            }
            if (category.Text == "2")
            {
                sum = 15000 * Convert.ToInt32(capacity.Text) * Convert.ToInt32(number_people.Text);
            }
            if (category.Text == "3")
            {
                sum = 30000 * Convert.ToInt32(capacity.Text) * Convert.ToInt32(number_people.Text);
            }
            if (breakfast.Text == "да")
            {
                sum += 1000 * Convert.ToInt32(capacity.Text) * Convert.ToInt32(number_people.Text);
            }
            if (vault.Text == "да")
            {
                sum += 1000;
            }

            summa.Text = Convert.ToString(sum);

        }

    }
}
