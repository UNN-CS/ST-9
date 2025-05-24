namespace ST_9
{
    public partial class Form1 : Form
    {

        int econom = 10000;
        int standart = 20000;
        int luks = 40000;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sum = 0;
            if (breakfast.Text.ToLower() == "да")
            {
                sum += 1000 * Convert.ToInt32(vmestimost.Text) * Convert.ToInt32(count_day.Text);
            }
            double cof = 100;
            for (int i = 2; i <= Convert.ToInt32(count_day.Text); i++)
            {
                cof += (100 - 5 * (i - 1));
            }
            if (seif.Text.ToLower() == "да")
            {
                cof += 5 * Convert.ToInt32(count_day.Text);
            }
            cof += 20 * (Convert.ToInt32(vmestimost.Text) - 1) * Convert.ToInt32(count_day.Text);

            if (kategory.Text.ToLower() == "люкс")
            {
                sum += (cof / 100) * luks;
            }
            if (kategory.Text.ToLower() == "стандарт")
            {
                sum += (cof / 100) * standart;
            }
            if (kategory.Text.ToLower() == "эконом")
            {
                sum += (cof / 100) * econom;
            }
            summa.Text =  Convert.ToString(sum);
        }
    }
}
