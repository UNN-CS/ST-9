namespace Hotel_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button_calc_Click(object sender, EventArgs e)
        {
            int sum = 500;
            int k_days = 500;
            int num_of_days = Convert.ToInt32(days.Text);
            int k_category = 200 * num_of_days;
            int k_cap = 200 * num_of_days;
            int k_safe = 500*num_of_days;
            int k_breakfast = 300 * num_of_days;
            sum += k_days * num_of_days +
                k_category * Convert.ToInt32(category.Text) +
                k_cap * Convert.ToInt32(capacity.Text) +
                k_safe * Convert.ToInt32(safe.Text) +
                k_breakfast * Convert.ToInt32(breakfast.Text);
            label_sum.Text = sum.ToString();
        }
    }
}
