using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            int fixPrice = 5000;
            if(!int.TryParse(days_of_stay_tb.Text, out int Days))
            {
                throw new Exception("Couldn't parse any numbers");
            }

            if (Days < 0)
            {
                throw new Exception("Incorrect number");
            }

            if (!int.TryParse(category_tb.Text, out int Category))
            {
                throw new Exception("Couldn't parse any numbers");
            }

            if (Category <=0 || Category >3)
            {
                throw new Exception("Incorrect number");
            }
            if (!int.TryParse(capacity_tb.Text, out int Capacity))
            {
                throw new Exception("Couldn't parse any numbers");
            }

            if (Capacity <= 0 || Capacity > 3)
            {
                throw new Exception("Incorrect number");
            }

            int Safe = 0;
            if(safe_tb.Text == "да")
            {
                Safe = 500;
            }
            int Breakfast = 0;
            if (breakfast_tb.Text == "да")
            {
                Breakfast = 1000;
            }

            int Sum = 0;
            Sum = fixPrice * Days * Category * Capacity + Safe + Breakfast;

            sum_tb.Text = Sum.ToString();
        }
    }
}
