namespace ST_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (category.Text == "1")
            {
                sum = 4000;
            }
            else if (category.Text == "2")
            {
                sum = 10000;
            }else if (category.Text == "3")
            {
                sum = 30000;
            }
            if (breakfast.Text.ToLower() == "да")
            {
                sum += 1000;
            }
            sum *= Convert.ToInt32(countDay.Text) * Convert.ToInt32(people.Text);

            if (safe.Text.ToLower() == "да")
            {
                sum += 1000 * Convert.ToInt32(countDay.Text);
            }

            summa.Text = Convert.ToString(sum);
        }
    }
}
