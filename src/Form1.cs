namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            int days = 0;
            int category = 0;
            int persons = 0;
            bool safe = false;
            bool breakfast = false;
            int total = 0;
            const int categoryBase = 3000;
            const int breakfastPrice = 1200;
            const int safePrice = 300;
            const int placeBase = 500;
               if(!int.TryParse(textboxDays.Text, out days))
            {
                throw new Exception("Couldn't parse any numbers");
            }
            if (days < 0)
            {
                throw new Exception("Incorrect number");
            }
            if (!int.TryParse(textboxCategory.Text, out category))
            {
                throw new Exception("Couldn't parse any numbers");
            }
            if (category<0||category>3)
            {
                throw new Exception("Incorrect number");
            }
            if (!int.TryParse(textboxPlaces.Text, out persons))
            {
                throw new Exception("Couldn't parse any numbers");
            }
            if (persons < 0 || persons > 3)
            {
                throw new Exception("Incorrect number");
            }
            if (textboxSafe.Text == "да")
            {
                safe = true;
            }else if (textboxSafe.Text != "нет") {
                throw new Exception("Couldn't parse y/n");
            }
            if (textboxBreakfast.Text == "да")
            {
                breakfast = true;
            }
            else if (textboxBreakfast.Text != "нет")
            {
                throw new Exception("Couldn't parse y/n");
            }
            total = (categoryBase * (Math.Abs(category - 4)) + persons * placeBase);
            if (safe) total += safePrice;
            if (breakfast) total += breakfastPrice;
            total *= days;
            textboxTotal.Text = total.ToString();

        }

        private void textboxDays_TextChanged(object sender, EventArgs e)
        {

        }

        private void textboxCategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void textboxPlaces_TextChanged(object sender, EventArgs e)
        {

        }

        private void textboxSafe_TextChanged(object sender, EventArgs e)
        {

        }

        private void textboxBreakfast_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
