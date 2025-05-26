namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Change(object sender, EventArgs e)
        {
            int days_c = (int)days.Value;
            string type_r = typeOfRoom.SelectedItem?.ToString() ?? "Эконом";
            int type_c = 0;
            roomCount.Text = "1";
            if (type_r == null) type_r = "";
            else
            {
                switch (type_r)
                {
                    case "Эконом":
                        {
                            type_c = 700;
                            break;
                        }
                    case "Средний":
                        {
                            type_c = 1000;
                            break;
                        }
                    case "Бизнес":
                        {
                            type_c = 2000;
                            roomCount.Text = "2";
                            break;
                        }
                    case "Люкс":
                        {
                            type_c = 5000;
                            roomCount.Text = "2";
                            break;
                        }
                    default: { type_c = 1000; break; }
                }
            }

            string beds_s = beds.SelectedItem?.ToString() ?? "одна односпальная";
            double beds_c = 0;

            //{ "одна односпальная", "одна двуспальная", "две односпальных", "две двуспальных",
            //"три односпальных", "одная двуспальная и одна односпальная" });
            if (beds_s == null) beds_s = "";
            else
            {
                switch (beds_s)
                {
                    case "одна односпальная":
                        {
                            beds_c = 0.7;
                            break;
                        }
                    case "одна двуспальная":
                        {
                            beds_c = 1;
                            //MessageBox.Show("Выбран элемент: " + beds.SelectedItem?.ToString());
                            break;
                        }
                    case "две односпальных":
                        {
                            beds_c = 2.5;
                            break;
                        }
                    case "две двуспальных":
                        {
                            beds_c = 3;
                            break;
                        }
                    case "три односпальных":
                        {
                            beds_c = 3;
                            break;
                        }
                    case "одна двуспальная и одна односпальная":
                        {
                            beds_c = 3;
                            break;
                        }
                    default:
                        {
                            beds_c = 1;
                            label1.Text = beds_s + " ee ";

                            break;
                        }
                }
            }

            int safe_c = safe.Checked ? 250 : 0;
            int breakfast_c = breakfast.Checked ? 550 : 0;

            price.Text = (((int)((type_c * beds_c + breakfast_c) * days_c + safe_c))).ToString();
        }

        private void typeOfRoom_SelectedValueChanged(object sender, EventArgs e)
        {
            Change(sender, e);
        }

        private void roomCount_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Change(sender, e);
        }
    }
}
