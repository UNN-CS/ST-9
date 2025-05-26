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
            string type_r = typeOfRoom.SelectedItem?.ToString() ?? "Ýêîíîì";
            int type_c = 0;
            roomCount.Text = "1";
            if (type_r == null) type_r = "";
            else
            {
                switch (type_r)
                {
                    case "Ýêîíîì":
                        {
                            type_c = 700;
                            break;
                        }
                    case "Ñðåäíèé":
                        {
                            type_c = 1000;
                            break;
                        }
                    case "Áèçíåñ":
                        {
                            type_c = 2000;
                            roomCount.Text = "2";
                            break;
                        }
                    case "Ëþêñ":
                        {
                            type_c = 5000;
                            roomCount.Text = "2";
                            break;
                        }
                    default: { type_c = 1000; break; }
                }
            }

            string beds_s = beds.SelectedItem?.ToString() ?? "îäíà îäíîñïàëüíàÿ";
            double beds_c = 0;

            //{ "îäíà îäíîñïàëüíàÿ", "îäíà äâóñïàëüíàÿ", "äâå îäíîñïàëüíûõ", "äâå äâóñïàëüíûõ",
            //"òðè îäíîñïàëüíûõ", "îäíàÿ äâóñïàëüíàÿ è îäíà îäíîñïàëüíàÿ" });
            if (beds_s == null) beds_s = "";
            else
            {
                switch (beds_s)
                {
                    case "îäíà îäíîñïàëüíàÿ":
                        {
                            beds_c = 0.7;
                            break;
                        }
                    case "îäíà äâóñïàëüíàÿ":
                        {
                            beds_c = 1;
                            //MessageBox.Show("Âûáðàí ýëåìåíò: " + beds.SelectedItem?.ToString());
                            break;
                        }
                    case "äâå îäíîñïàëüíûõ":
                        {
                            beds_c = 2.5;
                            break;
                        }
                    case "äâå äâóñïàëüíûõ":
                        {
                            beds_c = 3;
                            break;
                        }
                    case "òðè îäíîñïàëüíûõ":
                        {
                            beds_c = 3;
                            break;
                        }
                    case "îäíà äâóñïàëüíàÿ è îäíà îäíîñïàëüíàÿ":
                        {
                            beds_c = 3;
                            break;
                        }
                    default:
                        {
                            beds_c = 1;
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
